#!/usr/bin/python
# -*- coding: utf-8 -*-

import sys
import os
import re
import pickle
import math
import scipy.stats as stats
from shutil import copyfile, rmtree
from subprocess import call

defaultConf = 'MSPC_FunctionalEnrichment/annotations/hg19_annotations/hg19.conf.txt'

def absPaths(directory):
	paths = []
	for root, dirs, files in os.walk(os.path.abspath(directory)):
		for f in files:
			paths = paths + [os.path.join(root, f)]
	return paths

def loadConf(configPath):
	c = open(configPath, 'r')
	CONFIG = pickle.loads(c.read())
	c.close()
	return CONFIG

def saveConf(CONFIG, configPath):
	c = open(configPath, 'w')
	c.write(pickle.dumps(CONFIG))
	c.close()

def backupConf(leaf):
	n = 0
	bkpdir = os.path.join(os.path.dirname(leaf), 'config_backups')
	if not os.path.exists(bkpdir):
		os.mkdir(bkpdir)
	else:
		for x in os.listdir(bkpdir):
			if x.find('.backup') > -1:
				n += 1
	if n == 0:
		n = '.backup'
	else:
		n = '.' + str(n + 1) + '.backup'
	bkpfile = os.path.join(bkpdir, os.path.basename(leaf + n))
	copyfile(leaf, bkpfile)
	return bkpfile

def hasHeader(bed):
	h = open(bed, 'r')
	line = h.readline().strip()
	h.close()
	m = re.search('^chr\d+', line)
	if m == None and not line[:4] in ('chrX', 'chrY', 'chrM'):
		return True
	else:
		return False

def sortBed(bed):
	tmp = bed + '.tmp'
	bedHeader = hasHeader(bed)
	if bedHeader:
		n = 0
		h = open(bed + '.header', 'w')
		w = open(tmp, 'w')
		with open(bed, 'r') as b:
			for line in b:
				if n == 0:
					h.write(line)
				else:
					w.write(line)
				n += 1
		h.close()
		w.close()
		os.remove(bed)
		os.rename(tmp, bed)
	call('sort -k1,1 -k2,2n ' + bed + ' > ' + tmp, shell = True)
	if bedHeader:
		call('cat ' + bed + '.header ' + tmp + ' > ' + bed,
		     shell = True)
		os.remove(tmp)
		os.remove(bed + '.header')
	else:
		os.remove(bed)
		os.rename(tmp, bed)

def bedSize(bed, genomeSize):
	N, C = (0, 0)
	bedHeader = hasHeader(bed)
	with open(bed, 'r') as b:
		for line in b:
			if bedHeader:
				m = re.search('^chr\d+', line)
				if m == None and not line[:4] in ('chrX', 'chrY', 'chrM'):
					line = line.strip().split()
					C += int(line[2]) - int(line[1])
					N += 1
				bedHeader = False
			else:
				line = line.strip().split()
				C += int(line[2]) - int(line[1])
				N += 1
	return [N, C, C/float(genomeSize)]

def coverageDifference(pA, nA, pB, nB, b0 = 0, level = 0.95, pooled = True, percent = False):
	try:
		if percent:
			b = (pA - pB)/float(pB)
		else:
			b = pA - pB
		if pooled:
			q = (pA*nA + pB*nB)/float(nA + nB)
			SE = math.sqrt(q*(1 - q)*(1/float(nA) + 1/float(nB)))
		else:
			SE = math.sqrt(pA*(1 - pA)/float(nA) + pB*(1 - pB)/float(nB))
		z = (b-b0)/SE
		P = 2*(1 - stats.norm.cdf(abs(z)))
		l95 = b - stats.norm.ppf(level)*SE
		u95 = b + stats.norm.ppf(level)*SE
		# l95 = b - 1.96*SE
		# u95 = b + 1.96*SE
	except:
		b, SE, z, P, l95, u95 = (0, 1, 0, 1,
		                         -float("inf"), float("inf"))
	return {'estimate': b, 'z': z, 'SE': SE,
	        'ci95': (l95, u95), 'pvalue': P}

