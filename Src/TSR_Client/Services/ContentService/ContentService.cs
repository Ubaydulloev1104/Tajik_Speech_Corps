using Blazored.LocalStorage;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;
using TSR_Client.wwwroot.resources;
using TSR_Client.wwwroot.resources.languages;

namespace TSR_Client.Services.ContentService
{
    public class  ContentService : IContentService
    {
        private readonly IStringLocalizer<English> _english;
        private readonly IStringLocalizer<Russian> _russian;
        private readonly IStringLocalizer<Tajik> _tajik;
        private readonly ILocalStorageService _localStorageService;
        private static CultureInfo _applicationCulture = CultureInfo.CurrentCulture;
        public  ContentService(
            IStringLocalizer<English> english,
            IStringLocalizer<Russian> russian,
            IStringLocalizer<Tajik> tajik,
            ILocalStorageService localStorageService)
        {
            _english = english;
            _russian = russian;
            _tajik = tajik;
            _localStorageService = localStorageService;
        }

        public string this[string name] => _applicationCulture.Name switch
        {
            ApplicationCulturesNames.En => _english[name],
            ApplicationCulturesNames.Ru => _russian[name],
            ApplicationCulturesNames.Tj => _tajik[name],
            _ => _english[name]
        };

        public async Task ChangeCulture(string name)
        {
            _applicationCulture = new CultureInfo(name);
            await _localStorageService.SetItemAsStringAsync(nameof(ApplicationCulturesNames), name);
        }

        public async Task<string> GetCurrentCulture()
        {
            var cultureName = await _localStorageService.GetItemAsStringAsync(nameof(ApplicationCulturesNames));
            return cultureName switch
            {
                ApplicationCulturesNames.En => "En",
                ApplicationCulturesNames.Ru => "Ru",
                ApplicationCulturesNames.Tj => "Tj",
                _ => "en"
            };
        }

        public async Task InitializeCultureAsync()
        {
            var cultureName = await _localStorageService.GetItemAsStringAsync(nameof(ApplicationCulturesNames));
            if (!string.IsNullOrEmpty(cultureName))
            {
                _applicationCulture = new CultureInfo(cultureName);
            }
        }
    }

}
