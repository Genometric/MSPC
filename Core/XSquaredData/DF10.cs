﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.XSquaredData
{
    internal class DF10
    {
        internal double ChiSqrd(double x)
        {
            if (x < 1462.0)
                return Data[(int)Math.Round(x)];
            else
                return 0.0;
        }

        private readonly double[] Data = new double[]
        {
             #region .     Data     .
1   ,
9.99827884370044E-01    ,
9.96340153172656E-01    ,
9.81424063777859E-01    ,
9.47346982656289E-01    ,
8.91178018914151E-01    ,
8.15263244523772E-01    ,
7.25444953309605E-01    ,
6.28836935179873E-01    ,
5.32103576374715E-01    ,
4.40493285065212E-01    ,
3.57518002427925E-01    ,
2.85056500316631E-01    ,
2.23671816811499E-01    ,
1.72991607882071E-01    ,
1.32061856287721E-01    ,
9.96324004870460E-02    ,
7.43639798145803E-02    ,
5.49636414951049E-02    ,
4.02626823406100E-02    ,
2.92526880769611E-02    ,
2.10935655874380E-02    ,
1.51046006521784E-02    ,
1.07465783832828E-02    ,
7.60039068106700E-03    ,
5.34550548713406E-03    ,
3.74018590580994E-03    ,
2.60434028632595E-03    ,
1.80524884917385E-03    ,
1.24604474999419E-03    ,
8.56641210775300E-04    ,
5.86725482216744E-04    ,
4.00437663376030E-04    ,
2.72385834487655E-04    ,
1.84698146401452E-04    ,
1.24865252783038E-04    ,
8.41760980490267E-05    ,
5.65934518567893E-05    ,
3.79517089189165E-05    ,
2.53885385263497E-05    ,
1.69447439300674E-05    ,
1.12841695209784E-05    ,
7.49867835317813E-06    ,
4.97302684816503E-06    ,
3.29166533185509E-06    ,
2.17472725766718E-06    ,
1.43423877937528E-06    ,
9.44271626312421E-07    ,
6.20669870424926E-07    ,
4.07324330517049E-07    ,
2.66908342490450E-07    ,
1.74642818513361E-07    ,
1.14111503441794E-07    ,
7.44595318334945E-08    ,
4.85226207510792E-08    ,
3.15806510275203E-08    ,
2.05290843975090E-08    ,
1.33293078375735E-08    ,
8.64474309007225E-09    ,
5.60040185517030E-09    ,
3.62430095206149E-09    ,
2.34305189718725E-09    ,
1.51323404678300E-09    ,
9.76360384452012E-10    ,
6.29371035293333E-10    ,
4.05329350011469E-10    ,
2.60810852371337E-10    ,
1.67675911790536E-10    ,
1.07709432774637E-10    ,
6.91329405232828E-11    ,
4.43378174992322E-11    ,
2.84139064600646E-11    ,
1.81954968418457E-11    ,
1.16434546161075E-11    ,
7.44548581778740E-12    ,
4.75779188819801E-12    ,
3.03827245543954E-12    ,
1.93893939418321E-12    ,
1.23658817503060E-12    ,
7.88163124821120E-13    ,
5.02046431882913E-13    ,
3.19605873764313E-13    ,
2.03345624853007E-13    ,
1.29303425057541E-13    ,
8.21761968668685E-14    ,
5.21973387841621E-14    ,
3.31376962746175E-14    ,
2.10267924294993E-14    ,
1.33353822461775E-14    ,
8.45326410157845E-15    ,
5.35592612458163E-15    ,
3.39187926132369E-15    ,
2.14707002200271E-15    ,
1.35848942793711E-15    ,
8.59161150848840E-16    ,
5.43131846732299E-16    ,
3.43203738913424E-16    ,
2.16779682416643E-16    ,
1.36870095921703E-16    ,
8.63824462203875E-17    ,
5.44970198292052E-17    ,
3.43679505839508E-17    ,
2.16656312341148E-17    ,
1.36530286041515E-17    ,
8.60061720757840E-18    ,
5.41596864714289E-18    ,
3.40934873023784E-18    ,
2.14544927273723E-18    ,
1.34964427863790E-18    ,
8.48745057474024E-19    ,
5.33573964946451E-19    ,
3.35331171627028E-19    ,
2.10677216267740E-19    ,
1.32320742096777E-19    ,
8.30820678920481E-20    ,
5.21504334465288E-20    ,
3.27251757743794E-20    ,
2.05296537275024E-20    ,
1.28753459839089E-20    ,
8.07264519690233E-21    ,
5.06004606584259E-21    ,
3.17085696650298E-21    ,
1.98648042522343E-21    ,
1.24416855208103E-21    ,
7.79046235904188E-22    ,
4.87683520569011E-22    ,
3.05214715745792E-22    ,
1.90970844146348E-22    ,
1.19460549167235E-22    ,
7.47101105463457E-23    ,
4.67125139657989E-23    ,
2.92003201667216E-23    ,
1.82492064405382E-23    ,
1.14025978365573E-23    ,
7.12309120151959E-24    ,
4.44876502747771E-24    ,
2.77790927095636E-24    ,
1.73422564336926E-24    ,
1.08243894734440E-24    ,
6.75480399981967E-25    ,
4.21439149198562E-25    ,
2.62888147478808E-25    ,
1.63954120829952E-25    ,
1.02232749273435E-25    ,
6.37345998832577E-26    ,
3.97263927141297E-26    ,
2.47572695214760E-26    ,
1.54257817407023E-26    ,
9.60978124573735E-27    ,
5.98553175501523E-27    ,
3.72748505506253E-27    ,
2.32088708909972E-27    ,
1.44483422602789E-27    ,
8.99308904108734E-28    ,
5.59664254117940E-28    ,
3.48237045711681E-28    ,
2.16646660308715E-28    ,
1.34759546980168E-28    ,
8.38105076085553E-29    ,
5.21158275553485E-29    ,
3.24021540442252E-29    ,
2.01424375347836E-29    ,
1.25194391938713E-29    ,
7.78024412175507E-30    ,
4.83434734232156E-30    ,
3.00344386607818E-30    ,
1.86568784009730E-30    ,
1.15876927864965E-30    ,
7.19605077207094E-31    ,
4.46818793208199E-31    ,
2.77401821874470E-31    ,
1.72198172967495E-31    ,
1.06878363935701E-31    ,
6.63275180184787E-32    ,
4.11567467964101E-32    ,
2.55347915246643E-32    ,
1.58404753190819E-32    ,
9.82537957023315E-33    ,
6.09363320443992E-33    ,
3.77876361672781E-33    ,
2.34298859509248E-33    ,
1.45257390246476E-33    ,
9.00439386202439E-34    ,
5.58109575653372E-34    ,
3.45886618953902E-34    ,
2.14337365377594E-34    ,
1.32804377121763E-34    ,
8.22768754598439E-35    ,
5.09676500070504E-35    ,
3.15691839978378E-35    ,
1.95517003164014E-35    ,
1.21076157604262E-35    ,
7.49697628288380E-36    ,
4.64159807383484E-36    ,
2.87344761601928E-36    ,
1.77866377408941E-36    ,
1.10087932467751E-36    ,
6.81304568168372E-37    ,
4.21598502619956E-37    ,
2.60863567141582E-37    ,
1.61393053369773E-37    ,
9.98421010598765E-38    ,
6.17590301399202E-38    ,
3.81984302707090E-38    ,
2.36237731979603E-38    ,
1.46087181888567E-38    ,
9.03305076225388E-39    ,
5.58491632911839E-39    ,
3.45270246510555E-39    ,
2.13433349179417E-39    ,
1.31924791492578E-39    ,
8.15364751432847E-40    ,
5.03894003168220E-40    ,
3.11378444013092E-40    ,
1.92397908659983E-40    ,
1.18870734217932E-40    ,
7.34366201181812E-41    ,
4.53642661798816E-41    ,
2.80206952266128E-41    ,
1.73064526181954E-41    ,
1.06881285208436E-41    ,
6.60024496959784E-42    ,
4.07552474925030E-42    ,
2.51635794410989E-42    ,
1.55355635423766E-42    ,
9.59064062376890E-43    ,
5.92017486506562E-43    ,
3.65416419299382E-43    ,
2.25532158902991E-43    ,
1.39186168691345E-43    ,
8.58916905068028E-44    ,
5.29997678506808E-44    ,
3.27012841066148E-44    ,
2.01754830448387E-44    ,
1.24466260157020E-44    ,
7.67800109051409E-45    ,
4.73602286816485E-45    ,
2.92111572954993E-45    ,
1.80157913310594E-45    ,
1.11103510719184E-45    ,
6.85128968537376E-46    ,
4.22461595208410E-46    ,
2.60479012828549E-46    ,
1.60593912638747E-46    ,
9.90048562765234E-47    ,
6.10316662863603E-47    ,
3.76205811065455E-47    ,
2.31882270236588E-47    ,
1.42916241249742E-47    ,
8.80780819891360E-48    ,
5.42783396096926E-48    ,
3.34470577038959E-48    ,
2.06092487277502E-48    ,
1.26981218643846E-48    ,
7.82330244336399E-49    ,
4.81963619140423E-49    ,
2.96901305205967E-49    ,
1.82887436715484E-49    ,
1.12649626610987E-49    ,
6.93825018500914E-50    ,
4.27311585217159E-50    ,
2.63156490168515E-50    ,
1.62053494837465E-50    ,
9.97878821281625E-51    ,
6.14430102503641E-51    ,
3.78305473584613E-51    ,
2.32910147032273E-51    ,
1.43387079676218E-51    ,
8.82688837917083E-52    ,
5.43352202723569E-52    ,
3.34450297717577E-52    ,
2.05853508226921E-52    ,
1.26695637545191E-52    ,
7.79725831137067E-53    ,
4.79843065272888E-53    ,
2.95279794214698E-53    ,
1.81696100427490E-53    ,
1.11798252769117E-53    ,
6.87863269048762E-54    ,
4.23201379570965E-54    ,
2.60357468281640E-54    ,
1.60166309967316E-54    ,
9.85259473887185E-55    ,
6.06050118664835E-55    ,
3.72773545141647E-55    ,
2.29276951526617E-55    ,
1.41011532518464E-55    ,
8.67217297148343E-56    ,
5.33310875676733E-56    ,
3.27953523826999E-56    ,
2.01661808480016E-56    ,
1.23997990751832E-56    ,
7.62404430824988E-57    ,
4.68744392942568E-57    ,
2.88181970396847E-57    ,
1.77164910066621E-57    ,
1.08910292379507E-57    ,
6.69484591254444E-58    ,
4.11521778879980E-58    ,
2.52944807422183E-58    ,
1.55467475438033E-58    ,
9.55507905512573E-59    ,
5.87232534271196E-59    ,
3.60883596913661E-59    ,
2.21771385492259E-59    ,
1.36277868234731E-59    ,
8.37387990913821E-60    ,
5.14528955409594E-60    ,
3.16136560900734E-60    ,
1.94232347483755E-60    ,
1.19330219538363E-60    ,
7.33097065422737E-61    ,
4.50354801182997E-61    ,
2.76649889067880E-61    ,
1.69937295632904E-61    ,
1.04382937482846E-61    ,
6.41140254135450E-62    ,
3.93785212254144E-62    ,
2.41851450347572E-62    ,
1.48532342547240E-62    ,
9.12171526663331E-63    ,
5.60164063837136E-63    ,
3.43983278458033E-63    ,
2.11223811919593E-63    ,
1.29697611901342E-63    ,
7.96351390638446E-64    ,
4.88946414112929E-64    ,
3.00193750146021E-64    ,
1.84300275016230E-64    ,
1.13144741240310E-64    ,
6.94587419839611E-65    ,
4.26386747199435E-65    ,
2.61736819776236E-65    ,
1.60660967368814E-65    ,
9.86144278936588E-66    ,
6.05278376335614E-66    ,
3.71496363585423E-66    ,
2.28002061637492E-66    ,
1.39929044778955E-66    ,
8.58740493036336E-67    ,
5.26988429383123E-67    ,
3.23389094297700E-67    ,
1.98442595608050E-67    ,
1.21767040902819E-67    ,
7.47153809144773E-68    ,
4.58432893010217E-68    ,
2.81272390591856E-68    ,
1.72569522471658E-68    ,
1.05873393162872E-68    ,
6.49524279783225E-69    ,
3.98464698546742E-69    ,
2.44438886719625E-69    ,
1.49946663111586E-69    ,
9.19791634795695E-70    ,
5.64193820419309E-70    ,
3.46061663783227E-70    ,
2.12258470918096E-70    ,
1.30185621475589E-70    ,
7.98449649952314E-71    ,
4.89687083301933E-71    ,
3.00314592947344E-71    ,
1.84170883157249E-71    ,
1.12941181177333E-71    ,
6.92580999118634E-72    ,
4.24693665937843E-72    ,
2.60416219700135E-72    ,
1.59678844940994E-72    ,
9.79070376309018E-73    ,
6.00299087724798E-73    ,
3.68051651086924E-73    ,
2.25650988063330E-73    ,
1.38341716084019E-73    ,
8.48118613975267E-74    ,
5.19933277918948E-74    ,
3.18732452875386E-74    ,
1.95385652873827E-74    ,
1.19769665436193E-74    ,
7.34156836818795E-75    ,
4.50006480185236E-75    ,
2.75826926821939E-75    ,
1.69060671377529E-75    ,
1.03618336603375E-75    ,
6.35065914068998E-76    ,
3.89214680337812E-76    ,
2.38532689324456E-76    ,
1.46182350358843E-76    ,
8.95839854711330E-77    ,
5.48977168077154E-77    ,
3.36408304399642E-77    ,
2.06142583660572E-77    ,
1.26315708262127E-77    ,
7.73990634745735E-78    ,
4.74245048321463E-78    ,
2.90575314372043E-78    ,
1.78034224121838E-78    ,
1.09078008671604E-78    ,
6.68282188127353E-79    ,
4.09422406578989E-79    ,
2.50825922479620E-79    ,
1.53660546929134E-79    ,
9.41329199118357E-80    ,
5.76646869565837E-80    ,
3.53238191987079E-80    ,
2.16378809301848E-80    ,
1.32541295559776E-80    ,
8.11852551015941E-81    ,
4.97270416563083E-81    ,
3.04577384576058E-81    ,
1.86548731571472E-81    ,
1.14255368623768E-81    ,
6.99762474775141E-82    ,
4.28562780565093E-82    ,
2.62462988980963E-82    ,
1.60735407739882E-82    ,
9.84339600969800E-83    ,
6.02793169809482E-83    ,
3.69132003435553E-83    ,
2.26039915619399E-83    ,
1.38413573186250E-83    ,
8.47544242537626E-84    ,
5.18962857899823E-84    ,
3.17760874673896E-84    ,
1.94560583985900E-84    ,
1.19124101774400E-84    ,
7.29348007928315E-85    ,
4.46540025287529E-85    ,
2.73386118095711E-85    ,
1.67372091309775E-85    ,
1.02466096005167E-85    ,
6.27289392174208E-86    ,
3.84013371763899E-86    ,
2.35079871366848E-86    ,
1.43904799208196E-86    ,
8.80898540047955E-87    ,
5.39221653509937E-87    ,
3.30065111298093E-87    ,
2.02033237244271E-87    ,
1.23662200783871E-87    ,
7.56906278157487E-88    ,
4.63274368140075E-88    ,
2.83547293707561E-88    ,
1.73541701157357E-88    ,
1.06211940584221E-88    ,
6.50030937471331E-89    ,
3.97819382540257E-89    ,
2.43460841868546E-89    ,
1.48992225500066E-89    ,
9.11778697400843E-90    ,
5.57964617670068E-90    ,
3.41440710328909E-90    ,
2.08937023525749E-90    ,
1.27851846418503E-90    ,
7.82330282700518E-91    ,
4.78701569796244E-91    ,
2.92907958720946E-91    ,
1.79221100541793E-91    ,
1.09657613764292E-91    ,
6.70934558499055E-92    ,
4.10500120550812E-92    ,
2.51152905984040E-92    ,
1.53657921177278E-92    ,
9.40077290941583E-93    ,
5.75127412189014E-93    ,
3.51849150162537E-93    ,
2.15248885866384E-93    ,
1.31679242539643E-93    ,
8.05537500302872E-94    ,
4.92772287677951E-94    ,
3.01438620050365E-94    ,
1.84392667461277E-94    ,
1.12792590574616E-94    ,
6.89937418931476E-95    ,
4.22018086931757E-95    ,
2.58133695275103E-95    ,
1.57888549312917E-95    ,
9.65714838400083E-96    ,
5.90662692470178E-96    ,
3.61262243923569E-96    ,
2.20952052073177E-96    ,
1.35134414707754E-96    ,
8.26468647791029E-97    ,
5.05451308513340E-97    ,
3.09118375107393E-97    ,
1.89044002585366E-97    ,
1.15609521047292E-97    ,
7.06995983123290E-98    ,
4.32347514987141E-98    ,
2.64387972478806E-98    ,
1.61675107302477E-98    ,
9.88638200105206E-99    ,
6.04539123255418E-99    ,
3.69661533677266E-99    ,
2.26035661963256E-99    ,
1.38210980863365E-99    ,
8.45086269638000E-100   ,
5.16716681033188E-100   ,
3.15934378591940E-100   ,
1.93167601511858E-100   ,
1.18104028806600E-100   ,
7.22084778607358E-101   ,
4.41473609991382E-101   ,
2.69907164605677E-101   ,
1.65012640152094E-101   ,
1.00881881867951E-101   ,
6.16740298050135E-102   ,
3.77037627224547E-102   ,
2.30494360388270E-102   ,
1.40905901627133E-102   ,
8.61373206173031E-103   ,
5.26558776436897E-103   ,
3.21881266310450E-103   ,
1.96760501468322E-103   ,
1.20274494468010E-103   ,
7.35195068342641E-104   ,
4.49391735398916E-104   ,
2.74688864229367E-104   ,
1.67899934898112E-104   ,
1.02625084686913E-104   ,
6.27263592267881E-105   ,
3.83389495649853E-105   ,
2.34327867034176E-105   ,
1.43219216866296E-105   ,
8.75330980637792E-106   ,
5.34979300940356E-106   ,
3.26960550723212E-106   ,
1.99823911348532E-106   ,
1.22121829222344E-106   ,
7.46333465213666E-107   ,
4.56106565110435E-107   ,
2.78736310846860E-107   ,
1.70339203338783E-107   ,
1.04094920691469E-107   ,
6.36118976329992E-108   ,
3.88723747478406E-108   ,
2.37540532579737E-108   ,
1.45153774318808E-108   ,
8.86978140084721E-109   ,
5.41990281552046E-109   ,
3.31180000997561E-109   ,
2.02362832803157E-109   ,
1.23649243268554E-109   ,
7.55520516952960E-110   ,
4.61631218812957E-110   ,
2.82057862587705E-110   ,
1.72335759286286E-110   ,
1.05294761895612E-110   ,
6.43327811362316E-111   ,
3.93053893115649E-111   ,
2.40140889469371E-111   ,
1.46714955259792E-111   ,
8.96348580211828E-112   ,
5.47613043244922E-112   ,
3.34553018533918E-112   ,
2.04385679004436E-112   ,
1.24862013090712E-112   ,
7.62789285601068E-113   ,
4.65986394946242E-113   ,
2.84666467054223E-113   ,
1.73897695637790E-113   ,
1.06229663044291E-113   ,
6.48921646777664E-114   ,
3.96399611204927E-114   ,
2.42141204319631E-114   ,
1.47910401444193E-114   ,
9.03489879669402E-115   ,
5.51877192924288E-115   ,
3.37098017251582E-115   ,
2.05903896684867E-115   ,
1.25767250955594E-115   ,
7.68183941860250E-116   ,
4.69199513567722E-116   ,
2.86579138689878E-116   ,
1.75035565724521E-116   ,
1.06906167342236E-116   ,
6.52941035939110E-117   ,
3.98786026662533E-117   ,
2.43557037839524E-117   ,
1.48749746868332E-117   ,
9.08461665182060E-118   ,
5.54819623048500E-118   ,
3.38837816378246E-118   ,
2.06931596113842E-118   ,
1.26373679439652E-118   ,
7.71758391255304E-119   ,
4.71303879298850E-119   ,
2.87816448806860E-119   ,
1.75762072629395E-119   ,
1.07332117192121E-119   ,
6.55434383274058E-120   ,
4.00243008436203E-120   ,
2.44406817106402E-120   ,
1.49244357263587E-120   ,
9.11334025011205E-121   ,
5.56483545444681E-121   ,
3.39799052084386E-121   ,
2.07485192825806E-121   ,
1.26691413278499E-121   ,
7.73574945889505E-122   ,
4.72337872642884E-122   ,
2.88402033255762E-122   ,
1.76091769468322E-122   ,
1.07516471742019E-122   ,
6.56456833655676E-123   ,
4.00804493485878E-123   ,
2.44711424101402E-123   ,
1.49407079667081E-123   ,
9.12185984841690E-124   ,
5.56917563776456E-124   ,
3.40011613039628E-124   ,
2.07583064147907E-124   ,
1.26731750368618E-124   ,
7.73703052775744E-125   ,
4.72344176262093E-125   ,
2.88362121690131E-125   ,
1.76040773006094E-125   ,
1.07469132664084E-125   ,
6.56069212577800E-126   ,
4.00507842098420E-126   ,
2.44493803568692E-126   ,
1.49252003905946E-126   ,
9.11104057097219E-127   ,
5.56174791256886E-127   ,
3.39508103840958E-127   ,
2.07245222892513E-127   ,
1.26506973337196E-127   ,
7.72218087220003E-128   ,
4.71369041288120E-128   ,
2.87725091461544E-128   ,
1.75626492423105E-128   ,
1.07200779250744E-128   ,
6.54337023618671E-129   ,
3.99393228419283E-129   ,
2.43778592518986E-129   ,
1.48794237381586E-129   ,
9.08180871965629E-130   ,
5.54312018551195E-130   ,
3.38323339264025E-130   ,
2.06493009975892E-130   ,
1.26030162731273E-130   ,
7.69200217525025E-131   ,
4.69461597402475E-131   ,
2.86521048372730E-131   ,
1.74867374559747E-131   ,
1.06722713618668E-131   ,
6.51329507898746E-132   ,
3.97503069001174E-132   ,
2.42591773043242E-132   ,
1.48049694144385E-132   ,
9.03513896021533E-133   ,
5.51388935367585E-133   ,
3.36493871415081E-133   ,
2.05348807196120E-133   ,
1.25315022557368E-133   ,
7.64733345350457E-134   ,
4.66673209256467E-134   ,
2.84781435809999E-134   ,
1.73782666539070E-134   ,
1.06046716553055E-134   ,
6.47118768679411E-135   ,
3.94881491228196E-135   ,
2.40960349533016E-135   ,
1.47034898905577E-135   ,
8.97204242255456E-136   ,
5.47467407977130E-136   ,
3.34057551101375E-136   ,
2.03835770944186E-136   ,
1.24375718625791E-136   ,
7.58904124390166E-137   ,
4.63056880787839E-137   ,
2.82538673165338E-137   ,
1.72392196298685E-137   ,
1.05184914301722E-137   ,
6.41778962903047E-138   ,
3.91573842660642E-138   ,
2.38912050913030E-138   ,
1.45766806336223E-138   ,
8.89355573527406E-139   ,
5.42610813824407E-139   ,
3.31053124085328E-139   ,
2.01977587228520E-139   ,
1.23226729916004E-139   ,
7.51801058593458E-140   ,
4.58666708125743E-140   ,
2.79825823935549E-140   ,
1.70716171247875E-140   ,
1.04149656438158E-140   ,
6.35385560325727E-141   ,
3.87626241652430E-141   ,
2.36475058073331E-141   ,
1.44262635750898E-141   ,
8.80073099941190E-142   ,
5.36883433469675E-142   ,
3.27519862333983E-142   ,
1.99798248058815E-142   ,
1.21882712977818E-142   ,
7.43513679939416E-143   ,
4.53557381041255E-143   ,
2.76676293443640E-143   ,
1.68774994998603E-143   ,
1.02953404751684E-143   ,
6.28014669923618E-144   ,
3.83085169007755E-144   ,
2.33677756335468E-144   ,
1.42539721061334E-144   ,
8.69462669353088E-145   ,
5.30349899333500E-145   ,
3.23497229909260E-145   ,
1.97321848954546E-145   ,
1.20358379214033E-145   ,
7.34131804756469E-146   ,
4.47783732288231E-146   ,
2.73123555757860E-146   ,
1.66589101896890E-146   ,
1.01608632989023E-146   ,
6.19742432447636E-147   ,
3.77997099958681E-147   ,
2.30548512494732E-147   ,
1.40615375708752E-147   ,
8.57629949169816E-148   ,
5.23074700074674E-148   ,
3.19024582760805E-148   ,
1.94572407112357E-148   ,
1.18668384751229E-148   ,
7.23744866740995E-149   ,
4.41400333674536E-149   ,
2.69200909080867E-149   ,
1.64178808898521E-149   ,
1.00127737161675E-149   ,
6.10644477339850E-150   ,
3.72408175346754E-150   ,
2.27115475741562E-150   ,
1.38506772140197E-150   ,
8.44679696724343E-151   ,
5.15121738913238E-151   ,
3.14140901370576E-151   ,
1.91573699578755E-151   ,
1.16827232492219E-151   ,
7.12441324150811E-152   ,
4.34461137296538E-152   ,
2.64941258636461E-152   ,
1.61564184185751E-152   ,
9.85229560451639E-153   ,
6.00795441695004E-153   ,
3.66363910574173E-153   ,
2.23406401573926E-153   ,
1.36230835279286E-153   ,
8.30715114831362E-154   ,
5.06553943797404E-154   ,
3.08884554950315E-154   ,
1.88349120625533E-154   ,
1.14849185854848E-154   ,
7.00308138114170E-155   ,
4.27019160048669E-155   ,
2.60376925888810E-155   ,
1.58764931806676E-155   ,
9.68063014271729E-156   ,
5.90268548536189E-156   ,
3.59908940641767E-156   ,
2.19448497663930E-156   ,
1.33804149352889E-156   ,
8.15837288590487E-157   ,
4.97432926993460E-157   ,
3.03293095702277E-157   ,
1.84921557411511E-157   ,
1.12748193632985E-157   ,
6.87430318586316E-158   ,
4.19126209275217E-158   ,
2.55539482783244E-158   ,
1.55800291531220E-158   ,
9.49894976092522E-159   ,
5.79135241361768E-159   ,
3.53086799404052E-159   ,
2.15268290530323E-159   ,
1.31242877368451E-159   ,
8.00144699106909E-160   ,
4.87818691441277E-160   ,
2.97403081511819E-160   ,
1.81313282929255E-160   ,
1.10537825365602E-160   ,
6.73890534184408E-161   ,
4.10832647253304E-161   ,
2.50459609590750E-161   ,
1.52688953054469E-161   ,
9.30839296291666E-162   ,
5.67464871695854E-162   ,
3.45939731038825E-162   ,
2.10891511789696E-162   ,
1.28562692489979E-162   ,
7.83732809521976E-163   ,
4.77769381051674E-163   ,
2.91249925342402E-163   ,
1.77545865177719E-163   ,
1.08231216565216E-163   ,
6.59768781929535E-164   ,
4.02187192074406E-164   ,
2.45166974867475E-164   ,
1.49448983635997E-164   ,
9.11005996459514E-165   ,
5.55324436228628E-165   ,
3.38508531642036E-165   ,
2.06343002707728E-165   ,
1.25778720530470E-165   ,
7.66693718568582E-166   ,
4.67341072019500E-166   ,
2.84867769543077E-166   ,
1.73640091465860E-166   ,
1.05841023136197E-166   ,
6.45142112801477E-167   ,
3.93236752421710E-167   ,
2.39690135998718E-167   ,
1.46097768239567E-167   ,
8.90500909159045E-168   ,
5.42778360050751E-168   ,
3.30832418811583E-168   ,
2.01646635745366E-168   ,
1.22905492763108E-168   ,
7.49115876777278E-169   ,
4.56587602173659E-169   ,
2.78289383248870E-169   ,
1.69615906735976E-169   ,
1.03379384304002E-169   ,
6.30084408959580E-170   ,
3.84026293710877E-170   ,
2.34056458781049E-170   ,
1.42651961228906E-170   ,
8.69425387828363E-171   ,
5.29888322461814E-171   ,
3.22948927070955E-171   ,
1.96825251787458E-171   ,
1.19956908250230E-171   ,
7.31083860443757E-172   ,
4.45560435380551E-172   ,
2.71546081052943E-172   ,
1.65492364795102E-172   ,
1.00857893377149E-172   ,
6.14666208491296E-173   ,
3.74598733068567E-173   ,
2.28292054501579E-173   ,
1.39127448679458E-173   ,
8.47876081090822E-174   ,
5.16713121853532E-174   ,
3.14893826998190E-174   ,
1.91900611752152E-174   ,
1.16946204896021E-174   ,
7.12678198514815E-175   ,
4.34308558047194E-175   ,
2.64667661149279E-175   ,
1.61287591356227E-175   ,
9.82875756720906E-176   ,
5.98954573604289E-176   ,
3.64994860659062E-176   ,
2.22421732996482E-176   ,
1.35539320380653E-176   ,
8.25944765830822E-177   ,
5.03308576229134E-177   ,
3.06701065964022E-177   ,
1.86893361303625E-177   ,
1.13885938444241E-177   ,
6.93975247647806E-178   ,
4.22878404831731E-178   ,
2.57682361183515E-178   ,
1.57018757815390E-178   ,
9.56788729467201E-179   ,
5.83012998275333E-179   ,
3.55253284929683E-179   ,
2.16468970208647E-179   ,
1.31901850627046E-179   ,
8.03718233543145E-180   ,
4.89727456012853E-180   ,
2.98402728441280E-180   ,
1.81823007426102E-180   ,
1.10787968664805E-180   ,
6.75047110836387E-181   ,
4.11313810756482E-181   ,
2.50616830103490E-181   ,
1.52702064724125E-181   ,
9.30416337087087E-182   ,
5.66901351496756E-182   ,
3.45410399425205E-182   ,
2.10455888813788E-182   ,
1.28228486927210E-182   ,
7.81278224650856E-183   ,
4.76019445919871E-183   ,
2.90029013918858E-183   ,
1.76707905662823E-183   ,
1.07663452000514E-183   ,
6.55961595168093E-184   ,
3.99655987023080E-184   ,
2.43496114365966E-184   ,
1.48352733956943E-184   ,
9.03851087900498E-185   ,
5.50675852415946E-185   ,
3.35500368916475E-185   ,
2.04403250542709E-185   ,
1.24531845795143E-185   ,
7.58701405710647E-186   ,
4.62231132794706E-186   ,
2.81608230539316E-186   ,
1.71565256874599E-186   ,
1.04522839977404E-186   ,
6.36782204475272E-187   ,
3.87943517951711E-187   ,
2.36343656930935E-187   ,
1.43985008619671E-187   ,
8.77179516071286E-188   ,
5.34389073835845E-188   ,
3.25555132694968E-188   ,
1.98330458892983E-188   ,
1.20823714829599E-188   ,
7.36059384670825E-189   ,
4.48406016477633E-189   ,
2.73166802671358E-189   ,
1.66411112430719E-189   ,
1.01375882717240E-189   ,
6.17568162856704E-190   ,
3.76212376597922E-190   ,
2.29181307555952E-190   ,
1.39612159793937E-190   ,
8.50482225563459E-191   ,
5.18089970732265E-191   ,
3.15604422999648E-191   ,
1.92255570993648E-191   ,
1.17115060329417E-191   ,
7.13418759612082E-192   ,
4.34584540920709E-192   ,
2.64729290728307E-192   ,
1.61260386805100E-192   ,
9.82316369279987E-193   ,
5.98374465275792E-193   ,
3.64495956741010E-193   ,
2.22029342988710E-193   ,
1.35246499265902E-193   ,
8.23833970274555E-194   ,
5.01823930640295E-194   ,
3.05675796663482E-194   ,
1.86195317460469E-194   ,
1.13416039738439E-194   ,
6.90841196677212E-195   ,
4.20804142944197E-195   ,
2.56318421647019E-195   ,
1.56126876614514E-195   ,
9.50984777868814E-196   ,
5.79251951678663E-196   ,
3.52825119082252E-196   ,
2.14906495744518E-196   ,
1.30899397441204E-196   ,
7.97303765497867E-197   ,
4.85632842965093E-197   ,
2.95794678190119E-197   ,
1.80165129154708E-197   ,
1.09736018259759E-197   ,
6.68383533179414E-198   ,
4.07099316197210E-198   ,
2.47955128547283E-198   ,
1.51023285199716E-198   ,
9.19841141699065E-199   ,
5.60247401314762E-199   ,
3.41228249639107E-199   ,
2.07829990245544E-199   ,
1.26581305703356E-199   ,
7.70955026202202E-200   ,
4.69555184476727E-200   ,
2.85984412597116E-200   ,
1.74179169835069E-200   ,
1.06083589025638E-200   ,
6.46097902162329E-201   ,
3.93501688059751E-201   ,
2.39658598198055E-201   ,
1.45961251915545E-201   ,
8.88956067215658E-202   ,
5.41403644186208E-202   ,
3.29731328468623E-202   ,
2.00815585188290E-202   ,
1.22301782527353E-202   ,
7.44845727952232E-203   ,
4.53626118453538E-203   ,
2.76266326486110E-203   ,
1.68250373768273E-203   ,
1.02466596255950E-203   ,
6.24031874970040E-204   ,
3.80040107396834E-204   ,
2.31446325021930E-204   ,
1.40951385360035E-204   ,
8.58393882973918E-205   ,
5.22759686789809E-205   ,
3.18358006998572E-205   ,
1.93877621094997E-205   ,
1.18069522714833E-205   ,
7.19028586622067E-206   ,
4.37877605139598E-206   ,
2.66659795923633E-206   ,
1.62390487439003E-206   ,
9.88921608836301E-207   ,
6.02228618661949E-207   ,
3.66740741244506E-207   ,
2.23334170473343E-207   ,
1.36003299835943E-207   ,
8.28212863506019E-208   ,
5.04350849550064E-208   ,
3.07129692388874E-208   ,
1.87029072091600E-208   ,
1.13892389169893E-208   ,
6.93551253380607E-209   ,
4.22338521379881E-209   ,
2.57182319836664E-209   ,
1.56610114572828E-209   ,
9.53667081702353E-210   ,
5.80727065380342E-210   ,
3.53627178673571E-210   ,
2.15336426726053E-210   ,
1.31125654399198E-210   ,
7.98465468714628E-211   ,
4.86208913570507E-211   ,
2.96065637483529E-211   ,
1.80281601039353E-211   ,
1.09777446686376E-211   ,
6.68456521737878E-212   ,
4.07034787486258E-212   ,
2.47849597742174E-212   ,
1.50918763756562E-212   ,
9.18959968777996E-213   ,
5.59562091040347E-213   ,
3.40720540236140E-213   ,
2.07465883703494E-213   ,
1.26326193908315E-213   ,
7.69198595241315E-214   ,
4.68362274548704E-214   ,
2.85183035047508E-214   ,
1.73645617228708E-214   ,
1.05730997266819E-214   ,
6.43782543745085E-215   ,
3.91989499572233E-215   ,
2.38675610743186E-215   ,
1.45324898008770E-215   ,
8.84851496045499E-216   ,
5.38764700964189E-216   ,
3.28039591554013E-216   ,
1.99733898578306E-216   ,
1.21611791545394E-216   ,
7.40453836600898E-217   ,
4.50836101911780E-217   ,
2.74497115110895E-217   ,
1.67130335921803E-217   ,
1.01758616540679E-217   ,
6.19563052728263E-218   ,
3.77223065769124E-218   ,
2.29672704828845E-218   ,
1.39835985717590E-218   ,
8.51386839315473E-219   ,
5.18362220321088E-219   ,
3.15600859753270E-219   ,
1.92150466956487E-219   ,
1.16988492333429E-219   ,
7.12267749204045E-220   ,
4.33652501428320E-220   ,
2.64021244363959E-220   ,
1.60743839103816E-220   ,
9.78651908944112E-221   ,
5.95827590209233E-221   ,
3.62753344902318E-221   ,
2.20851675618812E-221   ,
1.34458552424898E-221   ,
8.18605440646965E-222   ,
4.98378487462617E-222   ,
3.03418751588380E-222   ,
1.84724295045701E-222   ,
1.12461557225809E-222   ,
6.84672121699953E-223   ,
4.16830679742590E-223   ,
2.53767026662316E-223   ,
1.54493136873544E-223   ,
9.40549549685313E-224   ,
5.72601734926129E-224   ,
3.48595786364459E-224   ,
2.12221853786297E-224   ,
1.29198232993837E-224   ,
7.86541326918729E-225   ,
4.78834048457061E-225   ,
2.91505672229277E-225   ,
1.77462872186506E-225   ,
1.08035507392066E-225   ,
6.57694245363419E-226   ,
4.00387109453632E-226   ,
2.43744403808725E-226   ,
1.48384228969098E-226   ,
9.03315292166150E-227   ,
5.49907332108321E-227   ,
3.34763570068453E-227   ,
2.03791190484874E-227   ,
1.24059823750979E-227   ,
7.55223428080380E-228   ,
4.59746351327837E-228   ,
2.79872143806892E-228   ,
1.70372543199367E-228   ,
1.03714168368974E-228   ,
6.31357183526388E-229   ,
3.84335693529749E-229   ,
2.33961755879978E-229   ,
1.42422165986012E-229   ,
8.66979572586656E-230   ,
5.27762721387022E-230   ,
3.21267745502430E-230   ,
1.95566342187040E-230   ,
1.19047334245505E-230   ,
7.24675892920026E-231   ,
4.41129938688159E-231   ,
2.68526922926824E-231   ,
1.63458580069730E-231   ,
9.95007137881717E-232   ,
6.05680038306560E-232   ,
3.68687928002233E-232   ,
2.24426000460745E-232   ,
1.36611109902418E-232   ,
8.31567427957383E-233   ,
5.06182961933512E-233   ,
3.08117369035044E-233   ,
1.87552754420492E-233   ,
1.14164038314736E-233   ,
6.94918400471462E-234   ,
4.22996637638272E-234   ,
2.57477116465957E-234   ,
1.56725252557246E-234   ,
9.53977084303629E-235   ,
5.80678213124564E-235   ,
3.53453062027580E-235   ,
2.15142690233885E-235   ,
1.30954393578646E-235   ,
7.97098858852210E-236   ,
4.85180053613459E-236   ,
2.95319638739857E-236   ,
1.79754743960306E-236   ,
1.09412524189064E-236   ,
6.65966465451363E-237   ,
4.05355745952888E-237   ,
2.46728295065158E-237   ,
1.50175897374998E-237   ,
9.14071503922439E-238   ,
5.56363669718553E-238   ,
3.38638254537936E-238   ,
2.06116108457142E-238   ,
1.25454578947632E-238   ,
7.63589184053513E-239   ,
4.64763153123041E-239   ,
2.82880026115062E-239   ,
1.72175579100746E-239   ,
1.04794743409740E-239   ,
6.37831736356296E-240   ,
3.88214213733105E-240   ,
2.36284603820635E-240   ,
1.43812985638412E-240   ,
8.75305121851129E-241   ,
5.32745178564490E-241   ,
3.24248726823372E-241   ,
1.97349361929521E-241   ,
1.20113513656983E-241   ,
7.31049385651360E-242   ,
4.44938784239778E-242   ,
2.70802404175528E-242   ,
1.64817557691797E-242   ,
1.00312058369237E-242   ,
6.10522285076906E-243   ,
3.71576819834229E-243   ,
2.26148869757924E-243   ,
1.37638188333837E-243   ,
8.37687806146559E-244   ,
5.09828561796289E-244   ,
3.10287910498181E-244   ,
1.88844471118594E-244   ,
1.14932385964828E-244   ,
6.99486443402224E-245   ,
4.25711041469910E-245   ,
2.59089171485089E-245   ,
1.57682082680125E-245   ,
9.59652883180267E-246   ,
5.84042887087442E-246   ,
3.55446342502638E-246   ,
2.16322705748885E-246   ,
1.31652439602018E-246   ,
8.01224953186164E-247   ,
4.87616927883661E-247   ,
2.96757590407297E-247   ,
1.80602457180749E-247   ,
1.09911777728422E-247   ,
6.68903657382666E-248   ,
4.07081786511073E-248   ,
2.47741371780354E-248   ,
1.50769734944830E-248   ,
9.17547537116994E-249   ,
5.58395291453505E-249   ,
3.39823723767858E-249   ,
2.06806610598229E-249   ,
1.25855997672536E-249   ,
7.65917858640952E-250   ,
4.66110897478561E-250   ,
2.83658042118011E-250   ,
1.72623425662554E-250   ,
1.05051715358234E-250   ,
6.39300958171993E-251   ,
3.89050837073413E-251   ,
2.36758808911144E-251   ,
1.44080343263989E-251   ,
8.76803187998734E-252   ,
5.33578480069620E-252   ,
3.24708227235956E-252   ,
1.97600065088397E-252   ,
1.20248503322792E-252   ,
7.31764073287322E-253   ,
4.45308819977357E-253   ,
2.70988163726126E-253   ,
1.64906646720611E-253   ,
1.00351718640580E-253   ,
6.10675203986812E-254   ,
3.71616147711771E-254   ,
2.26140156891282E-254   ,
1.37613051287697E-254   ,
8.37414323833017E-255   ,
5.09588915684744E-255   ,
3.10097589022495E-255   ,
1.88701623825964E-255   ,
1.14829035768074E-255   ,
6.98757743101085E-256   ,
4.25206980466147E-256   ,
2.58745601779343E-256   ,
1.57450631742838E-256   ,
9.58108473589129E-257   ,
5.83020464350095E-257   ,
3.54773982422693E-257   ,
2.15883064912399E-257   ,
1.31366381431246E-257   ,
7.99371666872674E-258   ,
4.86420778410060E-258   ,
2.95988161642422E-258   ,
1.80109002782280E-258   ,
1.09596165918857E-258   ,
6.66889933256631E-259   ,
4.05799796827511E-259   ,
2.46926871186011E-259   ,
1.50253206293052E-259   ,
9.14277456679766E-260   ,
5.56328290026361E-260   ,
3.38519078847696E-260   ,
2.05984257228172E-260   ,
1.25338295633320E-260   ,
7.62662543194717E-261   ,
4.64066190521337E-261   ,
2.82375050747725E-261   ,
1.71819162463718E-261   ,
1.04548006968398E-261   ,
6.36148935814497E-262   ,
3.87080006881839E-262   ,
2.35527469036485E-262   ,
1.43311579337265E-262   ,
8.72006841964979E-263   ,
5.30587969082237E-263   ,
3.22844804002110E-263   ,
1.96439627862849E-263   ,
1.19526252953192E-263   ,
7.27271232973707E-264   ,
4.42515429472643E-264   ,
2.69252244666647E-264   ,
1.63828386686558E-264   ,
9.96822622960143E-265   ,
6.06520561020529E-265   ,
3.69038845522781E-265   ,
2.24541982315221E-265   ,
1.36622408740115E-265   ,
8.31275998729567E-266   ,
5.05786769047366E-266   ,
3.07743301518882E-266   ,
1.87244330140128E-266   ,
1.13927264006444E-266   ,
6.93179301458177E-267   ,
4.21757129131131E-267   ,
2.56612736753220E-267   ,
1.56132356963746E-267   ,
9.49962706926807E-268   ,
5.77988412262380E-268   ,
3.51666211879461E-268   ,
2.13964189526978E-268   ,
1.30181863047386E-268   ,
7.92061334851741E-269   ,
4.81910165247469E-269   ,
2.93205630455075E-269   ,
1.78392861064207E-269   ,
1.08537942259680E-269   ,
6.60365910784565E-270   ,
4.01778469321558E-270   ,
2.44448640605404E-270   ,
1.48726222541277E-270   ,
9.04870489325187E-271   ,
5.50534144836601E-271   ,
3.34950826603231E-271   ,
2.03787156299369E-271   ,
1.23985679594632E-271   ,
7.54336649989109E-272   ,
4.58942055469071E-272   ,
2.79221895762583E-272   ,
1.69879140616913E-272   ,
1.03354552224865E-272   ,
6.28808112882315E-273   ,
3.82565353839574E-273   ,
2.32751298251063E-273   ,
1.41604666146341E-273   ,
8.61513317295417E-274   ,
5.24137708771287E-274   ,
3.18880379432534E-274   ,
1.94003325057307E-274   ,
1.18029219001239E-274   ,
7.18073466728000E-275   ,
4.36864961189139E-275   ,
2.65781370566936E-275   ,
1.61696586060723E-275   ,
9.83730546090476E-276   ,
5.98481123818803E-276   ,
3.64102577150518E-276   ,
2.21511381436865E-276   ,
1.34761965531126E-276   ,
8.19856091315030E-277   ,
4.98777576027343E-277   ,
3.03441667235564E-277   ,
1.84604598893939E-277   ,
1.12307516127176E-277   ,
6.83241340172432E-278   ,
4.15660215663683E-278   ,
2.52872610438116E-278   ,
1.53838181361123E-278   ,
9.35891484895622E-279   ,
5.69358580017445E-279   ,
3.46373980041493E-279   ,
2.10718986185284E-279   ,
1.28192050914371E-279   ,
7.79861614140584E-280   ,
4.74430952467122E-280   ,
2.88620722771507E-280   ,
1.75582419785794E-280   ,
1.06815327583619E-280   ,
6.49808146536424E-281   ,
3.95308139223886E-281   ,
2.40483559866732E-281   ,
1.46296541860175E-281   ,
8.89983105960904E-282   ,
5.41412776849105E-282   ,
3.29362594535157E-282   ,
2.00363720809833E-282   ,
1.21888558437708E-282   ,
7.41490915471084E-283   ,
4.51073975034192E-283   ,
2.74402934442727E-283   ,
1.66927841320392E-283   ,
1.01547175901003E-283   ,
6.17740351375211E-284   ,
3.75788202169869E-284   ,
2.28601651185953E-284   ,
1.39063974348954E-284   ,
8.45958317903671E-285   ,
5.14614890155521E-285   ,
3.13050778407991E-285   ,
1.90434787598958E-285   ,
1.15844879766074E-285   ,
7.04703553129033E-286   ,
4.28681894631574E-286   ,
2.60773157337975E-286   ,
1.58631596119942E-286   ,
9.64973904671379E-287   ,
5.87003258148529E-287   ,
3.57079175687839E-287   ,
2.17213896200682E-287   ,
1.32132532698996E-287   ,
8.03768508416105E-288   ,
4.88935181307034E-288   ,
2.97420339682937E-288   ,
1.80921058395579E-288   ,
1.10054207971363E-288   ,
6.69457891611583E-289   ,
4.07229245222474E-289   ,
2.47715815886625E-289   ,
1.50684158158040E-289   ,
9.16601453559838E-290   ,
5.57561237853593E-290   ,
3.39159271242460E-290   ,
2.06306970800636E-290   ,
1.25494069222859E-290   ,
7.63363856020586E-291   ,
4.64343186432656E-291   ,
2.82452651487231E-291   ,
1.71811144311632E-291   ,
1.04509579845928E-291   ,
6.35711358909457E-292   ,
3.86689999702428E-292   ,
2.35214998461323E-292   ,
1.43075800175179E-292   ,
8.70294941734859E-293   ,
5.29377940501903E-293   ,
3.22006268918828E-293   ,
1.95867292847461E-293   ,
1.19140281940904E-293   ,
7.24693616909559E-294   ,
4.40807890486816E-294   ,
2.68128757681474E-294   ,
1.63093459826153E-294   ,
9.92039147980003E-295   ,
6.03420682525216E-295   ,
3.67037713112701E-295   ,
2.23254544277137E-295   ,
1.35796646406551E-295   ,
8.25994020505140E-296   ,
5.02416489236504E-296   ,
3.05597635200468E-296   ,
1.85881093634046E-296   ,
1.13062757936757E-296   ,
6.87706387985643E-297   ,
4.18297886834297E-297   ,
2.54429469677531E-297   ,
1.54756281522036E-297   ,
9.41300504851082E-298   ,
5.72542105643123E-298   ,
3.48245653778044E-298   ,
2.11818122943347E-298   ,
1.28836720839637E-298   ,
7.83637749163309E-299   ,
4.76639657902780E-299   ,
2.89910630681416E-299   ,
1.76334483219645E-299   ,
1.07253012987227E-299   ,
6.52350347236677E-300   ,
3.96781523047358E-300   ,
2.41335454868180E-300   ,
1.46787801720096E-300   ,
8.92807753433508E-301   ,
5.43031584417667E-301   ,
3.30286922958006E-301   ,
2.00889307028544E-301   ,
1.22185991221792E-301   ,
7.43164861867705E-302   ,
4.52010032895433E-302   ,
2.74922408213331E-302   ,
1.67213509947303E-302   ,
1.01702527653453E-302   ,
6.18573472587779E-303   ,
3.76227033611643E-303   ,
2.28827317095167E-303   ,
1.39176177270681E-303   ,
8.46488581715684E-304   ,
5.14844967348986E-304   ,
3.13134563604606E-304   ,
1.90451638000052E-304   ,
1.15834409038524E-304   ,
7.04514010020187E-305   ,
4.28490160102131E-305   ,
2.60610101990169E-305   ,
1.58504214450145E-305   ,
9.64027800207401E-306   ,
5.86323740372943E-306   ,
3.56602660238828E-306   ,
2.16885669580439E-306   ,
1.31909573762490E-306   ,
8.02270749287403E-307   ,
4.87938157713529E-307   ,
2.96761658816288E-307   ,
1.80488685898599E-307   ,
1.09771945682254E-307   ,
6.67623981481127E-308   ,
4.06042677764225E-308   ,
2.46950908069597E-308
#endregion	
        };
    }
}