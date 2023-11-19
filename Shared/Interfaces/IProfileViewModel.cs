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

        public Task UpdateProfile();
        public Task GetProfile();
    }
}