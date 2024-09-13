"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[702],{5680:(e,t,a)=>{a.d(t,{xA:()=>m,yg:()=>s});var n=a(6540);function l(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function r(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function i(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?r(Object(a),!0).forEach((function(t){l(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):r(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function g(e,t){if(null==e)return{};var a,n,l=function(e,t){if(null==e)return{};var a,n,l={},r=Object.keys(e);for(n=0;n<r.length;n++)a=r[n],t.indexOf(a)>=0||(l[a]=e[a]);return l}(e,t);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);for(n=0;n<r.length;n++)a=r[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(l[a]=e[a])}return l}var p=n.createContext({}),d=function(e){var t=n.useContext(p),a=t;return e&&(a="function"==typeof e?e(t):i(i({},t),e)),a},m=function(e){var t=d(e.components);return n.createElement(p.Provider,{value:t},e.children)},y="mdxType",o={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},u=n.forwardRef((function(e,t){var a=e.components,l=e.mdxType,r=e.originalType,p=e.parentName,m=g(e,["components","mdxType","originalType","parentName"]),y=d(a),u=l,s=y["".concat(p,".").concat(u)]||y[u]||o[u]||r;return a?n.createElement(s,i(i({ref:t},m),{},{components:a})):n.createElement(s,i({ref:t},m))}));function s(e,t){var a=arguments,l=t&&t.mdxType;if("string"==typeof e||l){var r=a.length,i=new Array(r);i[0]=u;var g={};for(var p in t)hasOwnProperty.call(t,p)&&(g[p]=t[p]);g.originalType=e,g[y]="string"==typeof e?e:l,i[1]=g;for(var d=2;d<r;d++)i[d]=a[d];return n.createElement.apply(null,i)}return n.createElement.apply(null,a)}u.displayName="MDXCreateElement"},2978:(e,t,a)=>{a.r(t),a.d(t,{assets:()=>p,contentTitle:()=>i,default:()=>o,frontMatter:()=>r,metadata:()=>g,toc:()=>d});var n=a(8168),l=(a(6540),a(5680));const r={title:"Arguments"},i=void 0,g={unversionedId:"cli/args",id:"cli/args",title:"Arguments",description:"Call Example:",source:"@site/docs/cli/args.md",sourceDirName:"cli",slug:"/cli/args",permalink:"/MSPC/docs/cli/args",draft:!1,editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/cli/args.md",tags:[],version:"current",frontMatter:{title:"Arguments"},sidebar:"someSidebar",previous:{title:"Output",permalink:"/MSPC/docs/cli/output"},next:{title:"Parser Configuration",permalink:"/MSPC/docs/cli/parser"}},p={},d=[{value:"Call Example:",id:"call-example",level:2},{value:"Display help text",id:"display-help-text",level:2},{value:"Arguments Quick Reference",id:"arguments-quick-reference",level:2},{value:"Arguments",id:"arguments",level:2},{value:"Input",id:"input",level:3},{value:"Replicate Type",id:"replicate-type",level:3},{value:"Weak Threshold",id:"weak-threshold",level:3},{value:"Stringency Threshold",id:"stringency-threshold",level:3},{value:"Gamma",id:"gamma",level:3},{value:"C",id:"c",level:3},{value:"Alpha",id:"alpha",level:3},{value:"Multiple Intersections",id:"multiple-intersections",level:3},{value:"Degree of Parallelism",id:"degree-of-parallelism",level:3},{value:"Input Parser Configuration",id:"input-parser-configuration",level:3},{value:"Output Path",id:"output-path",level:3},{value:"Exclude Header",id:"exclude-header",level:3}],m={toc:d},y="wrapper";function o(e){let{components:t,...a}=e;return(0,l.yg)(y,(0,n.A)({},m,a,{components:t,mdxType:"MDXLayout"}),(0,l.yg)("h2",{id:"call-example"},"Call Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"// minimum\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -s 1E-8 -w 1E-4\n")),(0,l.yg)("h2",{id:"display-help-text"},"Display help text"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -?\n")),(0,l.yg)("h2",{id:"arguments-quick-reference"},"Arguments Quick Reference"),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Argument"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Short arg"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid Values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default Value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#input"},"Input")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-i")),(0,l.yg)("td",{parentName:"tr",align:null},"BED file"),(0,l.yg)("td",{parentName:"tr",align:null},"none")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#replicate-type"},"Replicate Type")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-r")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"bio"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"tec")),(0,l.yg)("td",{parentName:"tr",align:null},"none")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#stringency-threshold"},"Stringency threshold")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-s")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"double")),(0,l.yg)("td",{parentName:"tr",align:null},"none")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#weak-threshold"},"Weak threshold")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-w")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"double")),(0,l.yg)("td",{parentName:"tr",align:null},"none")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#gamma"},"Gamma")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-g")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"double")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#stringency-threshold"},"Stringency threshold"))),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#c"},"C")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-c")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"int")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"1"))),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#alpha"},"Alpha")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-a")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"double")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"0.05"))),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#multiple-intersections"},"Multiple Intersections")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-m")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"lowest"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"highest")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"lowest"))),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#degree-of-parallelism"},"Degree of Parallelism")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-d")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"int")),(0,l.yg)("td",{parentName:"tr",align:null},"Maximum allowed by the underlying scheduler")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#input-parser-configuration"},"Input Parser Configuration")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-p")),(0,l.yg)("td",{parentName:"tr",align:null},"File path"),(0,l.yg)("td",{parentName:"tr",align:null},"none")),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#output-path"},"Output path")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-o")),(0,l.yg)("td",{parentName:"tr",align:null},"Directory path"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"session_")," + ",(0,l.yg)("inlineCode",{parentName:"td"},"<Timestamp>"))),(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("a",{parentName:"td",href:"#exclude-header"},"Exclude Header")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"False")," (not provided)")))),(0,l.yg)("h2",{id:"arguments"},"Arguments"),(0,l.yg)("p",null,"In the following we explain arguments in details. "),(0,l.yg)("h3",{id:"input"},"Input"),(0,l.yg)("p",null,"Sample files are listed after the ",(0,l.yg)("inlineCode",{parentName:"p"},"-i")," or ",(0,l.yg)("inlineCode",{parentName:"p"},"--input")," argument."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-i")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--input")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},"BED file"),(0,l.yg)("td",{parentName:"tr",align:null},"none")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -i rep3.bed -r bio -w 1e-4 -s 1e-8\n")),(0,l.yg)("p",null,"You may also pass the input files space-delimited:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed rep2.bed rep3.bed -r bio -w 1e-4 -s 1e-8\n")),(0,l.yg)("p",null,(0,l.yg)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/Wildcard_character"},"Wildcard characters")," can be\nused to specify multiple files; for instance:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"# read all the files with .bed extension as input:\n$ dotnet mspc.dll -i *.bed -r bio -w 1e-4 -s 1e-8\n\n# read multiple set of files in different directories:\n$ dotnet mspc.dll -i C:\\setA\\*.bed -i C:\\setB\\sci-ATAC*.bed -r bio -w 1e-4 -s 1e-8\n")),(0,l.yg)("admonition",{type:"info"},(0,l.yg)("p",{parentName:"admonition"},"By default, MSPC reads input files unstranded, if your\ndata are stranded, you may ",(0,l.yg)("a",{parentName:"p",href:"parser"},"adjust the parser")," accordingly.")),(0,l.yg)("h3",{id:"replicate-type"},"Replicate Type"),(0,l.yg)("p",null,"Samples could be biological or technical replicates. MSPC differentiates between\nthe two replicate types based on the fact that less variations between technical\nreplicates is expected compared to biological replicates. Replicate type can be\nspecified using the following argument:"),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-r")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--replicate")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"Bio"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"Biological"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"Tec"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"Technical")),(0,l.yg)("td",{parentName:"tr",align:null},"none")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r tec -w 1e-4 -s 1e-8\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r biological -w 1e-4 -s 1e-8\n")),(0,l.yg)("h3",{id:"weak-threshold"},"Weak Threshold"),(0,l.yg)("p",null,"It sets a threshold on p-values, such that peaks with p-value between this\nand stringency threshold, are considered ",(0,l.yg)("a",{parentName:"p",href:"/MSPC/docs/method/sets#weak"},"weak peaks"),"."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-w")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--tauW")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},"Double"),(0,l.yg)("td",{parentName:"tr",align:null},"none")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),(0,l.yg)("h3",{id:"stringency-threshold"},"Stringency Threshold"),(0,l.yg)("p",null,"It sets a threshold on p-values, where peaks with p-value lower than\nthis threshold, are considered ",(0,l.yg)("a",{parentName:"p",href:"/MSPC/docs/method/sets#stringent"},"stringent"),"."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-s")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--tauS")),(0,l.yg)("td",{parentName:"tr",align:null},"\u2713"),(0,l.yg)("td",{parentName:"tr",align:null},"Double"),(0,l.yg)("td",{parentName:"tr",align:null},"none")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8\n")),(0,l.yg)("h3",{id:"gamma"},"Gamma"),(0,l.yg)("p",null,"It sets the combined stringency threshold. Peaks with\ncombined p-value below this threshold are ",(0,l.yg)("a",{parentName:"p",href:"/MSPC/docs/method/sets#confirmed"},"confirmed"),"."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-g")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--gamma")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},"Double"),(0,l.yg)("td",{parentName:"tr",align:null},"Equal to Stringency Threshold")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8\n")),(0,l.yg)("h3",{id:"c"},"C"),(0,l.yg)("p",null,"It sets the minimum number of overlapping peaks required before MSPC\ncombines their p-value. For example, given three replicates (rep1, rep2\nand rep3), if ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 3"),", a peak on rep1 must overlap with at least two\npeaks, one from rep2 and one from rep3, before MSPC combines their\np-value; otherwise, MSPC discards the peak. If ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 2"),", a peak on rep1\nmust overlap with at least one peak from either rep2 or rep3, before\nMSPC combines their p-values; otherwise MSPC discards the peak."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-c")),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},"String"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"1"))))),(0,l.yg)("p",null,"The value of ",(0,l.yg)("inlineCode",{parentName:"p"},"C")," can be given in ",(0,l.yg)("em",{parentName:"p"},"absolute")," (e.g., ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 2")," will\nrequire at least ",(0,l.yg)("inlineCode",{parentName:"p"},"2")," samples) or ",(0,l.yg)("em",{parentName:"p"},"percentage")," of input samples\n(e.g., ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 50%")," will require at least ",(0,l.yg)("inlineCode",{parentName:"p"},"50%")," of input samples) formats."),(0,l.yg)("p",null,"The minimum value of ",(0,l.yg)("inlineCode",{parentName:"p"},"C")," is ",(0,l.yg)("inlineCode",{parentName:"p"},"1"),". If a value less than ",(0,l.yg)("inlineCode",{parentName:"p"},"1")," is given\n(e.g., ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 0"),", ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 0%"),", or ",(0,l.yg)("inlineCode",{parentName:"p"},"C = -1"),"), MSPC automatically sets it\nto ",(0,l.yg)("inlineCode",{parentName:"p"},"1")," (i.e., ",(0,l.yg)("inlineCode",{parentName:"p"},"C = 1"),")."),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 2\n\ndotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 50%\n")),(0,l.yg)("p",null,"Note, you do not need to enclose a value for ",(0,l.yg)("inlineCode",{parentName:"p"},"C")," in ",(0,l.yg)("inlineCode",{parentName:"p"},'"')," to represent\nit as a string; the values are automatically considered as string type\nobjects. In other words, you do not need to enter the value as ",(0,l.yg)("inlineCode",{parentName:"p"},'C "3"'),"."),(0,l.yg)("h3",{id:"alpha"},"Alpha"),(0,l.yg)("p",null,"It sets the threshold for ",(0,l.yg)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini%E2%80%93Hochberg_procedure"},"Benjamini-Hochberg multiple testing correction"),"."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-a")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--alpha")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},"Double"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"0.05"))))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -a 0.05\n")),(0,l.yg)("h3",{id:"multiple-intersections"},"Multiple Intersections"),(0,l.yg)("p",null,"When multiple peaks from a sample overlap with a given peak,\nthis argument defines which of the peaks to be considered:\nthe one with lowest p-value, or the one with highest p-value? "),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-m")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--multipleIntersections")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"Lowest"),", ",(0,l.yg)("inlineCode",{parentName:"td"},"Highest")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"Lowest"))))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -m lowest\n")),(0,l.yg)("h3",{id:"degree-of-parallelism"},"Degree of Parallelism"),(0,l.yg)("p",null,"It sets the number of parallel threads MSPC can utilize simultaneously when processing data."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-d")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--degreeOfParallelism")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"int")),(0,l.yg)("td",{parentName:"tr",align:null},"Number of logical processors on the current machine")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -d 12\n")),(0,l.yg)("h3",{id:"input-parser-configuration"},"Input Parser Configuration"),(0,l.yg)("p",null,"Sets the path to a JSON file containing the configuration\nfor the input BED file parser."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-p")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--parser")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},"File path"),(0,l.yg)("td",{parentName:"tr",align:null},"none")))),(0,l.yg)("p",null,"Refer to ",(0,l.yg)("a",{parentName:"p",href:"/MSPC/docs/cli/parser"},"this page")," on how to configure the input parser\nusing a JSON object."),(0,l.yg)("h3",{id:"output-path"},"Output Path"),(0,l.yg)("p",null,"Sets the path in which analysis results should be persisted.\nIf it is not given, the default folder is name ",(0,l.yg)("inlineCode",{parentName:"p"},"session_")," + ",(0,l.yg)("inlineCode",{parentName:"p"},"<Timestamp>"),".\nIf a given folder name already exists, and is not empty, MSPC\nwill append ",(0,l.yg)("inlineCode",{parentName:"p"},"_n")," where ",(0,l.yg)("inlineCode",{parentName:"p"},"n")," is an integer until no duplicate is\nfound. See the ",(0,l.yg)("a",{parentName:"p",href:"output"},"Output")," page on the contents of\nthis folder."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"-o")),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--output")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null},"Directory path"),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"session_")," + ",(0,l.yg)("inlineCode",{parentName:"td"},"<Timestamp>"))))),(0,l.yg)("h3",{id:"exclude-header"},"Exclude Header"),(0,l.yg)("p",null,"This is a flag (i.e., it does not require any value),\nif provided, MSPC does not add a header to the files\nit generates. If not provided (default), MSPC will add\na header to all the files it generates."),(0,l.yg)("table",null,(0,l.yg)("thead",{parentName:"table"},(0,l.yg)("tr",{parentName:"thead"},(0,l.yg)("th",{parentName:"tr",align:null},"Short"),(0,l.yg)("th",{parentName:"tr",align:null},"Long"),(0,l.yg)("th",{parentName:"tr",align:null},"Required"),(0,l.yg)("th",{parentName:"tr",align:null},"Valid values"),(0,l.yg)("th",{parentName:"tr",align:null},"Default value"))),(0,l.yg)("tbody",{parentName:"table"},(0,l.yg)("tr",{parentName:"tbody"},(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"--excludeHeader")),(0,l.yg)("td",{parentName:"tr",align:null},"Optional"),(0,l.yg)("td",{parentName:"tr",align:null}),(0,l.yg)("td",{parentName:"tr",align:null},(0,l.yg)("inlineCode",{parentName:"td"},"False")," (not provided)")))),(0,l.yg)("p",null,"Example:"),(0,l.yg)("pre",null,(0,l.yg)("code",{parentName:"pre",className:"language-shell"},"dotnet mspc.dll --excludeHeader\ndotnet mspc.dll --excludeHeader true\ndotnet mspc.dll --excludeHeader false\n")))}o.isMDXComponent=!0}}]);