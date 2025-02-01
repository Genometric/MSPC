"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[905],{5837:(e,i,s)=>{s.r(i),s.d(i,{assets:()=>d,contentTitle:()=>l,default:()=>m,frontMatter:()=>c,metadata:()=>n,toc:()=>h});const n=JSON.parse('{"id":"method/about","title":"About","description":"The analysis of ChIP-seq samples outputs a number of enriched regions","source":"@site/docs/method/about.md","sourceDirName":"method","slug":"/method/about","permalink":"/MSPC/docs/method/about","draft":false,"unlisted":false,"editUrl":"https://github.com/Genometric/MSPC/tree/dev/website/docs/method/about.md","tags":[],"version":"current","frontMatter":{"title":"About"},"sidebar":"someSidebar","previous":{"title":"Sample Data","permalink":"/MSPC/docs/sample_data"},"next":{"title":"Sets","permalink":"/MSPC/docs/method/sets"}}');var a=s(4848),r=s(8453),t=s(9030),o=s(9324);const c={title:"About"},l=void 0,d={},h=[{value:"Remarks",id:"remarks",level:2}];function p(e){const i={a:"a",code:"code",em:"em",h2:"h2",li:"li",p:"p",ul:"ul",...(0,r.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(i.p,{children:'The analysis of ChIP-seq samples outputs a number of enriched regions\n(commonly known as "peaks"), each indicating a protein-DNA interaction\nor a specific chromatin modification.'}),"\n",(0,a.jsx)(i.p,{children:"When replicate samples are analyzed, overlapping peaks are expected.\nThis repeated evidence can therefore be used to locally lower the\nminimum significance required to accept a peak. MSPC is a method for\njoint analysis of peaks."}),"\n",(0,a.jsx)(i.p,{children:'Given a set of peaks from (biological or technical) replicates,\nMSPC combines the p-values of overlapping enriched regions, and allows\nthe "rescue" of weak peaks occurring in more than one replicate.'}),"\n",(0,a.jsx)(i.h2,{id:"remarks",children:"Remarks"}),"\n",(0,a.jsxs)(i.p,{children:['MSPC rigorously combines the statistical significance of the overlapping\nenriched regions (ER), in order to "rescue" weak peaks, which would probably\nbe discarded in a single-sample analysis, when their combined evidence\nacross multiple samples is sufficiently strong.\n[',(0,a.jsx)(i.a,{href:"https://doi.org/10.1093/bioinformatics/btv293",children:"Ref"}),"]"]}),"\n",(0,a.jsx)(i.p,{children:'MSPC takes as input, for each replicate, a list of ERs and a measure of\ntheir individual significance in terms of a p-value.\nStarting from a permissive call, it divides the initial ERs in "stringent"\n(highly significant) and "weak" (moderately significant), and it assesses\nthe presence of overlapping enriched regions across multiple replicates.\nNon-overlapping regions can be penalized or discarded according to specific\nneeds.'}),"\n",(0,a.jsxs)(i.p,{children:["The significance of the overlapping regions is rigorously combined with the\n",(0,a.jsx)(i.a,{href:"https://en.wikipedia.org/wiki/Fisher%27s_method",children:"Fisher's method"})," to obtain\na global score. This score is assessed against an adjustable threshold on\nthe combined evidence, and peaks in each replicate are either confirmed or\ndiscarded."]}),"\n",(0,a.jsxs)(i.p,{children:["Finally, in order to account for multiple testing correction, MSPC applies the\n",(0,a.jsx)(i.a,{href:"https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini%E2%80%93Hochberg_procedure",children:"Benjamini-Hochberg procedure"}),",\nand outputs ERs with false-discovery rate smaller than an adjustable threshold."]}),"\n",(0,a.jsx)(i.p,{children:"This flow is captured in the following flowchart for each ER of each replicate:"}),"\n",(0,a.jsx)(o.A,{alt:"Simplified Flow Chart",sources:{light:(0,t.Ay)("/img/simplified_flow_chart.svg"),dark:(0,t.Ay)("/img/simplified_flow_chart_dark.svg")}}),"\n",(0,a.jsxs)(i.p,{children:["(This flowchart is a simplified version of the flowchart available in\n",(0,a.jsx)(i.a,{href:"https://doi.org/10.1093/bioinformatics/btv293",children:"MSPC's manuscript"}),".)"]}),"\n",(0,a.jsx)(i.p,{children:"In other words:"}),"\n",(0,a.jsxs)(i.ul,{children:["\n",(0,a.jsxs)(i.li,{children:["for each replicate, MSPC classifies input ERs as ",(0,a.jsx)(i.em,{children:"stringent"}),", ",(0,a.jsx)(i.em,{children:"weak"}),",\nand ",(0,a.jsx)(i.em,{children:"background"}),", based on their p-value;"]}),"\n",(0,a.jsxs)(i.li,{children:["performs a comparative analysis, and based on the combined stringency test,\nit classifies ",(0,a.jsx)(i.em,{children:"stringent"})," and ",(0,a.jsx)(i.em,{children:"weak"})," ERs as ",(0,a.jsx)(i.em,{children:"confirmed"})," or ",(0,a.jsx)(i.em,{children:"discarded"}),";"]}),"\n",(0,a.jsxs)(i.li,{children:["based on false-discovery rate, it classifies ",(0,a.jsx)(i.em,{children:"confirmed"})," ERs as\n",(0,a.jsx)(i.em,{children:"true-positive"})," or ",(0,a.jsx)(i.em,{children:"false-positive"}),"."]}),"\n"]}),"\n",(0,a.jsx)(i.p,{children:"The following figure is a schematic view of this procedure."}),"\n",(0,a.jsx)(o.A,{alt:"Sets",sources:{light:(0,t.Ay)("/img/sets.svg"),dark:(0,t.Ay)("/img/sets_dark.svg")}}),"\n",(0,a.jsxs)(i.p,{children:["(The number of ERs in different sets reported in this figure, are the\nresult of ",(0,a.jsx)(i.code,{children:"wgEncodeSydhTfbsK562CmycStdAlnRep1"})," and\n",(0,a.jsx)(i.code,{children:"wgEncodeSydhTfbsK562CmycStdAlnRep2"})," (available for download from\n",(0,a.jsx)(i.a,{href:"../sample_data",children:"Sample Data page"}),") comparative analysis using\n",(0,a.jsx)(i.code,{children:"-r bio -w 1e-4 -s 1e-8"})," parameters.)"]})]})}function m(e={}){const{wrapper:i}={...(0,r.R)(),...e.components};return i?(0,a.jsx)(i,{...e,children:(0,a.jsx)(p,{...e})}):p(e)}},8453:(e,i,s)=>{s.d(i,{R:()=>t,x:()=>o});var n=s(6540);const a={},r=n.createContext(a);function t(e){const i=n.useContext(r);return n.useMemo((function(){return"function"==typeof e?e(i):{...i,...e}}),[i,e])}function o(e){let i;return i=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:t(e.components),n.createElement(r.Provider,{value:i},e.children)}}}]);