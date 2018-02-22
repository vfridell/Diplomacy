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
            if (!_mapNodes.TryGetValue(shortName, out n)) throw new ArgumentException($"No such territory short name {shortName}");
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

            Territory stp = Territories.Get("stp");
            Territory bul = Territories.Get("bul");
            Territory spa = Territories.Get("spa");
            mapNode = new MapNode("St Petersburg North Coast", "stp_nc", stp);
            _mapNodes.Add("stp_nc", mapNode);
            _territoryToMapNodeDictionary[stp].Add(mapNode);

            mapNode = new MapNode("St Petersburg South Coast", "stp_sc", stp);
            _mapNodes.Add("stp_sc", mapNode);
            _territoryToMapNodeDictionary[stp].Add(mapNode);

            mapNode = new MapNode("Bulgaria East Coast", "bul_ec", bul);
            _mapNodes.Add("bul_ec", mapNode);
            _territoryToMapNodeDictionary[bul].Add(mapNode);

            mapNode = new MapNode("Bulgaria South Coast", "bul_sc", bul);
            _mapNodes.Add("bul_sc", mapNode);
            _territoryToMapNodeDictionary[bul].Add(mapNode);

            mapNode = new MapNode("Spain North Coast", "spa_nc", spa);
            _mapNodes.Add("spa_nc", mapNode);
            _territoryToMapNodeDictionary[spa].Add(mapNode);

            mapNode = new MapNode("Spain South Coast", "spa_sc", spa);
            _mapNodes.Add("spa_sc", mapNode);
            _territoryToMapNodeDictionary[spa].Add(mapNode);

        }
    }
}
