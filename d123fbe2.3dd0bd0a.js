(window.webpackJsonp=window.webpackJsonp||[]).push([[16],{88:function(e,t,a){"use strict";a.r(t),a.d(t,"frontMatter",(function(){return r})),a.d(t,"metadata",(function(){return c})),a.d(t,"rightToc",(function(){return i})),a.d(t,"default",(function(){return O}));var n=a(3),b=a(7),l=(a(0),a(96)),r={title:"Arguments"},c={unversionedId:"cli/args",id:"cli/args",isDocsHomePage:!1,title:"Arguments",description:"Call Example:",source:"@site/docs/cli/args.md",slug:"/cli/args",permalink:"/MSPC/docs/cli/args",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/cli/args.md",version:"current",sidebar:"someSidebar",previous:{title:"Output",permalink:"/MSPC/docs/cli/output"},next:{title:"Parser Configuration",permalink:"/MSPC/docs/cli/parser"}},i=[{value:"Call Example:",id:"call-example",children:[]},{value:"Arguments Quick Reference",id:"arguments-quick-reference",children:[]},{value:"Arguments",id:"arguments",children:[{value:"Input",id:"input",children:[]},{value:"Input Folder",id:"input-folder",children:[]},{value:"Replicate Type",id:"replicate-type",children:[]},{value:"Weak Threshold",id:"weak-threshold",children:[]},{value:"Stringency Threshold",id:"stringency-threshold",children:[]},{value:"Gamma",id:"gamma",children:[]},{value:"C",id:"c",children:[]},{value:"Alpha",id:"alpha",children:[]},{value:"Multiple Intersections",id:"multiple-intersections",children:[]},{value:"Degree of Parallelism",id:"degree-of-parallelism",children:[]},{value:"Input Parser Configuration",id:"input-parser-configuration",children:[]},{value:"Output Path",id:"output-path",children:[]}]}],p={rightToc:i};function O(e){var t=e.components,a=Object(b.a)(e,["components"]);return Object(l.b)("wrapper",Object(n.a)({},p,a,{components:t,mdxType:"MDXLayout"}),Object(l.b)("h2",{id:"call-example"},"Call Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"// minimum\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -s 1E-8 -w 1E-4\n")),Object(l.b)("h2",{id:"arguments-quick-reference"},"Arguments Quick Reference"),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Argument"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short arg"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid Values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default Value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#input"}),"Input")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713*"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-i")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"BED file"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#input-folder"}),"Input Folder")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713*"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-f")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Folder path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#replicate-type"}),"Replicate Type")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-r")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"bio"),", ",Object(l.b)("inlineCode",{parentName:"td"},"tec")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#stringency-threshold"}),"Stringency threshold")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-s")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"double")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#weak-threshold"}),"Weak threshold")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-w")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"double")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#gamma"}),"Gamma")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-g")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"double")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"tauS")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#c"}),"C")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-c")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"int")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"1"))),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#alpha"}),"Alpha")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-a")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"double")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"0.05"))),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#multiple-intersections"}),"Multiple Intersections")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-m")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"Lowest"),", ",Object(l.b)("inlineCode",{parentName:"td"},"Highest")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"Lowest"))),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#degree-of-parallelism"}),"Degree of Parallelism")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-d")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"int")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"host processors count")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#input-parser-configuration"}),"Input Parser Configuration")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-p")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"File path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")),Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("a",Object(n.a)({parentName:"td"},{href:"#output-path"}),"Output path")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-o")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Directory path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"session_")," + ",Object(l.b)("inlineCode",{parentName:"td"},"<Timestamp>"))))),Object(l.b)("ul",null,Object(l.b)("li",{parentName:"ul"},"At least one of these arguments should be provided.")),Object(l.b)("h2",{id:"arguments"},"Arguments"),Object(l.b)("p",null,"In the following we explain arguments in details. "),Object(l.b)("h3",{id:"input"},"Input"),Object(l.b)("p",null,"Sample files are listed after the ",Object(l.b)("inlineCode",{parentName:"p"},"-i")," or ",Object(l.b)("inlineCode",{parentName:"p"},"--input")," argument."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-i")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--input")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"BED file"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -i rep3.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("p",null,Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"https://en.wikipedia.org/wiki/Wildcard_character"}),"Wildcard characters")," can be\nused to specify multiple files; for instance:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"# read all the files with .bed extension as input:\n$ dotnet mspc.dll -i *.bed -r bio -w 1e-4 -s 1e-8\n\n# read multiple set of files in different directories:\n$ dotnet mspc.dll -i C:\\setA\\*.bed -i C:\\setB\\sci-ATAC*.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("p",null,"The ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"#input"}),Object(l.b)("inlineCode",{parentName:"a"},"--input"))," argument can be used toghether with ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"#input-folder"}),Object(l.b)("inlineCode",{parentName:"a"},"--folder"))," argument."),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -f C:\\data\\*.bed -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("p",null,"See ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"#input-folder"}),Object(l.b)("inlineCode",{parentName:"a"},"--folder"))," argument section for details."),Object(l.b)("h3",{id:"input-folder"},"Input Folder"),Object(l.b)("p",null,"Sample files can be read from a folder specified using wildcard characters."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-f")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--folder")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Folder path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -f C:\\data\\*.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("p",null,"The ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"#input-folder"}),Object(l.b)("inlineCode",{parentName:"a"},"--folder"))," argument can be used together with the ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"#input"}),Object(l.b)("inlineCode",{parentName:"a"},"--input"))," argument. "),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -f C:\\data\\*.bed -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("h3",{id:"replicate-type"},"Replicate Type"),Object(l.b)("p",null,"Samples could be biological or technical replicates. MSPC differentiates between\nthe two replicate types based on the fact that less variations between technical\nreplicates is expected compared to biological replicates. Replicate type can be\nspecified using the following argument:"),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-r")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--replicate")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"Bio"),", ",Object(l.b)("inlineCode",{parentName:"td"},"Biological"),", ",Object(l.b)("inlineCode",{parentName:"td"},"Tec"),", ",Object(l.b)("inlineCode",{parentName:"td"},"Technical")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r tec -w 1e-4 -s 1e-8\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r biological -w 1e-4 -s 1e-8\n")),Object(l.b)("h3",{id:"weak-threshold"},"Weak Threshold"),Object(l.b)("p",null,"It sets a threshold on p-values, such that peaks with p-value between this\nand stringency threshold, are considered ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"/MSPC/docs/method/sets#weak"}),"weak peaks"),"."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-w")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--tauW")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Double"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("h3",{id:"stringency-threshold"},"Stringency Threshold"),Object(l.b)("p",null,"It sets a threshold on p-values, where peaks with p-value lower than\nthis threshold, are considered ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"/MSPC/docs/method/sets#stringent"}),"stringent"),"."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-s")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--tauS")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\u2713"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Double"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),Object(l.b)("h3",{id:"gamma"},"Gamma"),Object(l.b)("p",null,"It sets the combined stringency threshold. Peaks with\ncombined p-value below this threshold are ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"/MSPC/docs/method/sets#confirmed"}),"confirmed"),"."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-g")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--gamma")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Double"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Equal to Stringency Threshold")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8\n")),Object(l.b)("h3",{id:"c"},"C"),Object(l.b)("p",null,"It sets the minimum number of overlapping peaks required before MSPC\ncombines their p-value. For example, given three replicates (rep1, rep2\nand rep3), if ",Object(l.b)("inlineCode",{parentName:"p"},"C = 3"),", a peak on rep1 must overlap with at least two\npeaks, one from rep2 and one from rep3, before MSPC combines their\np-value; otherwise, MSPC discards the peak. If ",Object(l.b)("inlineCode",{parentName:"p"},"C = 2"),", a peak on rep1\nmust overlap with at least one peak from either rep2 or rep3, before\nMSPC combines their p-values; otherwise MSPC discards the peak."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-c")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null})),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"String"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"1"))))),Object(l.b)("p",null,"The value of ",Object(l.b)("inlineCode",{parentName:"p"},"C")," can be given in ",Object(l.b)("em",{parentName:"p"},"absolute")," (e.g., ",Object(l.b)("inlineCode",{parentName:"p"},"C = 2")," will\nrequire at least ",Object(l.b)("inlineCode",{parentName:"p"},"2")," samples) or ",Object(l.b)("em",{parentName:"p"},"percentage")," of input samples\n(e.g., ",Object(l.b)("inlineCode",{parentName:"p"},"C = 50%")," will require at least ",Object(l.b)("inlineCode",{parentName:"p"},"50%")," of input samples) formats."),Object(l.b)("p",null,"The minimum value of ",Object(l.b)("inlineCode",{parentName:"p"},"C")," is ",Object(l.b)("inlineCode",{parentName:"p"},"1"),". If a value less than ",Object(l.b)("inlineCode",{parentName:"p"},"1")," is given\n(e.g., ",Object(l.b)("inlineCode",{parentName:"p"},"C = 0"),", ",Object(l.b)("inlineCode",{parentName:"p"},"C = 0%"),", or ",Object(l.b)("inlineCode",{parentName:"p"},"C = -1"),"), MSPC automatically sets it\nto ",Object(l.b)("inlineCode",{parentName:"p"},"1")," (i.e., ",Object(l.b)("inlineCode",{parentName:"p"},"C = 1"),")."),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 2\n\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 50%\n")),Object(l.b)("p",null,"Note, you do not need to enclose a value for ",Object(l.b)("inlineCode",{parentName:"p"},"C")," in ",Object(l.b)("inlineCode",{parentName:"p"},'"')," to represent\nit as a string; the values are automatically considered as string type\nobjects. In other words, you do not need to enter the value as ",Object(l.b)("inlineCode",{parentName:"p"},'C "3"'),"."),Object(l.b)("h3",{id:"alpha"},"Alpha"),Object(l.b)("p",null,"It sets the threshold for ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini%E2%80%93Hochberg_procedure"}),"Benjamini-Hochberg multiple testing correction"),"."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-a")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--alpha")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Double"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"0.05"))))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -a 0.05\n")),Object(l.b)("h3",{id:"multiple-intersections"},"Multiple Intersections"),Object(l.b)("p",null,"When multiple peaks from a sample overlap with a given peak,\nthis argument defines which of the peaks to be considered:\nthe one with lowest p-value, or the one with highest p-value? "),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-m")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--multipleIntersections")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"Lowest"),", ",Object(l.b)("inlineCode",{parentName:"td"},"Highest")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"Lowest"))))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -m lowest\n")),Object(l.b)("h3",{id:"degree-of-parallelism"},"Degree of Parallelism"),Object(l.b)("p",null,"It sets the number of parallel threads MSPC can utilize simultaneously when processing data."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-d")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--degreeOfParallelism")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"int")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Number of logical processors on the current machine")))),Object(l.b)("p",null,"Example:"),Object(l.b)("pre",null,Object(l.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -d 12\n")),Object(l.b)("h3",{id:"input-parser-configuration"},"Input Parser Configuration"),Object(l.b)("p",null,"Sets the path to a JSON file containing the configuration\nfor the input BED file parser."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-p")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--parser")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"File path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"none")))),Object(l.b)("p",null,"Refer to ",Object(l.b)("a",Object(n.a)({parentName:"p"},{href:"/MSPC/docs/cli/parser"}),"this page")," on how to configure the input parser\nusing a JSON object."),Object(l.b)("h3",{id:"output-path"},"Output Path"),Object(l.b)("p",null,"Sets the path in which analysis results should be persisted."),Object(l.b)("table",null,Object(l.b)("thead",{parentName:"table"},Object(l.b)("tr",{parentName:"thead"},Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Short"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Long"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Required"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Valid values"),Object(l.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"Default value"))),Object(l.b)("tbody",{parentName:"table"},Object(l.b)("tr",{parentName:"tbody"},Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"-o")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"--output")),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Optional"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Directory path"),Object(l.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(l.b)("inlineCode",{parentName:"td"},"session_")," + ",Object(l.b)("inlineCode",{parentName:"td"},"<Timestamp>"))))))}O.isMDXComponent=!0},96:function(e,t,a){"use strict";a.d(t,"a",(function(){return j})),a.d(t,"b",(function(){return o}));var n=a(0),b=a.n(n);function l(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function r(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function c(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?r(Object(a),!0).forEach((function(t){l(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):r(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function i(e,t){if(null==e)return{};var a,n,b=function(e,t){if(null==e)return{};var a,n,b={},l=Object.keys(e);for(n=0;n<l.length;n++)a=l[n],t.indexOf(a)>=0||(b[a]=e[a]);return b}(e,t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(e);for(n=0;n<l.length;n++)a=l[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(b[a]=e[a])}return b}var p=b.a.createContext({}),O=function(e){var t=b.a.useContext(p),a=t;return e&&(a="function"==typeof e?e(t):c(c({},t),e)),a},j=function(e){var t=O(e.components);return b.a.createElement(p.Provider,{value:t},e.children)},d={inlineCode:"code",wrapper:function(e){var t=e.children;return b.a.createElement(b.a.Fragment,{},t)}},m=b.a.forwardRef((function(e,t){var a=e.components,n=e.mdxType,l=e.originalType,r=e.parentName,p=i(e,["components","mdxType","originalType","parentName"]),j=O(a),m=n,o=j["".concat(r,".").concat(m)]||j[m]||d[m]||l;return a?b.a.createElement(o,c(c({ref:t},p),{},{components:a})):b.a.createElement(o,c({ref:t},p))}));function o(e,t){var a=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var l=a.length,r=new Array(l);r[0]=m;var c={};for(var i in t)hasOwnProperty.call(t,i)&&(c[i]=t[i]);c.originalType=e,c.mdxType="string"==typeof e?e:n,r[1]=c;for(var p=2;p<l;p++)r[p]=a[p];return b.a.createElement.apply(null,r)}return b.a.createElement.apply(null,a)}m.displayName="MDXCreateElement"}}]);