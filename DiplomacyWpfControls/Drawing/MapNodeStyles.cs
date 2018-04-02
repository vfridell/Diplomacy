using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;
using DiplomacyLib;

namespace DiplomacyWpfControls.Drawing
{
    public static class MapNodeStyles
    {
        private static Dictionary<MapNode, MapNodeRenderStyle> _nodePositions = new Dictionary<MapNode, MapNodeRenderStyle>()
        {
{ MapNodes.Get("nao"), MapNodeRenderStyle.Get("nao",-966.35551414622,879.395301396087)},
{ MapNodes.Get("nwg"), MapNodeRenderStyle.Get("nwg",156.146511383039,848.085537757972)},
{ MapNodes.Get("cly"), MapNodeRenderStyle.Get("cly",-544.863367270573,982.81852382018)},
{ MapNodes.Get("lvp"), MapNodeRenderStyle.Get("lvp",-554.424304320857,1200.4145229956)},
{ MapNodes.Get("mao"), MapNodeRenderStyle.Get("mao",-977.319514347776,2267.45461523315)},
{ MapNodes.Get("iri"), MapNodeRenderStyle.Get("iri",-801.982031572259,1397.30492171619)},
{ MapNodes.Get("bar"), MapNodeRenderStyle.Get("bar",1802.17793844374,831.450099379519)},
{ MapNodes.Get("nwy"), MapNodeRenderStyle.Get("nwy",656.078102944031,1045.16998625738)},
{ MapNodes.Get("nth"), MapNodeRenderStyle.Get("nth",166.846510375723,1227.84304417079)},
{ MapNodes.Get("edi"), MapNodeRenderStyle.Get("edi",-257.382745181598,1039.57700012197)},
{ MapNodes.Get("yor"), MapNodeRenderStyle.Get("yor",-265.830127154692,1198.07025443541)},
{ MapNodes.Get("wal"), MapNodeRenderStyle.Get("wal",-558.040126219409,1399.50916730476)},
{ MapNodes.Get("eng"), MapNodeRenderStyle.Get("eng",-488.248372820673,1718.93699161172)},
{ MapNodes.Get("bre"), MapNodeRenderStyle.Get("bre",-515.769508676939,2050.40452414931)},
{ MapNodes.Get("gas"), MapNodeRenderStyle.Get("gas",-443.081974967413,2287.9276504411)},
{ MapNodes.Get("spa"), MapNodeRenderStyle.Get("spa",-292.277650446416,2531.44863460802)},
{ MapNodes.Get("por"), MapNodeRenderStyle.Get("por",-575.109672130859,2532.15435120222)},
{ MapNodes.Get("wes"), MapNodeRenderStyle.Get("wes",-526.331998483998,2871.95890672911)},
{ MapNodes.Get("naf"), MapNodeRenderStyle.Get("naf",-690.033235130649,3118.80038351056)},
{ MapNodes.Get("lon"), MapNodeRenderStyle.Get("lon",-259.268196931356,1381.2605057872)},
{ MapNodes.Get("bel"), MapNodeRenderStyle.Get("bel",16.386598592814,1681.35274070275)},
{ MapNodes.Get("pic"), MapNodeRenderStyle.Get("pic",-195.797484295542,1853.33001832968)},
{ MapNodes.Get("stp"), MapNodeRenderStyle.Get("stp",1806.28739553022,1073.76460098795)},
{ MapNodes.Get("ska"), MapNodeRenderStyle.Get("ska",581.684644725003,1245.47134963804)},
{ MapNodes.Get("fin"), MapNodeRenderStyle.Get("fin",1219.74598940856,1144.82151793998)},
{ MapNodes.Get("swe"), MapNodeRenderStyle.Get("swe",895.465735854034,1270.16619169985)},
{ MapNodes.Get("den"), MapNodeRenderStyle.Get("den",831.628228201562,1554.08364661343)},
{ MapNodes.Get("hel"), MapNodeRenderStyle.Get("hel",558.940749952356,1597.13908424589)},
{ MapNodes.Get("hol"), MapNodeRenderStyle.Get("hol",298.599939942125,1678.48234104485)},
{ MapNodes.Get("bot"), MapNodeRenderStyle.Get("bot",1509.86593019887,1243.30870051039)},
{ MapNodes.Get("mos"), MapNodeRenderStyle.Get("mos",1823.74489467619,2000.03423112463)},
{ MapNodes.Get("lvn"), MapNodeRenderStyle.Get("lvn",1553.15283731991,1588.10856907707)},
{ MapNodes.Get("par"), MapNodeRenderStyle.Get("par",-250.121321739943,2094.68575626184)},
{ MapNodes.Get("bur"), MapNodeRenderStyle.Get("bur",-6.19279441929611,2271.99564097131)},
{ MapNodes.Get("mar"), MapNodeRenderStyle.Get("mar",5.20561019377401,2540.98778197473)},
{ MapNodes.Get("lyo"), MapNodeRenderStyle.Get("lyo",-86.4286968579205,2754.07577024352)},
{ MapNodes.Get("tys"), MapNodeRenderStyle.Get("tys",22.6616115019905,2953.89299200347)},
{ MapNodes.Get("tun"), MapNodeRenderStyle.Get("tun",-258.849758506294,3120.00421735188)},
{ MapNodes.Get("pie"), MapNodeRenderStyle.Get("pie",315.410905229916,2515.77113593842)},
{ MapNodes.Get("tus"), MapNodeRenderStyle.Get("tus",339.934372509122,2738.19264949673)},
{ MapNodes.Get("tyr"), MapNodeRenderStyle.Get("tyr",642.070772197192,2509.830093314)},
{ MapNodes.Get("ven"), MapNodeRenderStyle.Get("ven",650.734696406313,2740.72458583878)},
{ MapNodes.Get("rom"), MapNodeRenderStyle.Get("rom",463.070867483901,2912.02757765293)},
{ MapNodes.Get("nap"), MapNodeRenderStyle.Get("nap",528.943324727172,3074.12741447435)},
{ MapNodes.Get("ion"), MapNodeRenderStyle.Get("ion",705.052685546544,3302.09091803819)},
{ MapNodes.Get("apu"), MapNodeRenderStyle.Get("apu",757.761357776785,2916.03864269618)},
{ MapNodes.Get("adr"), MapNodeRenderStyle.Get("adr",977.291913256661,2785.25494230404)},
{ MapNodes.Get("alb"), MapNodeRenderStyle.Get("alb",1243.77665850145,2926.87379918854)},
{ MapNodes.Get("gre"), MapNodeRenderStyle.Get("gre",1542.89699120835,2931.58920878111)},
{ MapNodes.Get("aeg"), MapNodeRenderStyle.Get("aeg",1741.0550967001,3133.40857108404)},
{ MapNodes.Get("eas"), MapNodeRenderStyle.Get("eas",2044.5858471761,3246.0350198591)},
{ MapNodes.Get("tri"), MapNodeRenderStyle.Get("tri",1031.82680126544,2617.53888001451)},
{ MapNodes.Get("ser"), MapNodeRenderStyle.Get("ser",1370.72523738492,2702.03797724179)},
{ MapNodes.Get("bul"), MapNodeRenderStyle.Get("bul",1658.86297397186,2705.84332032521)},
{ MapNodes.Get("con"), MapNodeRenderStyle.Get("con",1890.62093096582,2940.23609582963)},
{ MapNodes.Get("smy"), MapNodeRenderStyle.Get("smy",2222.81259873479,2944.85370232612)},
{ MapNodes.Get("syr"), MapNodeRenderStyle.Get("syr",2495.6763038483,2941.76667653255)},
{ MapNodes.Get("vie"), MapNodeRenderStyle.Get("vie",1033.72449228012,2461.58014797546)},
{ MapNodes.Get("bud"), MapNodeRenderStyle.Get("bud",1370.05305399598,2509.8359634261)},
{ MapNodes.Get("bla"), MapNodeRenderStyle.Get("bla",1994.62159381963,2500.20502763417)},
{ MapNodes.Get("rum"), MapNodeRenderStyle.Get("rum",1657.34181234043,2501.00131706533)},
{ MapNodes.Get("ank"), MapNodeRenderStyle.Get("ank",2108.83948381004,2736.76382113325)},
{ MapNodes.Get("arm"), MapNodeRenderStyle.Get("arm",2295.27658949849,2507.38385851659)},
{ MapNodes.Get("sev"), MapNodeRenderStyle.Get("sev",2042.85321792581,2263.22921377074)},
{ MapNodes.Get("gal"), MapNodeRenderStyle.Get("gal",1266.44695374018,2263.06325672083)},
{ MapNodes.Get("ukr"), MapNodeRenderStyle.Get("ukr",1599.50527839777,2251.82800123319)},
{ MapNodes.Get("bal"), MapNodeRenderStyle.Get("bal",1176.04034541803,1509.30654391919)},
{ MapNodes.Get("kie"), MapNodeRenderStyle.Get("kie",519.046297672547,1889.76356701192)},
{ MapNodes.Get("ruh"), MapNodeRenderStyle.Get("ruh",224.557280427682,1893.39136593584)},
{ MapNodes.Get("ber"), MapNodeRenderStyle.Get("ber",838.688261087955,1885.07552071934)},
{ MapNodes.Get("mun"), MapNodeRenderStyle.Get("mun",521.354221312086,2117.7616706984)},
{ MapNodes.Get("pru"), MapNodeRenderStyle.Get("pru",1246.91135375101,1757.18731722052)},
{ MapNodes.Get("war"), MapNodeRenderStyle.Get("war",1543.52851498385,2001.74422412255)},
{ MapNodes.Get("sil"), MapNodeRenderStyle.Get("sil",1238.70180120461,2004.18574841737)},
{ MapNodes.Get("boh"), MapNodeRenderStyle.Get("boh",878.20593453982,2262.5568862854)},


        };

        public static MapNodeRenderStyle Get(MapNode mapNode) => _nodePositions[mapNode];
    }
}
