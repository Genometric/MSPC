"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[722],{3975:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>u,contentTitle:()=>c,default:()=>h,frontMatter:()=>o,metadata:()=>t,toc:()=>d});const t=JSON.parse('{"id":"quick_start","title":"Quick Start","description":"You may run MSPC as a command-line application, a","source":"@site/docs/quick_start.md","sourceDirName":".","slug":"/quick_start","permalink":"/MSPC/docs/quick_start","draft":false,"unlisted":false,"editUrl":"https://github.com/Genometric/MSPC/tree/dev/website/docs/quick_start.md","tags":[],"version":"current","frontMatter":{"title":"Quick Start"},"sidebar":"someSidebar","previous":{"title":"Welcome","permalink":"/MSPC/docs/"},"next":{"title":"Installation","permalink":"/MSPC/docs/installation"}}');var l=a(4848),s=a(8453),r=a(5537),i=a(9329);const o={title:"Quick Start"},c=void 0,u={},d=[{value:"Quick Start: Command-line",id:"quick-start-command-line",level:2},{value:"Preparation",id:"preparation",level:3},{value:"Run",id:"run",level:3},{value:"Output",id:"output",level:3},{value:"Quick Start: R package",id:"quick-start-r-package",level:2},{value:"Install and Load",id:"install-and-load",level:3},{value:"Run",id:"run-1",level:3},{value:"See Also",id:"see-also",level:2}];function p(e){const n={a:"a",admonition:"admonition",code:"code",h2:"h2",h3:"h3",li:"li",ol:"ol",p:"p",pre:"pre",strong:"strong",ul:"ul",...(0,s.R)(),...e.components};return(0,l.jsxs)(l.Fragment,{children:[(0,l.jsxs)(n.p,{children:["You may run MSPC as a command-line application, a\n",(0,l.jsx)(n.a,{href:"library/install",children:"C# library"}),", or an R package available from Bioconductor.\nIn the following, we provide quick start for using MSPC as a\n",(0,l.jsx)(n.a,{href:"#quick-start-command-line",children:"command-line application"})," and an\n",(0,l.jsx)(n.a,{href:"#quick-start-r-package",children:"R package"}),", and provide details in their\nrespective pages."]}),"\n",(0,l.jsx)(n.h2,{id:"quick-start-command-line",children:"Quick Start: Command-line"}),"\n",(0,l.jsx)(n.h3,{id:"preparation",children:"Preparation"}),"\n",(0,l.jsxs)(n.ol,{children:["\n",(0,l.jsxs)(n.li,{children:["Install a self-contained release of MSPC, using either of following commands\ndepending on your runtime (see ",(0,l.jsx)(n.a,{href:"/MSPC/docs/installation",children:"installation"})," page for detailed\ninstallation options):"]}),"\n"]}),"\n",(0,l.jsxs)(r.A,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],children:[(0,l.jsx)(i.A,{value:"win",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O win-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip\n"})})}),(0,l.jsx)(i.A,{value:"linux",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O linux-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip\n"})})}),(0,l.jsx)(i.A,{value:"mac",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O osx-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip\n"})})})]}),"\n",(0,l.jsxs)(n.ol,{start:"2",children:["\n",(0,l.jsx)(n.li,{children:"Extract the downloaded archive and browse to the containing directory:"}),"\n"]}),"\n",(0,l.jsxs)(r.A,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],children:[(0,l.jsx)(i.A,{value:"win",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"unzip win-x64.zip -d mspc; cd mspc\n"})})}),(0,l.jsx)(i.A,{value:"linux",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"unzip -q linux-x64.zip -d mspc && cd mspc && chmod +x mspc\n"})})}),(0,l.jsx)(i.A,{value:"mac",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"unzip -q osx-x64.zip -d mspc && cd mspc && chmod +x mspc\n"})})})]}),"\n",(0,l.jsx)(n.p,{children:"Notice that if you are working on Windows x64, you will need to download the program unzip."}),"\n",(0,l.jsxs)(n.ol,{start:"3",children:["\n",(0,l.jsx)(n.li,{children:"Download sample data:"}),"\n"]}),"\n",(0,l.jsxs)(r.A,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],children:[(0,l.jsx)(i.A,{value:"win",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .\n"})})}),(0,l.jsx)(i.A,{value:"linux",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n"})})}),(0,l.jsx)(i.A,{value:"mac",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n"})})})]}),"\n",(0,l.jsx)(n.h3,{id:"run",children:"Run"}),"\n",(0,l.jsx)(n.p,{children:"To run MSPC use the following command depending on your runtime:"}),"\n",(0,l.jsxs)(r.A,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],children:[(0,l.jsx)(i.A,{value:"win",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:".\\mspc.exe -i .\\rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})}),(0,l.jsx)(i.A,{value:"linux",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})}),(0,l.jsx)(i.A,{value:"mac",children:(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-shell",children:"./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})})]}),"\n",(0,l.jsx)(n.h3,{id:"output",children:"Output"}),"\n",(0,l.jsxs)(n.p,{children:["MSPC creates a folder in the current execution path named ",(0,l.jsx)(n.code,{children:"session_X_Y"}),",\nwhere ",(0,l.jsx)(n.code,{children:"X"})," and ",(0,l.jsx)(n.code,{children:"Y"})," are execution date and time respectively, which contains\nthe following files and folders:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-bash",children:".\n\u2514\u2500\u2500 session_20191126_222131330\n    \u251c\u2500\u2500 ConsensusPeaks.bed\n\t\u251c\u2500\u2500 ConsensusPeaks_mspc_peaks.txt\n\t\u251c\u2500\u2500 EventsLog_20191126_2221313409928.txt\n    \u251c\u2500\u2500 rep1\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u2514\u2500\u2500 Weak.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Weak_mspc_peaks.txt\n    \u2514\u2500\u2500 rep2\n     \xa0\xa0 \u251c\u2500\u2500 Background.bed\n     \xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n     \xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n      \xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n     \xa0\xa0 \u2514\u2500\u2500 Weak.bed\n        \u2514\u2500\u2500 Weak_mspc_peaks.txt\n"})}),"\n",(0,l.jsxs)(n.admonition,{type:"info",children:[(0,l.jsxs)(n.p,{children:["Note that in these instructions you download a\n",(0,l.jsx)(n.a,{href:"https://docs.microsoft.com/en-us/dotnet/core/deploying/#publish-self-contained",children:"self-contained deployment"}),"\nof MSPC, hence you do ",(0,l.jsx)(n.strong,{children:"not"})," need to install .NET (or\nany other dependencies) for the program to execute."]}),(0,l.jsxs)(n.p,{children:["For other installation options see the ",(0,l.jsx)(n.a,{href:"installation",children:"installation page"}),"."]})]}),"\n",(0,l.jsx)(n.h2,{id:"quick-start-r-package",children:"Quick Start: R package"}),"\n",(0,l.jsxs)(n.p,{children:["MSPC is also distributed as a\n",(0,l.jsx)(n.a,{href:"https://bioconductor.org/packages/release/bioc/html/rmspc.html",children:"Bioconductor package"}),".\nTo use in R programming language, you may take the following steps."]}),"\n",(0,l.jsx)(n.h3,{id:"install-and-load",children:"Install and Load"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-r",children:'if (!require("BiocManager"))\n    install.packages("BiocManager")\nBiocManager::install("rmspc")\n\nlibrary(rmspc)\n'})}),"\n",(0,l.jsx)(n.h3,{id:"run-1",children:"Run"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-r",children:'# Load example data available in the package.\npath <- system.file("extdata", package = "rmspc")\n\n# Run MSPC\nresults <- mspc(\n        input = input, \n        replicateType = "Technical",\n        stringencyThreshold = 1e-8,\n        weakThreshold = 1e-4, \n        gamma = 1e-8,\n        keep = FALSE,\n        GRanges = TRUE,\n        multipleIntersections = "Lowest",\n        c = 2,\n        alpha = 0.05)\n'})}),"\n",(0,l.jsx)(n.admonition,{type:"info",children:(0,l.jsxs)(n.p,{children:["The MSPC R package can load data from files and Granges objects.\nFor a complete documentation on the package, please refer to the\n",(0,l.jsx)(n.a,{href:"https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html",children:"bioconductor documentation"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"see-also",children:"See Also"}),"\n",(0,l.jsxs)(n.ul,{children:["\n",(0,l.jsx)(n.li,{children:(0,l.jsx)(n.a,{href:"/MSPC/docs/",children:"Welcome page"})}),"\n",(0,l.jsx)(n.li,{children:(0,l.jsx)(n.a,{href:"/MSPC/docs/cli/input",children:"Input file format"})}),"\n",(0,l.jsx)(n.li,{children:(0,l.jsx)(n.a,{href:"/MSPC/docs/cli/output",children:"Output files"})}),"\n",(0,l.jsx)(n.li,{children:(0,l.jsx)(n.a,{href:"/MSPC/docs/method/sets",children:"Sets"})}),"\n"]})]})}function h(e={}){const{wrapper:n}={...(0,s.R)(),...e.components};return n?(0,l.jsx)(n,{...e,children:(0,l.jsx)(p,{...e})}):p(e)}},9329:(e,n,a)=>{a.d(n,{A:()=>r});a(6540);var t=a(4164);const l={tabItem:"tabItem_Ymn6"};var s=a(4848);function r(e){let{children:n,hidden:a,className:r}=e;return(0,s.jsx)("div",{role:"tabpanel",className:(0,t.A)(l.tabItem,r),hidden:a,children:n})}},5537:(e,n,a)=>{a.d(n,{A:()=>w});var t=a(6540),l=a(4164),s=a(5627),r=a(6347),i=a(372),o=a(604),c=a(1861),u=a(8749);function d(e){var n,a;return null!=(n=null==(a=t.Children.toArray(e).filter((e=>"\n"!==e)).map((e=>{if(!e||(0,t.isValidElement)(e)&&function(e){const{props:n}=e;return!!n&&"object"==typeof n&&"value"in n}(e))return e;throw new Error("Docusaurus error: Bad <Tabs> child <"+("string"==typeof e.type?e.type:e.type.name)+'>: all children of the <Tabs> component should be <TabItem>, and every <TabItem> should have a unique "value" prop.')})))?void 0:a.filter(Boolean))?n:[]}function p(e){const{values:n,children:a}=e;return(0,t.useMemo)((()=>{const e=null!=n?n:function(e){return d(e).map((e=>{let{props:{value:n,label:a,attributes:t,default:l}}=e;return{value:n,label:a,attributes:t,default:l}}))}(a);return function(e){const n=(0,c.XI)(e,((e,n)=>e.value===n.value));if(n.length>0)throw new Error('Docusaurus error: Duplicate values "'+n.map((e=>e.value)).join(", ")+'" found in <Tabs>. Every value needs to be unique.')}(e),e}),[n,a])}function h(e){let{value:n,tabValues:a}=e;return a.some((e=>e.value===n))}function m(e){let{queryString:n=!1,groupId:a}=e;const l=(0,r.W6)(),s=function(e){let{queryString:n=!1,groupId:a}=e;if("string"==typeof n)return n;if(!1===n)return null;if(!0===n&&!a)throw new Error('Docusaurus error: The <Tabs> component groupId prop is required if queryString=true, because this value is used as the search param name. You can also provide an explicit value such as queryString="my-search-param".');return null!=a?a:null}({queryString:n,groupId:a});return[(0,o.aZ)(s),(0,t.useCallback)((e=>{if(!s)return;const n=new URLSearchParams(l.location.search);n.set(s,e),l.replace(Object.assign({},l.location,{search:n.toString()}))}),[s,l])]}function x(e){const{defaultValue:n,queryString:a=!1,groupId:l}=e,s=p(e),[r,o]=(0,t.useState)((()=>function(e){var n;let{defaultValue:a,tabValues:t}=e;if(0===t.length)throw new Error("Docusaurus error: the <Tabs> component requires at least one <TabItem> children component");if(a){if(!h({value:a,tabValues:t}))throw new Error('Docusaurus error: The <Tabs> has a defaultValue "'+a+'" but none of its children has the corresponding value. Available values are: '+t.map((e=>e.value)).join(", ")+". If you intend to show no default tab, use defaultValue={null} instead.");return a}const l=null!=(n=t.find((e=>e.default)))?n:t[0];if(!l)throw new Error("Unexpected error: 0 tabValues");return l.value}({defaultValue:n,tabValues:s}))),[c,d]=m({queryString:a,groupId:l}),[x,g]=function(e){let{groupId:n}=e;const a=function(e){return e?"docusaurus.tab."+e:null}(n),[l,s]=(0,u.Dv)(a);return[l,(0,t.useCallback)((e=>{a&&s.set(e)}),[a,s])]}({groupId:l}),b=(()=>{const e=null!=c?c:x;return h({value:e,tabValues:s})?e:null})();(0,i.A)((()=>{b&&o(b)}),[b]);return{selectedValue:r,selectValue:(0,t.useCallback)((e=>{if(!h({value:e,tabValues:s}))throw new Error("Can't select invalid tab value="+e);o(e),d(e),g(e)}),[d,g,s]),tabValues:s}}var g=a(9136);const b={tabList:"tabList__CuJ",tabItem:"tabItem_LNqP"};var f=a(4848);function v(e){let{className:n,block:a,selectedValue:t,selectValue:r,tabValues:i}=e;const o=[],{blockElementScrollPositionUntilNextRender:c}=(0,s.a_)(),u=e=>{const n=e.currentTarget,a=o.indexOf(n),l=i[a].value;l!==t&&(c(n),r(l))},d=e=>{var n;let a=null;switch(e.key){case"Enter":u(e);break;case"ArrowRight":{var t;const n=o.indexOf(e.currentTarget)+1;a=null!=(t=o[n])?t:o[0];break}case"ArrowLeft":{var l;const n=o.indexOf(e.currentTarget)-1;a=null!=(l=o[n])?l:o[o.length-1];break}}null==(n=a)||n.focus()};return(0,f.jsx)("ul",{role:"tablist","aria-orientation":"horizontal",className:(0,l.A)("tabs",{"tabs--block":a},n),children:i.map((e=>{let{value:n,label:a,attributes:s}=e;return(0,f.jsx)("li",Object.assign({role:"tab",tabIndex:t===n?0:-1,"aria-selected":t===n,ref:e=>{o.push(e)},onKeyDown:d,onClick:u},s,{className:(0,l.A)("tabs__item",b.tabItem,null==s?void 0:s.className,{"tabs__item--active":t===n}),children:null!=a?a:n}),n)}))})}function j(e){let{lazy:n,children:a,selectedValue:s}=e;const r=(Array.isArray(a)?a:[a]).filter(Boolean);if(n){const e=r.find((e=>e.props.value===s));return e?(0,t.cloneElement)(e,{className:(0,l.A)("margin-top--md",e.props.className)}):null}return(0,f.jsx)("div",{className:"margin-top--md",children:r.map(((e,n)=>(0,t.cloneElement)(e,{key:n,hidden:e.props.value!==s})))})}function k(e){const n=x(e);return(0,f.jsxs)("div",{className:(0,l.A)("tabs-container",b.tabList),children:[(0,f.jsx)(v,Object.assign({},n,e)),(0,f.jsx)(j,Object.assign({},n,e))]})}function w(e){const n=(0,g.A)();return(0,f.jsx)(k,Object.assign({},e,{children:d(e.children)}),String(n))}},8453:(e,n,a)=>{a.d(n,{R:()=>r,x:()=>i});var t=a(6540);const l={},s=t.createContext(l);function r(e){const n=t.useContext(s);return t.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function i(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(l):e.components||l:r(e.components),t.createElement(s.Provider,{value:n},e.children)}}}]);