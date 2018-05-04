using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;
using DiplomacyLib;

namespace DiplomacyUnity
{
    public static class MapNodeStyles
    {
        private static Dictionary<MapNode, MapNodeRenderStyle> _nodePositions = new Dictionary<MapNode, MapNodeRenderStyle>()
        {
{ MapNodes.Get("nao"), MapNodeRenderStyle.Get("nao",-1070.84313423375,1058.26392586028)},
{ MapNodes.Get("nwg"), MapNodeRenderStyle.Get("nwg",84.2070300422808,1053.72513703145)},
{ MapNodes.Get("cly"), MapNodeRenderStyle.Get("cly",-696.518433702593,1211.05096924724)},
{ MapNodes.Get("lvp"), MapNodeRenderStyle.Get("lvp",-683.662920644085,1397.23362780993)},
{ MapNodes.Get("mao"), MapNodeRenderStyle.Get("mao",-1077.31951434778,2365.25036964458)},
{ MapNodes.Get("iri"), MapNodeRenderStyle.Get("iri",-946.858232447592,1617.18111550585)},
{ MapNodes.Get("bar"), MapNodeRenderStyle.Get("bar",1443.39184672932,1053.69905135654)},
{ MapNodes.Get("nwy"), MapNodeRenderStyle.Get("nwy",725.111792907784,1149.24494259602)},
{ MapNodes.Get("nth"), MapNodeRenderStyle.Get("nth",53.3836501131231,1483.00121012305)},
{ MapNodes.Get("edi"), MapNodeRenderStyle.Get("edi",-412.730059594508,1261.82595209899)},
{ MapNodes.Get("yor"), MapNodeRenderStyle.Get("yor",-409.210454667514,1396.38523261225)},
{ MapNodes.Get("wal"), MapNodeRenderStyle.Get("wal",-683.395372184992,1578.08208329179)},
{ MapNodes.Get("eng"), MapNodeRenderStyle.Get("eng",-565.552364747816,1796.8737389594)},
{ MapNodes.Get("bre"), MapNodeRenderStyle.Get("bre",-615.769508676939,2148.20027856074)},
{ MapNodes.Get("gas"), MapNodeRenderStyle.Get("gas",-543.081974967413,2385.72340485253)},
{ MapNodes.Get("spa"), MapNodeRenderStyle.Get("spa",-392.277650446416,2629.24438901945)},
{ MapNodes.Get("por"), MapNodeRenderStyle.Get("por",-675.109672130859,2629.95010561365)},
{ MapNodes.Get("wes"), MapNodeRenderStyle.Get("wes",-626.331998483998,2969.75466114054)},
{ MapNodes.Get("naf"), MapNodeRenderStyle.Get("naf",-790.033235130649,3235.81243752985)},
{ MapNodes.Get("lon"), MapNodeRenderStyle.Get("lon",-420.599004794311,1581.07135732655)},
{ MapNodes.Get("bel"), MapNodeRenderStyle.Get("bel",-83.613401407186,1779.14849511418)},
{ MapNodes.Get("pic"), MapNodeRenderStyle.Get("pic",-295.797484295542,1951.12577274111)},
{ MapNodes.Get("stp"), MapNodeRenderStyle.Get("stp",1568.6670461792,1334.90626039026)},
{ MapNodes.Get("ska"), MapNodeRenderStyle.Get("ska",411.378596686982,1433.3152142773)},
{ MapNodes.Get("fin"), MapNodeRenderStyle.Get("fin",961.183412982385,1302.74791532902)},
{ MapNodes.Get("swe"), MapNodeRenderStyle.Get("swe",725.159687816013,1443.051322714)},
{ MapNodes.Get("den"), MapNodeRenderStyle.Get("den",731.628228201562,1651.87940102486)},
{ MapNodes.Get("hel"), MapNodeRenderStyle.Get("hel",458.940749952356,1694.93483865732)},
{ MapNodes.Get("hol"), MapNodeRenderStyle.Get("hol",198.599939942125,1776.27809545628)},
{ MapNodes.Get("bot"), MapNodeRenderStyle.Get("bot",1213.90651970992,1458.07828567485)},
{ MapNodes.Get("mos"), MapNodeRenderStyle.Get("mos",1723.74489467619,2097.82998553606)},
{ MapNodes.Get("lvn"), MapNodeRenderStyle.Get("lvn",1453.15283731991,1685.9043234885)},
{ MapNodes.Get("par"), MapNodeRenderStyle.Get("par",-350.121321739943,2192.48151067327)},
{ MapNodes.Get("bur"), MapNodeRenderStyle.Get("bur",27.1462530087407,2293.19236813685)},
{ MapNodes.Get("mar"), MapNodeRenderStyle.Get("mar",-91.9573887971189,2618.92452932241)},
{ MapNodes.Get("lyo"), MapNodeRenderStyle.Get("lyo",-186.428696857921,2851.87152465495)},
{ MapNodes.Get("tys"), MapNodeRenderStyle.Get("tys",-77.3383884980095,3070.90504602276)},
{ MapNodes.Get("tun"), MapNodeRenderStyle.Get("tun",-358.849758506294,3237.01627137117)},
{ MapNodes.Get("pie"), MapNodeRenderStyle.Get("pie",215.410905229916,2613.56689034985)},
{ MapNodes.Get("tus"), MapNodeRenderStyle.Get("tus",239.934372509122,2835.98840390816)},
{ MapNodes.Get("tyr"), MapNodeRenderStyle.Get("tyr",542.070772197192,2607.62584772543)},
{ MapNodes.Get("ven"), MapNodeRenderStyle.Get("ven",550.734696406313,2838.52034025021)},
{ MapNodes.Get("rom"), MapNodeRenderStyle.Get("rom",363.070867483901,3009.82333206436)},
{ MapNodes.Get("nap"), MapNodeRenderStyle.Get("nap",428.943324727172,3191.13946849364)},
{ MapNodes.Get("ion"), MapNodeRenderStyle.Get("ion",605.052685546544,3419.10297205748)},
{ MapNodes.Get("apu"), MapNodeRenderStyle.Get("apu",657.761357776785,3033.05069671547)},
{ MapNodes.Get("adr"), MapNodeRenderStyle.Get("adr",907.761357776785,2883.05069671547)},
{ MapNodes.Get("alb"), MapNodeRenderStyle.Get("alb",1157.76135777678,3043.88585320783)},
{ MapNodes.Get("gre"), MapNodeRenderStyle.Get("gre",1442.89699120835,3048.6012628004)},
{ MapNodes.Get("aeg"), MapNodeRenderStyle.Get("aeg",1641.0550967001,3250.42062510333)},
{ MapNodes.Get("eas"), MapNodeRenderStyle.Get("eas",1974.94262368429,3352.20536798261)},
{ MapNodes.Get("tri"), MapNodeRenderStyle.Get("tri",956.535655775716,2715.33463442594)},
{ MapNodes.Get("ser"), MapNodeRenderStyle.Get("ser",1270.72523738492,2799.83373165322)},
{ MapNodes.Get("bul"), MapNodeRenderStyle.Get("bul",1558.86297397186,2803.63907473664)},
{ MapNodes.Get("con"), MapNodeRenderStyle.Get("con",1768.93751917426,2972.68284386182)},
{ MapNodes.Get("smy"), MapNodeRenderStyle.Get("smy",2072.94075161419,2962.12206210421)},
{ MapNodes.Get("syr"), MapNodeRenderStyle.Get("syr",2339.29943319023,2952.53001277317)},
{ MapNodes.Get("vie"), MapNodeRenderStyle.Get("vie",958.360841505815,2559.37590238689)},
{ MapNodes.Get("bud"), MapNodeRenderStyle.Get("bud",1270.05305399598,2607.63171783753)},
{ MapNodes.Get("bla"), MapNodeRenderStyle.Get("bla",1894.62159381963,2598.0007820456)},
{ MapNodes.Get("rum"), MapNodeRenderStyle.Get("rum",1557.34181234043,2598.79707147676)},
{ MapNodes.Get("ank"), MapNodeRenderStyle.Get("ank",1997.99777791426,2789.02441078239)},
{ MapNodes.Get("arm"), MapNodeRenderStyle.Get("arm",2195.27658949849,2605.17961292802)},
{ MapNodes.Get("sev"), MapNodeRenderStyle.Get("sev",1942.85321792581,2361.02496818217)},
{ MapNodes.Get("gal"), MapNodeRenderStyle.Get("gal",1166.44695374018,2360.85901113226)},
{ MapNodes.Get("ukr"), MapNodeRenderStyle.Get("ukr",1499.50527839777,2349.62375564462)},
{ MapNodes.Get("bal"), MapNodeRenderStyle.Get("bal",1076.04034541803,1607.10229833062)},
{ MapNodes.Get("kie"), MapNodeRenderStyle.Get("kie",419.046297672547,1987.55932142335)},
{ MapNodes.Get("ruh"), MapNodeRenderStyle.Get("ruh",124.557280427682,1991.18712034727)},
{ MapNodes.Get("ber"), MapNodeRenderStyle.Get("ber",738.688261087955,1982.87127513077)},
{ MapNodes.Get("mun"), MapNodeRenderStyle.Get("mun",594.411282867623,2175.83941098233)},
{ MapNodes.Get("pru"), MapNodeRenderStyle.Get("pru",1158.1633495025,1854.98307163195)},
{ MapNodes.Get("war"), MapNodeRenderStyle.Get("war",1443.52851498385,2099.53997853398)},
{ MapNodes.Get("sil"), MapNodeRenderStyle.Get("sil",1152.88680625015,2096.30750081059)},
{ MapNodes.Get("boh"), MapNodeRenderStyle.Get("boh",778.20593453982,2360.35264069683)},



        };

        public static MapNodeRenderStyle Get(MapNode mapNode) => _nodePositions[mapNode];
    }
}
