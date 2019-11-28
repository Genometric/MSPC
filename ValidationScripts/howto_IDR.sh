# Use official code from ENCODE, see https://www.encodeproject.org/software/idr/
# The code is based on the R package idr, that cannot be used directly if the peaks in the two replicates are not matched (i.e. if the peaks don't overlap perfectly)

# Download and Install
wget https://github.com/kundajelab/idr/archive/2.0.4.zip
unzip 2.0.4.zip
cd 2.0.4/
python3 setup.py install

# Usage 
# Column 12 in the output file reports -log10(Global IDR value)
idr --samples peaks_rep1.bed peaks_rep2.bed --soft-idr-threshold 0.05

# Get peaks passing IDR threshold of 5%
IDR_THRESH_LOG=$(awk -v p=0.05 'BEGIN{print -log(p)/log(10)}')
awk 'BEGIN{OFS="\t"} $12>='"${IDR_THRESH_TRANSFORMED}"' {print $1,$2,$3,$4,$5,$6,$7,$8,$9,$10,$11,$12}' idrValues.txt | sort | uniq > IDR0.05.bed

