#!/usr/bin/python
# -*- coding: utf-8 -*-

import sys
import os
from subprocess import call

print '### RECURSIVE EXECUTION'

roots = [os.path.join(sys.argv[1], x) for x in os.listdir(sys.argv[1]) \
         if os.path.isdir(os.path.join(sys.argv[1], x))]

print
for root in roots:
	print '## Subdirectory:', os.path.basename(root)
	call('mspc_fe.py --run ' + root + ' ' + sys.argv[2] +\
	     ' ' + sys.argv[3],
	     shell = True)

print '### All subdirectories explored.'

