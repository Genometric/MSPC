#' @describeIn mspc runs the mspc program from the command line
#' @return List of output of the mspc program
#' @noRd
#' @importFrom processx run
#' @importFrom stringr str_replace
#' @keywords internal

runMspc <- function(cmdArgs){
        cmdCommand <- c("dotnet")
        out <- processx::run(command=cmdCommand, args=cmdArgs, echo=TRUE)
        status <- out$status
        outStdout <- stringr::str_replace(out$stdout,pattern = "\r",replacement ="\n")
        outStdout <- strsplit(outStdout, split="\n")
        outStdout <- unlist(outStdout)
        exportDir <- gsub("Export Directory: ", "", outStdout[1])
        output <- list(out = out,status = status,exportDir = exportDir )
        return(output)
}
