#' @describeIn mspc Get a list of files in a folder
#'
#' @param path A directory path
#' @return List of files in a folder and subfolders.
#' @importFrom stats setNames

get_files <- function(path) {
  folders <- list.dirs(path,recursive=FALSE,full.names=FALSE)
  if (length(folders)==0) list.files(path)
  else {
    sublist <- lapply(paste0(path,"/",folders),get_files)
    stats::setNames(sublist,folders)
  }
}
