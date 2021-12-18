"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[995],{3905:function(e,n,t){t.d(n,{Zo:function(){return m},kt:function(){return s}});var a=t(7294);function o(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function r(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);n&&(a=a.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,a)}return t}function c(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?r(Object(t),!0).forEach((function(n){o(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):r(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function d(e,n){if(null==e)return{};var t,a,o=function(e,n){if(null==e)return{};var t,a,o={},r=Object.keys(e);for(a=0;a<r.length;a++)t=r[a],n.indexOf(t)>=0||(o[t]=e[t]);return o}(e,n);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);for(a=0;a<r.length;a++)t=r[a],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(o[t]=e[t])}return o}var l=a.createContext({}),p=function(e){var n=a.useContext(l),t=n;return e&&(t="function"==typeof e?e(n):c(c({},n),e)),t},m=function(e){var n=p(e.components);return a.createElement(l.Provider,{value:n},e.children)},h={inlineCode:"code",wrapper:function(e){var n=e.children;return a.createElement(a.Fragment,{},n)}},i=a.forwardRef((function(e,n){var t=e.components,o=e.mdxType,r=e.originalType,l=e.parentName,m=d(e,["components","mdxType","originalType","parentName"]),i=p(t),s=o,f=i["".concat(l,".").concat(s)]||i[s]||h[s]||r;return t?a.createElement(f,c(c({ref:n},m),{},{components:t})):a.createElement(f,c({ref:n},m))}));function s(e,n){var t=arguments,o=n&&n.mdxType;if("string"==typeof e||o){var r=t.length,c=new Array(r);c[0]=i;var d={};for(var l in n)hasOwnProperty.call(n,l)&&(d[l]=n[l]);d.originalType=e,d.mdxType="string"==typeof e?e:o,c[1]=d;for(var p=2;p<r;p++)c[p]=t[p];return a.createElement.apply(null,c)}return a.createElement.apply(null,t)}i.displayName="MDXCreateElement"},4653:function(e,n,t){t.r(n),t.d(n,{frontMatter:function(){return d},metadata:function(){return l},toc:function(){return p},default:function(){return h}});var a=t(7462),o=t(3366),r=(t(7294),t(3905)),c=["components"],d={title:"Sample Data"},l={unversionedId:"sample_data",id:"sample_data",isDocsHomePage:!1,title:"Sample Data",description:"Download",source:"@site/docs/sample_data.md",sourceDirName:".",slug:"/sample_data",permalink:"/MSPC/docs/sample_data",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/sample_data.md",version:"current",frontMatter:{title:"Sample Data"},sidebar:"someSidebar",previous:{title:"Installation",permalink:"/MSPC/docs/installation"},next:{title:"About",permalink:"/MSPC/docs/method/about"}},p=[],m={toc:p};function h(e){var n=e.components,t=(0,o.Z)(e,c);return(0,r.kt)("wrapper",(0,a.Z)({},m,t,{components:n,mdxType:"MDXLayout"}),(0,r.kt)("p",null,(0,r.kt)("a",{parentName:"p",href:"http://www.bioinformatics.deib.polimi.it/genomic_computing/MSPC/packages/ENCODE_Samples.zip"},"Download"),"\na dataset of test peaks (37 MB)."),(0,r.kt)("p",null,"Peaks were called using ",(0,r.kt)("a",{parentName:"p",href:"http://liulab.dfci.harvard.edu/MACS/"},"MACS2")," with the arguments: ",(0,r.kt)("inlineCode",{parentName:"p"},"--auto-bimodal -p 0.0001 -g hs"),"."),(0,r.kt)("p",null,"BAM files of the test samples were obtained from ",(0,r.kt)("a",{parentName:"p",href:"https://www.encodeproject.org//"},"ENCODE"),":"),(0,r.kt)("ul",null,(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep1.bam"},"wgEncodeOpenChromChipK562CmycAlnRep1.bam")," (412 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep2.bam"},"wgEncodeOpenChromChipK562CmycAlnRep2.bam")," (286 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep3.bam"},"wgEncodeOpenChromChipK562CmycAlnRep3.bam")," (326 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIggrabAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycIggrabAlnRep1.bam")," (390 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIggrabAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycIggrabAlnRep2.bam")," (528 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycStdAlnRep1.bam")," (220 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycStdAlnRep2.bam")," (209 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep1.bam")," (386 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep2.bam")," (832 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna30StdAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycIfna30StdAlnRep1.bam")," (417 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna30StdAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycIfna30StdAlnRep2.bam")," (723 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep1.bam")," (555 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep2.bam")," (833 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep1.bam"},"wgEncodeSydhTfbsK562CmycStdAlnRep1.bam")," (221 MB);"),(0,r.kt)("li",{parentName:"ul"},(0,r.kt)("a",{parentName:"li",href:"ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep2.bam"},"wgEncodeSydhTfbsK562CmycStdAlnRep2.bam")," (209 MB).")))}h.isMDXComponent=!0}}]);