if len(sys.argv) == 1:
	print 'OSError: MspcFE missing operand: ' +\
	      'please, use "mspc_fe.py --usage" for help'

elif sys.argv[1] == '--usage':
	print '\n## MSPC Functional Enrichment usage ##\n\n' +\
	      '1. Show this help:\n' +\
	      '    mspc_fe.py --usage\n\n' +\
	      '2. Show default configuration ' +\
	      '(recommended before doing any change):\n' +\
	      '    mspc_fe.py --default\n\n' +\
	      '3. Run functional enrichment testing:\n' +\
	      '    mspc_fe.py --run INPUT_DIRECTORY SUBJECT_BED_TAG ' +\
	      'ALTERNATE_BED_TAG\n' +\
	      '   Example:\n' +\
	      '    mspc_fe.py --run MSPC_FunctionalEnrichment/data/test ' +\
	      'ConsensusPeaks.bed optimal.bed\n\n' +\
	      '4. Run IDR [URL: https://github.com/nboley/idr]:\n' +\
	      '    mspc_fe.py --idr INPUT_DIRECTORY REP1_BED_TAG ' +\
	      'REP2_BED_TAG IDR_PVALUE_THRESHOLD [--soft]\n' +\
	      '   Example:\n' +\
	      '    mspc_fe.py --idr MSPC_FunctionalEnrichment/data/test ' +\
	      'replicate_1.bed replicate_2.bed 0.0001\n\n' +\
	      '5. Update annotations:\n' +\
	      '    mspc_fe.py --update FEATURE_FILE CONFIG_FILE\n' +\
	      '   Example:\n' +\
	      '    mspc_fe.py --update CpG_islands_hg19.bed ' +\
	      'MSPC_FunctionalEnrichment/annotations/hg19_annotations/' +\
	      'hg19.conf.txt\n\n' +\
	      '6. Update genome:\n' +\
	      '    mspc_fe.py --update-genome GENOME_RELEASE SIZE_(bp) ' +\
	      'CONFIG_FILE\n' +\
	      '   Example:\n' +\
	      '    mspc_fe.py --update-genome hg19 3100000000 ' +\
	      'MSPC_FunctionalEnrichment/annotations/hg19_annotations/' +\
	      'hg19.conf.txt\n\n' +\
	      '7. Change annotation directory:\n' +\
	      '    mspc_fe.py --ann-directory ANNOTATION_BED_DIR ' +\
	      'CONFIG_FILE\n\n' +\
	      '8. Remove backups for the current configuration:\n' +\
	      '    mspc_fe.py --rmconf\n\n' +\
	      '9. Enable/disable DNA sequence retrieval ' +\
	      'for the current configuration:\n' +\
	      '    mspc_fe.py --dna-sequence\n'

elif sys.argv[1] == '--default':
	MspcFE_conf = loadConf(defaultConf)
	print '\nConf.File:', defaultConf, '\n'
	print 'Directory:', MspcFE_conf['annotationDirectory']
	print '   Genome:', MspcFE_conf['genome']
	print '     Size:', MspcFE_conf['genomeSize'],\
	                    MspcFE_conf['unitSize']
	print 'Fetch DNA:', MspcFE_conf['fetchDnaSequence']
	print '   Format:', MspcFE_conf['annotationFormat'], '\n'
	for k in MspcFE_conf.keys():
		if k not in ('genome', 'genomeSize', 'unitSize',
		             'annotationFormat', 'annotationDirectory',
		             'fetchDnaSequence'):
			print k + ':', MspcFE_conf[k]
	print

elif sys.argv[1] == '--update':
	if not os.path.isfile(sys.argv[2]):
		raise OSError, 'feature file not found'
	if not os.path.isfile(sys.argv[3]):
		raise OSError, 'configuration file not found'
	feature = os.path.basename(sys.argv[2]).split('.')[0]
	MspcFE_conf = loadConf(sys.argv[3])
	if feature in MspcFE_conf.keys():
		print 'Updating feature:', feature
	else:
		print 'Creating new feature:', feature
	MspcFE_conf[feature] = bedSize(sys.argv[2],
	                               MspcFE_conf['genomeSize'])
	backup = backupConf(sys.argv[3])
	saveConf(MspcFE_conf, sys.argv[3])
	print 'Configuration updated.'
	print 'Previous configuration saved in:', backup

