using Prometheus;
using System.Collections.Generic;

namespace PrometheusSample.Middlewares
{
    public class MetricsHub
    {
        private static Dictionary<string, Counter> _counterDictionary = new Dictionary<string, Counter>();

        private static Dictionary<string, Dictionary<string, Gauge>> _gaugeDictionary = new Dictionary<string, Dictionary<string, Gauge>>();

        private static Dictionary<string, Summary> _summaryDictionary = new Dictionary<string, Summary>();

        private static Dictionary<string, Histogram> _histogramDictionary = new Dictionary<string, Histogram>();


        public Counter GetCounter(string key)
        {
            if (_counterDictionary.ContainsKey(key))
            {
                return _counterDictionary[key];
            }
            else
            {
                return null;
            }
        }
        public Dictionary<string, Gauge> GetGauge(string key)
        {
            if (_gaugeDictionary.ContainsKey(key))
            {
                return _gaugeDictionary[key];
            }
            else
            {
                return null;
            }
        }

        public Summary GetSummary(string key)
        {
            if (_summaryDictionary.ContainsKey(key))
            {
                return _summaryDictionary[key];
            }
            else
            {
                return null;
            }
        }
        public Histogram GetHistogram(string key)
        {
            if (_histogramDictionary.ContainsKey(key))
            {
                return _histogramDictionary[key];
            }
            else
            {
                return null;
            }
        }
        public void AddCounter(string key, Counter counter)
        {
            _counterDictionary.Add(key, counter);
        }
        public void AddGauge(string key, Dictionary<string, Gauge> gauges)
        {
            _gaugeDictionary.Add(key, gauges);
        }
        public void AddSummary(string key, Summary summary)
        {
            _summaryDictionary.Add(key, summary);
        }
        public void AddHistogram(string key, Histogram histogram)
        {
            _histogramDictionary.Add(key, histogram);
        }
    }
}
