using System;
using System.Security.Claims;
using System.Threading.Tasks;
using PC_Designer.Shared;
using PC_Designer.Shared.Services;
using PC_Designer.ViewModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PC_Designer.Client.Handlers
{
    // public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    // {

    //     private readonly HttpClient _httpClient;
    //     private readonly ILocalStorageService _localStorageService;

    //     public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
    //     {
    //         _httpClient = httpClient;
    //         _localStorageService = localStorageService;
    //     }

    //     public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    //     {
    //         User currentUser = await GetUserByJWTAsync(); //_httpClient.GetFromJsonAsync<User>("user/getcurrentuser");

    //         if (currentUser != null && currentUser.EmailAddress != null)
    //         {
    //             //create a claims
    //             var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
    //             var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId));
    //             //create claimsIdentity
    //             var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");
    //             //create claimsPrincipal
    //             var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    //             return new AuthenticationState(claimsPrincipal);
    //         }
    //         else
    //             return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    //     }

    //     public async Task<User> GetUserByJWTAsync()
    //     {
    //         //pulling the token from localStorage
    //         var jwtToken = await _localStorageService.GetItemAsStringAsync("jwt_token");
    //         if(jwtToken == null) return null;
        
    //         //preparing the http request
    //         var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt");
    //         requestMessage.Content = new StringContent(jwtToken);
        
    //         requestMessage.Content.Headers.ContentType
    //             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        
    //         //making the http request
    //         var response = await _httpClient.SendAsync(requestMessage);
        
    //         var responseStatusCode = response.StatusCode;
    //         var returnedUser = await response.Content.ReadFromJsonAsync<User>();
        
    //         //returning the user if found
    //         if(returnedUser != null) return await Task.FromResult(returnedUser);
    //         else return null;
    //     }
    // }

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

    	private readonly ILoginViewModel _loginViewModel;
        private readonly IAccessTokenService _accessTokenService;

        public CustomAuthenticationStateProvider(ILoginViewModel loginViewModel, 
            IAccessTokenService accessTokenService)
        {
            _loginViewModel = loginViewModel;
            _accessTokenService = accessTokenService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await GetUserByJWTAsync();

            if (currentUser != null && currentUser.EmailAddress != null)
            {
                //create claimsPrincipal
                var claimsPrincipal = GetClaimsPrinciple(currentUser);
                return new AuthenticationState(claimsPrincipal);
            }
            else
            {
                await _accessTokenService.RemoveAccessTokenAsync("jwt_token");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task MarkUserAsAuthenticated()
        {
            var user = await GetUserByJWTAsync();
            var claimsPrincipal = GetClaimsPrinciple(user);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _accessTokenService.RemoveAccessTokenAsync("jwt_token");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task<User> GetUserByJWTAsync()
        {
            //pulling the token from localStorage
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            if (jwtToken == null) return null;

            jwtToken = $@"""{jwtToken}""";
            return await _loginViewModel.GetUserByJWTAsync(jwtToken);
        }

        private ClaimsPrincipal GetClaimsPrinciple(User currentUser)
        {
            //create a claims
            var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId));
            // var claimRole = new Claim(ClaimTypes.Role, currentUser.Role == null ? "" : currentUser.Role);

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");    //  claimRole
            //create claimsPrincipal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }
}