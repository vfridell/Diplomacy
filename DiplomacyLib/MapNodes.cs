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

        private static Dictionary<string, MapNode> _mapNodes = new Dictionary<string, MapNode>();

        public static MapNode Get(string shortName)
        {
            MapNode n;
            if (!_mapNodes.TryGetValue(shortName, out n)) throw new ArgumentException($"No such territory short name {shortName}");
            return n;
        }

        static MapNodes()
        {
            foreach (Territory t in Territories.AsReadOnlyList)
            {
                _mapNodes.Add(t.ShortName, new MapNode(t.Name, t.ShortName, t));
            }

            Territory stp = Territories.Get("stp");
            Territory bul = Territories.Get("bul");
            Territory spa = Territories.Get("spa");
            _mapNodes.Add("stp_nc", new MapNode("St Petersburg North Coast", "stp_nc", stp));
            _mapNodes.Add("stp_sc", new MapNode("St Petersburg South Coast", "stp_sc", stp));
            _mapNodes.Add("bul_ec", new MapNode("Bulgaria East Coast", "bul_ec", stp));
            _mapNodes.Add("bul_sc", new MapNode("Bulgaria South Coast", "bul_sc", stp));
            _mapNodes.Add("spa_nc", new MapNode("Spain North Coast", "spa_nc", stp));
            _mapNodes.Add("spa_sc", new MapNode("Spain South Coast", "spa_sc", stp));
        }
    }
}
