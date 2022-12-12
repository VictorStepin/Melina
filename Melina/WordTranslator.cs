using YandexLinguistics.NET;
using YandexLinguistics.NET.Dictionary;

namespace Melina
{
    internal class WordTranslator
    {
        private const string apiKey = "dict.1.1.20221128T160855Z.e20aa226216c5a51.6ba3b7275b7fc521dd221545f0440991aec3d6c0";

        private readonly Language inputLanguage;
        private readonly Language outputLanguage;
        private readonly DictionaryService service;

        public WordTranslator ()
        {
            inputLanguage = Language.En;
            outputLanguage = Language.Ru;
            service = new DictionaryService(apiKey);
        }

        public string[] GetTranslations(string word)
        {
            var dictionaryEntry = service.LookupAsync(new LanguagePair(inputLanguage, outputLanguage), word).Result;

            var translations = new List<string>();
            foreach (var definition in dictionaryEntry.Definitions)
            {
                foreach (var translation in definition.Translations)
                {
                    translations.Add(translation.Text);
                    if (translation.Synonyms != null)
                    {
                        foreach (var synonym in translation.Synonyms)
                        {
                            translations.Add(synonym.Text);
                        }
                    }
                }
            }

            return translations.ToArray();
        }
    }
}