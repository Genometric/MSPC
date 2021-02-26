(window.webpackJsonp=window.webpackJsonp||[]).push([[9],{107:function(e,t,a){"use strict";var n=a(0),r=a(108);t.a=function(){var e=Object(n.useContext)(r.a);if(null==e)throw new Error("`useUserPreferencesContext` is used outside of `Layout` Component.");return e}},108:function(e,t,a){"use strict";var n=a(0),r=Object(n.createContext)(void 0);t.a=r},125:function(e,t,a){"use strict";var n=a(0),r=a.n(n),c=a(107),l=a(97),i=a(54),o=a.n(i),s=37,u=39;t.a=function(e){var t=e.lazy,a=e.block,i=e.children,b=e.defaultValue,p=e.values,m=e.groupId,d=e.className,O=Object(c.a)(),f=O.tabGroupChoices,j=O.setTabGroupChoices,g=Object(n.useState)(b),v=g[0],h=g[1];if(null!=m){var y=f[m];null!=y&&y!==v&&p.some((function(e){return e.value===y}))&&h(y)}var w=function(e){h(e),null!=m&&j(m,e)},x=[];return r.a.createElement("div",null,r.a.createElement("ul",{role:"tablist","aria-orientation":"horizontal",className:Object(l.a)("tabs",{"tabs--block":a},d)},p.map((function(e){var t=e.value,a=e.label;return r.a.createElement("li",{role:"tab",tabIndex:0,"aria-selected":v===t,className:Object(l.a)("tabs__item",o.a.tabItem,{"tabs__item--active":v===t}),key:t,ref:function(e){return x.push(e)},onKeyDown:function(e){!function(e,t,a){switch(a.keyCode){case u:!function(e,t){var a=e.indexOf(t)+1;e[a]?e[a].focus():e[0].focus()}(e,t);break;case s:!function(e,t){var a=e.indexOf(t)-1;e[a]?e[a].focus():e[e.length-1].focus()}(e,t)}}(x,e.target,e)},onFocus:function(){return w(t)},onClick:function(){w(t)}},a)}))),t?Object(n.cloneElement)(i.filter((function(e){return e.props.value===v}))[0],{className:"margin-vert--md"}):r.a.createElement("div",{className:"margin-vert--md"},i.map((function(e,t){return Object(n.cloneElement)(e,{key:t,hidden:e.props.value!==v})}))))}},126:function(e,t,a){"use strict";var n=a(3),r=a(0),c=a.n(r);t.a=function(e){var t=e.children,a=e.hidden,r=e.className;return c.a.createElement("div",Object(n.a)({role:"tabpanel"},{hidden:a,className:r}),t)}},79:function(e,t,a){"use strict";a.r(t),a.d(t,"frontMatter",(function(){return o})),a.d(t,"metadata",(function(){return s})),a.d(t,"rightToc",(function(){return u})),a.d(t,"default",(function(){return p}));var n=a(3),r=a(7),c=(a(0),a(96)),l=a(125),i=a(126),o={title:"Quick Start"},s={unversionedId:"quick_start",id:"quick_start",isDocsHomePage:!1,title:"Quick Start",description:"Preparation",source:"@site/docs/quick_start.md",slug:"/quick_start",permalink:"/MSPC/docs/quick_start",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/quick_start.md",version:"current",sidebar:"someSidebar",previous:{title:"Welcome",permalink:"/MSPC/docs/"},next:{title:"Installation",permalink:"/MSPC/docs/installation"}},u=[{value:"Preparation",id:"preparation",children:[]},{value:"Run",id:"run",children:[]},{value:"Output",id:"output",children:[]},{value:"See Also",id:"see-also",children:[]}],b={rightToc:u};function p(e){var t=e.components,a=Object(r.a)(e,["components"]);return Object(c.b)("wrapper",Object(n.a)({},b,a,{components:t,mdxType:"MDXLayout"}),Object(c.b)("h2",{id:"preparation"},"Preparation"),Object(c.b)("ol",null,Object(c.b)("li",{parentName:"ol"},"Install a self-contained release of MSPC, using either of following commands\ndepending on your runtime (see ",Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/installation"}),"installation")," page for detailed\ninstallation options):")),Object(c.b)(l.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(i.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip\n"))),Object(c.b)(i.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip\n"))),Object(c.b)(i.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip\n")))),Object(c.b)("ol",{start:2},Object(c.b)("li",{parentName:"ol"},"Extract the downloaded archive and browse to the containing directory:")),Object(c.b)(l.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(i.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip mspc.zip -d mspc; cd mspc\n"))),Object(c.b)(i.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip mspc.zip -d mspc && cd mspc\n"))),Object(c.b)(i.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip mspc.zip -d mspc && cd mspc\n")))),Object(c.b)("p",null,"   Notice that if you are working on Windows x64, you will need to download the program unzip."),Object(c.b)("ol",{start:3},Object(c.b)("li",{parentName:"ol"},"Download sample data:")),Object(c.b)(l.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(i.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .\n"))),Object(c.b)(i.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n"))),Object(c.b)(i.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n")))),Object(c.b)("h2",{id:"run"},"Run"),Object(c.b)("p",null,"To run MSPC use the following command depending on your runtime:"),Object(c.b)(l.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(i.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),".\\mspc.exe -i .\\rep1.bed -i .\\rep2.bed -r bio -w 1e-4 -s 1e-8\n"))),Object(c.b)(i.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"./mspc.dll -i .\\rep1.bed -i .\\rep2.bed -r bio -w 1e-4 -s 1e-8\n"))),Object(c.b)(i.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"./mspc.dll -i .\\rep1.bed -i .\\rep2.bed -r bio -w 1e-4 -s 1e-8\n")))),Object(c.b)("h2",{id:"output"},"Output"),Object(c.b)("p",null,"MSPC creates a folder in the current execution path named ",Object(c.b)("inlineCode",{parentName:"p"},"session_X_Y"),", where ",Object(c.b)("inlineCode",{parentName:"p"},"X")," and ",Object(c.b)("inlineCode",{parentName:"p"},"Y")," are execution date and time respectively, which contains the following files and folders:"),Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-bash"}),".\n\u2514\u2500\u2500 session_20191126_222131330\n    \u251c\u2500\u2500 ConsensusPeaks.bed\n    \u251c\u2500\u2500 ConsensusPeaks_mspc_peaks.txt\n    \u251c\u2500\u2500 EventsLog_20191126_2221313409928.txt\n    \u251c\u2500\u2500 rep1\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u2514\u2500\u2500 Weak.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Weak_mspc_peaks.txt\n    \u2514\u2500\u2500 rep2\n     \xa0\xa0 \u251c\u2500\u2500 Background.bed\n     \xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n     \xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n      \xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n     \xa0\xa0 \u2514\u2500\u2500 Weak.bed\n        \u2514\u2500\u2500 Weak.bed\n")),Object(c.b)("h2",{id:"see-also"},"See Also"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/"}),"Welcome page")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/cli/input"}),"Input file format")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/cli/output"}),"Output files"))))}p.isMDXComponent=!0},96:function(e,t,a){"use strict";a.d(t,"a",(function(){return b})),a.d(t,"b",(function(){return d}));var n=a(0),r=a.n(n);function c(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function l(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function i(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?l(Object(a),!0).forEach((function(t){c(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):l(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function o(e,t){if(null==e)return{};var a,n,r=function(e,t){if(null==e)return{};var a,n,r={},c=Object.keys(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(r[a]=e[a])}return r}var s=r.a.createContext({}),u=function(e){var t=r.a.useContext(s),a=t;return e&&(a="function"==typeof e?e(t):i(i({},t),e)),a},b=function(e){var t=u(e.components);return r.a.createElement(s.Provider,{value:t},e.children)},p={inlineCode:"code",wrapper:function(e){var t=e.children;return r.a.createElement(r.a.Fragment,{},t)}},m=r.a.forwardRef((function(e,t){var a=e.components,n=e.mdxType,c=e.originalType,l=e.parentName,s=o(e,["components","mdxType","originalType","parentName"]),b=u(a),m=n,d=b["".concat(l,".").concat(m)]||b[m]||p[m]||c;return a?r.a.createElement(d,i(i({ref:t},s),{},{components:a})):r.a.createElement(d,i({ref:t},s))}));function d(e,t){var a=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var c=a.length,l=new Array(c);l[0]=m;var i={};for(var o in t)hasOwnProperty.call(t,o)&&(i[o]=t[o]);i.originalType=e,i.mdxType="string"==typeof e?e:n,l[1]=i;for(var s=2;s<c;s++)l[s]=a[s];return r.a.createElement.apply(null,l)}return r.a.createElement.apply(null,a)}m.displayName="MDXCreateElement"},97:function(e,t,a){"use strict";function n(e){var t,a,r="";if("string"==typeof e||"number"==typeof e)r+=e;else if("object"==typeof e)if(Array.isArray(e))for(t=0;t<e.length;t++)e[t]&&(a=n(e[t]))&&(r&&(r+=" "),r+=a);else for(t in e)e[t]&&(r&&(r+=" "),r+=t);return r}t.a=function(){for(var e,t,a=0,r="";a<arguments.length;)(e=arguments[a++])&&(t=n(e))&&(r&&(r+=" "),r+=t);return r}}}]);