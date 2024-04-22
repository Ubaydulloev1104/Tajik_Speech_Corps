﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.User.Queries;
using TSR_Accoun_Application.Contracts.User.Responses;
using System;

namespace TSR_Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly IdentityHttpClient _identityHttp;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http,
            IdentityHttpClient identityHttp)
        {
            _localStorageService = localStorageService;
            _http = http;
            _identityHttp = identityHttp;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authToken = await GetTokenAsync();
            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (authToken != null && !string.IsNullOrEmpty(authToken.AccessToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken.AccessToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.AccessToken.Replace("\"", ""));
                    _identityHttp.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.AccessToken.Replace("\"", ""));
                }
                catch
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        private async Task<JwtTokenResponse> GetTokenAsync()
        {
            var token = await _localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
            if (token == null)
            {
                return null;
            }

            if (token.AccessTokenValidateTo <= DateTime.Now)
            {
                var refreshResponse = await _identityHttp.PostAsJsonAsync("auth/refresh",
                    new GetAccessTokenUsingRefreshTokenQuery
                    {
                        RefreshToken = token.RefreshToken,
                        AccessToken = token.AccessToken
                    });
                if (!refreshResponse.IsSuccessStatusCode)
                {
                    return null;
                }

                token = await refreshResponse.Content.ReadFromJsonAsync<JwtTokenResponse>();
                await _localStorageService.SetItemAsync("authToken", token);
            }

            return token;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return jwtObject.Claims;
        }
    }
}