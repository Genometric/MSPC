#! /usr/bin/Rscript

suppressPackageStartupMessages(library("ggplot2"))

args <- (commandArgs(TRUE))
setwd(dirname(args[1]))
etest <- read.delim(args[1], stringsAsFactors = FALSE)

if (args[2] %in% c("boxplot", "all")) {
	box <- paste(c(strsplit(args[1], "\\.")[[1]][1],
	             "_boxplot.pdf"),
	             collapse = "")
	# Enriched peaks threshold
	E <- etest[etest$N_peaks >= 20,]
	# Using enrichment test z score
	Enrichment_score <- E$z
	# Using enrichment test P-value
	#Enrichment_score <- -log10(E$pvalue + 2.2E-16)
	Dataset <- as.factor(E$peakset)
	# Boxplot building
	
	ggplot(E, aes(x = Dataset, y = Enrichment_score, fill = Dataset)) +
	geom_boxplot(outlier.size = 0.5) +
	#geom_boxplot(outlier.size = -1) +                # No outliers
	scale_fill_manual(values = c("deepskyblue", "green3", "gold")) +
	theme_bw() +
	theme(panel.border = element_blank(),
		  panel.grid.major = element_line(),
		  panel.grid.minor = element_blank(),
		  axis.line = element_line(colour = "black"),
		  axis.text = element_text(size = 12),
		  legend.key.size = unit(1, "cm"),
		  legend.text = element_text(size = 12))
	# +
	#scale_y_continuous(limits = c(-40, 40))         # rescale y axis
}

if (args[2] %in% c("density", "all")) {
	den <- paste(c(strsplit(args[1], "\\.")[[1]][1],
	             "_density.pdf"),
	             collapse = "")
	
	E <- etest[etest$N_peaks >= 20,]
	
	plot(density(E$z[E$peakset == "common"], bw = 5),
		 col = "blue",
		 lwd = 2,
		 ylim = c(0, 0.08),
		 xlab = "Enrichment z value",
		 main = "MSPC vs IDR functional enrichment")
	lines(density(E$z[E$peakset == "specific"], bw = 5),
		  col = "red3",
		  lwd = 2)
	lines(density(E$z[E$peakset == "missing"], bw = 5),
		  col = "darkorange",
		  lwd = 2)
	legend("topright",
		   fill = c("blue", "red3", "darkorange"),
		   bg = "white",
	legend = c("Common peak enrichment",
			   "MSPC-specific enrichment",
			   "IDR-specific enrichment"),
	lty = c(1, 1, 1))
	abline(v = 0, lty = 2)
}

if (args[2] %in% c("scatter", "all")) {
	scatter <- paste(c(strsplit(args[1], "\\.")[[1]][1],
	                 "_scatter.pdf"),
	                 collapse = "")
	
	# Enriched peaks threshold
	E <- etest[etest$N_peaks >= 20,]
	# Defining dataset colors
	E$color <- "None"
	E$color[E$peakset == "specific"] <- "darkred"
	E$color[E$peakset == "missing"] <- "darkorange"
	E$color[E$peakset == "common"] <- "blue"
	# Defining axis
	Enrichment_score <- E$z                   # Enrichment z score
	Pvalue <- -log10(E$pvalue + 2.2E-16)      # Enrichment P-value
	Dataset <- E$color
	x <- Enrichment_score
	y <- Pvalue
	
	ggplot(E, aes(x = Enrichment_score, y = Pvalue)) +
	geom_point(shape = 5, col = Dataset) +
	#geom_smooth(method = lm, formula = y ~ x,
	#            se = FALSE,
	#            size = 0.6,
	#            col = "darkblue",
	#            lty = 2) +
	#geom_text(x = 2.7, y = 6.5,
	#          label = "r = 0.847",
	#          parse = FALSE,
	#          cex = 6,
	#          col = "darkblue") +
	#geom_hline(yintercept = 4.088,
	#           col = "darkgreen", size = 0.5, lty = 2) +
	#geom_vline(xintercept = 4.187,
	#           col = "darkgreen", size = 0.5, lty = 2) +
	theme_bw() +
	theme(panel.border = element_blank(),
		  panel.grid.major = element_line(),
		  panel.grid.minor = element_blank(),
		  axis.line = element_line(colour = "black"))
	# +
	#scale_x_continuous(limits = c(0, 6)) +
	#scale_y_continuous(limits = c(0, 10))
}
