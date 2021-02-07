# We define the function mspc :

mspc<-function(Input,
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


  # We create a vector where we will store the unrequired arguments that are
  # given by the user

  unrequired_args<-c()

  # Step 1 : We first test if the values of the arguments are valid


  # We check if the Input argument refers to a file :
  for(i in 1:length(Input)){
    if(!file_test(op="-f",x=Input[i])){
      if(dir.exists(Input[i])){
        stop("The element ",i," of the Input argument given is a directory path.
             The value of this argument should be a bed file path.")
      }
      else{
        stop("The element ",i," of the Input argument is not a valid bed file path,
           the file does not exist in the directory specified.")
      }
    }
  }

  # We check if the Input argument is a bed file
  if(any(!tools::file_ext(Input)=="bed")){
    stop("one or several of the input files is not in the bed format")
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

  # We define the command line
  cmd_command <- "mspc.exe"


  # We define the arguments
  cmd_args <- c('-i',Input,"-r", Replicate_Type,"-s",Stringency_threshold,
                "-w", Weak_threshold)

  # We add the unrequired arguments given by the user
  cmd_args<-append(cmd_args,unrequired_args)

  results <- system2(command=cmd_command,
                args= cmd_args,
                stdout=TRUE,
                stderr=TRUE,
                wait = TRUE)

 return(results)
  }
