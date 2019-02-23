using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using satuwallet_android.Activities;
using satuwallet_android.Constants;
using satuwallet_android.Extensions;
using satuwallet_android.Interfaces;
using V4Fragment = Android.Support.V4.App.Fragment;

namespace satuwallet_android.Helpers
{
    public class RestService
    {
        public enum ContentType
        {
            [Description("application/www-x-form-urlencoded")]
            FormUrlEncoded,
            [Description("application/json")]
            Json,
        }

        HttpClient client;
        bool setAuthToken;

        public RestService()
        {
            Init();
        }
        public RestService(bool setAuthToken)
        {
            Init();
            this.setAuthToken = setAuthToken;
        }

        private void Init()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public HttpResponseMessage GetAsync(string url)
        {
            if (setAuthToken && url != ApiUrl.Token)
            {
                client.SetAuthToken();
            }
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.Json.GetDescription()));

            try
            {
                var response = client.GetAsync(url).Result;
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Get Error. " + e.Message)
                };
            }
        }

        public T Get<T>(string url) where T : class
        {
            var response = GetAsync(url);
            var objResult = ProcessResponse<T>(response);
            return objResult;
        }
        public void Get(string url, IResponseAction rAction)
        {
            var response = GetAsync(url);
            ProcessResponse(response, rAction);
        }


        public HttpResponseMessage Post(string url, string objJson)
        {
            if (setAuthToken && url != ApiUrl.Token)
            {
                client.SetAuthToken();
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.Json.GetDescription()));

            var content = new StringContent(objJson, Encoding.UTF8, ContentType.Json.GetDescription());
            try
            {
                var response = client.PostAsync(url, content).Result;
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Post Error. " + e.Message)
                };
            }
        }

        public T Post<T>(string url, string objJson) where T : class
        {
            var response = Post(url, objJson);
            var objResult = ProcessResponse<T>(response);
            return objResult;
        }
        public void Post(string url, string objJson, IResponseAction rAction)
        {
            var response = Post(url, objJson);
            ProcessResponse(response, rAction);
        }

        public HttpResponseMessage Post(string url, FormUrlEncodedContent obj)
        {
            if (setAuthToken && url != ApiUrl.Token)
            {
                client.SetAuthToken();
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.FormUrlEncoded.GetDescription()));

            try
            {
                var response = client.PostAsync(url, obj).Result;
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Post Error. " + e.Message)
                };
            }
        }
        public T Post<T>(string url, FormUrlEncodedContent obj) where T : class
        {
            var response = Post(url, obj);
            var objResult = ProcessResponse<T>(response);
            return objResult;
        }

        public void ProcessResponse(HttpResponseMessage response, IResponseAction rAction)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonResult = response.Content.ReadAsStringAsync().Result;
                        rAction.OnOk(jsonResult);
                        break;
                    case HttpStatusCode.BadRequest:
                        var msgResult = response.Content.ReadAsStringAsync().Result;
                        rAction.OnBadRequest(msgResult);
                        break;
                    case HttpStatusCode.Unauthorized:
                        //if (TokenManager.IsExist()) { 
                        //    TokenManager.GenerateRefreshToken();
                        //}
                        var msgResult2 = response.Content.ReadAsStringAsync().Result;
                        rAction.OnUnauthorized(msgResult2);

                        // JUST FORCE LOGOUT
                        
                        //if (currentActivity.GetType() == typeof(AppCompatActivity))
                        //{
                        //    var activity = (AppCompatActivity)actionPage;
                        //currentActivity.RunOnUiThread(() => GoBackToLogin(currentActivity));
                        //}
                        break;
                    //case HttpStatusCode.InternalServerError:
                    //    var msgResult2 = response.Content.ReadAsStringAsync().Result;
                    //    await actionPage.DisplayAlert("Server Error", msgResult2, "Ok");
                    //    break;
                    default:
                        rAction.OnError(response);
                        break;
                }
            }
            catch (Exception e)
            {
                rAction.OnLocalError(e);
                //await actionPage.DisplayAlert("Post Error", "Cannot read response. " + e.Message, "Ok");
            }
        }

        public T ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            //Activity currentActivity = ((MyApp)Application.Context).GetCurrentActivity();
            //var a1 = currentActivity.GetType();
            var actionPage = Application.Context;
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonResult = response.Content.ReadAsStringAsync().Result;
                        var objResult = JsonConvert.DeserializeObject<T>(jsonResult);
                        return objResult;
                    //break;
                    case HttpStatusCode.BadRequest:
                        var msgResult = response.Content.ReadAsStringAsync().Result;
                        //Device.BeginInvokeOnMainThread(async () => { await page.DisplayAlert("Error", "Cannot read response.", "Ok") });
                        //await actionPage.DisplayAlert("Error", msgResult, "Ok");
                        actionPage.TryShowDialog("Error", msgResult, "Ok");
                        break;
                    case HttpStatusCode.InternalServerError:
                        var msgResult2 = response.Content.ReadAsStringAsync().Result;
                        //await actionPage.DisplayAlert("Server Error", msgResult2, "Ok");
                        actionPage.TryShowDialog("Server Error", "Please contact admin. ", "Ok");

                        break;
                    case HttpStatusCode.Unauthorized:
                        // Normally should try refresh token, but for mock up go back to login <-- refresh token probably fail
                        //Application.Current.MainPage = new NavigationPage(new LoginPage());
                        if (actionPage.GetType() == typeof(AppCompatActivity))
                        {
                            var activity = (AppCompatActivity)actionPage;
                            activity.RunOnUiThread(() => GoBackToLogin(activity));
                        }
                        break;
                    default:
                        actionPage.TryShowDialog("Post Error", "Cannot read response.", "Ok");
                        break;
                }
            }
            catch (Exception e)
            {
                actionPage.TryShowDialog("Post Error", "Cannot read response." + e.Message, "Ok");
            }
            return null;
        }

        public string ConvertToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public FormUrlEncodedContent ConvertToFormUrlEncoded(Object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            var postData = new List<KeyValuePair<string, string>>();
            foreach (PropertyInfo property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual)
                {
                    continue;
                }
                postData.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(obj, null) + ""));
            }
            var content = new FormUrlEncodedContent(postData);
            return content;
        }

        public void GoBackToLogin(Activity activity)
        {
            var i = new Intent(Application.Context, typeof(LoginActivity));
            activity.StartActivity(i);
            activity.FinishAffinity();
        }
    }
}