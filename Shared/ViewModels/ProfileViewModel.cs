using PC_Designer.Shared;
using System.Net.Http.Json;

#nullable disable

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

        public ProfileViewModel()
        { }

        public ProfileViewModel(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task UpdateProfile()
        {
            User user = this;
            await _httpClient.PutAsJsonAsync("profile/updateprofile/" + this.UserId, user);
        }

        public async Task UpdateProfileWithImage(byte[] imageBytes)
        {
            User user = this;
            user.ProfilePictureData = imageBytes;

            await _httpClient.PutAsJsonAsync("profile/updateprofile/" + this.UserId, user);
        }

        public async Task GetProfile()
        {
            User user = await _httpClient.GetFromJsonAsync<User>("profile/getprofile/" + this.UserId);
            if (user != null)
            {
                LoadCurrentObject(user);
            }
        }

        public async Task Save()
        {   
            try
            {
                await _httpClient.GetFromJsonAsync<User>($"profile/updatetheme?userId={this.UserId}&DarkTheme={this.DarkTheme.ToString()}");
                await _httpClient.GetFromJsonAsync<User>($"profile/updatenotifications?userId={this.UserId}&Notifications={this.Notifications.ToString()}");
            }
            catch (HttpRequestException httpEx) { Console.WriteLine($"HTTP Request Error: {httpEx.Message}"); }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
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
            this.FirstName = profileViewModel.FirstName;
            this.LastName = profileViewModel.LastName;
            this.EmailAddress = profileViewModel.EmailAddress;
            this.AboutMe = profileViewModel.AboutMe;
            this.ProfilePictureData = profileViewModel.ProfilePictureData;
            this.DarkTheme = profileViewModel.DarkTheme;
            this.Notifications = profileViewModel.Notifications;
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
                DarkTheme = (user.DarkTheme == null || (long)user.DarkTheme == 0) ? false : true,
                Notifications = (user.Notifications == null || (long)user.Notifications == 0) ? false : true,
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
