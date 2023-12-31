using System;
using System.Security.Claims;
using System.Threading.Tasks;
using PC_Designer.Shared;
using PC_Designer.ViewModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace PC_Designer.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILoginViewModel _loginViewModel;
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ILoginViewModel loginViewModel, ILocalStorageService localStorageService)
        {
            _loginViewModel = loginViewModel;
            _localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await GetUserByJWTAsync();

            if (currentUser != null && currentUser.EmailAddress != null)
            {
                //  Create a claims
                var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId));
                var claimRole = new Claim(ClaimTypes.Role, currentUser.Role == null ? "" : currentUser.Role);

                //  Create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimRole }, "serverAuth");
                //  Create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return new AuthenticationState(claimsPrincipal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task<User> GetUserByJWTAsync()
        {
            //  Culling the token from localStorage
            var jwtToken = await _localStorageService.GetItemAsStringAsync("jwt_token");
            if (jwtToken == null) return null;

            return await _loginViewModel.GetUserByJWTAsync(jwtToken);
        }
    }
}
