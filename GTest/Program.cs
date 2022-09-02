using System;
using System.Text;
using System.Threading.Tasks;
using GTranslate.Translators;

namespace GTest
{

	internal static class Program
	{
		private static async Task Main()
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode; 
			
			Console.WriteLine("Translator Example\n");
			var translator = new AggregateTranslator(true);

			while (true)
			{
				Console.Write("Text (e]xit) : ");
				string text = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}

				if (text.ToLower() == "e")
				{
					break;
				}

				var fromLanguage = await translator.DetectLanguageAsync(text);
				if (fromLanguage == null)
				{
					Console.WriteLine("Cannot detect language");
					continue;
				}

				Console.Write($"Language from [{fromLanguage.ISO6391}]: ");
				string fromLang = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrEmpty(fromLang))
				{
					fromLang = fromLanguage.ISO6391;
				}


				Console.Write("Language to [en]: ");
				string toLanguage = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrEmpty(toLanguage))
				{
					toLanguage = "en";
				}

				try
				{
					var result = await translator.TranslateAsync(text, toLanguage, fromLang);
					Console.WriteLine();
					Console.WriteLine($">> {result.Translation}");
					Console.WriteLine($".. From {result.SourceLanguage}");
					Console.WriteLine($".. To   {result.TargetLanguage}");
					Console.WriteLine($".. Svc  {result.Service}");

					var xlit = await translator.TransliterateAsync(text, "en", fromLang);
					Console.WriteLine();
					Console.WriteLine($">> {xlit.Transliteration}");
					Console.WriteLine($".. From {xlit.SourceLanguage}");
					Console.WriteLine($".. To   {xlit.TargetLanguage}");
					Console.WriteLine($".. Svc  {xlit.Service}");
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
