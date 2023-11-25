using PC_Designer.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PC_Designer.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string ProfilePictureUrl { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; } = null!;
        public bool DarkTheme { get; set; }
        public bool Notifications { get; set; }
        public bool IsEditing { get; set; } = false;

        private HttpClient _httpClient = null!;


        public ProfileViewModel()
        { }

        public ProfileViewModel(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task UpdateProfile()
        {
            User user = this;
            await _httpClient.PutAsJsonAsync("user/updateprofile/" + this.UserId, user);
            Message = "Profile updated successfully";
        }

        public async Task GetProfile()
        {
            User? user = await _httpClient.GetFromJsonAsync<User>("user/getprofile/" + this.UserId);
            if (user != null)
            {
                LoadCurrentObject(user);
                Message = "Profile loaded successfully";
            }
        }

        public async Task Save()
        {   
            try
            {
                await _httpClient.GetFromJsonAsync<User>($"user/updatetheme?userId={this.UserId}&DarkTheme={this.DarkTheme.ToString()}");
                await _httpClient.GetFromJsonAsync<User>($"user/updatenotifications?userId={this.UserId}&Notifications={this.Notifications.ToString()}");
            }
            catch (HttpRequestException httpEx) { Message = $"HTTP Request Error: {httpEx.Message}"; }
            catch (Exception ex) { Message = $"Error: {ex.Message}"; }
        }

        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            FirstName = profileViewModel.FirstName;
            LastName = profileViewModel.LastName;
            EmailAddress = profileViewModel.EmailAddress;
            ProfilePictureUrl = profileViewModel.ProfilePictureUrl;
            DateOfBirth = profileViewModel.DateOfBirth;
            AboutMe = profileViewModel.AboutMe;
            this.DarkTheme = profileViewModel.DarkTheme;
            this.Notifications = profileViewModel.Notifications;
            //add more fields
        }

        public static implicit operator ProfileViewModel(User user)
        {
            return new ProfileViewModel
            {
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                EmailAddress = user.EmailAddress ?? string.Empty,
                ProfilePictureUrl = user.ProfilePictureUrl ?? string.Empty,
                DateOfBirth = user.DateOfBirth ?? DateTime.MinValue,
                AboutMe = user.AboutMe ?? string.Empty,
                DarkTheme = user.DarkTheme != null && user.DarkTheme != 0,
                Notifications = user.Notifications != null && user.Notifications != 0,
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
                ProfilePictureUrl = profileViewModel.ProfilePictureUrl,
                DateOfBirth = profileViewModel.DateOfBirth,
                AboutMe = profileViewModel.AboutMe,
                DarkTheme = profileViewModel.DarkTheme ? 1 : 0,
                Notifications = profileViewModel.Notifications ? 1 : 0,
                UserId = profileViewModel.UserId
            };
        }
    }
}
