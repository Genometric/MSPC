(window.webpackJsonp=window.webpackJsonp||[]).push([[9],{104:function(e,t,a){"use strict";var n=a(0),r=a(105);t.a=function(){var e=Object(n.useContext)(r.a);if(null==e)throw new Error("`useUserPreferencesContext` is used outside of `Layout` Component.");return e}},105:function(e,t,a){"use strict";var n=a(0),r=Object(n.createContext)(void 0);t.a=r},113:function(e,t,a){"use strict";var n=a(0),r=a.n(n),c=a(104),i=a(97),o=a(54),l=a.n(o),s=37,b=39;t.a=function(e){var t=e.lazy,a=e.block,o=e.children,p=e.defaultValue,u=e.values,m=e.groupId,d=e.className,O=Object(c.a)(),j=O.tabGroupChoices,f=O.setTabGroupChoices,v=Object(n.useState)(p),g=v[0],h=v[1];if(null!=m){var x=j[m];null!=x&&x!==g&&u.some((function(e){return e.value===x}))&&h(x)}var y=function(e){h(e),null!=m&&f(m,e)},w=[];return r.a.createElement("div",null,r.a.createElement("ul",{role:"tablist","aria-orientation":"horizontal",className:Object(i.a)("tabs",{"tabs--block":a},d)},u.map((function(e){var t=e.value,a=e.label;return r.a.createElement("li",{role:"tab",tabIndex:0,"aria-selected":g===t,className:Object(i.a)("tabs__item",l.a.tabItem,{"tabs__item--active":g===t}),key:t,ref:function(e){return w.push(e)},onKeyDown:function(e){!function(e,t,a){switch(a.keyCode){case b:!function(e,t){var a=e.indexOf(t)+1;e[a]?e[a].focus():e[0].focus()}(e,t);break;case s:!function(e,t){var a=e.indexOf(t)-1;e[a]?e[a].focus():e[e.length-1].focus()}(e,t)}}(w,e.target,e)},onFocus:function(){return y(t)},onClick:function(){y(t)}},a)}))),t?Object(n.cloneElement)(o.filter((function(e){return e.props.value===g}))[0],{className:"margin-vert--md"}):r.a.createElement("div",{className:"margin-vert--md"},o.map((function(e,t){return Object(n.cloneElement)(e,{key:t,hidden:e.props.value!==g})}))))}},114:function(e,t,a){"use strict";var n=a(3),r=a(0),c=a.n(r);t.a=function(e){var t=e.children,a=e.hidden,r=e.className;return c.a.createElement("div",Object(n.a)({role:"tabpanel"},{hidden:a,className:r}),t)}},79:function(e,t,a){"use strict";a.r(t),a.d(t,"frontMatter",(function(){return l})),a.d(t,"metadata",(function(){return s})),a.d(t,"rightToc",(function(){return b})),a.d(t,"default",(function(){return u}));var n=a(3),r=a(7),c=(a(0),a(96)),i=a(113),o=a(114),l={title:"Quick Start"},s={unversionedId:"quick_start",id:"quick_start",isDocsHomePage:!1,title:"Quick Start",description:"Preparation",source:"@site/docs/quick_start.md",slug:"/quick_start",permalink:"/MSPC/docs/quick_start",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/quick_start.md",version:"current",sidebar:"someSidebar",previous:{title:"Welcome",permalink:"/MSPC/docs/"},next:{title:"Installation",permalink:"/MSPC/docs/installation"}},b=[{value:"Preparation",id:"preparation",children:[]},{value:"Run",id:"run",children:[]},{value:"Output",id:"output",children:[]},{value:"See Also",id:"see-also",children:[]}],p={rightToc:b};function u(e){var t=e.components,a=Object(r.a)(e,["components"]);return Object(c.b)("wrapper",Object(n.a)({},p,a,{components:t,mdxType:"MDXLayout"}),Object(c.b)("h2",{id:"preparation"},"Preparation"),Object(c.b)("ol",null,Object(c.b)("li",{parentName:"ol"},"Install a self-contained release of MSPC, using either of following commands\ndepending on your runtime (see ",Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/installation"}),"installation")," page for detailed\ninstallation options):")),Object(c.b)(i.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(o.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip\n"))),Object(c.b)(o.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip\n"))),Object(c.b)(o.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip\n")))),Object(c.b)("ol",{start:2},Object(c.b)("li",{parentName:"ol"},"Extract the downloaded archive and browse to the containing directory:")),Object(c.b)(i.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(o.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip win-x64.zip -d mspc; cd mspc\n"))),Object(c.b)(o.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip -q linux-x64.zip -d mspc && cd mspc && chmod +x mspc\n"))),Object(c.b)(o.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"unzip -q osx-x64.zip -d mspc && cd mspc && chmod +x mspc\n")))),Object(c.b)("p",null,"   Notice that if you are working on Windows x64, you will need to download the program unzip."),Object(c.b)("ol",{start:3},Object(c.b)("li",{parentName:"ol"},"Download sample data:")),Object(c.b)(i.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(o.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .\n"))),Object(c.b)(o.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n"))),Object(c.b)(o.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .\n")))),Object(c.b)("h2",{id:"run"},"Run"),Object(c.b)("p",null,"To run MSPC use the following command depending on your runtime:"),Object(c.b)(i.a,{groupId:"operating-systems",defaultValue:"win",values:[{label:"Windows",value:"win"},{label:"Linux",value:"linux"},{label:"macOS",value:"mac"}],mdxType:"Tabs"},Object(c.b)(o.a,{value:"win",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),".\\mspc.exe -i .\\rep*.bed -r bio -w 1e-4 -s 1e-8\n"))),Object(c.b)(o.a,{value:"linux",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n"))),Object(c.b)(o.a,{value:"mac",mdxType:"TabItem"},Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-shell"}),"./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8\n")))),Object(c.b)("h2",{id:"output"},"Output"),Object(c.b)("p",null,"MSPC creates a folder in the current execution path named ",Object(c.b)("inlineCode",{parentName:"p"},"session_X_Y"),",\nwhere ",Object(c.b)("inlineCode",{parentName:"p"},"X")," and ",Object(c.b)("inlineCode",{parentName:"p"},"Y")," are execution date and time respectively, which contains\nthe following files and folders:"),Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-bash"}),".\n\u2514\u2500\u2500 session_20191126_222131330\n    \u251c\u2500\u2500 ConsensusPeaks.bed\n    \u251c\u2500\u2500 ConsensusPeaks_mspc_peaks.txt\n    \u251c\u2500\u2500 EventsLog_20191126_2221313409928.txt\n    \u251c\u2500\u2500 rep1\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n    \u2502\xa0\xa0 \u2514\u2500\u2500 Weak.bed\n    \u2502\xa0\xa0 \u251c\u2500\u2500 Weak_mspc_peaks.txt\n    \u2514\u2500\u2500 rep2\n     \xa0\xa0 \u251c\u2500\u2500 Background.bed\n     \xa0\xa0 \u251c\u2500\u2500 Background_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed.bed\n     \xa0\xa0 \u251c\u2500\u2500 Confirmed_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Discarded.bed\n     \xa0\xa0 \u251c\u2500\u2500 Discarded_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 FalsePositive_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 Stringent.bed\n      \xa0 \u251c\u2500\u2500 Stringent_mspc_peaks.txt\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive.bed\n     \xa0\xa0 \u251c\u2500\u2500 TruePositive_mspc_peaks.txt\n     \xa0\xa0 \u2514\u2500\u2500 Weak.bed\n        \u2514\u2500\u2500 Weak_mspc_peaks.txt\n")),Object(c.b)("div",{className:"admonition admonition-info alert alert--info"},Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(n.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(n.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(n.a)({parentName:"svg"},{fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"})))),"info")),Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"Note that in these instructions you download a\n",Object(c.b)("a",Object(n.a)({parentName:"p"},{href:"https://docs.microsoft.com/en-us/dotnet/core/deploying/#publish-self-contained"}),"self-contained deployment"),"\nof MSPC, hence you do ",Object(c.b)("strong",{parentName:"p"},"not")," need to install .NET (or\nany other dependencies) for the program to execute. "),Object(c.b)("p",{parentName:"div"},"For other installation options see the ",Object(c.b)("a",Object(n.a)({parentName:"p"},{href:"installation"}),"installation page"),".   "))),Object(c.b)("h2",{id:"see-also"},"See Also"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/"}),"Welcome page")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/cli/input"}),"Input file format")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/cli/output"}),"Output files")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("a",Object(n.a)({parentName:"li"},{href:"/MSPC/docs/method/sets"}),"Sets"))))}u.isMDXComponent=!0},96:function(e,t,a){"use strict";a.d(t,"a",(function(){return p})),a.d(t,"b",(function(){return d}));var n=a(0),r=a.n(n);function c(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function i(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function o(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?i(Object(a),!0).forEach((function(t){c(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):i(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function l(e,t){if(null==e)return{};var a,n,r=function(e,t){if(null==e)return{};var a,n,r={},c=Object.keys(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(r[a]=e[a])}return r}var s=r.a.createContext({}),b=function(e){var t=r.a.useContext(s),a=t;return e&&(a="function"==typeof e?e(t):o(o({},t),e)),a},p=function(e){var t=b(e.components);return r.a.createElement(s.Provider,{value:t},e.children)},u={inlineCode:"code",wrapper:function(e){var t=e.children;return r.a.createElement(r.a.Fragment,{},t)}},m=r.a.forwardRef((function(e,t){var a=e.components,n=e.mdxType,c=e.originalType,i=e.parentName,s=l(e,["components","mdxType","originalType","parentName"]),p=b(a),m=n,d=p["".concat(i,".").concat(m)]||p[m]||u[m]||c;return a?r.a.createElement(d,o(o({ref:t},s),{},{components:a})):r.a.createElement(d,o({ref:t},s))}));function d(e,t){var a=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var c=a.length,i=new Array(c);i[0]=m;var o={};for(var l in t)hasOwnProperty.call(t,l)&&(o[l]=t[l]);o.originalType=e,o.mdxType="string"==typeof e?e:n,i[1]=o;for(var s=2;s<c;s++)i[s]=a[s];return r.a.createElement.apply(null,i)}return r.a.createElement.apply(null,a)}m.displayName="MDXCreateElement"},97:function(e,t,a){"use strict";function n(e){var t,a,r="";if("string"==typeof e||"number"==typeof e)r+=e;else if("object"==typeof e)if(Array.isArray(e))for(t=0;t<e.length;t++)e[t]&&(a=n(e[t]))&&(r&&(r+=" "),r+=a);else for(t in e)e[t]&&(r&&(r+=" "),r+=t);return r}t.a=function(){for(var e,t,a=0,r="";a<arguments.length;)(e=arguments[a++])&&(t=n(e))&&(r&&(r+=" "),r+=t);return r}}}]);