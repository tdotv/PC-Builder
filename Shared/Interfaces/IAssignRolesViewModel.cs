using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public interface IAssignRolesViewModel
    {
        public IEnumerable<User> AllUsers { get; }

        public Task LoadAllUsers();
        public Task AssignRole(long userId, string role);
        public Task DeleteUser(long userId);
    }
}