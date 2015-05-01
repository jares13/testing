using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	public class MarkdownProcessor
	{
		public string Render(string input)
		{
            var emReplacer = new Regex(@"([^\w\\]|^)_(.*?[^\\])_(\W|$)");
            var strongReplacer = new Regex(@"([^\w\\]|^)__(.*?[^\\])__(\W|$)");
            input = strongReplacer.Replace(input,
                                match => match.Groups[1].Value +
                                        "<strong>" + match.Groups[2].Value + "</strong>" +
                                        match.Groups[3].Value);
            input = emReplacer.Replace(input,
                                match => match.Groups[1].Value +
                                        "<em>" + match.Groups[2].Value + "</em>" +
                                        match.Groups[3].Value);
            input = input.Replace(@"\_", "_");
            return input;
        }
	}

	[TestFixture]
	public class MarkdownProcessor_should
	{
		private readonly MarkdownProcessor md = new MarkdownProcessor();

		//TODO see Markdown.txt
        [TestCase("no mark up text", Result = "no mark up text", TestName = "no mark up")]
        [TestCase("__strong__", Result = "<strong>strong</strong>", TestName = "strong tag")]
        [TestCase("_em_", Result = "<em>em</em>", TestName = "em tag")]
        [TestCase("sometext_em_anothertext", Result = "sometext_em_anothertext", TestName = "no em tag")]
        [TestCase("sometext ___em&strong___ anothertext", Result = "sometext <strong><em>em&strong</em></strong> anothertext", TestName = "em&strong")]
        [TestCase("sometext \\_em\\_ anothertext", Result = "sometext _em_ anothertext", TestName = @"not em in \")]
        public string render_text_with(string input)
        {
            return md.Render(input);
        }
    }
}
