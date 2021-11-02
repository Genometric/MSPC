"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[305],{3905:function(e,t,n){n.d(t,{Zo:function(){return p},kt:function(){return h}});var r=n(7294);function a(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){a(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function s(e,t){if(null==e)return{};var n,r,a=function(e,t){if(null==e)return{};var n,r,a={},i=Object.keys(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||(a[n]=e[n]);return a}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(a[n]=e[n])}return a}var c=r.createContext({}),l=function(e){var t=r.useContext(c),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},p=function(e){var t=l(e.components);return r.createElement(c.Provider,{value:t},e.children)},m={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},u=r.forwardRef((function(e,t){var n=e.components,a=e.mdxType,i=e.originalType,c=e.parentName,p=s(e,["components","mdxType","originalType","parentName"]),u=l(n),h=a,f=u["".concat(c,".").concat(h)]||u[h]||m[h]||i;return n?r.createElement(f,o(o({ref:t},p),{},{components:n})):r.createElement(f,o({ref:t},p))}));function h(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var i=n.length,o=new Array(i);o[0]=u;var s={};for(var c in t)hasOwnProperty.call(t,c)&&(s[c]=t[c]);s.originalType=e,s.mdxType="string"==typeof e?e:a,o[1]=s;for(var l=2;l<i;l++)o[l]=n[l];return r.createElement.apply(null,o)}return r.createElement.apply(null,n)}u.displayName="MDXCreateElement"},6307:function(e,t,n){n.r(t),n.d(t,{frontMatter:function(){return s},contentTitle:function(){return c},metadata:function(){return l},toc:function(){return p},default:function(){return u}});var r=n(7462),a=n(3366),i=(n(7294),n(3905)),o=["components"],s={title:"Welcome",slug:"/"},c=void 0,l={unversionedId:"welcome",id:"welcome",isDocsHomePage:!1,title:"Welcome",description:"The analysis of ChIP-seq samples outputs a number",source:"@site/docs/welcome.md",sourceDirName:".",slug:"/",permalink:"/MSPC/docs/",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/welcome.md",version:"current",frontMatter:{title:"Welcome",slug:"/"},sidebar:"someSidebar",next:{title:"Quick Start",permalink:"/MSPC/docs/quick_start"}},p=[],m={toc:p};function u(e){var t=e.components,n=(0,a.Z)(e,o);return(0,i.kt)("wrapper",(0,r.Z)({},m,n,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,'The analysis of ChIP-seq samples outputs a number\nof enriched regions (commonly known as "peaks"),\neach indicating a protein-DNA interaction or a\nspecific chromatin modification. When replicate\nsamples are analyzed, overlapping peaks are expected.\nThis repeated evidence can therefore be used to\nlocally lower the minimum significance required to\naccept a peak. Here, we propose a method for joint\nanalysis of weak peaks. Given a set of peaks from\n(biological or technical) replicates, the method\ncombines the p-values of overlapping enriched regions,\nand allows the "rescue" of weak peaks occurring in\nmore than one replicate.'),(0,i.kt)("p",null,"MSPC comparatively evaluates ChIP-seq peaks and\ncombines the statistical significance of\nrepeated evidences, with the aim of lowering the\nminimum significance required to \u201crescue\u201d\nweak peaks; hence reducing false-negatives. "),(0,i.kt)("p",null,"MSPC can be used from: "),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/cli/about"},(0,i.kt)("strong",{parentName:"a"},"Command Line Interface (CLI)")),": call MSPC CLI from your favorite terminal with necessary\narguments, and it writes the analysis results to BED files.")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/library/install"},(0,i.kt)("strong",{parentName:"a"},"As a library in your project")),": call it from your program, and it returns analysis\nresults to your program.")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},(0,i.kt)("a",{parentName:"p",href:"https://bioconductor.org/packages/release/bioc/html/rmspc.html"},(0,i.kt)("strong",{parentName:"a"},"As an R package from Bioconductor")),":\nThe Bioconductor package is a wrapper around the CLI application, hence\noffering the same functionality as the CLI application. The package can be\ninvoked from R to process files or\n",(0,i.kt)("a",{parentName:"p",href:"https://bioconductor.org/packages/release/bioc/html/GenomicRanges.html"},"GenomicRanges"),"\n(Granges) objects, and the results are stored as Granges objects.\nPlease refer to ",(0,i.kt)("a",{parentName:"p",href:"https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html"},"Bioconductor user guide"),"."))),(0,i.kt)("h1",{id:"about"},"About"),(0,i.kt)("p",null,"With most peak callers (e.g, MACS), the false-positive\nrate is a function of a user-defined p-value threshold,\nwhere the more conservative thresholds result in lower\nfalse-positive rates---the penalty of which is the\nincrease in the number of false-negatives. While several\nprobabilistic methods are developed to jointly model\nbinding affinities across replicated samples to identify\ncombinatorial enrichment patterns (e.g.,\n",(0,i.kt)("a",{parentName:"p",href:"https://genomebiology.biomedcentral.com/articles/10.1186/gb-2013-14-4-r38"},"jMOSAiCS"),",\nor ",(0,i.kt)("a",{parentName:"p",href:"https://academic.oup.com/biostatistics/article/15/2/296/226404"},"this"),",\nor ",(0,i.kt)("a",{parentName:"p",href:"https://link.springer.com/chapter/10.1007/978-3-319-05269-4_14"},"this"),",\nor ",(0,i.kt)("a",{parentName:"p",href:"https://www.frontiersin.org/articles/10.3389/fgene.2018.00731/full"},"this"),"),\nthe more commonly used peak callers (e.g., MACS) operate\non single samples. Additionally, the choice of peak\ncaller is generally an established step in a ChIP-seq\nanalysis pipeline, since altering it may require changes\nto the data pre- and post-processing of the pipeline.\nTherefore, the remainder of this manuscript is focused\non the post peak calling methods to lower false-positive\nrates as they can be applied on the output of any peak\ncaller with minimal changes to the established analysis\npipeline, and can combine evidence across replicated samples."),(0,i.kt)("h1",{id:"motivation"},"Motivation"),(0,i.kt)("p",null,"Ultimately, with most peak callers (e.g, MACS), the false-positive\nrate is a function of a user-defined p-value threshold, where the more\nconservative thresholds result in lower false-positive rates---the penalty\nof which is the increase in the number of false-negatives. While several\nprobabilistic methods are developed to jointly model binding affinities\nacross replicated samples to identify combinatorial enrichment patterns\n(e.g.,\n",(0,i.kt)("a",{parentName:"p",href:"https://genomebiology.biomedcentral.com/articles/10.1186/gb-2013-14-4-r38"},"REF1"),",\n",(0,i.kt)("a",{parentName:"p",href:"https://academic.oup.com/biostatistics/article/15/2/296/226404"},"REF2"),",\n",(0,i.kt)("a",{parentName:"p",href:"https://link.springer.com/chapter/10.1007/978-3-319-05269-4_14"},"REF3"),",\n",(0,i.kt)("a",{parentName:"p",href:"https://academic.oup.com/bioinformatics/article/31/1/17/2366199"},"REF4"),",\n",(0,i.kt)("a",{parentName:"p",href:"https://www.frontiersin.org/articles/10.3389/fgene.2018.00731/full"},"REF5"),"),\nthe more commonly used peak callers\n(e.g., MACS) operate on single samples. Additionally, the choice of peak\ncaller is generally an established step in a ChIP-seq analysis pipeline,\nsince altering it may require changes to the data pre- and post-processing\nof the pipeline."))}u.isMDXComponent=!0}}]);