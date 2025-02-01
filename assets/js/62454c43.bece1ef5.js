"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[130],{5862:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>d,contentTitle:()=>c,default:()=>p,frontMatter:()=>o,metadata:()=>s,toc:()=>u});const s=JSON.parse('{"id":"cli/output","title":"Output","description":"MSPC persists the results of each execution to a separate folder. Users can specify","source":"@site/docs/cli/output.md","sourceDirName":"cli","slug":"/cli/output","permalink":"/MSPC/docs/cli/output","draft":false,"unlisted":false,"editUrl":"https://github.com/Genometric/MSPC/tree/dev/website/docs/cli/output.md","tags":[],"version":"current","frontMatter":{"title":"Output"},"sidebar":"someSidebar","previous":{"title":"Input","permalink":"/MSPC/docs/cli/input"},"next":{"title":"Arguments","permalink":"/MSPC/docs/cli/args"}}');var r=n(4848),a=n(8453),i=n(5537),l=n(9329);const o={title:"Output"},c=void 0,d={},u=[{value:"BED files",id:"bed-files",level:2},{value:"MSPC Peaks",id:"mspc-peaks",level:2},{value:"Log File",id:"log-file",level:2},{value:"See Also",id:"see-also",level:2}];function h(e){const t={a:"a",code:"code",em:"em",h2:"h2",li:"li",ol:"ol",p:"p",pre:"pre",ul:"ul",...(0,a.R)(),...e.components};return(0,r.jsxs)(r.Fragment,{children:[(0,r.jsxs)(t.p,{children:["MSPC persists the results of each execution to a separate folder. Users can specify\nthe output directory via the optional argument ",(0,r.jsx)(t.a,{href:"../cli/args#output-path",children:(0,r.jsx)(t.code,{children:"-o | --output"})}),";\nif not specified, MSPC creates an output directory with the following naming scheme."]}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:"session_[DATE]_[TIME]\n\n// For example:\nsession_20191126_222131330\n"})}),"\n",(0,r.jsx)(t.p,{children:"Each output folder contains the following information:"}),"\n",(0,r.jsxs)(t.p,{children:["A log file that contains the execution log;\nConsensus peaks in standard BED and MSPC format;\nOne folder per each replicates contains BED files containing\n",(0,r.jsx)(t.a,{href:"../method/sets#stringent",children:(0,r.jsx)(t.code,{children:"stringent"})}),", ",(0,r.jsx)(t.a,{href:"../method/sets#weak",children:(0,r.jsx)(t.code,{children:"weak"})}),",\n",(0,r.jsx)(t.a,{href:"../method/sets#background",children:(0,r.jsx)(t.code,{children:"background"})}),", ",(0,r.jsx)(t.a,{href:"../method/sets#confirmed",children:(0,r.jsx)(t.code,{children:"confirmed"})}),",\n",(0,r.jsx)(t.a,{href:"../method/sets#discarded",children:(0,r.jsx)(t.code,{children:"discarded"})}),", ",(0,r.jsx)(t.a,{href:"../method/sets#truepositive",children:(0,r.jsx)(t.code,{children:"true-positive"})}),",\nand ",(0,r.jsx)(t.a,{href:"../method/sets#falsepositive",children:(0,r.jsx)(t.code,{children:"false-positive"})})," peaks.\nYou may refer to the ",(0,r.jsx)(t.a,{href:"/MSPC/docs/method/sets",children:"Sets"})," page for a detailed\ndescription of each category."]}),"\n",(0,r.jsxs)(t.p,{children:["An MSPC generated output for two replicates ",(0,r.jsx)(t.code,{children:"rep1"})," and ",(0,r.jsx)(t.code,{children:"rep2"})," is as the following:"]}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:".\n\u2514\u2500\u2500 session_20210309_131747501\n    \u251c\u2500\u2500 ConsensusPeaks.bed\n\t\u251c\u2500\u2500 ConsensusPeaks_mspc_peaks.txt\n\t\u251c\u2500\u2500 EventsLog_20210309_1317475050929.txt\n    \u251c\u2500\u2500 rep1\n    \u2502   \u251c\u2500\u2500 Background.bed\n    \u2502   \u251c\u2500\u2500 Background_mspc_peaks.txt\n    \u2502   \u251c\u2500\u2500 Confirmed.bed\n    \u2502   \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n    \u2502   \u251c\u2500\u2500 Discarded.bed\n    \u2502   \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n    \u2502   \u251c\u2500\u2500 FalsePositive.bed\n    \u2502   \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n    \u2502   \u251c\u2500\u2500 Stringent.bed\n    \u2502   \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n    \u2502   \u251c\u2500\u2500 TruePositive.bed\n    \u2502   \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n    \u2502   \u2514\u2500\u2500 Weak.bed\n    \u2502   \u251c\u2500\u2500 Weak_mspc_peaks.txt\n    \u2514\u2500\u2500 rep2\n        \u251c\u2500\u2500 Background.bed\n        \u251c\u2500\u2500 Background_mspc_peaks.txt\n        \u251c\u2500\u2500 Confirmed.bed\n        \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n        \u251c\u2500\u2500 Discarded.bed\n        \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n        \u251c\u2500\u2500 FalsePositive.bed\n        \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n        \u251c\u2500\u2500 Stringent.bed\n        \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n        \u251c\u2500\u2500 TruePositive.bed\n        \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n        \u2514\u2500\u2500 Weak.bed\n        \u2514\u2500\u2500 Weak_mspc_peaks.txt\n"})}),"\n",(0,r.jsx)(t.h2,{id:"bed-files",children:"BED files"}),"\n",(0,r.jsxs)(t.p,{children:["The ",(0,r.jsx)(t.code,{children:"*.bed"})," files contain peaks in the standard\n",(0,r.jsx)(t.a,{href:"https://genome.ucsc.edu/FAQ/FAQformat.html#format1",children:"BED format"}),".\nThese files contain peaks parsed from input files organized under\nthe ",(0,r.jsx)(t.code,{children:"stringent"}),", ",(0,r.jsx)(t.code,{children:"weak"}),", ",(0,r.jsx)(t.code,{children:"background"}),", ",(0,r.jsx)(t.code,{children:"confirmed"}),", ",(0,r.jsx)(t.code,{children:"discarded"}),",\n",(0,r.jsx)(t.code,{children:"true-positive"})," and ",(0,r.jsx)(t.code,{children:"false-positive"})," groups. For example, peaks\nin a ",(0,r.jsx)(t.code,{children:"Confirmed.bed"})," and ",(0,r.jsx)(t.code,{children:"Discarded.bed"})," files may read is the following:"]}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:"$ head .\\rep1\\Confirmed.bed\nchr     start   stop    name     -1xlog10(p-value)\tstrand\nchr1    32600   32680   peak_4   4.08\t            .\nchr1    32726   32936   peak_5   17.5\t            .\nchr1    34689   34797   peak_6   5.82\t            .\nchr1    35083   35124   peak_7   4.59\t            .\nchr1    38593   38836   peak_8   10.7\t            .\n\n$ head .\\rep1\\Discarded.bed\nchr     start   stop    name     -1xlog10(p-value)\tstrand\nchr1    137343  137383  peak_10  4.8\t            .\nchr1    228585  228625  peak_12  4.37\t            .\nchr1    265059  265115  peak_14  5.22\t            .\nchr1    557793  557833  peak_29  4.16\t            .\nchr1    725914  725963  peak_34  5.95\t            .\n"})}),"\n",(0,r.jsxs)(t.p,{children:["Accordingly, the peak named ",(0,r.jsx)(t.code,{children:"MACS_peak_4 "})," is ",(0,r.jsx)(t.em,{children:"confirmed"})," while\na peak with ",(0,r.jsx)(t.code,{children:"MACS_peak_10 "})," name is ",(0,r.jsx)(t.em,{children:"discarded"}),"."]}),"\n",(0,r.jsx)(t.h2,{id:"mspc-peaks",children:"MSPC Peaks"}),"\n",(0,r.jsxs)(t.p,{children:["The ",(0,r.jsx)(t.code,{children:"*_mspc_peaks.txt"})," files group input peaks in different\ngroups similar to the ",(0,r.jsx)(t.code,{children:"*.bed"})," files. In addition, they\ncontain information about the analysis performed on each peak.\nThe additional information are:"]}),"\n",(0,r.jsxs)(t.ol,{children:["\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:["Combined probability of\neach peak that is X-squared of\n",(0,r.jsx)(t.a,{href:"https://en.wikipedia.org/wiki/Fisher%27s_method",children:"Fisher\u2019s method"}),";"]}),"\n"]}),"\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:["Right-tailed probability of the X-squared\n(represented in ",(0,r.jsx)(t.code,{children:"-Log10 (right-tailed probability)"}),";"]}),"\n"]}),"\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:["Benjamini\u2013Hochberg corrected p-value (represented in\n",(0,r.jsx)(t.code,{children:"-Log10 (Adjusted p-value)"}),"."]}),"\n"]}),"\n"]}),"\n",(0,r.jsx)(t.p,{children:"For example, peaks corresponding to the above-mentioned\npeaks in the confirmed and discarded sets are as the following."}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:"$ head .\\rep1\\Confirmed_mspc_peaks.txt\nchr     start   stop    name\t  -1xlog10(p-value)  strand  xSqrd\t  -1xlog10(Right-Tail Probability)  -1xlog10(AdjustedP-value)\nchr1    32600   32680   peak_4    4.08               .       222.936  46.359                            4.07\nchr1    32726   32936   peak_5    17.5               .       284.738  59.674                            16.146\nchr1    34689   34797   peak_6    5.82               .       74.005   14.49                             5.634\nchr1    35083   35124   peak_7    4.59               .       52.867   10.042                            4.537\nchr1    38593   38836   peak_8    10.7               .       121.576  24.609                            9.892\n\n$ head .\\rep1\\Discarded_mspc_peaks.txt\nchr     start   stop    name\t  -1xlog10(p-value)  strand  xSqrd   -1xlog10(Right-Tail Probability)  -1xlog10(AdjustedP-value)\nchr1    137343  137383  peak_10   4.8                .       22.105  4.8                               NaN\nchr1    228585  228625  peak_12   4.37               .       20.125  4.37                              NaN\nchr1    265059  265115  peak_14   5.22               .       24.039  5.22                              NaN\nchr1    557793  557833  peak_29   4.16               .       19.158  4.16                              NaN\nchr1    725914  725963  peak_34   5.95               .       27.401  5.95                              NaN\n"})}),"\n",(0,r.jsxs)(t.p,{children:["Note that the first five columns are identical between ",(0,r.jsx)(t.code,{children:"*.bed"}),"\nand ",(0,r.jsx)(t.code,{children:"*_mspc_peaks.txt"})," files, the columns 6, 7, and 8 are\nadded in the ",(0,r.jsx)(t.code,{children:"*_mspc_peaks.txt"})," files."]}),"\n",(0,r.jsx)(t.p,{children:"In order to reproduce these results, you may run the following commands:"}),"\n",(0,r.jsxs)(i.A,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],children:[(0,r.jsx)(l.A,{value:"win",children:(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .\n.\\mspc.exe -i .\\rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})}),(0,r.jsx)(l.A,{value:"linux",children:(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})}),(0,r.jsx)(l.A,{value:"mac",children:(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{className:"language-shell",children:"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n"})})})]}),"\n",(0,r.jsx)(t.h2,{id:"log-file",children:"Log File"}),"\n",(0,r.jsx)(t.p,{children:"This file contains the information, debugging messages,\nand exceptions that occurred during the execution.\nThe files reads as the following:"}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:"2021-03-09 15:47:22,524\t[1]\tINFO \tNOTE THAT THE LOG PATTERN IS: <Date> <#Thread> <Level> <Message>\n2021-03-09 15:47:22,570\t[1]\tINFO \tExport Directory: C:\\mspc\\session_20210309_154722332\n2021-03-09 15:47:22,572\t[1]\tINFO \tDegree of parallelism is set to 8.\n2021-03-09 15:47:22,595\t[1]\tINFO \t.::...Parsing Samples....::.\n2021-03-09 15:47:22,597\t[1]\tINFO \t   #\t            Filename\tRead peaks#\tMin p-value\tMean p-value\tMax p-value\t\n2021-03-09 15:47:22,597\t[1]\tINFO \t----\t--------------------\t-----------\t-----------\t------------\t-----------\t\n2021-03-09 15:47:22,906\t[1]\tINFO \t1/2\t.\\rep1.bed\t53,697\t2.239E-074\t1.085E-003\t1.000E-002\t\n2021-03-09 15:47:23,082\t[1]\tINFO \t2/2\t.\\rep2.bed\t37,717\t5.370E-301\t1.520E-004\t9.550E-003\t\n2021-03-09 15:47:23,084\t[1]\tINFO \t.::..Analyzing Samples...::.\n2021-03-09 15:47:23,093\t[5]\tINFO \t[1/4] Initializing\n2021-03-09 15:47:23,412\t[5]\tINFO \t[2/4] Processing samples\n...\n2021-03-09 15:47:23,749\t[1]\tINFO \t.::....Saving Results....::.\n2021-03-09 15:47:26,162\t[1]\tINFO \t.::..Summary Statistics..::.\n2021-03-09 15:47:26,163\t[1]\tINFO \t   #\t            Filename\tRead peaks#\tBackground\t    Weak\tStringent\tConfirmed\tDiscarded\tTruePositive\tFalsePositive\t\n2021-03-09 15:47:26,164\t[1]\tINFO \t----\t--------------------\t-----------\t----------\t--------\t---------\t---------\t---------\t------------\t-------------\t\n2021-03-09 15:47:26,178\t[1]\tINFO \t 1/2\t                rep1\t     53,697\t    47.05%\t  42.95%\t   10.01%\t   26.84%\t   26.12%\t      26.84%\t        0.00%\t\n2021-03-09 15:47:26,191\t[1]\tINFO \t 2/2\t                rep2\t     37,717\t    16.30%\t  50.35%\t   33.35%\t   43.48%\t   40.22%\t      43.48%\t        0.00%\t\n2021-03-09 15:47:26,192\t[1]\tINFO \t.::.Consensus Peaks Count.::.\n2021-03-09 15:47:26,192\t[1]\tINFO \t17,290\n2021-03-09 15:47:26,193\t[1]\tINFO \tElapsed time: 00:00:03.9396445\n2021-03-09 15:47:26,193\t[1]\tINFO \tAll processes successfully finished.\n\n"})}),"\n",(0,r.jsx)(t.p,{children:"Note that the logs are reported in the following format:"}),"\n",(0,r.jsx)(t.pre,{children:(0,r.jsx)(t.code,{children:"<Date> <#Thread> <Level> <Message>\n"})}),"\n",(0,r.jsxs)(t.ul,{children:["\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:[(0,r.jsx)(t.code,{children:"Message"})," is the description of each log entry;"]}),"\n"]}),"\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:["Possible values for ",(0,r.jsx)(t.code,{children:"Level"})," are ",(0,r.jsx)(t.code,{children:"INFO"}),", ",(0,r.jsx)(t.code,{children:"DEBUG"})," and ",(0,r.jsx)(t.code,{children:"ERR"}),";"]}),"\n"]}),"\n",(0,r.jsxs)(t.li,{children:["\n",(0,r.jsxs)(t.p,{children:[(0,r.jsx)(t.code,{children:"Thread"})," is the number of the process thread MSPC used for\nexecuting each process. MSPC runs operations in parallel\nusing ",(0,r.jsx)(t.code,{children:"n"})," threads, where ",(0,r.jsx)(t.code,{children:"n"})," is the degree of parallelism\nand reported at the beginning of the logs. It can be set\nvia the ",(0,r.jsx)(t.a,{href:"../cli/args#degree-of-parallelism",children:(0,r.jsx)(t.code,{children:"-d | -degree-of-parallelism argument"})}),".\nThis information is useful for debugging purposes only."]}),"\n"]}),"\n"]}),"\n",(0,r.jsx)(t.h2,{id:"see-also",children:"See Also"}),"\n",(0,r.jsxs)(t.ul,{children:["\n",(0,r.jsx)(t.li,{children:(0,r.jsx)(t.a,{href:"/MSPC/docs/method/about",children:"Method description"})}),"\n",(0,r.jsx)(t.li,{children:(0,r.jsx)(t.a,{href:"/MSPC/docs/method/sets",children:"Sets description"})}),"\n"]})]})}function p(e={}){const{wrapper:t}={...(0,a.R)(),...e.components};return t?(0,r.jsx)(t,{...e,children:(0,r.jsx)(h,{...e})}):h(e)}},9329:(e,t,n)=>{n.d(t,{A:()=>i});n(6540);var s=n(4164);const r={tabItem:"tabItem_Ymn6"};var a=n(4848);function i(e){let{children:t,hidden:n,className:i}=e;return(0,a.jsx)("div",{role:"tabpanel",className:(0,s.A)(r.tabItem,i),hidden:n,children:t})}},5537:(e,t,n)=>{n.d(t,{A:()=>_});var s=n(6540),r=n(4164),a=n(5627),i=n(6347),l=n(372),o=n(604),c=n(1861),d=n(8749);function u(e){var t,n;return null!=(t=null==(n=s.Children.toArray(e).filter((e=>"\n"!==e)).map((e=>{if(!e||(0,s.isValidElement)(e)&&function(e){const{props:t}=e;return!!t&&"object"==typeof t&&"value"in t}(e))return e;throw new Error("Docusaurus error: Bad <Tabs> child <"+("string"==typeof e.type?e.type:e.type.name)+'>: all children of the <Tabs> component should be <TabItem>, and every <TabItem> should have a unique "value" prop.')})))?void 0:n.filter(Boolean))?t:[]}function h(e){const{values:t,children:n}=e;return(0,s.useMemo)((()=>{const e=null!=t?t:function(e){return u(e).map((e=>{let{props:{value:t,label:n,attributes:s,default:r}}=e;return{value:t,label:n,attributes:s,default:r}}))}(n);return function(e){const t=(0,c.XI)(e,((e,t)=>e.value===t.value));if(t.length>0)throw new Error('Docusaurus error: Duplicate values "'+t.map((e=>e.value)).join(", ")+'" found in <Tabs>. Every value needs to be unique.')}(e),e}),[t,n])}function p(e){let{value:t,tabValues:n}=e;return n.some((e=>e.value===t))}function m(e){let{queryString:t=!1,groupId:n}=e;const r=(0,i.W6)(),a=function(e){let{queryString:t=!1,groupId:n}=e;if("string"==typeof t)return t;if(!1===t)return null;if(!0===t&&!n)throw new Error('Docusaurus error: The <Tabs> component groupId prop is required if queryString=true, because this value is used as the search param name. You can also provide an explicit value such as queryString="my-search-param".');return null!=n?n:null}({queryString:t,groupId:n});return[(0,o.aZ)(a),(0,s.useCallback)((e=>{if(!a)return;const t=new URLSearchParams(r.location.search);t.set(a,e),r.replace(Object.assign({},r.location,{search:t.toString()}))}),[a,r])]}function x(e){const{defaultValue:t,queryString:n=!1,groupId:r}=e,a=h(e),[i,o]=(0,s.useState)((()=>function(e){var t;let{defaultValue:n,tabValues:s}=e;if(0===s.length)throw new Error("Docusaurus error: the <Tabs> component requires at least one <TabItem> children component");if(n){if(!p({value:n,tabValues:s}))throw new Error('Docusaurus error: The <Tabs> has a defaultValue "'+n+'" but none of its children has the corresponding value. Available values are: '+s.map((e=>e.value)).join(", ")+". If you intend to show no default tab, use defaultValue={null} instead.");return n}const r=null!=(t=s.find((e=>e.default)))?t:s[0];if(!r)throw new Error("Unexpected error: 0 tabValues");return r.value}({defaultValue:t,tabValues:a}))),[c,u]=m({queryString:n,groupId:r}),[x,f]=function(e){let{groupId:t}=e;const n=function(e){return e?"docusaurus.tab."+e:null}(t),[r,a]=(0,d.Dv)(n);return[r,(0,s.useCallback)((e=>{n&&a.set(e)}),[n,a])]}({groupId:r}),g=(()=>{const e=null!=c?c:x;return p({value:e,tabValues:a})?e:null})();(0,l.A)((()=>{g&&o(g)}),[g]);return{selectedValue:i,selectValue:(0,s.useCallback)((e=>{if(!p({value:e,tabValues:a}))throw new Error("Can't select invalid tab value="+e);o(e),u(e),f(e)}),[u,f,a]),tabValues:a}}var f=n(9136);const g={tabList:"tabList__CuJ",tabItem:"tabItem_LNqP"};var b=n(4848);function j(e){let{className:t,block:n,selectedValue:s,selectValue:i,tabValues:l}=e;const o=[],{blockElementScrollPositionUntilNextRender:c}=(0,a.a_)(),d=e=>{const t=e.currentTarget,n=o.indexOf(t),r=l[n].value;r!==s&&(c(t),i(r))},u=e=>{var t;let n=null;switch(e.key){case"Enter":d(e);break;case"ArrowRight":{var s;const t=o.indexOf(e.currentTarget)+1;n=null!=(s=o[t])?s:o[0];break}case"ArrowLeft":{var r;const t=o.indexOf(e.currentTarget)-1;n=null!=(r=o[t])?r:o[o.length-1];break}}null==(t=n)||t.focus()};return(0,b.jsx)("ul",{role:"tablist","aria-orientation":"horizontal",className:(0,r.A)("tabs",{"tabs--block":n},t),children:l.map((e=>{let{value:t,label:n,attributes:a}=e;return(0,b.jsx)("li",Object.assign({role:"tab",tabIndex:s===t?0:-1,"aria-selected":s===t,ref:e=>{o.push(e)},onKeyDown:u,onClick:d},a,{className:(0,r.A)("tabs__item",g.tabItem,null==a?void 0:a.className,{"tabs__item--active":s===t}),children:null!=n?n:t}),t)}))})}function v(e){let{lazy:t,children:n,selectedValue:a}=e;const i=(Array.isArray(n)?n:[n]).filter(Boolean);if(t){const e=i.find((e=>e.props.value===a));return e?(0,s.cloneElement)(e,{className:(0,r.A)("margin-top--md",e.props.className)}):null}return(0,b.jsx)("div",{className:"margin-top--md",children:i.map(((e,t)=>(0,s.cloneElement)(e,{key:t,hidden:e.props.value!==a})))})}function k(e){const t=x(e);return(0,b.jsxs)("div",{className:(0,r.A)("tabs-container",g.tabList),children:[(0,b.jsx)(j,Object.assign({},t,e)),(0,b.jsx)(v,Object.assign({},t,e))]})}function _(e){const t=(0,f.A)();return(0,b.jsx)(k,Object.assign({},e,{children:u(e.children)}),String(t))}},8453:(e,t,n)=>{n.d(t,{R:()=>i,x:()=>l});var s=n(6540);const r={},a=s.createContext(r);function i(e){const t=s.useContext(a);return s.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function l(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(r):e.components||r:i(e.components),s.createElement(a.Provider,{value:t},e.children)}}}]);