(window.webpackJsonp=window.webpackJsonp||[]).push([[18],{89:function(e,n,t){"use strict";t.r(n),t.d(n,"frontMatter",(function(){return c})),t.d(n,"metadata",(function(){return s})),t.d(n,"rightToc",(function(){return i})),t.d(n,"default",(function(){return u}));var r=t(3),a=t(7),o=(t(0),t(95)),c={title:"(A)synchronous execution"},s={unversionedId:"library/asynch_exe",id:"library/asynch_exe",isDocsHomePage:!1,title:"(A)synchronous execution",description:"The MSPC Core offers synchronous and asynchronous analysis",source:"@site/docs/library/asynch_exe.md",slug:"/library/asynch_exe",permalink:"/MSPC/docs/library/asynch_exe",editUrl:"https://github.com/Genometric/MSPC/tree/dev/website/docs/library/asynch_exe.md",version:"current",sidebar:"someSidebar",previous:{title:"Basic Invocation",permalink:"/MSPC/docs/library/basic_invocation"}},i=[{value:"Synchronous Execution",id:"synchronous-execution",children:[]},{value:"Asynchronous Execution",id:"asynchronous-execution",children:[]}],l={rightToc:i};function u(e){var n=e.components,t=Object(a.a)(e,["components"]);return Object(o.b)("wrapper",Object(r.a)({},l,t,{components:n,mdxType:"MDXLayout"}),Object(o.b)("p",null,"The MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," offers synchronous and asynchronous analysis\nof datasets. In other words: "),Object(o.b)("ul",null,Object(o.b)("li",{parentName:"ul"},Object(o.b)("p",{parentName:"li"},"If executed synchronously, the caller is blocked until the\nanalysis has concluded;")),Object(o.b)("li",{parentName:"ul"},Object(o.b)("p",{parentName:"li"},"If executed asynchronously, the caller is not blocked and can continue executing other logics, meanwhile, MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," reports\nthe execution status and signals when the analysis has concluded. "))),Object(o.b)("blockquote",null,Object(o.b)("p",{parentName:"blockquote"},"Note that regardless of analysis invocation method, MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core"),"\nparallelizes the analysis of datasets (multi-threaded) whose\ndegree-of-parallelism can be adjusted.")),Object(o.b)("p",null,"Both methods are discussed in details in the following sections. "),Object(o.b)("h2",{id:"synchronous-execution"},"Synchronous Execution"),Object(o.b)("p",null,"A synchronous execution is the common function invocation, where\nthe command after function call is not executed until the called\nfunction returns. For instance: "),Object(o.b)("pre",null,Object(o.b)("code",Object(r.a)({parentName:"pre"},{className:"language-csharp"}),"var y = Func(x); // Blocked here until Func(x) returns\nvar z = y * 2;   // Not executed until Func(x) returns\n")),Object(o.b)("p",null,"The MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," can analyze datasets ",Object(o.b)("em",{parentName:"p"},"synchronously"),", which\ncan be invoked as the following:"),Object(o.b)("pre",null,Object(o.b)("code",Object(r.a)({parentName:"pre"},{className:"language-csharp"}),"using Genometric.MSPC.Core;\n\n// Setup:\nvar mspc = new Mspc();\nforeach (var sample in samples) // where `samples` is a list of parsed input datasets.\n    mspc.AddSample(sample.FileHashKey, sample);\n\n// Method 1:\nvar results = mspc.Run(options);\n\n// Method 2:\nmspc.Run(options);\nvar results = mspc.GetResults();\n")),Object(o.b)("p",null,"A synchronous execution is easier to implement and invoke;\nhowever, it renders the program irresponsive during the function\nexecution, which can be disadvantageous for long-running\nanalysis. Therefore, MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," also implements ",Object(o.b)("em",{parentName:"p"},"asynchronous"),"\nexecution of the analysis method."),Object(o.b)("h2",{id:"asynchronous-execution"},"Asynchronous Execution"),Object(o.b)("p",null,"Long-running functions are commonly executed asynchronously,\nwhich keeps the (graphical or command line) interface responsive,\nand allows executing other logics (e.g., show elapsed time)\nwhile the long-running function is being executed. For instance: "),Object(o.b)("pre",null,Object(o.b)("code",Object(r.a)({parentName:"pre"},{className:"language-csharp"}),"int y = AsynchFunc(x); // function invoked and execution is \n                       // continued to the next line regardless \n                       // of the function has returned or not.\nvar z = y * 2;         // executed even if `AsynchFunc` is busy, \n                       // and uses default value of `y`, which is `0`. \n")),Object(o.b)("p",null,"The MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," can analyze datasets ",Object(o.b)("em",{parentName:"p"},"asynchronously"),", which can\nbe used as the following in its basic form:"),Object(o.b)("pre",null,Object(o.b)("code",Object(r.a)({parentName:"pre"},{className:"language-csharp"}),"using Genometric.MSPC.Core;\n\n// Setup:\nvar mspc = new Mspc();\nforeach (var sample in samples) // where `samples` is a list of parsed input datasets.\n    mspc.AddSample(sample.FileHashKey, sample);\n\n\n// Invoke MSPC `Core` asynchronously\nmspc.RunAsync(options);\n// ...\n// Here implement any logic to be executed while MSPC Core is running\n// ...\n\n// Wait for MSPC's signal on analysis completion\nmspc.Done.WaitOne();\n\n// Get results\nvar results = mspc.GetResults();\n")),Object(o.b)("p",null,"While MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core")," is running asynchronously, it reports\nits progress, which you can display it as the following:"),Object(o.b)("pre",null,Object(o.b)("code",Object(r.a)({parentName:"pre"},{className:"language-csharp"}),'void Run()\n{\n    var mspc = new Mspc();\n    mspc.StatusChanged += _mspc_statusChanged;\n\n    mspc.RunAsync(_options);\n    mspc.Done.WaitOne();\n}\n\nprivate void _mspc_statusChanged(object sender, ValueEventArgs e)\n{\n    Console.WriteLine(e.Value.Step + "\\t" + e.Value.StepCount + "\\t" + e.Value.Message);\n}\n')),Object(o.b)("p",null,"See ",Object(o.b)("a",Object(r.a)({parentName:"p"},{href:"https://github.com/Genometric/MSPC/blob/edce42ecb18e7c447396f038e03f2fd7d911d70e/CLI/Orchestrator.cs#L19-L73"}),"MSPC CLI Orchestrator"),"\nfor a complete example of running MSPC ",Object(o.b)("inlineCode",{parentName:"p"},"Core"),"\n",Object(o.b)("em",{parentName:"p"},"asynchronously")," and reporting its status. "))}u.isMDXComponent=!0},95:function(e,n,t){"use strict";t.d(n,"a",(function(){return p})),t.d(n,"b",(function(){return h}));var r=t(0),a=t.n(r);function o(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function c(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);n&&(r=r.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,r)}return t}function s(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?c(Object(t),!0).forEach((function(n){o(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):c(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function i(e,n){if(null==e)return{};var t,r,a=function(e,n){if(null==e)return{};var t,r,a={},o=Object.keys(e);for(r=0;r<o.length;r++)t=o[r],n.indexOf(t)>=0||(a[t]=e[t]);return a}(e,n);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);for(r=0;r<o.length;r++)t=o[r],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(a[t]=e[t])}return a}var l=a.a.createContext({}),u=function(e){var n=a.a.useContext(l),t=n;return e&&(t="function"==typeof e?e(n):s(s({},n),e)),t},p=function(e){var n=u(e.components);return a.a.createElement(l.Provider,{value:n},e.children)},b={inlineCode:"code",wrapper:function(e){var n=e.children;return a.a.createElement(a.a.Fragment,{},n)}},d=a.a.forwardRef((function(e,n){var t=e.components,r=e.mdxType,o=e.originalType,c=e.parentName,l=i(e,["components","mdxType","originalType","parentName"]),p=u(t),d=r,h=p["".concat(c,".").concat(d)]||p[d]||b[d]||o;return t?a.a.createElement(h,s(s({ref:n},l),{},{components:t})):a.a.createElement(h,s({ref:n},l))}));function h(e,n){var t=arguments,r=n&&n.mdxType;if("string"==typeof e||r){var o=t.length,c=new Array(o);c[0]=d;var s={};for(var i in n)hasOwnProperty.call(n,i)&&(s[i]=n[i]);s.originalType=e,s.mdxType="string"==typeof e?e:r,c[1]=s;for(var l=2;l<o;l++)c[l]=t[l];return a.a.createElement.apply(null,c)}return a.a.createElement.apply(null,t)}d.displayName="MDXCreateElement"}}]);