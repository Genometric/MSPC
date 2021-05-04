#' @describeIn mspc Reads the inputs given by the user to the mspc function
#' @param Input Name of the inputs given ( Bed files or Granges objects). It could be a one sample file or multiple sample files. More information about the type of the inputs mspc can handle in Details
#' @param Input_folder Path of the location of the input files.
#' @param Directory_Granges_Inputs Folder path : when the inputs given are GRanges objects, bed files are created from these Granges objects. Where should they be stored ? The default value of the argument is the current working directory.

#' @return Name of input bed files. More information in Details
#'
#' @details
#'
#' When the user gives inputs as a bed file, the function returns the name of these bed files.
#' When the user gives inputs as Granges object, it exports it into bed files and returns
#' the name(s) of the bed files created by the function.
#' The bed files are created in the directory specified by Directory_Granges_Inputs
#' The names of the bed files created will be given to the mspc function as inputs.
#' @importFrom rtracklayer export
#' @importFrom methods is


read_inputs<-function(Input,Directory_Granges_Inputs){

  # We create an empty argument :
  Input_bed <-c()

  # we test if it's a GRange object
  if(is(object = Input,class2 ="GRanges")){
    # It is a GRanges object
    # I write the GRanges object as a BED file
    con<-paste0(Directory_Granges_Inputs,"/gr.bed")
    rtracklayer::export(object = Input,con = con,)
    Input_bed<-con
    attr(Input_bed,'type') <- '1'
  }
  # We test if it's a list of GRanges
  else if ((is(object = Input,class2 ="CompressedGRangesList"))) {

        # Here i need to write the file as a BED file
        # To do that, we need to access each element if the list of Granges objects

        for (i in 1:length(Input)){
          con<-paste0(Directory_Granges_Inputs,"/gr",i,".bed")
          # I have to unlist each Grange object of the list,
          # otherwise, it doesnt work
          rtracklayer::export(object = unlist(Input[i]),con = con)
          Input_bed<-append(Input_bed,con)
          attr(Input_bed,'type') <- '2'

        }
    }
  else{
    # Since the Input is neither e Granges object nor a Grange list
    # we suppose that it's a bed file, no transformation needed then!
    Input_bed<-Input
    attr(Input_bed,'type') <- '3'

  }
  return(Input_bed)
}
