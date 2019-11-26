# Summary boxplots generation script

# Takes the output of validation.py or recursive.py
# (i.e., EnrichmentTest_results.txt) as input

library("ggplot2")

args <- (commandArgs(TRUE))

etest <- read.delim(args[1], stringsAsFactors = FALSE)
E <- etest[etest$propAnnoPeaks > 0,]
A <- c("CpG_islands", "DNase_clusters", "Enhancers",
       "Exons", "Introns", "Non_RefSeq", "Promoters",
       "RefSeq_coding", "RefSeq_noncoding")


# Enrichment plots by ANNOTATION

# Wilcoxon's rank sum tests
for (j in A) {
  w <- wilcox.test(E$z[E$peakset == "specific" & E$annotation == j], E$z[E$peakset == "common" & E$annotation == j])
  cat(c(j, "::", w$p.value, "\n"))
}

Dataset <- as.factor(E$annotation)
Enrichment_score <- E$z
Peak_set <- as.factor(E$peakset)

jpeg("MSPC_FunctionalEnrichment/data/MACS/w3s8g4/EnrichmentTest_byAnnotation_boxplot_zscore.jpg", width = 25, height = 15, units = 'in', res = 300)
ggplot(E, aes(x = Dataset, y = Enrichment_score, fill = Peak_set)) +
#geom_boxplot(outlier.size = 0.5) +
geom_boxplot(outlier.size = -1) +
#scale_fill_manual(values = c("deepskyblue", "green3", "gold")) +
scale_fill_manual(values = c("deepskyblue", "gold")) +
theme_bw() +
theme(panel.border = element_blank(),
  panel.grid.major = element_line(),
  panel.grid.minor = element_blank(),
  axis.line = element_line(colour = "black"),
  axis.text = element_text(size = 18),
  axis.title = element_text(size = 16, face = "bold"),
  legend.key.size = unit(1, "cm"),
  legend.text = element_text(size = 18),
  legend.title = element_text(size = 16)) +
annotate("text", x = 1, y = 8700, size = 7,
         label = "N.cpg = 31,144\nP = 0.01321",
         parse = FALSE) +
annotate("text", x = 2, y = 7800, size = 7,
         label = "N.dnase = 2,107,358\nP = 3.447E-06",
         parse = FALSE) +
annotate("text", x = 3, y = 8700, size = 7,
         label = "N.enh = 226,711\nP = 1.087E-05",
         parse = FALSE) +
annotate("text", x = 4, y = 7800, size = 7,
         label = "N.exon = 313,276\nP = 0.002096",
         parse = FALSE) +
annotate("text", x = 5, y = 8700, size = 7,
         label = "N.intr = 172,751\nP = 0.01266",
         parse = FALSE) +
annotate("text", x = 6, y = 7800, size = 7,
         label = "N.nrs = 34,996\nP = 4.833E-05",
         parse = FALSE) +
annotate("text", x = 7, y = 8700, size = 7,
         label = "N.prom = 67,635\nP = 8.702E-04",
         parse = FALSE) +
annotate("text", x = 8, y = 7800, size = 7,
         label = "N.cod = 17,271\nP = 2.278E-04",
         parse = FALSE) +
annotate("text", x = 9, y = 8700, size = 7,
         label = "N.ncod = 8,236\nP = 0.007727",
         parse = FALSE) +
scale_y_continuous(limits = c(0, 8800))
dev.off()


# Wilcoxon's rank sum tests
for (j in A) {
  w <- wilcox.test(E$estimate[E$peakset == "specific" & E$annotation == j], E$estimate[E$peakset == "common" & E$annotation == j])
  cat(c(j, "::", w$p.value, "\n"))
}

Estimate <- E$estimate

