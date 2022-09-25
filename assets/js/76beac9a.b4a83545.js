"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[377],{3905:function(e,t,n){n.d(t,{Zo:function(){return p},kt:function(){return u}});var r=n(7294);function a(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){a(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function s(e,t){if(null==e)return{};var n,r,a=function(e,t){if(null==e)return{};var n,r,a={},i=Object.keys(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||(a[n]=e[n]);return a}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(a[n]=e[n])}return a}var l=r.createContext({}),c=function(e){var t=r.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},p=function(e){var t=c(e.components);return r.createElement(l.Provider,{value:t},e.children)},m={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},d=r.forwardRef((function(e,t){var n=e.components,a=e.mdxType,i=e.originalType,l=e.parentName,p=s(e,["components","mdxType","originalType","parentName"]),d=c(n),u=a,f=d["".concat(l,".").concat(u)]||d[u]||m[u]||i;return n?r.createElement(f,o(o({ref:t},p),{},{components:n})):r.createElement(f,o({ref:t},p))}));function u(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var i=n.length,o=new Array(i);o[0]=d;var s={};for(var l in t)hasOwnProperty.call(t,l)&&(s[l]=t[l]);s.originalType=e,s.mdxType="string"==typeof e?e:a,o[1]=s;for(var c=2;c<i;c++)o[c]=n[c];return r.createElement.apply(null,o)}return r.createElement.apply(null,n)}d.displayName="MDXCreateElement"},6002:function(e,t,n){n.r(t),n.d(t,{assets:function(){return d},contentTitle:function(){return p},default:function(){return h},frontMatter:function(){return c},metadata:function(){return m},toc:function(){return u}});var r=n(7462),a=n(3366),i=(n(7294),n(3905)),o=n(4996),s=n(941),l=["components"],c={title:"About"},p=void 0,m={unversionedId:"method/about",id:"method/about",title:"About",description:"The analysis of ChIP-seq samples outputs a number of enriched regions",source:"@site/docs/method/about.md",sourceDirName:"method",slug:"/method/about",permalink:"/MSPC/docs/method/about",draft:!1,editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/method/about.md",tags:[],version:"current",frontMatter:{title:"About"},sidebar:"someSidebar",previous:{title:"Sample Data",permalink:"/MSPC/docs/sample_data"},next:{title:"Sets",permalink:"/MSPC/docs/method/sets"}},d={},u=[{value:"Remarks",id:"remarks",level:2}],f={toc:u};function h(e){var t=e.components,n=(0,a.Z)(e,l);return(0,i.kt)("wrapper",(0,r.Z)({},f,n,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,'The analysis of ChIP-seq samples outputs a number of enriched regions\n(commonly known as "peaks"), each indicating a protein-DNA interaction\nor a specific chromatin modification.'),(0,i.kt)("p",null,"When replicate samples are analyzed, overlapping peaks are expected.\nThis repeated evidence can therefore be used to locally lower the\nminimum significance required to accept a peak. MSPC is a method for\njoint analysis of peaks."),(0,i.kt)("p",null,'Given a set of peaks from (biological or technical) replicates,\nMSPC combines the p-values of overlapping enriched regions, and allows\nthe "rescue" of weak peaks occurring in more than one replicate.'),(0,i.kt)("h2",{id:"remarks"},"Remarks"),(0,i.kt)("p",null,'MSPC rigorously combines the statistical significance of the overlapping\nenriched regions (ER), in order to "rescue" weak peaks, which would probably\nbe discarded in a single-sample analysis, when their combined evidence\nacross multiple samples is sufficiently strong.\n[',(0,i.kt)("a",{parentName:"p",href:"https://doi.org/10.1093/bioinformatics/btv293"},"Ref"),"]"),(0,i.kt)("p",null,'MSPC takes as input, for each replicate, a list of ERs and a measure of\ntheir individual significance in terms of a p-value.\nStarting from a permissive call, it divides the initial ERs in "stringent"\n(highly significant) and "weak" (moderately significant), and it assesses\nthe presence of overlapping enriched regions across multiple replicates.\nNon-overlapping regions can be penalized or discarded according to specific\nneeds. '),(0,i.kt)("p",null,"The significance of the overlapping regions is rigorously combined with the\n",(0,i.kt)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/Fisher%27s_method"},"Fisher's method")," to obtain\na global score. This score is assessed against an adjustable threshold on\nthe combined evidence, and peaks in each replicate are either confirmed or\ndiscarded."),(0,i.kt)("p",null,"Finally, in order to account for multiple testing correction, MSPC applies the\n",(0,i.kt)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini%E2%80%93Hochberg_procedure"},"Benjamini-Hochberg procedure"),",\nand outputs ERs with false-discovery rate smaller than an adjustable threshold. "),(0,i.kt)("p",null,"This flow is captured in the following flowchart for each ER of each replicate: "),(0,i.kt)(s.Z,{alt:"Simplified Flow Chart",sources:{light:(0,o.Z)("/img/simplified_flow_chart.svg"),dark:(0,o.Z)("/img/simplified_flow_chart_dark.svg")},mdxType:"ThemedImage"}),(0,i.kt)("p",null,"(This flowchart is a simplified version of the flowchart available in\n",(0,i.kt)("a",{parentName:"p",href:"https://doi.org/10.1093/bioinformatics/btv293"},"MSPC's manuscript"),".)"),(0,i.kt)("p",null,"In other words:"),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},"for each replicate, MSPC classifies input ERs as ",(0,i.kt)("em",{parentName:"li"},"stringent"),", ",(0,i.kt)("em",{parentName:"li"},"weak"),",\nand ",(0,i.kt)("em",{parentName:"li"},"background"),", based on their p-value;"),(0,i.kt)("li",{parentName:"ul"},"performs a comparative analysis, and based on the combined stringency test,\nit classifies ",(0,i.kt)("em",{parentName:"li"},"stringent")," and ",(0,i.kt)("em",{parentName:"li"},"weak")," ERs as ",(0,i.kt)("em",{parentName:"li"},"confirmed")," or ",(0,i.kt)("em",{parentName:"li"},"discarded"),";"),(0,i.kt)("li",{parentName:"ul"},"based on false-discovery rate, it classifies ",(0,i.kt)("em",{parentName:"li"},"confirmed")," ERs as\n",(0,i.kt)("em",{parentName:"li"},"true-positive")," or ",(0,i.kt)("em",{parentName:"li"},"false-positive"),".")),(0,i.kt)("p",null,"The following figure is a schematic view of this procedure."),(0,i.kt)(s.Z,{alt:"Sets",sources:{light:(0,o.Z)("/img/sets.svg"),dark:(0,o.Z)("/img/sets_dark.svg")},mdxType:"ThemedImage"}),(0,i.kt)("p",null,"(The number of ERs in different sets reported in this figure, are the\nresult of ",(0,i.kt)("inlineCode",{parentName:"p"},"wgEncodeSydhTfbsK562CmycStdAlnRep1")," and\n",(0,i.kt)("inlineCode",{parentName:"p"},"wgEncodeSydhTfbsK562CmycStdAlnRep2")," (available for download from\n",(0,i.kt)("a",{parentName:"p",href:"../sample_data"},"Sample Data page"),") comparative analysis using\n",(0,i.kt)("inlineCode",{parentName:"p"},"-r bio -w 1e-4 -s 1e-8")," parameters.)"))}h.isMDXComponent=!0}}]);