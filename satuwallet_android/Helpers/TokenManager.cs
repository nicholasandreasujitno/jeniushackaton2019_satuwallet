using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using satuwallet_android.Constants;
using satuwallet_android.Models;

namespace satuwallet_android.Helpers
{
    public static class TokenManager
    {
        public static bool IsExist()
        {
            var token = DbContext.GetConnection().Table<Token>().OrderByDescending(x => x.ExpiredDateUtc).FirstOrDefault();
            if (token == null)
            {
                return false;
            }
            return true;
        }

        public static bool IsExpired()
        {
            var token = DbContext.GetConnection().Table<Token>().OrderByDescending(x => x.ExpiredDateUtc).FirstOrDefault();
            if (token != null)
            {
                return token.IsExpired();
                //if (DateTime.UtcNow < token.ExpiredDateUtc)
                //{
                //    return false;
                //}
            }
            return true;
        }

        public static Token GetActiveToken()
        {
            if (IsExpired())
            {
                return null;
            }
            return GetLastToken();
        }

        private static Token GetLastToken()
        {
            var token = DbContext.GetConnection().Table<Token>().OrderByDescending(x => x.ExpiredDateUtc).FirstOrDefault();
            return token;
        }

        public static void SetAuthToken(this HttpClient httpClient)
        {
            // Try get active token
            var token = GetActiveToken();
            if (token != null)
            {
                // Token still active
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
            }
            else if (IsExist() && IsExpired())
            {
                // GENERATE REFRESH TOKEN HERE and set to token
                token = GenerateRefreshToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
            }
            //return token;
        }

        public static Token GenerateAccessToken(string username, string password)
        {
            var restService = new RestService();

            var accessTokenBinding = new AccessTokenBinding()
            {
                client_id = "frontEndWebApp",
                grant_type = "password",
                username = username, //ent_Username.Text,
                password = password, //ent_Password.Text
            };

            var data = restService.ConvertToFormUrlEncoded(accessTokenBinding);
            //var httpResponse = await restService.PostAsync(ApiUrl.Token, data);
            //restService.ProcessResponse(httpResponse, new LoginResponse());

            var token = restService.Post<Token>(ApiUrl.Token, data);
            if (token == null)
            {
                //await page.DisplayAlert("Login", "The username and password is incorrect.", "Ok");
            }
            else
            {
                var database = DbContext.GetConnection();
                database.BeginTransaction();
                try
                {
                    database.Execute("DELETE FROM Token");
                    database.InsertOrReplace(token);
                    database.Commit();
                }
                catch (Exception ex)
                {
                    database.Rollback();
                    throw ex;
                }
            }
            return token;
        }

        public static Token GenerateRefreshToken()
        {
            var token = GetLastToken();
            if (token == null)
            {
                return null;
            }

            var restService = new RestService();

            var refreshTokenBinding = new RefreshTokenBinding()
            {
                client_id = "frontEndWebApp",
                grant_type = "password",
                refresh_token = token.refresh_token
            };

            var data = restService.ConvertToFormUrlEncoded(refreshTokenBinding);
            //var httpResponse = await restService.PostAsync(ApiUrl.Token, data);
            //restService.ProcessResponse(httpResponse, new LoginResponse());

            token = restService.Post<Token>(ApiUrl.Token, data);
            if (token == null)
            {
                //await page.DisplayAlert("Login", "The username and password is incorrect.", "Ok");
            }
            else
            {

                var database = DbContext.GetConnection();
                database.BeginTransaction();
                try
                {
                    database.Execute("DELETE FROM Token");
                    database.InsertOrReplace(token);
                    database.Commit();
                }
                catch (Exception ex)
                {
                    database.Rollback();
                    throw ex;
                }
            }
            return token;
        }

        public static void ClearToken()
        {
            var database = DbContext.GetConnection();
            database.BeginTransaction();
            try
            {
                database.Execute("DELETE FROM Token");
                //database.Execute("DELETE FROM User");
                database.Commit();
            }
            catch (Exception ex)
            {
                database.Rollback();
                throw ex;
            }
        }
    }
}