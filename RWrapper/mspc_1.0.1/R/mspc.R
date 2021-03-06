
#' Runs the mspc program on R
#'
#' MSPC comparatively evaluates ChIP-seq peaks and combines the statistical significance
#' of repeated evidences, with the aim of lowering the minimum significance required
#' to "rescue" weak peaks; hence reducing false-negatives.
#'
#' @param Input Name of the inputs given ( Bed files or Granges objects). It could be a one sample file or multiple sample files. More information about the type of the inputs mspc can handle in Details
#' @param Input_folder Path of the location of the input files.
#' @param Directory_Granges_Inputs Folder path : when the inputs given are GRanges objects, bed files are created from these Granges objects. Where should they be stored ? The default value of the argument is the current working directory.
#' @param Mspc_path Installation path of the mspc executable, if it's installed. Otherwise, the version of mspc used is the one in the package.
#' @param Replicate_Type Character string : possible values : "Bio", "Biological", "Tec", "Technical". This argument defines the replicate type.
#' @param Stringency_threshold Integer : a threshold on p-values, where peaks with p-value lower than this threshold, are considered stringent.
#' @param Weak_threshold Integer : a threshold on p-values, such that peaks with p-value between this and stringency threshold, are considered weak peaks.
#' @param Gamma Integer : the combined stringency threshold. Peaks with combined p-value below this threshold are confirmed.
#' @param C Integer : the minimum number of overlapping peaks required before MSPC combines their p-value.
#' @param Alpha Integer : the threshold for Benjamini-Hochberg multiple testing correction.
#' @param Multiple_Intersections Character string : either "lowest" or "highest". When multiple peaks from a sample overlap with a given peak, this argument defines which of the peaks to be considered: the one with lowest p-value, or the one with highest p-value?
#' @param Degree_of_Parallelism  Integer : the number of parallel threads MSPC can utilize simultaneously when processing data.
#' @param Input_Parser_Configuration Path to a JSON file containing the configuration for the input BED file parser.
#' @param Output_path Path in which analysis results should be persisted.
#' @param Granges Logical : determines whether or not the wrapper function should read the results of MSPC and store them as GRanges objects in the R environment.The default value is FALSE.
#' @param Keep Logical : determines whether the mspc function should keep or delete all the files generated while running the mspc program.If set to FALSE, mspc will automatically set the argument Granges to TRUE. More information on the default value of the argument in Details.
#' @return A list of files, according to the values of the arguments. More information in Details
#'
#' @details
#'
#' Only one type of arguments is supported by mspc at a time, ie, the user can either
#' give all the inputs as bed files, or all the inputs as Granges objects.
#' Therefore, the user cannot give an input argument that contains
#' bed files and GRanges objects.
#' If the user gives the inputs as Granges objects, he can either give a single Granges object
#' or a list of Granges objects.
#' Similarly, the user can either give one bed file or a character string of multiplie bed file names.
#'
#' The mspc function prints the summary of the mspc program results.
#'
#' @export
#' @importFrom processx run
#' @importFrom tools file_path_sans_ext
#' @importFrom methods is
#' @importFrom rtracklayer import