elif sys.argv[1] == '--update-genome':
	if not os.path.isfile(sys.argv[4]):
		raise OSError, 'configuration file not found'
	MspcFE_conf = loadConf(sys.argv[4])
	MspcFE_conf['genome'] = sys.argv[2]
	MspcFE_conf['genomeSize'] = int(sys.argv[3])
	
	backup = backupConf(sys.argv[4])
	saveConf(MspcFE_conf, sys.argv[4])
	print 'Genome size updated.'
	print 'Previous configuration saved in:', backup

elif sys.argv[1] == '--ann-directory':
	if not os.path.isfile(sys.argv[3]):
		raise OSError, 'configuration file not found'
	MspcFE_conf = loadConf(sys.argv[3])
	MspcFE_conf['annotationDirectory'] = sys.argv[2]
	backup = backupConf(sys.argv[3])
	saveConf(MspcFE_conf, sys.argv[3])
	print 'Annotation directory updated.'
	print 'Previous configuration saved in:', backup

elif sys.argv[1] == '--fetch-dna':
	MspcFE_conf = loadConf(defaultConf)
	if MspcFE_conf['fetchDnaSequence']:
		MspcFE_conf['fetchDnaSequence'] = False
	else:
		MspcFE_conf['fetchDnaSequence'] = True
	MspcFE_conf = saveConf(MspcFE_conf, defaultConf)

elif sys.argv[1] == '--rmconf':
	ans = None
	while ans not in ('y', 'n', 'Y', 'N'):
		ans = raw_input('This will delete all backups for the ' +\
	                'current configuration.\nDo you wish to ' +\
	                'continue? [y/n]\n')
	if ans in ('y', 'Y'):
		bkpdir = os.path.join(os.path.dirname(defaultConf),
							  'config_backups')
		if os.path.exists(bkpdir):
			rmtree(bkpdir)
			print 'All backups deleted for the current configuration.'
		else:
			print 'No backups found for the current configuration.'
	else:
		print 'Aborted.'

