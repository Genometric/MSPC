#' Runs the mspc program on R
#'
#' MSPC comparatively evaluates ChIP-seq peaks and combines the
#' statistical significance of repeated evidences, with the aim
#' of lowering the minimum significance required to rescue
#' weak peaks; hence reducing false-negatives.
#'
#' @param input Character vector or GRanges object. 
#' The input argument is a character vector when the data the user wants to use 
#' as sample is in a BED file format. In this case, the data should 
#' be a tab-delimited file in BED format for each replicate, each 
#' containing enriched regions (aka peaks) called with a
#' permissive p-value threshold.
#' The input argument will then be a character vector, each element of the 
#' vector will have the file path of the BED file the user wants to use. 
#' The input argument is a GRanges object when the data the user 
#' wants to use as a sample is a GRanges object. 
#' In this case, the input argument is a single GRanges object
#' or a GRanges list, if the user wants to use multiple GRanges objects
#' as samples. This arguments is required. More information in Details
#' @param replicateType Character string. This argument defines
#' the replicate type. Possible values of the argument : 
#' 'Bio','Biological', 'Tec', 'Technical'. 
#' This arguments is required . More information in Details. 
#' @param stringencyThreshold Double. A threshold on p-values,
#' where peaks with p-value lower than this threshold, are
#' considered stringent.This arguments is required
#' @param WeakThreshold Double. A threshold on p-values,
#' such that peaks with p-value between this and stringency
#' threshold, are considered weak peaks. This arguments is required
#' @param gamma Double. The combined stringency threshold.
#' Peaks with combined p-value below this threshold are confirmed.
#' Default value is the value of the argument stringencyThreshold.
#' This arguments is optional.
#' @param c Integer or character string. the minimum number of overlapping peaks
#' required before the MSPC program combines their p-value.
#' The value of C can be given in absolute (e.g., C = 2 will require
#' at least 2 samples) or percentage of input samples (e.g., C = 50% 
#' will require at least 50% of input samples) formats.
#' Default value is 1. This arguments is optional.
#' More information in Details.
#' @param alpha Double. The threshold for Benjamini-Hochberg
#' multiple testing correction.
#' Default value is 0.05. This arguments is optional.
#' @param multipleIntersections Character string. When multiple peaks
#' from a sample overlap with a given peak, this argument
#' defines which of the peaks to be considered: the one with
#' lowest p-value, or the one with highest p-value?
#' Possible value of the argument are either 'Lowest'
#' or 'Highest'.
#' Default value is 'Lowest'. This arguments is optional.
#' @param degreeOfParallelism  Integer. The number of parallel
#' threads the MSPC program can utilize simultaneously when 
#' processing data.
#' Default value is the number of logical processors
#' on the current machine. This arguments is optional.
#' @param inputParserConfiguration File path. The path to a JSON file
#' containing the configuration for the input BED file parser.
#' This is an optional argument and it has no default value. 
#' @param outputPath Directory path. This argument sets the path in 
#' which analysis results should be persisted. 
#' This is an optional argument. More information in Details. 
#' @param keep Logical. This argument determines whether the mspc function
#' should keep or delete all the files generated while running
#' the MSPC program.This is an optional argument.
#' More information in Details.
#' @param GRanges Logical. Dtermines whether or not the mspc
#' function should import the files, created while running the mspc function, 
#' as GRanges objects into the R environment. The default value is FALSE.
#' However, when the keep argument is set to FALSE, the value 
#' of the argument GRanges is set to TRUE, and the value
#' given by the user to the GRanges argument is ignored. 
#' @param directoryGRangesInput Folder path. When the input argument is
#' a GRanges object (or GRanges list), the mspc function exports it as
#' a BED file ( or multiple BED files ).
#' The directoryGRangesInput argument specifies the 
#' directory where these BED files should be stored.
#' More information in Details. 
#' @param mspcPath File path. The MSPC program that the rmspc package
#' uses can be intalled from the official Github page of the MSPC program.
#' If the users wishes to use the version of the MSPC program he installs, 
#' he can specify the installation path of the mspc dll program installed
#' using the mspcPath argument.
#' The default value is NULL. If no value is given to this argument,
#' the MSPC program used is the one included in the rmspc package.
#'
#' @return 
#' The mspc function prints the results of the MSPC program.   
#' 
#' The mspc function also creates a set of files in the directory 
#' specified by the argument outputPath.
#' 
#' The function can return the following :
#' 
#' 1. status : Integer. The exit status of running the mspc function. A zero exit
#' status means the function ran successfully.   
#' 
#' 2. filesCreated : List of character vectors. The names of the files generated 
#' while running the mspc function.   
#' 
#' 3. GrangesObjects : GRanges list. All the files generated while running the mspc function
#' are imported as GRanges objects, and are combined in a GRanges list. 
#'
#' It is important to note that the mspc function does not always return these 3 elements.   
#' 
#' The output of the function depends on the arguments keep and GRanges given to the 
#' mspc function. 
#' 
#' More information regarding the output in Details. 
#'  
#' @details
#' 
#' input :     
#' 
#' The input can either be BED files or GRanges objects.
#' Only one type of inputs is supported by the mspc function 
#' at a time, ie, the user can either give all the inputs as names of 
#' BED files, or all the inputs as GRanges objects. 
#' Therefore, the user cannot give an input argument that contains BED files
#' and GRanges objects.
#' If the user gives the inputs as GRanges objects, he can either give a
#' single GRanges object or a list of GRanges objects.
#' Similarly, the user can either give one BED file or a character
#' string of multiple BED file names.
#'
#' replicateType:    
#' 
#' Samples could be biological or technical replicates. 
#' MSPC differentiates between the two replicate types
#' based on the fact that less variations between 
#' technical replicates is expected compared to biological 
#' replicates. 
#' 
#' C :    
#' 
#' It sets the minimum number of overlapping peaks required before MSPC
#' combines their p-value. For example, given three replicates 
#' (rep1, rep2 and rep3), if C = 3, a peak on rep1 must overlap 
#' with at least two peaks, one from rep2 and one from rep3,
#' before MSPC combines their p-value; otherwise, MSPC discards
#' the peak. If C = 2, a peak on rep1 must overlap with at 
#' least one peak from either rep2 or rep3, before MSPC combines
#' their p-values; otherwise MSPC discards the peak.
#' 
#' outputPath:    
#' 
#' When the mspc function is called, it creates a set of files 
#' in the directory specified by the argument outputPath. If no value is given
#' to this argument, a folder is created in the current working directory, under
#' the name "session_ + <Timestamp>". 
#' If a folder name is given to the argument outputPath, a folder under the name 
#' specified is created in the current working directory.
#' If a given folder name already exists, and is not empty, the MSPC program
#' will append _n where n is an integer until no duplicate is
#' found in which analysis results should be persisted.
#' 
#' keep :    
#' 
#' When the mspc function is called, it creates a set of files
#' in the user's computer. The user can chooses to keep or not the
#' files created. 
#' When the argument keep is set to FALSE, all the files 
#' are created in a temporary folder, which is deleted after
#' the R session is closed. 
#' When the argument keep is set to TRUE, the files are created
#' in the folder specified by the argument outputPath.
#' The default value of the argument keep is defined as follows : 
#' if the input argument is a GRanges object, the default value
#' of the keep argument is FALSE.
#' if the input argument is a character vector of 
#' the file path of input BED files, the default value of the
#' keep argument is TRUE. 
#' 
#' directoryGRangesInput :    
#' 
#' The default value is the current working directory. 
#' It is important to note that when the argument keep is set to FALSE, 
#' the value of this argument is set to a temporary folder.
#' If the input argument is a character vector of BED files names,
#' the argument directoryGRangesInput is ignored.
#' 
#' Output of the mspc function :    
#' 
#' When the value of the argument keep is set to FALSE,
#' the argument GRanges is automatically set to TRUE. 
#' all the files are created in a temporary folder, 
#' which is deleted after the R session is closed. 
#' The files created are also imported to the R environment 
#' as GRanges objects. 
#' 
#' In this case, the function mspc returns the following :  
#'
#' 1. status  
#' 
#' 2. GRangesObjects
#'
#' When the value of the argument GRanges is set to FALSE
#' and the argument keep is set to TRUE, no GRanges object will be 
#' imported to the R environment. 
#' In this case, the function mspc returns the following :  
#' 
#' 1. status   
#' 
#' 2. filesCreated. 
#' 
#' 
#' @export
#' @import BiocManager
#' @importFrom methods is
#' @importFrom rtracklayer import
#' @importFrom GenomicRanges GRangesList
#'
#' @examples
#'
#' path <- system.file("extdata", package="rmspc")
#' input1 <- paste0(path, "/rep1.bed")
#' input2 <- paste0(path, "/rep2.bed")
#' input <- c(input1, input2)
#' results <- mspc(
#' input = input, replicateType = "Technical",
#' stringencyThreshold = 1e-8,
#' WeakThreshold = 1e-4, gamma = 1e-8,
#' keep = FALSE,GRanges = TRUE,
#' multipleIntersections = "Lowest",
#' c = 2,alpha = 0.05)
#' 
#' 
#' library(GenomicRanges)
#' library(rtracklayer)
#' GR1 <- import(input1, format="bed")
#' GR2 <- import(input2, format="bed")
#' GR <- GRangesList("GR1"=GR1, "GR2"=GR2)
#' results <- mspc(
#' input = GR, replicateType = "Biological",
#' stringencyThreshold = 1e-8, WeakThreshold = 1e-4,
#' gamma =  1e-8, GRanges = TRUE, keep = FALSE,
#' multipleIntersections = "Highest",
#' c = 2,alpha = 0.05)
#'
mspc <- function(input, replicateType,
            stringencyThreshold, WeakThreshold, gamma=NULL,
            c=NULL, alpha=NULL, multipleIntersections=NULL,
            degreeOfParallelism=NULL, inputParserConfiguration=NULL,
            outputPath=NULL, GRanges=FALSE, keep=NULL,
            directoryGRangesInput=NULL, mspcPath=NULL) {
    keep <- keepValue(keep, input)
    tempDir <- tempdir(check=FALSE)
    if (keep == FALSE) {
        directoryGRangesInput <- tempDir
        outputPath <- tempDir
        GRanges <- TRUE
    }
    directoryGRangesInput <- checkArgs(directoryGRangesInput, GRanges, keep)
    input <- readInputs(input, directoryGRangesInput)
    if (is.null(mspcPath)) {
        zipPath <- system.file("CLI", package="rmspc")
        zipPath <- paste0(zipPath, "/mspc.zip")
        utils::unzip(zipfile = zipPath,exdir =tempDir )
        mspcPath <- paste0(tempDir,"/mspc.dll")
    }
    cmdArgs <- c(mspcPath, "-i", as.character(input), "-r", replicateType,
            "-s", stringencyThreshold, "-w", WeakThreshold)
    cmdArgs <- append(cmdArgs, unrequiredArgs(gamma ,c , alpha,
            multipleIntersections, degreeOfParallelism,
            inputParserConfiguration, outputPath, GRanges))
    output<-runMspc(cmdArgs)
    status <- output$status
    exportDir <- output$exportDir
    objCreated <- objectsCreated(input, directoryGRangesInput, exportDir)
    if (GRanges == TRUE) {
        temp <- list.files(
            path=exportDir, pattern="*.bed",
            recursive=TRUE, full.names=TRUE
        )
        GRangesObj <- lapply(temp, rtracklayer::import)
        namesGranges <- gsub(temp,pattern = paste0(exportDir,"/"),replacement = "",fixed = TRUE)
        namesGranges <- gsub(namesGranges,pattern = ".bed",replacement = "",fixed = TRUE)
        names(GRangesObj) <- namesGranges
    }
    results <- results(keep, GRanges, status, objCreated,
                GRangesObj, exportDir)
    return(results)
}