jpeg("MSPC_FunctionalEnrichment/data/MACS/w3s8g4/EnrichmentTest_byAnnotation_boxplot_estimate.jpg", width = 25, height = 15, units = 'in', res = 300)
ggplot(E, aes(x = Dataset, y = Estimate, fill = Peak_set)) +
#geom_boxplot(outlier.size = 0.5) +
geom_boxplot(outlier.size = -1) +
#scale_fill_manual(values = c("deepskyblue", "green3", "gold")) +
scale_fill_manual(values = c("deepskyblue", "gold")) +
theme_bw() +
theme(panel.border = element_blank(),
  panel.grid.major = element_line(),
  panel.grid.minor = element_blank(),
  axis.line = element_line(colour = "black"),
  axis.text = element_text(size = 18),
  axis.title = element_text(size = 16, face = "bold"),
  legend.key.size = unit(1, "cm"),
  legend.text = element_text(size = 18),
  legend.title = element_text(size = 16)) +
annotate("text", x = 1, y = 1.1, size = 7,
         label = "N.cpg = 31,144\nP = 0.6749",
         parse = FALSE) +
annotate("text", x = 2, y = 1, size = 7,
         label = "N.dnase = 2,107,358\nP = 0.108",
         parse = FALSE) +
annotate("text", x = 3, y = 1.1, size = 7,
         label = "N.enh = 226,711\nP = 0.02331",
         parse = FALSE) +
annotate("text", x = 4, y = 1, size = 7,
         label = "N.exon = 313,276\nP = 0.4198",
         parse = FALSE) +
annotate("text", x = 5, y = 1.1, size = 7,
         label = "N.intr = 172,751\nP = 0.4369",
         parse = FALSE) +
annotate("text", x = 6, y = 1, size = 7,
         label = "N.nrs = 34,996\nP = 0.4412",
         parse = FALSE) +
annotate("text", x = 7, y = 1.1, size = 7,
         label = "N.prom = 67,635\nP = 0.3187",
         parse = FALSE) +
annotate("text", x = 8, y = 1, size = 7,
         label = "N.cod = 17,271\nP = 0.9042",
         parse = FALSE) +
annotate("text", x = 9, y = 1.1, size = 7,
         label = "N.ncod = 8,236\nP = 0.02117",
         parse = FALSE) +
scale_y_continuous(limits = c(0, 1.2))
dev.off()


# Enrichment plots by FACTOR

Dataset <- as.factor(E$dataset)

jpeg("MSPC_FunctionalEnrichment/data/MACS/w3s8g4/EnrichmentTest_byFactor_boxplot_zscore.jpg", width = 25, height = 15, units = 'in', res = 300)
ggplot(E, aes(x = Dataset, y = Enrichment_score, fill = Peak_set)) +
#geom_boxplot(outlier.size = 0.5) +
geom_boxplot(outlier.size = -1) +
#scale_fill_manual(values = c("deepskyblue", "green3", "gold")) +
scale_fill_manual(values = c("deepskyblue", "gold")) +
theme_bw() +
theme(panel.border = element_blank(),
  panel.grid.major = element_line(),
  panel.grid.minor = element_blank(),
  axis.line = element_line(colour = "black"),
  axis.text = element_text(size = 17),
  axis.text.x = element_text(angle = 30, hjust = 1),
  axis.title = element_text(size = 16, face = "bold"),
  legend.key.size = unit(1, "cm"),
  legend.text = element_text(size = 18),
  legend.title = element_text(size = 16)) +
scale_y_continuous(limits = c(0, 8000))
dev.off()

jpeg("MSPC_FunctionalEnrichment/data/MACS/w3s8g4/EnrichmentTest_byFactor_boxplot_estimate.jpg", width = 25, height = 15, units = 'in', res = 300)
ggplot(E, aes(x = Dataset, y = Estimate, fill = Peak_set)) +
#geom_boxplot(outlier.size = 0.5) +
geom_boxplot(outlier.size = -1) +
#scale_fill_manual(values = c("deepskyblue", "green3", "gold")) +
scale_fill_manual(values = c("deepskyblue", "gold")) +
theme_bw() +
theme(panel.border = element_blank(),
  panel.grid.major = element_line(),
  panel.grid.minor = element_blank(),
  axis.line = element_line(colour = "black"),
  axis.text = element_text(size = 17),
  axis.text.x = element_text(angle = 30, hjust = 1),
  axis.title = element_text(size = 16, face = "bold"),
  legend.key.size = unit(1, "cm"),
  legend.text = element_text(size = 18),
  legend.title = element_text(size = 16)) +
scale_y_continuous(limits = c(0, 1))
dev.off()
