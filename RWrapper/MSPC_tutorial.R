# MSPC tutorial

# We install all the needed packages before installing mspc :

# From the CRAN :
install.packages(c("installr","processx", "BiocManager"), dependencies=TRUE)

# From Bioconductor :
# When asked : Do you want to install from sources the package which needs
# compilation, please choose no.

BiocManager::install("rtracklayer")

# We install Rtools
install.Rtools()

# We install the mspc package :
install.packages("mspc_1.0.1.tar.gz",
                 type = "source", repos = NULL)
library(mspc)

# Case 1 : Inputs as bed files

Input <- c("rep1.bed","rep2.bed")


results <-mspc(Input = Input, Replicate_Type = "technical",
               Stringency_threshold =1e-8,Directory_Granges_Inputs = "C:\\Users\\Meriem\\Desktop",
               Weak_threshold =1e-4,Gamma = 0.5,Output_path = "C:\\Users\\Meriem\\Desktop\\MSPC_Package",Granges = T)

results$status
results$files_created
results$Granges_objects

# Case 2 : Inputs as Granges

# We create a Granges list

install.packages("GenomicRanges")
library(GenomicRanges)

gr1 <- GRanges(
  seqnames = "chr2",
  ranges = IRanges(103, 106),
  strand = "-",
  score = 5L, GC = 0.45)

gr2 <- GRanges(
  seqnames = c("chr1", "chr1"),
  ranges = IRanges(c(107, 113), width = 3),
  strand = c("+", "-"),
  score = 3:4, GC = c(0.3, 0.5))
grl <- GRangesList("txA" = gr1, "txB" = gr2)

Input<-grl

results <-mspc(Input = Input, Replicate_Type = "technical",
               Stringency_threshold =1e-8,Directory_Granges_Inputs = "C:\\Users\\Meriem\\Desktop",
               Weak_threshold =1e-4,Gamma = 0.5,Output_path = "C:\\Users\\Meriem\\Desktop\\MSPC_Package",Granges = TRUE,Keep = T)
