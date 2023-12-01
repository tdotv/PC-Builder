using PC_Designer.Shared;
using PC_Designer.Shared.Extensions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PC_Designer.Shared.Services;
using System.Threading.Tasks;

namespace PC_Designer.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AboutMe { get; set; }
        public byte[] ProfilePictureData { get; set; }
        public bool Notifications { get; set; }
        public bool DarkTheme { get; set; }

        public bool IsEditing { get; set; }
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public ProfileViewModel()
        { }

        public ProfileViewModel(HttpClient httpClient, IAccessTokenService accessTokenService) 
        { 
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task UpdateProfile()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            await _httpClient.PutAsync<User>($"profile/updateprofile/{UserId}", this, jwtToken);

            // _toastService.ShowSuccess("Profile info has been saved successfully.");
        }

        public async Task GetProfile()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            ProfileViewModel user = await _httpClient.GetAsync<User>($"profile/getprofile/{UserId}",jwtToken);
            LoadCurrentObject(user);
        }

        public async Task UpdateTheme()
        {
            await _httpClient.GetFromJsonAsync<User>($"profile/updatetheme?userId={this.UserId}&DarkTheme={this.DarkTheme.ToString()}");
        }

        public async Task UpdateNotifications()
        {
            // User user = this;
            // await _httpClient.PutAsJsonAsync($"profile/updatenotifications/{this.UserId}", user);
        }

        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            FirstName = profileViewModel.FirstName;
            LastName = profileViewModel.LastName;
            EmailAddress = profileViewModel.EmailAddress;
            AboutMe = profileViewModel.AboutMe;
            ProfilePictureData = profileViewModel.ProfilePictureData;
            DarkTheme = profileViewModel.DarkTheme;
            Notifications = profileViewModel.Notifications;
            //add more fields
        }

        public static implicit operator ProfileViewModel(User user)
        {
            return new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                AboutMe = user.AboutMe,
                ProfilePictureData = user.ProfilePictureData,
                DarkTheme = user.DarkTheme != null && (long)user.DarkTheme != 0,
                Notifications = user.Notifications != null && (long)user.Notifications != 0,
                UserId = user.UserId
            };
        }

        public static implicit operator User(ProfileViewModel profileViewModel)
        {
            return new User
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                EmailAddress = profileViewModel.EmailAddress,
                AboutMe = profileViewModel.AboutMe,
                ProfilePictureData = profileViewModel.ProfilePictureData,
                DarkTheme = profileViewModel.DarkTheme ? 1 : 0,
                Notifications = profileViewModel.Notifications ? 1 : 0,
                UserId = profileViewModel.UserId
            };
        }
    }
}
