library(ggplot2)
library(dplyr)

# Enrichment test output table
E <- read.delim("MSPC_w4s8g6_enrichmentResults.txt", stringsAsFactors = FALSE)


# Figure 1. Enrichment score distribution for MSPC discarded peaks, 
#           MSPC rescued peaks but discarded by IDR, and peaks retained 
#           by both MSPC and IDR (green box), aggregated by 9 hg38 
#           annotations (x axis).

# Data preparation
Dataset <- as.factor(gsub("_", " ", E$annotation))
Enrichment_score <- E$z
Peakset <- as.factor(gsub("_", " and ", E$peakset))

# Creating Figure 1
jpeg("MSPC_Figure1_GenomicFeature_enrichment.jpg",
     width = 20,
     height = 10,
     units = 'in',
     res = 300)
Peakset <- factor(Peakset,
                  levels = c("Discarded", "MSPC and IDR", "MSPC"),
                  ordered = TRUE)
ggplot(E, aes(x = reorder(Dataset, Enrichment_score, mean),
       y = Enrichment_score, fill = Peakset)) +
geom_boxplot(outlier.size = -1) +
scale_fill_manual(values = c("gold", "green3", "deepskyblue"),
                  breaks = c("MSPC", "MSPC and IDR", "Discarded")) +
xlab("Annotation") +
ylab("Enrichment score") +
theme_bw() +
theme(panel.border = element_blank(),
      panel.grid.major = element_line(),
      panel.grid.minor = element_blank(),
      axis.line = element_line(colour = "black"),
      axis.text = element_text(size = 28),
      axis.text.x = element_text(angle = 0, vjust = 0.5, hjust = 0.5),
      axis.title = element_text(size = 25, face = "bold"),
      legend.key.size = unit(1, "cm"),
      legend.text = element_text(size = 23),
      legend.title = element_text(size = 23)) +
scale_y_continuous(limits = c(0, 8800)) +
coord_flip()
dev.off()


# Supplementary Figure 1. Enrichment score distribution for peaks 
#                         retained by both MSPC and IDR and rescued by 
#                         MSPC but discarded by IDR, aggregated by 48 
#                         ENCODE TFs.

# Dataset preparation
Dataset <- gsub("_", " ", E[E$peakset != "Discarded",]$dataset)
Enrichment_score <- E[E$peakset != "Discarded",]$z
Peakset <- E[E$peakset != "Discarded",]$peakset
Peakset <- gsub("_", " and ", Peakset)

# Prepare data for sorting by Peakset == "MSPC" level only
X <- data.frame(Dataset = Dataset,
                Enrichment_score = Enrichment_score,
                Peakset = Peakset)

reordered <- X %>% filter(Peakset == "MSPC") %>% 
             mutate(rDataset = reorder(Dataset,
                                       Enrichment_score,
                                       function(x) 1/(quantile(x)[4]+1))) %>% 
             select(Dataset, rDataset)
X <- X %>% inner_join(reordered, by = "Dataset")
head(X)
rDataset <- as.factor(X$rDataset)
Peakset <- as.factor(X$Peakset)

# Creating Supplementary Figure 1
jpeg("MSPC_SupplementaryFigure1_TF_enrichment.jpg",
     width = 20,
     height = 10,
     units = 'in',
     res = 300)
ggplot(X, aes(x = rDataset, y = Enrichment_score, fill = Peakset)) +
geom_boxplot(outlier.size = -1) +
scale_fill_manual(values = c("gold", "deepskyblue")) +
xlab("Transcription Factor") +
ylab("Enrichment score") +
theme_bw() +
theme(panel.border = element_blank(),
      panel.grid.major = element_line(),
      panel.grid.minor = element_blank(),
      axis.line = element_line(colour = "black"),
      axis.text = element_text(size = 20),
      axis.text.x = element_text(angle = 45, hjust = 1),
      axis.title = element_text(size = 25, face = "bold"),
      legend.key.size = unit(1, "cm"),
      legend.text = element_text(size = 23),
      legend.title = element_text(size = 23)) +
scale_y_continuous(limits = c(0, 8000))
dev.off()
