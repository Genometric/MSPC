
# Package MSPC
# Function read_inputs

# This function reads the input files given by the user and checks if
# the inputs are bed files or a GRanges objects.
# if the input is a bed file, nothing happens.
# if it's a Granges list or Granges object, it transforms it into bed files
# and returns the name of the bed files created by the function,
# that will be given to the program mspc

# Note :
# This function only accepts one type of arguments.
# The user should either give all the input arguments as bed files
# or as GRanges objects, iÉe, the user cannot give an input argument that contains
# bed files and GRanges objects.

library(GenomicRanges)
library(rtracklayer)

read_inputs<-function(Input){

  # We create an empty argument :
  Input_bed <-c()

  # we test it's a GRange object
  if(is(object = Input,class2 ="GRanges")){
    # It is a GRanges object
    # I write the GRanges object as a BED file
    con<-paste0("gr.bed")
    export(object = Input,con = con)
    Input_bed<-con
  }
  # We test if it's a list of GRanges
  else if ((is(object = Input,class2 ="CompressedGRangesList"))) {

    # Here i need to write the file as a BED file
    # Each element of the GRanges list is exported as a bed file

    for (i in 1:length(Input)){
      con<-paste0("gr",i,".bed")
      # I have to unlist each Grange object of the list,
      # otherwise, it doesnt work
      export(object = unlist(Input[i]),con = con)
      Input_bed<-append(Input_bed,con)
      # Note: the file is exported to the current working directory
      # I don't know if i should put the files in another directory
      }
    }
  else{
    # Since the Input is neither e Granges object nor a Grange list
    # we suppose that it's a bed file, no transformation needed then!
    Input_bed<-Input
  }
  return(Input_bed)
}
