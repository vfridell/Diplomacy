using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class MapNodes
    {
        public static IReadOnlyList<MapNode> AsReadOnlyList => _mapNodes.Values.ToList().AsReadOnly();
        public static IReadOnlyList<MapNode> GetMapNodes(Territory t) => _territoryToMapNodeDictionary[t].AsReadOnly();

        private static Dictionary<string, MapNode> _mapNodes = new Dictionary<string, MapNode>();
        private static Dictionary<Territory, List<MapNode>> _territoryToMapNodeDictionary = new Dictionary<Territory, List<MapNode>>();

        public static MapNode Get(string shortName)
        {
            MapNode n;
            if (!_mapNodes.TryGetValue(shortName, out n)) throw new ArgumentException($"No such map node short name {shortName}");
            return n;
        }

        static MapNodes()
        {
            MapNode mapNode;

            foreach (Territory t in Territories.AsReadOnlyList)
            {
                mapNode = new MapNode(t.Name, t.ShortName, t);
                _mapNodes.Add(t.ShortName, mapNode);
                _territoryToMapNodeDictionary.Add(t, new List<MapNode>() { mapNode });
            }

            // for fleet map
            AddMapNodeForSplitTerritory("St Petersburg North Coast", "stp_nc");
            AddMapNodeForSplitTerritory("St Petersburg South Coast", "stp_sc");
            AddMapNodeForSplitTerritory("Bulgaria East Coast", "bul_ec");
            AddMapNodeForSplitTerritory("Bulgaria South Coast", "bul_sc");
            AddMapNodeForSplitTerritory("Spain North Coast", "spa_nc");
            AddMapNodeForSplitTerritory("Spain South Coast", "spa_sc");

            // for convoy map
            AddMapNodeForSplitTerritory("alb_10", "alb_10");
            AddMapNodeForSplitTerritory("alb_9", "alb_9");
            AddMapNodeForSplitTerritory("ank_12", "ank_12");
            AddMapNodeForSplitTerritory("apu_10", "apu_10");
            AddMapNodeForSplitTerritory("apu_9", "apu_9");
            AddMapNodeForSplitTerritory("arm_12", "arm_12");
            AddMapNodeForSplitTerritory("bel_14", "bel_14");
            AddMapNodeForSplitTerritory("bel_19", "bel_19");
            AddMapNodeForSplitTerritory("ber_17", "ber_17");
            AddMapNodeForSplitTerritory("bre_19", "bre_19");
            AddMapNodeForSplitTerritory("bre_5", "bre_5");
            AddMapNodeForSplitTerritory("bul_ec_12", "bul_ec_12");
            AddMapNodeForSplitTerritory("bul_sc_11", "bul_sc_11");
            AddMapNodeForSplitTerritory("cly_1", "cly_1");
            AddMapNodeForSplitTerritory("cly_3", "cly_3");
            AddMapNodeForSplitTerritory("con_11", "con_11");
            AddMapNodeForSplitTerritory("con_12", "con_12");
            AddMapNodeForSplitTerritory("den_14", "den_14");
            AddMapNodeForSplitTerritory("den_15", "den_15");
            AddMapNodeForSplitTerritory("den_16", "den_16");
            AddMapNodeForSplitTerritory("den_17", "den_17");
            AddMapNodeForSplitTerritory("edi_14", "edi_14");
            AddMapNodeForSplitTerritory("edi_3", "edi_3");
            AddMapNodeForSplitTerritory("fin_18", "fin_18");
            AddMapNodeForSplitTerritory("gas_5", "gas_5");
            AddMapNodeForSplitTerritory("gre_11", "gre_11");
            AddMapNodeForSplitTerritory("gre_9", "gre_9");
            AddMapNodeForSplitTerritory("hol_14", "hol_14");
            AddMapNodeForSplitTerritory("hol_15", "hol_15");
            AddMapNodeForSplitTerritory("kie_15", "kie_15");
            AddMapNodeForSplitTerritory("kie_17", "kie_17");
            AddMapNodeForSplitTerritory("lon_14", "lon_14");
            AddMapNodeForSplitTerritory("lon_19", "lon_19");
            AddMapNodeForSplitTerritory("lvn_17", "lvn_17");
            AddMapNodeForSplitTerritory("lvn_18", "lvn_18");
            AddMapNodeForSplitTerritory("lvp_1", "lvp_1");
            AddMapNodeForSplitTerritory("lvp_2", "lvp_2");
            AddMapNodeForSplitTerritory("mar_6", "mar_6");
            AddMapNodeForSplitTerritory("naf_5", "naf_5");
            AddMapNodeForSplitTerritory("naf_7", "naf_7");
            AddMapNodeForSplitTerritory("nap_8", "nap_8");
            AddMapNodeForSplitTerritory("nap_9", "nap_9");
            AddMapNodeForSplitTerritory("nwy_14", "nwy_14");
            AddMapNodeForSplitTerritory("nwy_16", "nwy_16");
            AddMapNodeForSplitTerritory("nwy_3", "nwy_3");
            AddMapNodeForSplitTerritory("nwy_4", "nwy_4");
            AddMapNodeForSplitTerritory("pic_19", "pic_19");
            AddMapNodeForSplitTerritory("pie_6", "pie_6");
            AddMapNodeForSplitTerritory("por_5", "por_5");
            AddMapNodeForSplitTerritory("pru_17", "pru_17");
            AddMapNodeForSplitTerritory("rom_8", "rom_8");
            AddMapNodeForSplitTerritory("rum_12", "rum_12");
            AddMapNodeForSplitTerritory("sev_12", "sev_12");
            AddMapNodeForSplitTerritory("smy_11", "smy_11");
            AddMapNodeForSplitTerritory("smy_13", "smy_13");
            AddMapNodeForSplitTerritory("spa_nc_5", "spa_nc_5");
            AddMapNodeForSplitTerritory("spa_sc_5", "spa_sc_5");
            AddMapNodeForSplitTerritory("spa_sc_6", "spa_sc_6");
            AddMapNodeForSplitTerritory("spa_sc_7", "spa_sc_7");
            AddMapNodeForSplitTerritory("stp_nc_4", "stp_nc_4");
            AddMapNodeForSplitTerritory("stp_sc_18", "stp_sc_18");
            AddMapNodeForSplitTerritory("swe_16", "swe_16");
            AddMapNodeForSplitTerritory("swe_17", "swe_17");
            AddMapNodeForSplitTerritory("swe_18", "swe_18");
            AddMapNodeForSplitTerritory("syr_13", "syr_13");
            AddMapNodeForSplitTerritory("tri_10", "tri_10");
            AddMapNodeForSplitTerritory("tun_7", "tun_7");
            AddMapNodeForSplitTerritory("tun_8", "tun_8");
            AddMapNodeForSplitTerritory("tun_9", "tun_9");
            AddMapNodeForSplitTerritory("tus_6", "tus_6");
            AddMapNodeForSplitTerritory("tus_8", "tus_8");
            AddMapNodeForSplitTerritory("ven_10", "ven_10");
            AddMapNodeForSplitTerritory("wal_19", "wal_19");
            AddMapNodeForSplitTerritory("wal_2", "wal_2");
            AddMapNodeForSplitTerritory("yor_14", "yor_14");

        }

        static void AddMapNodeForSplitTerritory(string name, string shortName)
        {
            string territoryShortName = shortName.Substring(0, shortName.IndexOf("_") );
            Territory t = Territories.Get(territoryShortName);
            MapNode mapNode = new MapNode(name, shortName, t);
            _mapNodes.Add(shortName, mapNode);
            _territoryToMapNodeDictionary[t].Add(mapNode);
        }

        public static MapNode ConvoyParent(this MapNode mapNode)
        {
            if (mapNode.ShortName.Count(c => c == '_') == 0) return mapNode;
            return Get(mapNode.ShortName.Substring(0, mapNode.ShortName.LastIndexOf('_')));
        }
    }
}