elif sys.argv[1] == '--run':
	
	print '\n## Feature enrichment started ...\n'
	DATA = [x for x in os.listdir(sys.argv[2]) \
	        if os.path.isdir(os.path.join(sys.argv[2], x))]
	DIM = len(DATA)
	if DIM > 1:
		otf = os.path.join(sys.argv[2], 'EnrichmentTest_results.txt')
		oth = open(otf, 'w')
		oth.write('dataset\tannotation\tN_anno\tpeakset\tN_peaks\t' +\
		          'estimate\tCI95\tz\tSE\tpvalue\n')
	for root in DATA:
		#print root
		print '# Processing data:', root
		pfx = os.path.join(sys.argv[2], root)
		cpf = os.path.join(pfx, "common.bed")
		spf = os.path.join(pfx, "specific.bed")
		mpf = os.path.join(pfx, "missing.bed")
		sbj = None
		alt = None
		for f in absPaths(pfx):
			if f.find(sys.argv[3]) > -1:
				sbj = f
			if f.find(sys.argv[4]) > -1:
				alt = f
		
		sortBed(sbj)
		sortBed(alt)
		raw = alt
		alt = alt.split('.')[0] + '_merged.bed'
		#alt = alt.replace('-', '_')
		
		call('bedtools merge -i ' + raw + ' > ' + alt + '.tmp',
		     shell = True)
		call('awk \'!seen[$0]++\' ' + alt + '.tmp > ' + alt,
		     shell = True)
		os.remove(alt + '.tmp')
		
		#print raw
		#print alt
		
		if sbj != None and alt != None:
			call('bedtools intersect -a ' + sbj + ' -b ' + alt +\
			     ' -wa > ' + cpf + '.tmp', shell = True)
			call('awk \'!seen[$0]++\' ' + cpf + '.tmp > ' + cpf,
			     shell = True)
			os.remove(cpf + '.tmp')
			call('bedtools intersect -a ' + sbj + ' -b ' + alt +\
			     ' -v > ' + spf, shell = True)
			call('bedtools intersect -a ' + alt + ' -b ' + sbj +\
			     ' -v > ' + mpf, shell = True)
			os.remove(alt)
			print '# Bed files created!'
		else:
			print 'WARNING:', root + ': input files not found\n'
		
		## Feature enrichment
		print '# Feature enrichment calculation ...'
		
		MspcFE_conf = loadConf(defaultConf)
		anndir = os.path.join(os.path.dirname(cpf), 'annotations')
		bkgdir = os.path.join(os.path.dirname(cpf), 'background')
		C = bedSize(cpf, MspcFE_conf['genomeSize'])
		S = bedSize(spf, MspcFE_conf['genomeSize'])
		M = bedSize(mpf, MspcFE_conf['genomeSize'])
		#U = bedSize(sbj, MspcFE_conf['genomeSize'])
		U = (C, S, M)
		if not os.path.exists(anndir):
			os.mkdir(anndir)
		if not os.path.exists(bkgdir):
			os.mkdir(bkgdir)
		
		A = {}
		res = os.path.join(pfx, root + "_results.txt")
		ran = open(res, 'w')
		ran.write('dataset\tannotation\tN_anno\tpeakset\tN_peaks\t' +\
		          'estimate\tCI95\tz\tSE\tpvalue\n')
		for annotation in absPaths(MspcFE_conf['annotationDirectory']):
			tag = os.path.basename(annotation).split('.')[0]
			#print '# Annotating', tag, '...'
			A[tag] = []
			for x, y in zip((cpf, spf, mpf), U):
				base = os.path.basename(x).split('.')[0]
				gan = os.path.join(anndir,\
				      os.path.basename(annotation).split('.')[0] +\
				      '_' + base + '.gan')
				bkg = os.path.join(bkgdir,\
				      os.path.basename(annotation).split('.')[0] +\
				      '_' + base + '.bkg')
				#call('bedtools intersect -a ' + x +\
				#     ' -b ' + annotation + ' -wa > ' + gan + '.tmp',
				#     shell = True)
				call('bedtools intersect -a ' + annotation +\
				     ' -b ' + x + ' -wa > ' + gan + '.tmp',
				     shell = True)
				call('awk \'!seen[$0]++\' ' + gan + '.tmp > ' + gan,
				     shell = True)
				os.remove(gan + '.tmp')
				#call('bedtools intersect -a ' + x +\
				#     ' -b ' + annotation + ' -v > ' + bkg,
				#     shell = True)
				call('bedtools intersect -a ' + annotation +\
				     ' -b ' + x + ' -v > ' + bkg,
				     shell = True)
				
				Q = bedSize(gan, MspcFE_conf['genomeSize'])
				#B = bedSize(bkg, MspcFE_conf['genomeSize'])
				B = bedSize(bkg, MspcFE_conf['genomeSize'] - y[1])
				
				# pseudocount
				Q[0] += 1
				if B[0] == 0:
					B[0] = 1
				if y[1] == 0:
					y[1] = 1
				# -----------
				#print Q[0], B[0], B[1]/float(y[1])
								
				# A/U vs notA/(G-U)
				#print Q[1]/float(y[1]), Q[0], B[2], B[0]
				A[tag] = coverageDifference(Q[1]/float(y[1]), Q[1],
				                            B[2], B[1])
				
				#print A[tag]
				if (Q[0] == 1):
					line = root + '\t' + tag + '\t' +\
					       str(MspcFE_conf[tag][0]) + '\t' +\
					       os.path.basename(x).split('.')[0] + '\t' +\
					       str(Q[0] - 1) + '\t0\tNA\t0\tNA\t1\n'
				else:
					line = root + '\t' + tag + '\t' +\
					       str(MspcFE_conf[tag][0]) + '\t' +\
					       os.path.basename(x).split('.')[0] + '\t' +\
					       str(Q[0] - 1) + '\t' +\
					       str(A[tag]['estimate']) + '\t' +\
					       str(round(A[tag]['ci95'][0], 5)) + '-' +\
					       str(round(A[tag]['ci95'][1], 5)) + '\t' +\
					       str(A[tag]['z']) + '\t' +\
					       str(A[tag]['SE']) + '\t' +\
					       str(A[tag]['pvalue']) + '\n'
				ran.write(line)
				if DIM > 1:
					oth.write(line)
				# Unlock to increase verbosity
				'''
				print
				print ' Dataset:', base
				print 'Estimate:', round(A[tag]['estimate'], 5)
				print '   CI95%:', round(A[tag]['ci95'][0], 5),\
				                   round(A[tag]['ci95'][1], 5)
				print '       z:', round(A[tag]['z'], 5)
				print 'StdError:', round(A[tag]['SE'], 5)
				print ' P-value:', A[tag]['pvalue'], '\n'
				'''
			#print '# Done.\n'
		ran.close()
		print '# Done.'
		
		print '# Creating enrichment boxplot ...'
		call(['Rscript', '--vanilla',
		      'MSPC_FunctionalEnrichment/R/enrichment_plots.R',
		      os.path.join(os.path.expanduser('~'), res),
		      'boxplot'])
		box0 = os.path.join(os.path.dirname(res), 'Rplots.pdf')
		box1 = res.split('.')[0] + '_boxplot.pdf'
		os.rename(box0, box1)
		print '# Done.\n'
		
		if MspcFE_conf['fetchDnaSequence']:
			print '# Fetching genomic DNA sequence ...'
			print '# Genome release:', MspcFE_conf['genome']
			
			for x in (cpf, spf, mpf):
				pass
			
			# !!! WORK IN PROGRESS !!! #
			
			#for x in (cpf, spf, mpf):
			#	call('euNet.py filter ' + x + ' -c 1 -o ' + x +\
			#	     '.tmp -v', shell = True)
			#	os.rename(x + ".tmp", x)
			#	call("euNet.py refinder " + x + " " +\
			#	     MspcFE_conf['genome'],
			#		 shell = True)
			
			print '# Done.\n'
	
	if DIM > 1:
		oth.close()
		print '# Creating summary plots ...'
		call(['Rscript', '--vanilla',
		      'MSPC_FunctionalEnrichment/R/enrichment_plots.R',
		      os.path.join(os.path.expanduser('~'), otf),
		      'boxplot'])
		box0 = os.path.join(os.path.dirname(otf), 'Rplots.pdf')
		box1 = otf.split('.')[0] + '_boxplot.pdf'
		os.rename(box0, box1)
		print '# Done.\n'
	
	print '## END.\n'

