"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[175],{3905:function(e,t,n){n.d(t,{Zo:function(){return c},kt:function(){return u}});var a=n(7294);function r(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,a)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){r(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function s(e,t){if(null==e)return{};var n,a,r=function(e,t){if(null==e)return{};var n,a,r={},i=Object.keys(e);for(a=0;a<i.length;a++)n=i[a],t.indexOf(n)>=0||(r[n]=e[n]);return r}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(a=0;a<i.length;a++)n=i[a],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(r[n]=e[n])}return r}var p=a.createContext({}),l=function(e){var t=a.useContext(p),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},c=function(e){var t=l(e.components);return a.createElement(p.Provider,{value:t},e.children)},m={inlineCode:"code",wrapper:function(e){var t=e.children;return a.createElement(a.Fragment,{},t)}},d=a.forwardRef((function(e,t){var n=e.components,r=e.mdxType,i=e.originalType,p=e.parentName,c=s(e,["components","mdxType","originalType","parentName"]),d=l(n),u=r,k=d["".concat(p,".").concat(u)]||d[u]||m[u]||i;return n?a.createElement(k,o(o({ref:t},c),{},{components:n})):a.createElement(k,o({ref:t},c))}));function u(e,t){var n=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var i=n.length,o=new Array(i);o[0]=d;var s={};for(var p in t)hasOwnProperty.call(t,p)&&(s[p]=t[p]);s.originalType=e,s.mdxType="string"==typeof e?e:r,o[1]=s;for(var l=2;l<i;l++)o[l]=n[l];return a.createElement.apply(null,o)}return a.createElement.apply(null,n)}d.displayName="MDXCreateElement"},7945:function(e,t,n){n.r(t),n.d(t,{frontMatter:function(){return s},contentTitle:function(){return p},metadata:function(){return l},toc:function(){return c},default:function(){return d}});var a=n(7462),r=n(3366),i=(n(7294),n(3905)),o=["components"],s={title:"About"},p=void 0,l={unversionedId:"cli/about",id:"cli/about",isDocsHomePage:!1,title:"About",description:"MSPC mspc.dll is a command line interface to the MSPC method;",source:"@site/docs/cli/about.md",sourceDirName:"cli",slug:"/cli/about",permalink:"/MSPC/docs/cli/about",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/cli/about.md",version:"current",frontMatter:{title:"About"},sidebar:"someSidebar",previous:{title:"Consensus peaks",permalink:"/MSPC/docs/method/consensus"},next:{title:"Input",permalink:"/MSPC/docs/cli/input"}},c=[{value:"See Also",id:"see-also",children:[]}],m={toc:c};function d(e){var t=e.components,n=(0,r.Z)(e,o);return(0,i.kt)("wrapper",(0,a.Z)({},m,n,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,"MSPC ",(0,i.kt)("inlineCode",{parentName:"p"},"mspc.dll")," is a command line interface to the MSPC method;\nit parses input datasets (e.g., BED files), invokes the method\nto analyze them, and persists the results. "),(0,i.kt)("p",null,"The ",(0,i.kt)("inlineCode",{parentName:"p"},"mspc.dll")," is a cross-platform command-line application, and\ncan be invoked from any shell, such as Windows PowerShell, Linux\nshell, or Mac OS Terminal. A basic ",(0,i.kt)("inlineCode",{parentName:"p"},"mspc.dll")," invocation takes four\narguments (i.e., ",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/cli/args#input"},"input"),",\n",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/cli/args#replicate-type"},"replicate type"),", and\n",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/cli/args#stringency-threshold"},"stringency")," and\n",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/cli/args#weak-threshold"},"weak")," thresholds), runs\nMSPC ",(0,i.kt)("inlineCode",{parentName:"p"},"Core"),", and persists results.\nFor instance, see the following sample execution: "),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-shell"},"$ dotnet .\\mspc.dll -i rep1.bed -i rep2.bed \\\n                    -r bio -w 1e-4 -s 1e-8\n\n")),(0,i.kt)("p",null,"Output:"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-shell"},".::........Parsing Samples.........::.\n     #                Filename    Read peaks#     Min p-value     Mean p-value    Max p-value\n----    --------------------    -----------     -----------     ------------    -----------\n 1/2                    rep1         53,697      2.239E-074       1.085E-003     1.000E-002\n 2/2                    rep2         37,717      5.370E-301       1.520E-004     9.550E-003\n\n.::.......Analyzing Samples........::.\n\n[1/4] Initializing\n[2/4] Processing samples\n  \u2514\u2500\u2500 60,004/60,004     (100.00%) peaks processed\n[3/4] Performing Multiple testing correction\n[4/4] Creating consensus peaks set\n\n.::.........Saving Results.........::.\n\n\n.::.......Summary Statistics.......::.\n\n   #                Filename    Read peaks#     Background          Weak        Stringent       Confirmed       Discarded       TruePositive    FalsePositive\n----    --------------------    -----------     ----------      --------        ---------       ---------       ---------       ------------    -------------\n 1/2                    rep1         53,697         47.05%        42.95%           10.01%          26.84%          26.12%             26.84%            0.00%\n 2/2                    rep2         37,717         16.30%        50.35%           33.35%          43.48%          40.22%             43.48%            0.00%\n\n.::.....Consensus Peaks Count......::.\n\n17,290\n\nAll processes successfully finished\n")),(0,i.kt)("p",null,"In this example, MSPC is called using two samples\n(i.e., ",(0,i.kt)("inlineCode",{parentName:"p"},"-i rep1.bed -i rep2.bed"),"), which are considered as\nbiological replicates (",(0,i.kt)("inlineCode",{parentName:"p"},"-r bio"),") with ",(0,i.kt)("inlineCode",{parentName:"p"},"1E-4")," and ",(0,i.kt)("inlineCode",{parentName:"p"},"1E-8")," thresholds\non p-values defining stringent and weak peaks respectively. "),(0,i.kt)("p",null,"Once executed, MSPC reports the following: "),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},"For each parsed sample, it reports the number of parsed peaks,\nminimum and maximum p-values parsed from the file;")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},"Reports analysis steps;")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},"Summary statistics containing the following information:"),(0,i.kt)("ul",{parentName:"li"},(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},"per-sample summary statistics that informs the number\nof analyzed peaks, and what percentage of these peaks\nare identified as ",(0,i.kt)("em",{parentName:"p"},"Stringent"),", ",(0,i.kt)("em",{parentName:"p"},"Weak"),", ",(0,i.kt)("em",{parentName:"p"},"Confirmed"),",\nand etc. (see ",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/method/sets"},"sets description"),")."),(0,i.kt)("div",{parentName:"li",className:"admonition admonition-info alert alert--info"},(0,i.kt)("div",{parentName:"div",className:"admonition-heading"},(0,i.kt)("h5",{parentName:"div"},(0,i.kt)("span",{parentName:"h5",className:"admonition-icon"},(0,i.kt)("svg",{parentName:"span",xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"},(0,i.kt)("path",{parentName:"svg",fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"}))),"info")),(0,i.kt)("div",{parentName:"div",className:"admonition-content"},(0,i.kt)("p",{parentName:"div"},"The percentages reported for ",(0,i.kt)("em",{parentName:"p"},"Stringent"),", ",(0,i.kt)("em",{parentName:"p"},"Weak"),", and ",(0,i.kt)("em",{parentName:"p"},"Background"),"\nsets, should add-up to ",(0,i.kt)("em",{parentName:"p"},"100%"),"; however, the percentage reported for\n",(0,i.kt)("em",{parentName:"p"},"Confirmed")," and ",(0,i.kt)("em",{parentName:"p"},"Discarded")," is not expected to add-up to ",(0,i.kt)("em",{parentName:"p"},"100%")," if\nthe percentage of ",(0,i.kt)("em",{parentName:"p"},"Background")," set is not ",(0,i.kt)("em",{parentName:"p"},"0%"),". Similarly, the\npercentages reported for ",(0,i.kt)("strong",{parentName:"p"},"TruePositive")," and ",(0,i.kt)("strong",{parentName:"p"},"FalsePositive"),"\nsets will not add-up to ",(0,i.kt)("em",{parentName:"p"},"100%")," if the percentage of ",(0,i.kt)("em",{parentName:"p"},"Background"),"\nand ",(0,i.kt)("em",{parentName:"p"},"Discarded")," is not ",(0,i.kt)("em",{parentName:"p"},"0%")," (see ",(0,i.kt)("a",{parentName:"p",href:"/MSPC/docs/method/sets"},"sets description"),").")))))),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("p",{parentName:"li"},"Number of consensus peaks."))),(0,i.kt)("h2",{id:"see-also"},"See Also"),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("a",{parentName:"li",href:"/MSPC/docs/method/about"},"Method description")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("a",{parentName:"li",href:"/MSPC/docs/cli/args"},"CLI arguments"))))}d.isMDXComponent=!0}}]);