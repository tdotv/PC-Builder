using System.Net;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace PC_Designer.Client.Handlers
{
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        public ILocalStorageService _localStorageService { get; set; }
        public CustomAuthorizationHandler(ILocalStorageService localStorageService)
        {
            //injecting local storage service
            _localStorageService = localStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //getting token from the localstorage
            var jwtToken = await _localStorageService.GetItemAsync<string>("jwt_token");
            
            //adding the token in authorization header
            if (jwtToken != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            if (request.RequestUri.AbsoluteUri.Contains("registeruser"))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            
            //sending the request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}