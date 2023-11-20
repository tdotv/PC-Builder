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
        public byte[] DateOfBirth { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        private HttpClient _httpClient = null!;

        public ProfileViewModel()
        { }

        public ProfileViewModel(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task UpdateProfile()
        {
            User user = this;
            await _httpClient.PutAsJsonAsync("user/updateprofile/1", user);
            // Message = "Profile updated successfully";
        }

        public async Task GetProfile()
        {
            User? user = await _httpClient.GetFromJsonAsync<User>("user/getprofile/1");
            LoadCurrentObject(user);
            // Message = "Profile loaded successfully";
        }

        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            FirstName = profileViewModel.FirstName;
            LastName = profileViewModel.LastName;
            EmailAddress = profileViewModel.EmailAddress;
            ProfilePictureUrl = profileViewModel.ProfilePictureUrl;
            DateOfBirth = profileViewModel.DateOfBirth;
            AboutMe = profileViewModel.AboutMe;
            //add more fields
        }

        public static implicit operator ProfileViewModel(User user)
        {
            return new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                ProfilePictureUrl = user.ProfilePictureUrl,
                DateOfBirth = user.DateOfBirth,
                AboutMe = user.AboutMe,
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
                UserId = profileViewModel.UserId
            };
        }
    }
}
