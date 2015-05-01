using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.Courses.Testing.Implementations
{
	public class WordsStatisticsCorrectImplementation : IWordsStatistics
	{
		private IDictionary<string, int> _stats = new Dictionary<string, int>();

		public void AddWord(string word)
		{
			if (string.IsNullOrEmpty(word)) return;
			if (word.Length > 10) word = word.Substring(0, 10);
			int count;
			_stats[word.ToLower()] = _stats.TryGetValue(word.ToLower(), out count) ? count + 1 : 1;
		}

		public IEnumerable<Tuple<int, string>> GetStatistics()
		{
			return _stats.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Select(kv => Tuple.Create(kv.Value, kv.Key));
		}
	}
}