mspc<-function(Input,
               Input_folder = NULL,
               Directory_Granges_Inputs = NULL,
               Mspc_path = NULL,
               Replicate_Type,
               Stringency_threshold,
               Weak_threshold,
               Gamma=NULL,
               C=NULL,
               Alpha=NULL,
               Multiple_Intersections=NULL,
               Degree_of_Parallelism = NULL,
               Input_Parser_Configuration=NULL,
               Output_path=NULL,
               Granges=FALSE,
               Keep = NULL){


  # We check the value of the argument Directory_Granges_Inputs
  if(is.null(Directory_Granges_Inputs)){
    Directory_Granges_Inputs<-getwd()
  }

  # We set the default value of the argument Keep according to the type of Inputs if the user
  # doesn't give a value of the argument keep

   if(is.null(Keep)){
     if(is(object = Input,class2 ="GRanges") |is(object = Input,class2 ="CompressedGRangesList") ){
       # It means it's not a bed file
       Keep = FALSE
     }
     else{
       Keep = TRUE
     }
   }

  # We check the value of Keep

  if (Keep==FALSE){

    # We define a temporary working directory
    temp_dir <- tempdir(check = FALSE)
    # We then set all the directory paths to this temporary directory
    # which means the GRanges objects ( if exists) will be exported to
    # the temporary folder
    # the files generated by the mspc program will also be created
    # in this temporary folder
    Directory_Granges_Inputs <- temp_dir
    Output_path <- temp_dir

    # We set the argument Granges to TRUE
    # We automatically load all the mspc generated files into the R environment
    # when the argument keep if FALSE
    Granges = TRUE
  }

    # We read the Input argument using the read_inputs function :

  Input<-read_inputs(Input,Directory_Granges_Inputs)


  # We create 2 vectors where we will store the unrequired arguments
  # and required arguments that are given by the user

  unrequired_args<-c()
  cmd_args<- c()

  # Step 1 : We first check what are the unrequired arguments given by the user

  # We check if the user gave a value for Input_folder :

  if (!is.null(Input_folder)){
    cmd_args<-c("-f",Input_folder)
  }
  #else{
  #Input_folder = getwd()
  #}

  # We create a vector where we specify the command to execute the mspc executable : mspc.exe
  cmd_command<-c()

  if(!is.null(Mspc_path)){
      cmd_command<-Mspc_path
  }
  else{

    # If Mspc_path = NULL
    # We use the mspc in the R package
    Mspc_path <- system.file("CLI", package="mspc")
    Mspc_path <- paste0(Mspc_path,"/mspc.exe")
    cmd_command<-Mspc_path
  }


  # We check if the user gave a value for the argument Gamma

  if(!is.null(Gamma)){
    unrequired_args<-append(unrequired_args,c("-g",Gamma))
  }

  # We check if the user gave a value for the arg C

  if(!is.null(C)){
    unrequired_args<-append(unrequired_args,c("-c",C))
  }

  # We check if the user gave a value for the arg Alpha

  if(!is.null(Alpha)){
    unrequired_args<-append(unrequired_args,c("-a",Alpha))
  }

  # We check if the user gave a value for the arg Multiple_Intersections

  if(!is.null(Multiple_Intersections)){
    unrequired_args<-append(unrequired_args,c("-m",Multiple_Intersections))
  }

  # We check if the user gave a value for the arg Degree_of_Parallelism

  if(!is.null(Degree_of_Parallelism)){
    unrequired_args<-append(unrequired_args,c("-d",Degree_of_Parallelism))
  }

  # We check if the user gave a value for the arg Input_Parser_Configuration

  if(!is.null(Input_Parser_Configuration)){
    unrequired_args<-append(unrequired_args,c("-p",Input_Parser_Configuration))
  }

  #  We check if the user gave a value for the arg Output_path

  if(!is.null(Output_path)){
    unrequired_args<-append(unrequired_args,c("-o",Output_path))
  }

  # Step 2 : We now call the executable


  # We define the required arguments
  cmd_args <- append(cmd_args,c('-i',as.character(Input),"-r", Replicate_Type,"-s",Stringency_threshold,
                                "-w", Weak_threshold))

  # We add the unrequired arguments given by the user
  cmd_args<-append(cmd_args,unrequired_args)

  # We exclude the header of the bed files if we want to import them as Granges obj

  if(Granges==TRUE){
    cmd_args<-append(cmd_args,"--excludeHeader")
  }

  # We call the command line using the run function
  out<-processx::run(command =cmd_command, args =cmd_args,echo = TRUE)

  status <- out$status
  out_stdout<-strsplit (x =out$stdout,split =  "\r\n")
  out_stdout<-unlist(out_stdout)

  # We capture the export directory from the output of mspc

  Export_dir <- gsub("Export Directory: ", "", out_stdout[1])

  # Sometimes, there is an additional white space in the export directory, so we remove it
  # to be sure
  Export_dir<-gsub(" ", "", Export_dir, fixed = TRUE)

  # We create a list that will contain the names of all the files created while running mspc
  # ( including the bed files that might be created if the user gave inputs as GRanges objects)

  obj_created <- list()

  if(attr(Input,"type")!=3){
    # It means it's not a bed file, which means the user gave a granges object as inputs,
    # which was then exported as a bed file, which means we have to list the bed
    # files created as well

    # We need to capture the name of the files created from the object Input, since the object
    # Input contains the file path and file name, and we only want the file name
    inp<-c()
    if(length(Input)>1){
      # it means that we have several Granges objects
      for (i in 1:length(Input)){
        inp<-c(inp,gsub(paste0(Directory_Granges_Inputs,"/"), "", Input[i],fixed = T))
        obj_created[[1]]<-inp
      }
    }else{
      obj_created[[1]]<-gsub(paste0(Directory_Granges_Inputs,"/"), "", as.character(Input),fixed = T)
    }
    names(obj_created)<-"Granges"
  }


  # We write the names of the files created by mspc
  files<-setdiff(list.files(Export_dir), list.dirs(path = Export_dir,recursive = FALSE, full.names = FALSE))

  for (i in 1:length(files)){
    names(files)<-tools::file_path_sans_ext(files)
    obj_created<-c(obj_created,files[i])
  }

  # Now write the names of the files in each folder
  folder_files<-get_files(Export_dir)
  obj_created<-c(obj_created,folder_files)

  if(Granges==TRUE){
    # This means the user wants us to return as well the bed files created as
    # granges objects
    temp = list.files(path = Export_dir ,pattern="*.bed",recursive = TRUE,full.names = TRUE)
    Granges_obj = lapply(setNames(temp, make.names(gsub("*.bed$", "", temp))), import)
  }

  if(Keep == TRUE){
    if (Granges==TRUE){
      results<-list(status=status,
                    files_created = obj_created,
                    Granges_objects=Granges_obj
                    )
    }else{
      results<-list(status=status,
                    files_created =obj_created
                    )
    }
    cat("A list of files were created by the mspc program in the directory :\n",Export_dir,"\n")
  }else{
    results<-list(status=status,
                  Granges_objects=Granges_obj)
  }

  return(results)
}
