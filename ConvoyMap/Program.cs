using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib;
using System.IO;
using DiplomacyLib.Models;

namespace ConvoyMap
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter streamWriter = new StreamWriter(File.Open("ConvoyMap.txt", FileMode.Truncate)))
            {

                streamWriter.WriteLine("public static Dictionary<string, List<string>> ConvoyMap = new Dictionary<string, List<string>>() \n{" );

                int src_num = 0;
                foreach (var kvp in MapAdjacencyStrings.FleetMap)
                {
                    var source = MapNodes.Get(kvp.Key);
                    if (source.Territory.TerritoryType != TerritoryType.Sea) continue;
                    src_num++;
                    streamWriter.Write($"{{ \"{kvp.Key}\", new List<string>() {{");

                    foreach (string targetNodeName in kvp.Value)
                    {
                        var dest = MapNodes.Get(targetNodeName);
                        if (source.Territory.TerritoryType == TerritoryType.Sea || dest.Territory.TerritoryType == TerritoryType.Sea)
                        {
                            if (dest.Territory.TerritoryType == TerritoryType.Coast)
                                streamWriter.Write($"\"{targetNodeName}_{src_num}\", ");
                            else
                                streamWriter.Write($"\"{targetNodeName}\", ");
                        }

                    }
                    streamWriter.WriteLine("} }, ");
                }

                streamWriter.WriteLine("};");
            }

            using (StreamWriter streamWriter = new StreamWriter(File.Open("ConvoyMap_SplitCoast.dot", FileMode.Truncate)))
            {

                streamWriter.WriteLine(@"strict graph FleetDiplomacyGraph
{
graph[overlap = ""false"", splines = ""true"", sep = .1, model = ""subset"" mode = ""major""]
edge[fontsize = 6, labelfloat = ""false"", labelangle = 35, labeldistance = 1.75]
node[fontsize = 16]"
                );

                int src_num = 0;
                foreach (var kvp in MapAdjacencyStrings.FleetMap)
                {
                    StringBuilder nodesSB = new StringBuilder();
                    var source = MapNodes.Get(kvp.Key);
                    if (source.Territory.TerritoryType != TerritoryType.Sea) continue;
                    src_num++;
                    nodesSB.AppendLine($"{source}[color=blue]");
                    // "{ \"nao\", new List<string>() {\"nwg", "cly", "lvp", "mao", "iri"} },
                    streamWriter.Write($"{source} -- {{");

                    foreach (string targetNodeName in kvp.Value)
                    {
                        var dest = MapNodes.Get(targetNodeName);
                        if (source.Territory.TerritoryType == TerritoryType.Sea || dest.Territory.TerritoryType == TerritoryType.Sea)
                        {
                            if (dest.Territory.TerritoryType == TerritoryType.Coast)
                                streamWriter.Write($"{targetNodeName}_{src_num} ");
                            else
                                streamWriter.Write($"{targetNodeName} ");
                        }

                        if(dest.Territory.TerritoryType == TerritoryType.Coast) nodesSB.AppendLine($"{targetNodeName}_{src_num}[color=green, shape=box]");
                    }
                    streamWriter.WriteLine("} ");

                    streamWriter.WriteLine();
                    streamWriter.Write(nodesSB.ToString());
                }

                streamWriter.WriteLine();
                streamWriter.WriteLine("}");
            }
        }
    }
}
