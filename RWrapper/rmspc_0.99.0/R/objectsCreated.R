#' @describeIn mspc function that lists the objects returned when the MSPC 
#' program is run 
#' @return List of file names created
#' @noRd
#' @importFrom tools file_path_sans_ext
#' @keywords internal

objectsCreated <- function(input, directoryGRangesInput, exportDir){
    objCreated <- list()
    if (attr(input, "type") != 3) {
        inp <- c()
        if (length(input) > 1) {
            for (i in seq_len(length(input))) {
                inp <- c(inp, gsub(paste0(directoryGRangesInput, "/"),
                    "", input[i],
                    fixed=TRUE
                ))
                objCreated[[1]] <- inp
            }
        } else {
            objCreated[[1]] <- gsub(paste0(
                directoryGRangesInput,
                "/"
            ), "", as.character(input), fixed=TRUE)
        }
        names(objCreated) <- "GRanges"
    }
    files <- setdiff(list.files(exportDir), list.dirs(
        path=exportDir,
        recursive=FALSE, full.names=FALSE
    ))
    for (i in seq_len(length(files))) {
        names(files) <- tools::file_path_sans_ext(files)
        objCreated <- c(objCreated, files[i])
    }
    folderFiles <- getFiles(exportDir)
    objCreated <- c(objCreated, folderFiles)
    return(objCreated)
}