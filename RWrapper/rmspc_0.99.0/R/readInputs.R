#' @describeIn mspc Reads the inputs given by the user to the
#' mspc function
#' @return Name of input bed files. More information in Details
#' @details
#' When the user gives inputs as a bed file, the function returns
#' the name of these bed files.
#' When the user gives inputs as Granges object, it exports it
#' into bed files and returns
#' the name(s) of the bed files created by the function.
#' The bed files are created in the directory specified by
#' directoryGRangesInput
#' The names of the bed files created will be given to the
#' mspc function as inputs.
#' @importFrom rtracklayer export
#' @importFrom methods is
#' @noRd
#' @keywords internal

readInputs <- function(input, directoryGRangesInput) {
    inputBed <- c()
    if (is(object = input, class2 = "GRanges")) {
        con <- paste0(
            directoryGRangesInput,
            "/gr.bed"
        )
        rtracklayer::export(
            object = input,
            con = con,
        )
        inputBed <- con
        attr(inputBed, "type") <- "1"
    } else if ((is(
        object = input,
        class2 = "CompressedGRangesList"
    ))) {
        for (i in seq_len(length(input))) {
            con <- paste0(
                directoryGRangesInput,
                "/gr", i, ".bed"
            )
            rtracklayer::export(
                object = unlist(input[i]),
                con = con
            )
            inputBed <- append(
                inputBed,
                con
            )
            attr(inputBed, "type") <- "2"
        }
    } else {
        inputBed <- input
        attr(inputBed, "type") <- "3"
    }
    return(inputBed)
}
