#' @describeIn mspc Checks the value of certain arguments
#' @return Value of argument directoryGRangesInput
#' @noRd
#' @keywords internal
#'
checkArgs <- function(directoryGRangesInput, GRanges, keep) {
    if (is.null(directoryGRangesInput)) {
        directoryGRangesInput <- getwd()
    }
    else {
        if (!dir.exists(directoryGRangesInput)) {
            stop("the directory specified by the directoryGRangesInput
             argument specified does not exist! ")
        }
    }
    if (!is.logical(GRanges)) {
        stop("The argument GRanges should be logical ")
    }
    if (!is.logical(keep)) {
        stop("The argument keep should be logical ")
    }
    return(directoryGRangesInput)
}
