"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[288],{1582:(e,s,i)=>{i.r(s),i.d(s,{assets:()=>o,contentTitle:()=>a,default:()=>x,frontMatter:()=>l,metadata:()=>n,toc:()=>h});const n=JSON.parse('{"id":"method/sets","title":"Sets","description":"MSPC classifies peaks under Background, Weak, Stringent,","source":"@site/docs/method/sets.md","sourceDirName":"method","slug":"/method/sets","permalink":"/MSPC/docs/method/sets","draft":false,"unlisted":false,"editUrl":"https://github.com/Genometric/MSPC/tree/dev/website/docs/method/sets.md","tags":[],"version":"current","frontMatter":{"title":"Sets"},"sidebar":"someSidebar","previous":{"title":"About","permalink":"/MSPC/docs/method/about"},"next":{"title":"Consensus peaks","permalink":"/MSPC/docs/method/consensus"}}');var r=i(4848),d=i(8453),t=i(9030),c=i(9324);const l={title:"Sets"},a=void 0,o={},h=[{value:"Background",id:"background",level:2},{value:"Weak",id:"weak",level:2},{value:"Stringent",id:"stringent",level:2},{value:"Confirmed",id:"confirmed",level:2},{value:"Discarded",id:"discarded",level:2},{value:"TruePositive",id:"truepositive",level:2},{value:"FalsePositive",id:"falsepositive",level:2},{value:"See Also",id:"see-also",level:2}];function p(e){const s={a:"a",code:"code",h2:"h2",li:"li",ol:"ol",p:"p",ul:"ul",...(0,d.R)(),...e.components};return(0,r.jsxs)(r.Fragment,{children:[(0,r.jsxs)(s.p,{children:["MSPC classifies peaks under ",(0,r.jsx)(s.code,{children:"Background"}),", ",(0,r.jsx)(s.code,{children:"Weak"}),", ",(0,r.jsx)(s.code,{children:"Stringent"}),",\n",(0,r.jsx)(s.code,{children:"Confirmed"}),", ",(0,r.jsx)(s.code,{children:"Discarded"}),", ",(0,r.jsx)(s.code,{children:"True-Positive"}),", and ",(0,r.jsx)(s.code,{children:"False-Positive"}),"\ncategories. (Refer to ",(0,r.jsx)(s.a,{href:"/MSPC/docs/method/about",children:"method description"}),"\nfor details.) The following figure is a schematic view of\nthese categories and their relation."]}),"\n",(0,r.jsx)(c.A,{alt:"Sets",sources:{light:(0,t.Ay)("/img/sets.svg"),dark:(0,t.Ay)("/img/sets_dark.svg")}}),"\n",(0,r.jsxs)(s.p,{children:["(The number of ERs in different sets reported in this figure, are the\nresult of ",(0,r.jsx)(s.code,{children:"wgEncodeSydhTfbsK562CmycStdAlnRep1"})," and\n",(0,r.jsx)(s.code,{children:"wgEncodeSydhTfbsK562CmycStdAlnRep2"})," (available for download from\n",(0,r.jsx)(s.a,{href:"../sample_data",children:"Sample Data page"}),") comparative analysis using\n",(0,r.jsx)(s.code,{children:"-r bio -w 1e-4 -s 1e-8"})," parameters.)"]}),"\n",(0,r.jsx)(s.h2,{id:"background",children:"Background"}),"\n",(0,r.jsxs)(s.p,{children:["Peaks with ",(0,r.jsx)(s.code,{children:"p-value >= "}),(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#weak-threshold",children:(0,r.jsx)(s.code,{children:"weak threshold"})}),"."]}),"\n",(0,r.jsx)(s.h2,{id:"weak",children:"Weak"}),"\n",(0,r.jsxs)(s.p,{children:["Peaks with ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#stringency-threshold",children:(0,r.jsx)(s.code,{children:"stringency threshold"})}),(0,r.jsx)(s.code,{children:"<= p-value <"}),(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#weak-threshold",children:(0,r.jsx)(s.code,{children:"weak threshold"})}),"."]}),"\n",(0,r.jsx)(s.h2,{id:"stringent",children:"Stringent"}),"\n",(0,r.jsxs)(s.p,{children:["Peaks with ",(0,r.jsx)(s.code,{children:"p-value < "}),(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#stringency-threshold",children:(0,r.jsx)(s.code,{children:"stringency threshold"})}),"."]}),"\n",(0,r.jsx)(s.h2,{id:"confirmed",children:"Confirmed"}),"\n",(0,r.jsx)(s.p,{children:"Peaks that:"}),"\n",(0,r.jsxs)(s.ol,{children:["\n",(0,r.jsxs)(s.li,{children:["are supported by at least ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#c",children:(0,r.jsx)(s.code,{children:"c"})})," peaks from replicates, and"]}),"\n",(0,r.jsxs)(s.li,{children:["their combined stringency (xSquared) satisfies the ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#gamma",children:"given threshold"}),":\n",(0,r.jsx)(s.code,{children:"xSquared >= the inverse of the right-tailed probability of Gamma"})," and"]}),"\n",(0,r.jsxs)(s.li,{children:["if ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#replicate-type",children:"technical replicate"}),", passed all the\ntests, and if ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#replicate-type",children:"biological replicate"}),",\npassed at least one test."]}),"\n"]}),"\n",(0,r.jsxs)(s.p,{children:["(see ",(0,r.jsx)(s.a,{href:"/MSPC/docs/method/about",children:"method description"}),")"]}),"\n",(0,r.jsx)(s.h2,{id:"discarded",children:"Discarded"}),"\n",(0,r.jsx)(s.p,{children:"Peaks that:"}),"\n",(0,r.jsxs)(s.ol,{children:["\n",(0,r.jsxs)(s.li,{children:["does not have minimum required (i.e., ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#c",children:(0,r.jsx)(s.code,{children:"c"})}),") supporting evidence, or"]}),"\n",(0,r.jsxs)(s.li,{children:["their combined stringency (xSquared) does not satisfy the ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#gamma",children:"given threshold"}),", or"]}),"\n",(0,r.jsxs)(s.li,{children:["if ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#replicate-type",children:"technical replicate"}),", failed a test."]}),"\n"]}),"\n",(0,r.jsx)(s.h2,{id:"truepositive",children:"TruePositive"}),"\n",(0,r.jsxs)(s.p,{children:["The confirmed peaks that pass the Benjamini-Hochberg multiple\ntesting correction at level ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#alpha",children:(0,r.jsx)(s.code,{children:"alpha"})}),"."]}),"\n",(0,r.jsx)(s.h2,{id:"falsepositive",children:"FalsePositive"}),"\n",(0,r.jsxs)(s.p,{children:["The confirmed peaks that fail Benjamini-Hochberg multiple\ntesting correction at level ",(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/args#alpha",children:(0,r.jsx)(s.code,{children:"alpha"})}),"."]}),"\n",(0,r.jsx)(s.h2,{id:"see-also",children:"See Also"}),"\n",(0,r.jsxs)(s.ul,{children:["\n",(0,r.jsx)(s.li,{children:(0,r.jsx)(s.a,{href:"/MSPC/docs/method/about",children:"Method description"})}),"\n",(0,r.jsx)(s.li,{children:(0,r.jsx)(s.a,{href:"/MSPC/docs/cli/output",children:"CLI output"})}),"\n"]})]})}function x(e={}){const{wrapper:s}={...(0,d.R)(),...e.components};return s?(0,r.jsx)(s,{...e,children:(0,r.jsx)(p,{...e})}):p(e)}},8453:(e,s,i)=>{i.d(s,{R:()=>t,x:()=>c});var n=i(6540);const r={},d=n.createContext(r);function t(e){const s=n.useContext(d);return n.useMemo((function(){return"function"==typeof e?e(s):{...s,...e}}),[s,e])}function c(e){let s;return s=e.disableParentContext?"function"==typeof e.components?e.components(r):e.components||r:t(e.components),n.createElement(d.Provider,{value:s},e.children)}}}]);