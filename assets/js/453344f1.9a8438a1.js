"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[948],{3905:function(e,t,n){n.d(t,{Zo:function(){return l},kt:function(){return d}});var r=n(7294);function o(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function i(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){o(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function s(e,t){if(null==e)return{};var n,r,o=function(e,t){if(null==e)return{};var n,r,o={},a=Object.keys(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||(o[n]=e[n]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(o[n]=e[n])}return o}var p=r.createContext({}),c=function(e){var t=r.useContext(p),n=t;return e&&(n="function"==typeof e?e(t):i(i({},t),e)),n},l=function(e){var t=c(e.components);return r.createElement(p.Provider,{value:t},e.children)},u={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},m=r.forwardRef((function(e,t){var n=e.components,o=e.mdxType,a=e.originalType,p=e.parentName,l=s(e,["components","mdxType","originalType","parentName"]),m=c(n),d=o,f=m["".concat(p,".").concat(d)]||m[d]||u[d]||a;return n?r.createElement(f,i(i({ref:t},l),{},{components:n})):r.createElement(f,i({ref:t},l))}));function d(e,t){var n=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=n.length,i=new Array(a);i[0]=m;var s={};for(var p in t)hasOwnProperty.call(t,p)&&(s[p]=t[p]);s.originalType=e,s.mdxType="string"==typeof e?e:o,i[1]=s;for(var c=2;c<a;c++)i[c]=n[c];return r.createElement.apply(null,i)}return r.createElement.apply(null,n)}m.displayName="MDXCreateElement"},6606:function(e,t,n){n.r(t),n.d(t,{frontMatter:function(){return s},metadata:function(){return p},toc:function(){return c},default:function(){return u}});var r=n(7462),o=n(3366),a=(n(7294),n(3905)),i=["components"],s={title:"Consensus peaks"},p={unversionedId:"method/consensus",id:"method/consensus",isDocsHomePage:!1,title:"Consensus peaks",description:"A consensus peak is created on a position of genome where is covered by at least one peak from the output sets of the processed replicates. Accordingly, a consensus peak has the following characteristics:",source:"@site/docs/method/consensus.md",sourceDirName:"method",slug:"/method/consensus",permalink:"/MSPC/docs/method/consensus",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/method/consensus.md",version:"current",frontMatter:{title:"Consensus peaks"},sidebar:"someSidebar",previous:{title:"Sets",permalink:"/MSPC/docs/method/sets"},next:{title:"About",permalink:"/MSPC/docs/cli/about"}},c=[],l={toc:c};function u(e){var t=e.components,n=(0,o.Z)(e,i);return(0,a.kt)("wrapper",(0,r.Z)({},l,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("p",null,"A consensus peak is created on a position of genome where is covered by at least one peak from the ",(0,a.kt)("inlineCode",{parentName:"p"},"output")," sets of the processed replicates. Accordingly, a consensus peak has the following characteristics:"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"Coordinate")," (i.e., ",(0,a.kt)("em",{parentName:"p"},"chromosome"),", ",(0,a.kt)("em",{parentName:"p"},"start"),", and ",(0,a.kt)("em",{parentName:"p"},"stop"),") ",(0,a.kt)("br",null),"\nThe ",(0,a.kt)("strong",{parentName:"p"},"coordinate")," of a consensus peak is the union of the coordinates of the peaks from ",(0,a.kt)("inlineCode",{parentName:"p"},"output")," sets which overlap that position on the genome. For instance: "),(0,a.kt)("pre",{parentName:"li"},(0,a.kt)("code",{parentName:"pre"},"  // From the Output set of Replicate 1 \n  chr1    10    20    macs_peak_1    20.9\n\n  // From the Output set of Replicate 2 \n  chr1    15    25    macs_peak_1    14.6\n\n  // The resulting consensus peak \n  chr1    10    25    mspc_peak_1    163.484\nNote that the p-values are in `-log10` format. \n"))),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"Value")," ",(0,a.kt)("br",null),"\nThe value of each consensus peak is ",(0,a.kt)("inlineCode",{parentName:"p"},"X^2")," which is calculated by combining the ",(0,a.kt)("inlineCode",{parentName:"p"},"p-value"),"s of the overlapping peaks using ",(0,a.kt)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/Fisher%27s_method"},"Fisher's combined probability test"),":"),(0,a.kt)("pre",{parentName:"li"},(0,a.kt)("code",{parentName:"pre"},"  X_i^2 = -2 \\times \\sum_{i=1}^k \\ln(p_i)\n")))),(0,a.kt)("p",null,"where ",(0,a.kt)("inlineCode",{parentName:"p"},"X_i^2")," is the value of consensus peak ",(0,a.kt)("inlineCode",{parentName:"p"},"i"),", and ",(0,a.kt)("inlineCode",{parentName:"p"},"p_i")," is the p-value of overlapping peaks. For instance, the ",(0,a.kt)("inlineCode",{parentName:"p"},"X^2")," of aforementioned consensus peaks is ",(0,a.kt)("inlineCode",{parentName:"p"},"163.484"),", which is calculated by combining the p-values of overlapping peaks (i.e., ",(0,a.kt)("inlineCode",{parentName:"p"},"20.9")," and ",(0,a.kt)("inlineCode",{parentName:"p"},"14.6"),") using the ",(0,a.kt)("a",{parentName:"p",href:"https://en.wikipedia.org/wiki/Fisher%27s_method"},"Fisher's method"),"."))}u.isMDXComponent=!0}}]);