using DiplomacyLib.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiplomacyLib.Analysis
{

    public class FeatureMeasurementCollection : IEnumerable<FeatureMeasurement>
    {
        List<FeatureMeasurement> _list = new List<FeatureMeasurement>();
        Dictionary<string, List<FeatureMeasurement>> _nameDictionary = new Dictionary<string, List<FeatureMeasurement>>();
        Dictionary<Territory, List<FeatureMeasurement>> _territoryDictionary = new Dictionary<Territory, List<FeatureMeasurement>>();
        Dictionary<Powers, List<FeatureMeasurement>> _powersDictionary = new Dictionary<Powers, List<FeatureMeasurement>>();

        public IEnumerable<FeatureMeasurement> this[string n] { get => _nameDictionary[n]; protected set { } }
        public IEnumerable<FeatureMeasurement> this[Territory t] { get => _territoryDictionary[t]; protected set { } }
        public IEnumerable<FeatureMeasurement> this[Powers p] { get => _powersDictionary[p]; protected set { } }

        public int Count => _list.Count;

        public IEnumerable<FeatureMeasurement> ByTerritory(Territory t)
        {
            if (!_territoryDictionary.ContainsKey(t)) return Enumerable.Empty<FeatureMeasurement>();
            return _territoryDictionary[t];
        }

        public IEnumerable<FeatureMeasurement> ByPower(Powers p)
        {
            if (!_powersDictionary.ContainsKey(p)) return Enumerable.Empty<FeatureMeasurement>();
            return _powersDictionary[p];
        }

        public IEnumerable<FeatureMeasurement> ByName(string n)
        {
            if (!_nameDictionary.ContainsKey(n)) return Enumerable.Empty<FeatureMeasurement>();
            return _nameDictionary[n];
        }

        public Dictionary<Territory, List<FeatureMeasurement>> ByTerritory() => _territoryDictionary;
        public Dictionary<Powers, List<FeatureMeasurement>> ByPower() => _powersDictionary;
        public Dictionary<string, List<FeatureMeasurement>> ByName() => _nameDictionary;

        public void Add(FeatureMeasurement item)
        {
            _list.Add(item);
            AddSubCollections(item);
        }

        private void AddSubCollections(FeatureMeasurement item)
        {
            if (!string.IsNullOrEmpty(item.Name))
            {
                if (!_nameDictionary.ContainsKey(item.Name)) _nameDictionary.Add(item.Name, new List<FeatureMeasurement>() { item });
                else _nameDictionary[item.Name].Add(item);
            }

            if (!_powersDictionary.ContainsKey(item.Power)) _powersDictionary.Add(item.Power, new List<FeatureMeasurement>() { item });
            else _powersDictionary[item.Power].Add(item);

            if (item.Territory != null)
            {
                if (!_territoryDictionary.ContainsKey(item.Territory)) _territoryDictionary.Add(item.Territory, new List<FeatureMeasurement>() { item });
                else _territoryDictionary[item.Territory].Add(item);
            }

        }

        public void Clear()
        {
            _list.Clear();
            _nameDictionary.Clear();
            _territoryDictionary.Clear();
            _powersDictionary.Clear();
        }

        public bool Contains(string featureName)
        {
            return _nameDictionary.ContainsKey(featureName);
        }

        public IEnumerator<FeatureMeasurement> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }
}