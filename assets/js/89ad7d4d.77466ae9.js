"use strict";(self.webpackChunkmspc=self.webpackChunkmspc||[]).push([[767],{3905:function(e,t,n){n.d(t,{Zo:function(){return u},kt:function(){return m}});var r=n(7294);function o(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function i(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){o(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function c(e,t){if(null==e)return{};var n,r,o=function(e,t){if(null==e)return{};var n,r,o={},a=Object.keys(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||(o[n]=e[n]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(o[n]=e[n])}return o}var l=r.createContext({}),s=function(e){var t=r.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):i(i({},t),e)),n},u=function(e){var t=s(e.components);return r.createElement(l.Provider,{value:t},e.children)},p={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},f=r.forwardRef((function(e,t){var n=e.components,o=e.mdxType,a=e.originalType,l=e.parentName,u=c(e,["components","mdxType","originalType","parentName"]),f=s(n),m=o,d=f["".concat(l,".").concat(m)]||f[m]||p[m]||a;return n?r.createElement(d,i(i({ref:t},u),{},{components:n})):r.createElement(d,i({ref:t},u))}));function m(e,t){var n=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=n.length,i=new Array(a);i[0]=f;var c={};for(var l in t)hasOwnProperty.call(t,l)&&(c[l]=t[l]);c.originalType=e,c.mdxType="string"==typeof e?e:o,i[1]=c;for(var s=2;s<a;s++)i[s]=n[s];return r.createElement.apply(null,i)}return r.createElement.apply(null,n)}f.displayName="MDXCreateElement"},6425:function(e,t,n){n.r(t),n.d(t,{frontMatter:function(){return c},contentTitle:function(){return l},metadata:function(){return s},toc:function(){return u},default:function(){return f}});var r=n(7462),o=n(3366),a=(n(7294),n(3905)),i=["components"],c={title:"Install"},l=void 0,s={unversionedId:"library/install",id:"library/install",isDocsHomePage:!1,title:"Install",description:"MSPC Core is published on NuGet,",source:"@site/docs/library/install.md",sourceDirName:"library",slug:"/library/install",permalink:"/MSPC/docs/library/install",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/library/install.md",version:"current",frontMatter:{title:"Install"},sidebar:"someSidebar",previous:{title:"Parser Configuration",permalink:"/MSPC/docs/cli/parser"},next:{title:"Basic Invocation",permalink:"/MSPC/docs/library/basic_invocation"}},u=[],p={toc:u};function f(e){var t=e.components,n=(0,o.Z)(e,i);return(0,a.kt)("wrapper",(0,r.Z)({},p,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("p",null,"MSPC ",(0,a.kt)("inlineCode",{parentName:"p"},"Core")," is published on ",(0,a.kt)("a",{parentName:"p",href:"https://www.nuget.org/packages/Genometric.MSPC.Core/"},"NuGet"),",\nand can be added to your .NET project using any of the following methods: "),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-shell"},"# .NET CLI\n> dotnet add package Genometric.MSPC.Core --version 3.0.0\n\n# Package Manager\nPM> Install-Package Genometric.MSPC.Core -Version 3.0.0\n")),(0,a.kt)("p",null,"See how to install a NuGet package in Visual Studio on ",(0,a.kt)("a",{parentName:"p",href:"https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui"},"Windows"),"\nor ",(0,a.kt)("a",{parentName:"p",href:"https://docs.microsoft.com/en-us/visualstudio/mac/nuget-walkthrough"},"Mac"),",\nor ",(0,a.kt)("a",{parentName:"p",href:"https://docs.microsoft.com/en-us/nuget/consume-packages/ways-to-install-a-package"},"different ways to install a NuGet package"),"."))}f.isMDXComponent=!0}}]);