# Summary boxplots generation script for hg38 annotations

# Takes the output of validation.py or recursive.py
# (i.e., EnrichmentTest_results.txt) as input

library("ggplot2")

args <- (commandArgs(TRUE))

etest <- read.delim(args[1], stringsAsFactors = FALSE)
E <- etest[etest$propAnnoPeaks > 0,]


# Enrichment plots by ANNOTATION

Dataset <- as.factor(E$annotation)
Enrichment_score <- E$z
Peak_set <- as.factor(E$peakset)

jpeg("EnrichmentTest_byAnnotation_boxplot_zscore.jpg", width = 25, height = 15, units = 'in', res = 300)
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
         label = "N.cpg = 31,144",
         parse = FALSE) +
annotate("text", x = 2, y = 7800, size = 7,
         label = "N.dnase = 2,107,358",
         parse = FALSE) +
annotate("text", x = 3, y = 8700, size = 7,
         label = "N.enh = 226,711",
         parse = FALSE) +
annotate("text", x = 4, y = 7800, size = 7,
         label = "N.exon = 313,276",
         parse = FALSE) +
annotate("text", x = 5, y = 8700, size = 7,
         label = "N.intr = 172,751",
         parse = FALSE) +
annotate("text", x = 6, y = 7800, size = 7,
         label = "N.nrs = 34,996",
         parse = FALSE) +
annotate("text", x = 7, y = 8700, size = 7,
         label = "N.prom = 67,635",
         parse = FALSE) +
annotate("text", x = 8, y = 7800, size = 7,
         label = "N.cod = 17,271",
         parse = FALSE) +
annotate("text", x = 9, y = 8700, size = 7,
         label = "N.ncod = 8,236",
         parse = FALSE) +
scale_y_continuous(limits = c(0, 8800))
dev.off()

Estimate <- E$estimate

jpeg("EnrichmentTest_byAnnotation_boxplot_estimate.jpg", width = 25, height = 15, units = 'in', res = 300)
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
         label = "N.cpg = 31,144",
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

jpeg("EnrichmentTest_byFactor_boxplot_zscore.jpg", width = 25, height = 15, units = 'in', res = 300)
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

jpeg("EnrichmentTest_byFactor_boxplot_estimate.jpg", width = 25, height = 15, units = 'in', res = 300)
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
