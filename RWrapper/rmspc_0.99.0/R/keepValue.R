#' @describeIn mspc Defines the value of the argument keep
#' @return value of argument keep, according to the class of the input argument
#' @noRd
#' @keywords internal

keepValue <- function(keep, input) {
    if (is.null(keep)) {
        if (is(object=input, class2="GRanges") | is(
            object=input,
            class2="CompressedGRangesList"
        )) {
            keep <- FALSE
        } else {
            keep <- TRUE
        }
    }
    return(keep)
}
