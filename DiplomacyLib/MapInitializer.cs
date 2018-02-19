using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class MapInitializer
    {
        static UndirectedGraph<MapNode, UndirectedEdge<MapNode>> FullMap = new UndirectedGraph<MapNode, UndirectedEdge<MapNode>>();
        static UndirectedGraph<MapNode, UndirectedEdge<MapNode>> FleetMap = new UndirectedGraph<MapNode, UndirectedEdge<MapNode>>();
        static UndirectedGraph<MapNode, UndirectedEdge<MapNode>> ArmyMap = new UndirectedGraph<MapNode, UndirectedEdge<MapNode>>();

        static MapInitializer()
        {
            foreach (MapNode n in MapNodes.AsReadOnlyList)
            {
                // add all the territories as map nodes to the full map, ignoring the *_nc, *_sc and *_ec nodes
                if(!n.ShortName.Contains('_')) FullMap.AddVertex(n);

                if (n.Territory.TerritoryType == TerritoryType.Sea || n.Territory.TerritoryType == TerritoryType.Coast)
                    FleetMap.AddVertex(n);

                if (n.Territory.TerritoryType == TerritoryType.Inland || n.Territory.TerritoryType == TerritoryType.Coast)
                    ArmyMap.AddVertex(n);
            }

            FleetMap.RemoveVertex(MapNodes.Get("spa"));
            FleetMap.RemoveVertex(MapNodes.Get("bul"));
            FleetMap.RemoveVertex(MapNodes.Get("stp"));

            ArmyMap.RemoveVertex(MapNodes.Get("spa_nc"));
            ArmyMap.RemoveVertex(MapNodes.Get("spa_sc"));
            ArmyMap.RemoveVertex(MapNodes.Get("bul_ec"));
            ArmyMap.RemoveVertex(MapNodes.Get("bul_sc"));
            ArmyMap.RemoveVertex(MapNodes.Get("stp_nc"));
            ArmyMap.RemoveVertex(MapNodes.Get("stp_sc"));


        }
    }
}
