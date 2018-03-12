using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;
using QuickGraph.Graphviz.Dot;

namespace DiplomacyLib.Visualize
{
    public static class MapNodeStyles
    {
        private static Dictionary<MapNode, MapNodeRenderStyle> _nodePositions = new Dictionary<MapNode, MapNodeRenderStyle>()
        {
             { MapNodes.Get("iri"), MapNodeRenderStyle.Get("iri", 103.45, 545.36)},
 { MapNodes.Get("mao"), MapNodeRenderStyle.Get("mao", 31.113, 370.4)},
 { MapNodes.Get("nwg"), MapNodeRenderStyle.Get("nwg", 288.93, 714.94)},
 { MapNodes.Get("cly"), MapNodeRenderStyle.Get("cly", 144.25, 661.89)},
 { MapNodes.Get("lvp"), MapNodeRenderStyle.Get("lvp", 193.25, 606.8)},
 { MapNodes.Get("nao"), MapNodeRenderStyle.Get("nao", 69.142, 719.75)},
 { MapNodes.Get("eng"), MapNodeRenderStyle.Get("eng", 182.31, 448.79)},
 { MapNodes.Get("wal"), MapNodeRenderStyle.Get("wal", 168.65, 521.87)},
 { MapNodes.Get("bar"), MapNodeRenderStyle.Get("bar", 590.78, 773.94)},
 { MapNodes.Get("nth"), MapNodeRenderStyle.Get("nth", 296.58, 533.58)},
 { MapNodes.Get("edi"), MapNodeRenderStyle.Get("edi", 206.27, 650.8)},
 { MapNodes.Get("nwy"), MapNodeRenderStyle.Get("nwy", 348.17, 659.49)},
 { MapNodes.Get("stp"), MapNodeRenderStyle.Get("stp", 638.75, 627.78)},
 { MapNodes.Get("wes"), MapNodeRenderStyle.Get("wes", 83.053, 174.95)},
 { MapNodes.Get("bre"), MapNodeRenderStyle.Get("bre", 98.054, 375.59)},
 { MapNodes.Get("spa"), MapNodeRenderStyle.Get("spa", 136.59, 239.63)},
 { MapNodes.Get("por"), MapNodeRenderStyle.Get("por", 74.53, 256.9)},
 { MapNodes.Get("gas"), MapNodeRenderStyle.Get("gas", 136.58, 291.09)},
 { MapNodes.Get("naf"), MapNodeRenderStyle.Get("naf", 32.385, 130.52)},
 { MapNodes.Get("tys"), MapNodeRenderStyle.Get("tys", 221.9, 152.54)},
 { MapNodes.Get("mar"), MapNodeRenderStyle.Get("mar", 198.64, 272.24)},
 { MapNodes.Get("pie"), MapNodeRenderStyle.Get("pie", 288.85, 272.25)},
 { MapNodes.Get("tus"), MapNodeRenderStyle.Get("tus", 282.74, 228.22)},
 { MapNodes.Get("lyo"), MapNodeRenderStyle.Get("lyo", 218.12, 197.35)},
 { MapNodes.Get("tun"), MapNodeRenderStyle.Get("tun", 214.87, 108.09)},
 { MapNodes.Get("ion"), MapNodeRenderStyle.Get("ion", 449.03, 18.385)},
 { MapNodes.Get("rom"), MapNodeRenderStyle.Get("rom", 308.83, 184.2)},
 { MapNodes.Get("nap"), MapNodeRenderStyle.Get("nap", 374.08, 110.55)},
 { MapNodes.Get("adr"), MapNodeRenderStyle.Get("adr", 410.72, 213.48)},
 { MapNodes.Get("aeg"), MapNodeRenderStyle.Get("aeg", 566.18, 111.39)},
 { MapNodes.Get("eas"), MapNodeRenderStyle.Get("eas", 650.07, 77.602)},
 { MapNodes.Get("apu"), MapNodeRenderStyle.Get("apu", 374.07, 154.68)},
 { MapNodes.Get("alb"), MapNodeRenderStyle.Get("alb", 472.77, 178.95)},
 { MapNodes.Get("gre"), MapNodeRenderStyle.Get("gre", 504.14, 134.91)},
 { MapNodes.Get("ven"), MapNodeRenderStyle.Get("ven", 350.86, 257.87)},
 { MapNodes.Get("tri"), MapNodeRenderStyle.Get("tri", 427.4, 258.07)},
 { MapNodes.Get("con"), MapNodeRenderStyle.Get("con", 609.27, 176.5)},
 { MapNodes.Get("smy"), MapNodeRenderStyle.Get("smy", 669.45, 132.48)},
 { MapNodes.Get("bul"), MapNodeRenderStyle.Get("bul", 551.49, 220.54)},
 { MapNodes.Get("sev"), MapNodeRenderStyle.Get("sev", 717.11, 372.01)},
 { MapNodes.Get("ank"), MapNodeRenderStyle.Get("ank", 685.34, 210.5)},
 { MapNodes.Get("rum"), MapNodeRenderStyle.Get("rum", 570.17, 270.2)},
 { MapNodes.Get("arm"), MapNodeRenderStyle.Get("arm", 786.61, 218.85)},
 { MapNodes.Get("syr"), MapNodeRenderStyle.Get("syr", 766.13, 137.55)},
 { MapNodes.Get("hel"), MapNodeRenderStyle.Get("hel", 314.87, 488.72)},
 { MapNodes.Get("ska"), MapNodeRenderStyle.Get("ska", 358.88, 549.22)},
 { MapNodes.Get("lon"), MapNodeRenderStyle.Get("lon", 233.32, 518.74)},
 { MapNodes.Get("yor"), MapNodeRenderStyle.Get("yor", 231.46, 562.77)},
 { MapNodes.Get("den"), MapNodeRenderStyle.Get("den", 376.87, 504.8)},
 { MapNodes.Get("bel"), MapNodeRenderStyle.Get("bel", 226.25, 400.28)},
 { MapNodes.Get("hol"), MapNodeRenderStyle.Get("hol", 272.84, 444.31)},
 { MapNodes.Get("kie"), MapNodeRenderStyle.Get("kie", 334.9, 440.98)},
 { MapNodes.Get("swe"), MapNodeRenderStyle.Get("swe", 410.75, 622.35)},
 { MapNodes.Get("bot"), MapNodeRenderStyle.Get("bot", 446.78, 568.49)},
 { MapNodes.Get("ber"), MapNodeRenderStyle.Get("ber", 397.24, 450.62)},
 { MapNodes.Get("lvn"), MapNodeRenderStyle.Get("lvn", 525.1, 497.73)},
 { MapNodes.Get("pru"), MapNodeRenderStyle.Get("pru", 461.55, 466.7)},
 { MapNodes.Get("bal"), MapNodeRenderStyle.Get("bal", 438.93, 511.48)},
 { MapNodes.Get("fin"), MapNodeRenderStyle.Get("fin", 509.03, 610.74)},
 { MapNodes.Get("pic"), MapNodeRenderStyle.Get("pic", 160.17, 380.86)},
 { MapNodes.Get("mos"), MapNodeRenderStyle.Get("mos", 665.08, 493.18)},
 { MapNodes.Get("war"), MapNodeRenderStyle.Get("war", 516.9, 422)},
 { MapNodes.Get("par"), MapNodeRenderStyle.Get("par", 166.83, 336.83)},
 { MapNodes.Get("bur"), MapNodeRenderStyle.Get("bur", 228.85, 316.26)},
 { MapNodes.Get("ruh"), MapNodeRenderStyle.Get("ruh", 288.96, 390.13)},
 { MapNodes.Get("mun"), MapNodeRenderStyle.Get("mun", 341.53, 346.11)},
 { MapNodes.Get("sil"), MapNodeRenderStyle.Get("sil", 451.15, 406.45)},
 { MapNodes.Get("bla"), MapNodeRenderStyle.Get("bla", 672.81, 264.64)},
 { MapNodes.Get("ukr"), MapNodeRenderStyle.Get("ukr", 594.78, 375.89)},
 { MapNodes.Get("bud"), MapNodeRenderStyle.Get("bud", 504.15, 281.75)},
 { MapNodes.Get("gal"), MapNodeRenderStyle.Get("gal", 525.63, 358.88)},
 { MapNodes.Get("ser"), MapNodeRenderStyle.Get("ser", 489.44, 223.1)},
 { MapNodes.Get("tyr"), MapNodeRenderStyle.Get("tyr", 374.19, 302.09)},
 { MapNodes.Get("vie"), MapNodeRenderStyle.Get("vie", 440.5, 315.08)},
 { MapNodes.Get("boh"), MapNodeRenderStyle.Get("boh", 403.76, 362.21)},

        };

        public static MapNodeRenderStyle Get(MapNode mapNode) => _nodePositions[mapNode];
    }
}
