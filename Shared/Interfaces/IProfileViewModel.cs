using System.Threading.Tasks;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public interface IProfileViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public bool Notifications { get; set; }
        public bool DarkTheme { get; set; }
        public bool IsEditing { get; set; }
        
        public Task Save();
        public Task UpdateProfile();
        public Task GetProfile();
    }
}