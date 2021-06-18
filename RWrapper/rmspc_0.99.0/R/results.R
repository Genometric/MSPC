#' @describeIn mspc Creates the output of the mspc function
#' @return List of results of mspc
#' @noRd
#' @keywords internal

results <- function(keep, GRanges, status,objCreated, GRangesObj,
                exportDir) {
    if (keep == TRUE) {
        if (GRanges == TRUE) {
            results <- list(
                status = status, filesCreated = objCreated,
                GRangesObjects = GRangesObj
            )
        } else {
            results <- list(status = status, filesCreated = objCreated)
        }
        cat(
            "A list of files were created by the mspc program
        in the directory :\n",
            exportDir, "\n"
        )
    } else {
        results <- list(status = status, GRangesObjects = GRangesObj)
    }
    return(results)
}
