using MudBlazor;
using System.Threading.Tasks;
using System;
using TSR_Accoun_Application.Contracts.Profile.Responses;
using TSR_Client.Services.ContentService;
using TSR_Client.Enums;
using TSR_Client.Services.UserPreferences;
using TSR_Client.wwwroot.resources;

namespace TSR_Client.Services
{
    public class LayoutService
    {
        public readonly IUserPreferencesService _userPreferencesService;
        public readonly IContentService _contentService;
        public UserProfileResponse User;
        private UserPreferences.UserPreferences _userPreferences;
        private bool _systemPreferences;
        public bool IsRtl { get; private set; } = false;
        public DarkLightMode DarkModeToggle = DarkLightMode.System;
        public bool IsDarkMode { get; private set; }
        public MudTheme CurrentTheme { get; private set; }
        public LayoutService(IUserPreferencesService userPreferencesService, IContentService contentService)
        {
            _userPreferencesService = userPreferencesService;
            _contentService = contentService;
        }
        public void SetDarkMode(bool value)
        {
            IsDarkMode = value;
        }

        public async Task ApplyUserPreferences(bool isDarkModeDefaultTheme)
        {
            _systemPreferences = isDarkModeDefaultTheme;
            _userPreferences = await _userPreferencesService.LoadUserPreferences();
            if (_userPreferences != null)
            {
                IsDarkMode = _userPreferences.DarkLightTheme switch
                {
                    DarkLightMode.Dark => true,
                    DarkLightMode.Light => false,
                    DarkLightMode.System => isDarkModeDefaultTheme,
                    _ => IsDarkMode
                };
            }
            else
            {
                IsDarkMode = isDarkModeDefaultTheme;
                _userPreferences = new UserPreferences.UserPreferences { DarkLightTheme = DarkLightMode.System };
                await _userPreferencesService.SaveUserPreferences(_userPreferences);
            }

            var lang = await _contentService.GetCurrentCulture();
            _lang = lang.IsNullOrEmpty() ? "En" : lang;
        }

        public async Task OnSystemPreferenceChanged(bool newValue)
        {
            _systemPreferences = newValue;
            if (DarkModeToggle == DarkLightMode.System)
            {
                IsDarkMode = newValue;
                OnMajorUpdateOccured();
            }
        }

        public event EventHandler MajorUpdateOccured;

        private void OnMajorUpdateOccured() => MajorUpdateOccured?.Invoke(this, EventArgs.Empty);

        public async Task ToggleDarkMode()
        {
            switch (DarkModeToggle)
            {
                case DarkLightMode.System:
                    DarkModeToggle = DarkLightMode.Light;
                    IsDarkMode = false;
                    break;
                case DarkLightMode.Light:
                    DarkModeToggle = DarkLightMode.Dark;
                    IsDarkMode = true;
                    break;
                case DarkLightMode.Dark:
                    DarkModeToggle = DarkLightMode.System;
                    IsDarkMode = _systemPreferences;
                    break;
            }

            _userPreferences.DarkLightTheme = DarkModeToggle;
            await _userPreferencesService.SaveUserPreferences(_userPreferences);
            OnMajorUpdateOccured();
        }

        public string _lang = "En";

        public async Task ChangeLanguage(string lang)
        {
            _lang = lang;

            if (lang == "Ru")
                await _contentService.ChangeCulture(ApplicationCulturesNames.Ru);
            if (lang == "En")
                await _contentService.ChangeCulture(ApplicationCulturesNames.En);
            if (lang == "Tj")
                await _contentService.ChangeCulture(ApplicationCulturesNames.Tj);

            OnMajorUpdateOccured();
        }

        public void SetBaseTheme(MudTheme theme)
        {
            CurrentTheme = theme;
            OnMajorUpdateOccured();
        }

        public DocPages GetDocsBasePage(string uri)
        {
            if (uri.Contains("/jobs")) return DocPages.Word;
            if (uri.Contains("/contact")) return DocPages.Contact;
            if (uri.Contains("/profile")) return DocPages.Profile;
            if (uri.Contains("/applications")) return DocPages.Applications;
            return DocPages.Home;
        }
    }
}

