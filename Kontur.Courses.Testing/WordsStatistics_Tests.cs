using System;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing
{
	public class WordsStatisticsTests
	{
		public Func<IWordsStatistics> CreateStat = () => new WordsStatisticsCorrectImplementation(); // меняется на разные реализации при запуске exe
		public IWordsStatistics Stat;

		[SetUp]
		public void SetUp()
		{
			Stat = CreateStat();
		}

		[Test]
		public void no_stats_if_no_words()
		{
			CollectionAssert.IsEmpty(Stat.GetStatistics());
		}

		[Test]
		public void same_word_twice()
		{
			Stat.AddWord("xxx");
			Stat.AddWord("xxx");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "xxx") }, Stat.GetStatistics());
		}

		[Test]
		public void single_word()
		{
			Stat.AddWord("hello");
			CollectionAssert.AreEqual(new[] { Tuple.Create(1, "hello") }, Stat.GetStatistics());
		}

		[Test]
		public void two_same_words_one_other()
		{
			Stat.AddWord("hello");
			Stat.AddWord("world");
			Stat.AddWord("world");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "world"), Tuple.Create(1, "hello") }, Stat.GetStatistics());
		}

        [Test]
        public void uses_first_ten_letters()
        {
            Stat.AddWord("almostsame1");
            Stat.AddWord("almostsame2");
            CollectionAssert.AreEqual(new[]{ Tuple.Create(2, "almostsame") }, Stat.GetStatistics());
        }
        [Test]
        public void uses_lower_case()
        {
            Stat.AddWord("AlmoStsAme");
            Stat.AddWord("alMOsTsaMe");
            CollectionAssert.AreEqual(new[] { Tuple.Create(2, "almostsame") }, Stat.GetStatistics());
        }
        [Test]
        public void order_by_key()
        {
            Stat.AddWord("B");
            Stat.AddWord("A");
            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "a"), Tuple.Create(1, "b") }, Stat.GetStatistics());

        }

	    [Test]
	    public void able_to_work_with_numeric()
	    {
	        Stat.AddWord("1234567890");
            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "1234567890") }, Stat.GetStatistics());
	    }

        [Test]
        public void null_string_check()
        {
            Stat.AddWord(null);
            
        }

        [Test]
        public void empty_string_check()
        {
            Stat.AddWord("");
            CollectionAssert.IsEmpty(Stat.GetStatistics());
        }

	    [Test, Timeout(2)]
	    public void NAGRUZIL_NEREALNO()
	    {
            for (int i = 0; i < 1000; i++)
	        {
	            Stat.AddWord(i.ToString());
	        }
        }

	    [Test]
	    public void test_name()
	    {
	        IWordsStatistics anotherDict = CreateStat();
            Stat.AddWord("word");
            anotherDict.AddWord("word");
            CollectionAssert.AreEqual(new[]{Tuple.Create(1,"word")},Stat.GetStatistics());
	    }

        [Test]
        public void has_no_collisions()
        {
            Stat.AddWord("88");
            Stat.AddWord("106");
            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "106"), Tuple.Create(1, "88") }, Stat.GetStatistics());
        }
	}
}