elif sys.argv[1] == '--idr':
	
	print '\n## IDR computation started ...\n'
	
	if len(sys.argv) > 6 and sys.argv[6] == '--soft':
		P = '--soft-idr-threshold ' + sys.argv[5]
		soft = True
	else:
		P = '--idr-threshold ' + sys.argv[5]
		soft = False
	
	DATA = [x for x in os.listdir(sys.argv[2]) \
	        if os.path.isdir(os.path.join(sys.argv[2], x))]
	
	for root in DATA:
		print '# Processing data:', root
		pfx = os.path.join(sys.argv[2], root)
		if soft:
			out = os.path.join(pfx, 'idrValues.txt')
		else:
			out = os.path.join(pfx, 'idrValues_p' + sys.argv[5] +\
			                   '.txt')			
		for f in absPaths(pfx):
			if f.find(sys.argv[3]) > -1:
				rep1 = f
			if f.find(sys.argv[4]) > -1:
				rep2 = f
		sortBed(rep1)
		sortBed(rep2)
		call('idr --samples ' + rep1 + ' ' + rep2 + ' ' + P +\
		     ' --output-file ' + out,
		     shell = True)
		if soft:
			p = str(-1*math.log(float(sys.argv[5]))/math.log(10))
			call("awk '{ if($12 >= " + p + ") { print }}' " + out +\
			     " > " + os.path.join(os.path.dirname(out),
			     "idrValues_postFilter_p" + sys.argv[5] + ".txt"),
			     shell = True)
	
	print '## END.\n'

else:
	print 'OSError:', sys.argv[1] + ': invalid command\n'+\
	      'Please, use "mspc_fe.py --usage" for help'
