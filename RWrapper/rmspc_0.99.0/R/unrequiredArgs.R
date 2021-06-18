#' @describeIn mspc define the arguments to give to the mspc program
#' @return a vector of unrequired arguments to give to mspc 
#' @noRd
#' @keywords internal

unrequiredArgs <- function(gamma = NULL,
            c = NULL, alpha = NULL, multipleIntersections = NULL,
            degreeOfParallelism = NULL, inputParserConfiguration = NULL,
            outputPath = NULL, GRanges = FALSE) {

    args <- c()
    if (!is.null(gamma)) {
        args <- append(args, c("-g", gamma))
    }
    if (!is.null(c)) {
        args <- append(args, c("-c", c))
    }
    if (!is.null(alpha)) {
        args <- append(args, c("-a", alpha))
    }
    if (!is.null(multipleIntersections)) {
        args <- append(args, c("-m", multipleIntersections))
    }
    if (!is.null(degreeOfParallelism)) {
        args <- append(args, c("-d", degreeOfParallelism))
    }
    if (!is.null(inputParserConfiguration)) {
        args <- append(args, c("-p", inputParserConfiguration))
    }
    if (!is.null(outputPath)) {
        args <- append(args, c("-o", outputPath))
    }
    if (GRanges == TRUE) {
        args <- append(args, "--excludeHeader")
    }
    return(args)
}
