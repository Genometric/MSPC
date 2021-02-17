# We define the function mspc :


#' @title Wrapper function for the mspc function
#'
#' @param Input name of the sample BED file. It could be a character string if there one sample file, or a character vector if there are multiple sample files
#' @param Input_folder path of the location of the input files
#' @param Mspc_path the installation path of the mspc executable
#' @param Replicate_Type a character string, possible values : "Bio", "Biological", "Tec", "Technical". This argument defines the replicate type
#' @param Stringency_threshold integer : a threshold on p-values, where peaks with p-value lower than this threshold, are considered stringent
#' @param Weak_threshold integer : a threshold on p-values, such that peaks with p-value between this and stringency threshold, are considered weak peaks.
#' @param Gamma integer : the combined stringency threshold. Peaks with combined p-value below this threshold are confirmed
#' @param C integer : the minimum number of overlapping peaks required before MSPC combines their p-value
#' @param Alpha integer : the threshold for Benjamini-Hochberg multiple testing correction
#' @param Multiple_Intersections A character string, either "lowest" or "highest". When multiple peaks from a sample overlap with a given peak, this argument defines which of the peaks to be considered: the one with lowest p-value, or the one with highest p-value?
#' @param Degree_of_Parallelism  integer : the number of parallel threads MSPC can utilize simultaneously when processing data.
#' @param Input_Parser_Configuration the path to a JSON file containing the configuration for the input BED file parser.
#' @param Output_path the path in which analysis results should be persisted.


