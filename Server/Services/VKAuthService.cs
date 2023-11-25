// using PC_Designer.Shared;
// using PC_Designer.ViewModels;

// public class VkAuthService : IVkUserViewModel
// {
//     private readonly IDbService _dbService;

//     public VkAuthService(IDbService dbService) { _dbService = dbService ;}

//     public async Task<VkUserViewModel> AuthenticateWithVkAsync(string accessToken, int vkUserId)
//     {
//         string query = "SELECT * FROM dbo.VkUsers WHERE VkUserId = @VkUserId";
//         VkUserViewModel existingUser = _dbService.GetAsync<VkUser>(query, new { VkUserId = vkUserId });

//         if(existingUser == null)
//         {
//             string insertQuery = "INSERT INTO dbo.VkUsers (VkUserId, AccessToken) VALUES (@VkUserId, @AccessToken)";
//             await _dbService.Insert<VkUserViewModel>(insertQuery, new { VkUserId = vkUserId, AccessToken = accessToken });

//             existingUser = _dbService.GetAsync<VkUserViewModel(query, new { VkUserId = vkUserId });
//         }

//         return existingUser;
//     }
// }