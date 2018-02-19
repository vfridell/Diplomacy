using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class Territories
    {
        public static IReadOnlyList<Territory> AsReadOnlyList => _territories.Values.ToList().AsReadOnly();

        private static Dictionary<string, Territory> _territories;
        
        public static Territory Get(string shortName)
        {
            Territory t;
            if (!_territories.TryGetValue(shortName, out t)) throw new ArgumentException($"No such territory short name {shortName}");
            return t;
        }

        static Territories()
        {
            _territories = new Dictionary<string, Territory>() {
            // Sea Territories
            {"adr", new Territory("Adriatic Sea", "adr", false, Powers.None, TerritoryType.Sea) },
            {"aeg", new Territory("Aegean Sea", "aeg", false, Powers.None, TerritoryType.Sea) },
            {"bal", new Territory("Baltic Sea", "bal", false, Powers.None, TerritoryType.Sea) },
            {"bar", new Territory("Barents Sea", "bar", false, Powers.None, TerritoryType.Sea) },
            {"bla", new Territory("Black Sea", "bla", false, Powers.None, TerritoryType.Sea) },
            {"eas", new Territory("Eastern Mediterranean", "eas", false, Powers.None, TerritoryType.Sea) },
            {"eng", new Territory("English Channel", "eng", false, Powers.None, TerritoryType.Sea) },
            {"lyo", new Territory("Gulf of Lyon", "lyo", false, Powers.None, TerritoryType.Sea) },
            {"bot", new Territory("Gulf of Bothnia", "bot", false, Powers.None, TerritoryType.Sea) },
            {"hel", new Territory("Helgoland Bight", "hel", false, Powers.None, TerritoryType.Sea) },
            {"ion", new Territory("Ionian Sea", "ion", false, Powers.None, TerritoryType.Sea) },
            {"iri", new Territory("Irish Sea", "iri", false, Powers.None, TerritoryType.Sea) },
            {"mao", new Territory("Mid-Atlantic Ocean", "mao", false, Powers.None, TerritoryType.Sea) },
            {"nao", new Territory("North Atlantic Ocean", "nao", false, Powers.None, TerritoryType.Sea) },
            {"nth", new Territory("North Sea", "nth", false, Powers.None, TerritoryType.Sea) },
            {"nwg", new Territory("Norwegian Sea", "nwg", false, Powers.None, TerritoryType.Sea) },
            {"ska", new Territory("Skagerrak", "ska", false, Powers.None, TerritoryType.Sea) },
            {"tys", new Territory("Tyrrhenian Sea", "tys", false, Powers.None, TerritoryType.Sea) },
            {"wes", new Territory("Western Mediterranean", "wes", false, Powers.None, TerritoryType.Sea) },

            // Coast Territories
            {"alb", new Territory("Albania", "alb", false, Powers.None, TerritoryType.Coast) },
            {"ank", new Territory("Ankara", "ank", true, Powers.Turkey, TerritoryType.Coast) },
            {"apu", new Territory("Apulia", "apu", false, Powers.None, TerritoryType.Coast) },
            {"arm", new Territory("Armenia", "arm", false, Powers.None, TerritoryType.Coast) },
            {"bel", new Territory("Belgium", "bel", true, Powers.None, TerritoryType.Coast) },
            {"ber", new Territory("Berlin", "ber", true, Powers.Germany, TerritoryType.Coast) },
            {"bre", new Territory("Brest", "bre", true, Powers.France, TerritoryType.Coast) },
            {"bul", new Territory("Bulgaria", "bul", true, Powers.None, TerritoryType.Coast) },
            {"cly", new Territory("Clyde", "cly", false, Powers.None, TerritoryType.Coast) },
            {"con", new Territory("Constantinople", "con", true, Powers.Turkey, TerritoryType.Coast) },
            {"den", new Territory("Denmark", "den", true, Powers.None, TerritoryType.Coast) },
            {"edi", new Territory("Edinburgh", "edi", true, Powers.England, TerritoryType.Coast) },
            {"fin", new Territory("Finland", "fin", false, Powers.None, TerritoryType.Coast) },
            {"gas", new Territory("Gascony", "gas", false, Powers.None, TerritoryType.Coast) },
            {"gre", new Territory("Greece", "gre", true, Powers.None, TerritoryType.Coast) },
            {"hol", new Territory("Holland", "hol", true, Powers.None, TerritoryType.Coast) },
            {"kie", new Territory("Kiel", "kie", true, Powers.Germany, TerritoryType.Coast) },
            {"lon", new Territory("London", "lon", true, Powers.England, TerritoryType.Coast) },
            {"lvn", new Territory("Livonia", "lvn", false, Powers.None, TerritoryType.Coast) },
            {"lvp", new Territory("Liverpool", "lvp", true, Powers.England, TerritoryType.Coast) },
            {"mar", new Territory("Marseilles", "mar", true, Powers.France, TerritoryType.Coast) },
            {"naf", new Territory("North Africa", "naf", false, Powers.None, TerritoryType.Coast) },
            {"nap", new Territory("Naples", "nap", true, Powers.Italy, TerritoryType.Coast) },
            {"nwy", new Territory("Norway", "nwy", true, Powers.None, TerritoryType.Coast) },
            {"pic", new Territory("Picardy", "pic", false, Powers.None, TerritoryType.Coast) },
            {"pie", new Territory("Piedmont", "pie", false, Powers.None, TerritoryType.Coast) },
            {"por", new Territory("Portugal", "por", true, Powers.None, TerritoryType.Coast) },
            {"pru", new Territory("Prussia", "pru", false, Powers.None, TerritoryType.Coast) },
            {"rom", new Territory("Rome", "rom", true, Powers.Italy, TerritoryType.Coast) },
            {"rum", new Territory("Rumania", "rum", true, Powers.None, TerritoryType.Coast) },
            {"sev", new Territory("Sevastopol", "sev", true, Powers.Russia, TerritoryType.Coast) },
            {"smy", new Territory("Smyrna", "smy", true, Powers.Turkey, TerritoryType.Coast) },
            {"spa", new Territory("Spain", "spa", true, Powers.None, TerritoryType.Coast) },
            {"stp", new Territory("St Petersburg", "stp", true, Powers.Russia, TerritoryType.Coast) },
            {"swe", new Territory("Sweden", "swe", true, Powers.None, TerritoryType.Coast) },
            {"syr", new Territory("Syria", "syr", false, Powers.None, TerritoryType.Coast) },
            {"tri", new Territory("Trieste", "tri", true, Powers.Austria, TerritoryType.Coast) },
            {"tun", new Territory("Tunis", "tun", true, Powers.None, TerritoryType.Coast) },
            {"tus", new Territory("Tuscany", "tus", false, Powers.None, TerritoryType.Coast) },
            {"ven", new Territory("Venice", "ven", true, Powers.Italy, TerritoryType.Coast) },
            {"wal", new Territory("Wales", "wal", false, Powers.None, TerritoryType.Coast) },
            {"yor", new Territory("Yorkshire", "yor", false, Powers.None, TerritoryType.Coast) },

            // Inland Territories
            {"boh", new Territory("Bohemia", "boh", false, Powers.None, TerritoryType.Inland) },
            {"bud", new Territory("Budapest", "bud", true, Powers.Austria, TerritoryType.Inland) },
            {"bur", new Territory("Burgundy", "bur", false, Powers.None, TerritoryType.Inland) },
            {"gal", new Territory("Galicia", "gal", false, Powers.None, TerritoryType.Inland) },
            {"mos", new Territory("Moscow", "mos", true, Powers.Russia, TerritoryType.Inland) },
            {"mun", new Territory("Munich", "mun", true, Powers.Germany, TerritoryType.Inland) },
            {"par", new Territory("Paris", "par", true, Powers.France, TerritoryType.Inland) },
            {"ruh", new Territory("Ruhr", "ruh", false, Powers.None, TerritoryType.Inland) },
            {"ser", new Territory("Serbia", "ser", true, Powers.None, TerritoryType.Inland) },
            {"sil", new Territory("Silesia", "sil", false, Powers.None, TerritoryType.Inland) },
            {"tyr", new Territory("Tyrolia", "tyr", false, Powers.None, TerritoryType.Inland) },
            {"ukr", new Territory("Ukraine", "ukr", false, Powers.None, TerritoryType.Inland) },
            {"vie", new Territory("Vienna", "vie", true, Powers.Austria, TerritoryType.Inland) },
            {"war", new Territory("Warsaw", "war", true, Powers.Russia, TerritoryType.Inland) },

            };


        }
    }
}