mspc<-function(Input,
               Input_folder = NULL ,
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
               Output_path=NULL){


  # We create 2 vectorz where we will store the unrequired arguments
  # and required arugments that are given by the user

  unrequired_args<-c()
  cmd_args<- c()

  # Step 1 : We first test if the values of the arguments are valid

  #We check if the value of Input_folder is valid :

  if (!is.null(Input_folder)){
    if(!dir.exists(Input_folder)){
      stop("the input folder specified does not exist ")
    }
    cmd_args<-c("-f",Input_folder)
  }
  #else{
    #Input_folder = getwd()
  #}


  # We check if the Input argument refers to an existing file :
  # We can't check this as long as the issue with combining
  # the arguments -i and -f hasn't been fixed

  #for(i in 1:length(Input)){
    #We determine the file path of each element of Input
   # path_input<-paste0(Input_folder,"\\",Input[i])

   # if(!file_test(op="-f",x = path_input)){
   #   stop("The element ",i," of the Input argument is not a valid bed file path,the file does not exist.")
   # }
  #}

  # We check if the Input argument is a bed file
  if(any(!tools::file_ext(Input)=="bed")){
    stop("one or several of the input files is not in the bed format")
  }

  # We check if the value of mspc_path is valid :

  # We create a vector where specify the command to execute the executable
  cmd_command<-c()

  if(!is.null(Mspc_path)){
    # We test if the user gave a directory or a file path
    if(tools::file_ext(Mspc_path)==""){
      #This means that the user gave a directory path of the mspc executable

      # We test if the directory exists
      if(!dir.exists(Mspc_path)){
        stop("the Mspc path specified does not exist ")
      }

      # We check if the executable exists in the folder specified
      if(!file_test(op="-f",x=paste0(Mspc_path,"\\mspc.exe"))){
        stop("The executable mspc does not exist in the folder specified")
      }

      # We now know the executable is in the folder,
      # which means we need to add the file name at the end of the path
      else{
        cmd_command<-paste0(Mspc_path,"\\mspc.exe")
      }
    }

    # We test if the file path given is of an executable file
    else if(tools::file_ext(Mspc_path)=="exe"){
      cmd_command<-Mspc_path
    }
    else{
      stop("the path given is neither a folder path nor the mspc executable file")
    }
  }
  # If Mspc_path = NULL
  else{

    # We check if the path of mspc.exe is included in the PATH variable
    #path is the PATH variable
    path<-Sys.getenv("PATH")
    if(!grepl(pattern = "mspc",x = path)){
      # This means that the installation path of mspc is not in PATH
      stop("The installation path of mspc is not included in the PATH variable
           and it has not been specified")
    }
    else{
      # This means the installation path of mspc is included in the PATH variable
      # which means we don't have to specify the mspc.exe path to execute it
      cmd_command<-c("mspc.exe")
    }
  }

  # We check if the value of Replicate_Type is valid
  Replicate_Type = match.arg(Replicate_Type, c("bio", "biological", "tec", "technical"))


  # We check if the value of Weak_threshold is valid

  # We add the following line of code in case the user gave the argument
  # Weak_threshold as a character
  Weak_threshold = as.numeric(Weak_threshold)

  # We test if the argument Weak_threshold is a numeric value
  if(is.na(Weak_threshold)){
    stop(" The value of Weak_threshold given is not a numeric value")
  }

  # We check if the value of Stringency_threshold is valid

  # We add the following line of code in case the user gave the argument
  # Stringency_threshold as a character
  Stringency_threshold = as.numeric(Stringency_threshold)

  # We test if the argument Stringency_threshold is a numeric value
  if(is.na(Stringency_threshold)){
    stop(" The value of Stringency_threshold given is not a numeric value")
  }


  # We check if the value of Gamma is valid

  if(!is.null(Gamma)){

    # We add the following line of code in case the user gave the argument
    # Gamma as a character
    Gamma = as.numeric(Gamma)

    # We test if the argument Gamma is a numeric value
    if(is.na(Gamma)){
      stop(" The value of Gamma given is not a numeric value")
    }
    unrequired_args<-append(unrequired_args,c("-g",Gamma))
  }

  # We check if the value of C is valid :

  if(!is.null(C)){
    # We first check if it's in percentage
    if(grepl(x = C,pattern = "%")){
      if(is.na(as.numeric(gsub(pattern = "%",replacement ="",x = C)))){
        stop("the value of C in invalid")
      }
    }

    # We check if it's a numeric value
    else {
      if(is.na(as.numeric(C))){
        stop("the value of C is not numeric ")
      }
      #We check if it's an integer
      else{
        C <-as.numeric(C)
        if(!(C == round(C))){
          stop("the value of C is not an integer")
        }
      }
    }
    unrequired_args<-append(unrequired_args,c("-c",C))
  }

  # We check if the value of Alpha is valid

  if(!is.null(Alpha)){

    # We add the following line of code in case the user gave the argument
    # Alpha as a character
    Alpha = as.numeric(Alpha)
    # We test if the argument alpha is a numeric value
    if(is.na(Alpha)){
      stop(" The value of Alpha given is not a numeric value")
    }
    unrequired_args<-append(unrequired_args,c("-a",Alpha))
  }

  # We test if the value of the argument Multiple_Intersections is valid

  if(!is.null(Multiple_Intersections)){
    Multiple_Intersections = match.arg(Multiple_Intersections, c("lowest", "highest"))
    unrequired_args<-append(unrequired_args,c("-m",Multiple_Intersections))
  }

  # We test if the value of the argument Degree_of_Parallelism is valid

  if(!is.null(Degree_of_Parallelism)){

    # We test if the value given is a numeric
    if(is.na(as.numeric(Degree_of_Parallelism))){
      stop("the number of parallel threads given is not a numeric value.")
    }

    # We add the following line of code in case the user gave the argument
    # Degree_of_Parallelism as a character

    Degree_of_Parallelism = as.numeric(Degree_of_Parallelism)

    # We test if the value given is an integer
    if(!(Degree_of_Parallelism ==round(Degree_of_Parallelism))){
      stop("the number of parallel threads given is not an integer")
    }

    # We test if the value is valid
    if (Degree_of_Parallelism > parallel::detectCores(logical = T)| Degree_of_Parallelism < 1){
      stop("the value of the number of parallel threads given is not valid.")
    }

    unrequired_args<-append(unrequired_args,c("-d",Degree_of_Parallelism))

  }

  # We check if the value of Input_Parser_Configuration is valid

  if(!is.null(Input_Parser_Configuration)){
    if(!file_test(op="-f",x=Input_Parser_Configuration)){
      if(dir.exists(Input_Parser_Configuration)){
        stop("the Input_Parser_Configuration argument given is a directory path.
             The value of this argument should be a file path.")
      }
      else{
        stop("the Input_Parser_Configuration is not a valid file path,
           the file does not exist in the directory specified.")
      }

    }

    #We test if the file given a JSON file
    if(!tools::file_ext(Input_Parser_Configuration)=="json"){
      stop("the Input Parser Configuration is not in the Json format")
    }

    unrequired_args<-append(unrequired_args,c("-p",Input_Parser_Configuration))
  }

  # We check if the value of Output_path is valid

  if(!is.null(Output_path)){

    # We test if the output path given exists
    if(!dir.exists(Output_path)){
      stop("the Output directory specified does not exist")
    }
    unrequired_args<-append(unrequired_args,c("-o",Output_path))

  }


  # Step 2 : We now call the executable


  # We define the arguments
  cmd_args <- append(cmd_args,c('-i',Input,"-r", Replicate_Type,"-s",Stringency_threshold,
                "-w", Weak_threshold))

  # We add the unrequired arguments given by the user
  cmd_args<-append(cmd_args,unrequired_args)

  results <- system2(command=cmd_command,
                     args= cmd_args,
                     stdout=TRUE,
                     stderr=TRUE,
                     wait = TRUE)

  return(results)
}
