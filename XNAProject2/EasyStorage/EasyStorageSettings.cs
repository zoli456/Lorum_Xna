using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EasyStorage
{
    /*
     * This whole file is basically a big no-op on Windows Phone because none of the strings are used
     * on that platform. However it is included so that games can have a cross-platform game using
     * these APIs without having to use an #if !WINDOWS_PHONE around it.
     */

    /// <summary>
    ///     The languages supported by EasyStorage.
    /// </summary>
    public enum Language
    {
        English,
        Magyar
    }

    /// <summary>
    ///     Used to access settings for EasyStorage.
    /// </summary>
    public static class EasyStorageSettings
    {
        // map the two letter language value to our enumeration
        private static readonly Dictionary<string, Language> languageMap = new Dictionary<string, Language>
        {
            { "en", Language.English },
            { "hu", Language.Magyar }
        };

        // map our languages to string culture values for creating new CultureInfo objects. 
        // the only part that really matters to us is the language, so the region portion is
        // simply an acceptable value picked arbitrarily.
        private static readonly Dictionary<Language, string> cultureMap = new Dictionary<Language, string>
        {
            { Language.English, "en-US" },
            { Language.Magyar, "hu-HU" }
        };

        /// <summary>
        ///     Restricts the EasyStorage system to the specified languages. If the system is currently
        ///     set to a language not listed here, EasyStorage will use the first language given. This
        ///     method does reset the SaveDevice strings, so it's best to call this before setting
        ///     your strings explicitly.
        /// </summary>
        /// <param name="supportedLanguages">The set of supported languages.</param>
        public static void SetSupportedLanguages(params Language[] supportedLanguages)
        {
#if !WINDOWS_PHONE
            // make sure we didn't get null
            if (supportedLanguages == null)
                throw new ArgumentNullException("supportedLanguages");

            // make sure we didn't get an empty collection
            if (supportedLanguages.Length == 0)
                throw new ArgumentException("supportedLanguages");

            // make sure all languages specified are actually valid
            if (supportedLanguages.Any(l => l < Language.English || l > Language.Magyar))
                throw new ArgumentException("supportedLanguages");

            // is the current language unsupported

            // try to find the current language
            var currentLanguage = Language.English;
            var contains = supportedLanguages.Any(language => Equals(language, currentLanguage));
            var supportedLanguage =
                languageMap.TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower(),
                    out currentLanguage) && contains;

            // if we're running a non-supported language, default to the first given language
            if (!supportedLanguage)
                // Strings.Culture = new CultureInfo(cultureMap[supportedLanguages[0]]);
                // since the Strings.Culture changed, we need to reset the strings to make sure
                // they are compliant with the desired supported languages
                ResetSaveDeviceStrings();
#endif
        }

        /// <summary>
        ///     Resets the SaveDevice strings to their default values.
        /// </summary>
        public static void ResetSaveDeviceStrings()
        {
        }
    }
}