"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[217],{3905:function(e,t,n){n.d(t,{Zo:function(){return c},kt:function(){return d}});var a=n(7294);function r(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function l(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,a)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?l(Object(n),!0).forEach((function(t){r(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):l(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function i(e,t){if(null==e)return{};var n,a,r=function(e,t){if(null==e)return{};var n,a,r={},l=Object.keys(e);for(a=0;a<l.length;a++)n=l[a],t.indexOf(n)>=0||(r[n]=e[n]);return r}(e,t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(e);for(a=0;a<l.length;a++)n=l[a],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(r[n]=e[n])}return r}var s=a.createContext({}),u=function(e){var t=a.useContext(s),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},c=function(e){var t=u(e.components);return a.createElement(s.Provider,{value:t},e.children)},p={inlineCode:"code",wrapper:function(e){var t=e.children;return a.createElement(a.Fragment,{},t)}},m=a.forwardRef((function(e,t){var n=e.components,r=e.mdxType,l=e.originalType,s=e.parentName,c=i(e,["components","mdxType","originalType","parentName"]),m=u(n),d=r,f=m["".concat(s,".").concat(d)]||m[d]||p[d]||l;return n?a.createElement(f,o(o({ref:t},c),{},{components:n})):a.createElement(f,o({ref:t},c))}));function d(e,t){var n=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var l=n.length,o=new Array(l);o[0]=m;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i.mdxType="string"==typeof e?e:r,o[1]=i;for(var u=2;u<l;u++)o[u]=n[u];return a.createElement.apply(null,o)}return a.createElement.apply(null,n)}m.displayName="MDXCreateElement"},5162:function(e,t,n){n.d(t,{Z:function(){return o}});var a=n(7294),r=n(6010),l="tabItem_Ymn6";function o(e){var t=e.children,n=e.hidden,o=e.className;return a.createElement("div",{role:"tabpanel",className:(0,r.Z)(l,o),hidden:n},t)}},5488:function(e,t,n){n.d(t,{Z:function(){return d}});var a=n(7462),r=n(7294),l=n(6010),o=n(2389),i=n(7392),s=n(7094),u=n(2466),c="tabList__CuJ",p="tabItem_LNqP";function m(e){var t,n,o=e.lazy,m=e.block,d=e.defaultValue,f=e.values,h=e.groupId,b=e.className,v=r.Children.map(e.children,(function(e){if((0,r.isValidElement)(e)&&"value"in e.props)return e;throw new Error("Docusaurus error: Bad <Tabs> child <"+("string"==typeof e.type?e.type:e.type.name)+'>: all children of the <Tabs> component should be <TabItem>, and every <TabItem> should have a unique "value" prop.')})),g=null!=f?f:v.map((function(e){var t=e.props;return{value:t.value,label:t.label,attributes:t.attributes}})),k=(0,i.l)(g,(function(e,t){return e.value===t.value}));if(k.length>0)throw new Error('Docusaurus error: Duplicate values "'+k.map((function(e){return e.value})).join(", ")+'" found in <Tabs>. Every value needs to be unique.');var w=null===d?d:null!=(t=null!=d?d:null==(n=v.find((function(e){return e.props.default})))?void 0:n.props.value)?t:v[0].props.value;if(null!==w&&!g.some((function(e){return e.value===w})))throw new Error('Docusaurus error: The <Tabs> has a defaultValue "'+w+'" but none of its children has the corresponding value. Available values are: '+g.map((function(e){return e.value})).join(", ")+". If you intend to show no default tab, use defaultValue={null} instead.");var y=(0,s.U)(),N=y.tabGroupChoices,C=y.setTabGroupChoices,O=(0,r.useState)(w),P=O[0],S=O[1],T=[],E=(0,u.o5)().blockElementScrollPositionUntilNextRender;if(null!=h){var x=N[h];null!=x&&x!==P&&g.some((function(e){return e.value===x}))&&S(x)}var M=function(e){var t=e.currentTarget,n=T.indexOf(t),a=g[n].value;a!==P&&(E(t),S(a),null!=h&&C(h,String(a)))},I=function(e){var t,n=null;switch(e.key){case"ArrowRight":var a,r=T.indexOf(e.currentTarget)+1;n=null!=(a=T[r])?a:T[0];break;case"ArrowLeft":var l,o=T.indexOf(e.currentTarget)-1;n=null!=(l=T[o])?l:T[T.length-1]}null==(t=n)||t.focus()};return r.createElement("div",{className:(0,l.Z)("tabs-container",c)},r.createElement("ul",{role:"tablist","aria-orientation":"horizontal",className:(0,l.Z)("tabs",{"tabs--block":m},b)},g.map((function(e){var t=e.value,n=e.label,o=e.attributes;return r.createElement("li",(0,a.Z)({role:"tab",tabIndex:P===t?0:-1,"aria-selected":P===t,key:t,ref:function(e){return T.push(e)},onKeyDown:I,onFocus:M,onClick:M},o,{className:(0,l.Z)("tabs__item",p,null==o?void 0:o.className,{"tabs__item--active":P===t})}),null!=n?n:t)}))),o?(0,r.cloneElement)(v.filter((function(e){return e.props.value===P}))[0],{className:"margin-top--md"}):r.createElement("div",{className:"margin-top--md"},v.map((function(e,t){return(0,r.cloneElement)(e,{key:t,hidden:e.props.value!==P})}))))}function d(e){var t=(0,o.Z)();return r.createElement(m,(0,a.Z)({key:String(t)},e))}},9250:function(e,t,n){n.r(t),n.d(t,{assets:function(){return m},contentTitle:function(){return c},default:function(){return h},frontMatter:function(){return u},metadata:function(){return p},toc:function(){return d}});var a=n(7462),r=n(3366),l=(n(7294),n(3905)),o=n(5488),i=n(5162),s=["components"],u={title:"Installation"},c=void 0,p={unversionedId:"installation",id:"installation",title:"Installation",description:"MSPC can be used as command-line application, a C# library",source:"@site/docs/installation.md",sourceDirName:".",slug:"/installation",permalink:"/MSPC/docs/installation",draft:!1,editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/installation.md",tags:[],version:"current",frontMatter:{title:"Installation"},sidebar:"someSidebar",previous:{title:"Quick Start",permalink:"/MSPC/docs/quick_start"},next:{title:"Sample Data",permalink:"/MSPC/docs/sample_data"}},m={},d=[{value:"Method A: Framework Dependent",id:"method-a-framework-dependent",level:2},{value:"Install .NET 5.0",id:"install-net-50",level:3},{value:"Install MSPC",id:"install-mspc",level:3},{value:"Method B: Self-Contained",id:"method-b-self-contained",level:2}],f={toc:d};function h(e){var t=e.components,n=(0,r.Z)(e,s);return(0,l.kt)("wrapper",(0,a.Z)({},f,n,{components:t,mdxType:"MDXLayout"}),(0,l.kt)("p",null,"MSPC can be used as command-line application, a C# library\n(",(0,l.kt)("a",{parentName:"p",href:"https://www.nuget.org/packages/Genometric.MSPC.Core"},"distributed via nuget"),"),\nor an R package\n(",(0,l.kt)("a",{parentName:"p",href:"https://bioconductor.org/packages/release/bioc/html/rmspc.html"},"distributed via Bioconductor"),").\nThis page documents installing MSPC as a command-line application, for\ninstalling it as a C# library, please refer to ",(0,l.kt)("a",{parentName:"p",href:"library/install"},"this page"),",\nor ",(0,l.kt)("a",{parentName:"p",href:"https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html"},"Bioconductor user guide"),"\nfor installing/using it in R programming language."),(0,l.kt)("p",null,"A prerequisite for MSPC installation is ",(0,l.kt)("a",{parentName:"p",href:"https://dotnet.microsoft.com/download/dotnet/5.0"},".NET 5.0"),"\nor newer. We provide two methods for MSPC installation depending on whether\n.NET 5.0 is installed on your machine or you can install it,\nor .NET 5.0 is not installed and you cannot install it, respectively\n",(0,l.kt)("a",{parentName:"p",href:"#method-a"},"Method A")," or ",(0,l.kt)("a",{parentName:"p",href:"#method-b"},"Method B"),"."),(0,l.kt)("h2",{id:"method-a-framework-dependent"},"Method A: Framework Dependent"),(0,l.kt)("p",null,"First we check if .NET 5.0 is installed (not to be confused with .NET Framework), and install it if it is not, then we install MSPC."),(0,l.kt)("h3",{id:"install-net-50"},"Install .NET 5.0"),(0,l.kt)("p",null,"Open a command line shell (e.g., PowerShell) and run the following command:"),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-shell"},"$ dotnet --info\n.NET SDK (reflecting any global.json):\n Version:   5.0.102\n Commit:    71365b4d42\n\nRuntime Environment:\n OS Name:     Windows\n OS Version:  10.0.19042\n OS Platform: Windows\n RID:         win10-x64\n Base Path:   C:\\Program Files\\dotnet\\sdk\\5.0.102\\\n")),(0,l.kt)("p",null,"If the output is not as shown above, you would need to install\n.NET 5.0 (or newer) ",(0,l.kt)("a",{parentName:"p",href:"https://dotnet.microsoft.com/download/dotnet/5.0"},"following these instructions"),"."),(0,l.kt)("h3",{id:"install-mspc"},"Install MSPC"),(0,l.kt)("p",null,"You may install MSPC using either of the following methods:"),(0,l.kt)("ul",null,(0,l.kt)("li",{parentName:"ul"},(0,l.kt)("p",{parentName:"li"},"Goto ",(0,l.kt)("a",{parentName:"p",href:"https://github.com/Genometric/MSPC/releases/latest"},"this page")," and download ",(0,l.kt)("inlineCode",{parentName:"p"},"mspc.zip"),"\nand extract it to a path on your computer;")),(0,l.kt)("li",{parentName:"ul"},(0,l.kt)("p",{parentName:"li"},"Type the following command in your command line shell:"),(0,l.kt)("pre",{parentName:"li"},(0,l.kt)("code",{parentName:"pre",className:"language-shell"},'$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/mspc.zip"\n$ unzip mspc.zip -d mspc\n')))),(0,l.kt)("h2",{id:"method-b-self-contained"},"Method B: Self-Contained"),(0,l.kt)("p",null,"Install MSPC using either of the following commands depending on your runtime:"),(0,l.kt)(o.Z,{defaultValue:"win",values:[{label:"Windows x64",value:"win"},{label:"Linux x64",value:"linux"},{label:"macOS x64",value:"mac"}],mdxType:"Tabs"},(0,l.kt)(i.Z,{value:"win",mdxType:"TabItem"},(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre"},'$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip"\n$ unzip mspc.zip -d mspc\n'))),(0,l.kt)(i.Z,{value:"linux",mdxType:"TabItem"},(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre"},'$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip"\n$ unzip mspc.zip -d mspc\n'))),(0,l.kt)(i.Z,{value:"mac",mdxType:"TabItem"},(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre"},'$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip"\n$ unzip mspc.zip -d mspc\n')))))}h.isMDXComponent=!0}}]);