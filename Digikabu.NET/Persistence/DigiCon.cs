﻿

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Digikabu.NET.Persistence
{
    //Digikabu API Controller
    class DigiCon
    {
        private static string username;
        private static string password;

        ///Will be used for API Requests
        static HttpClient client = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer()
        });

        ///Will contain the Bearer auth key
        static string auth = "";

        /// <summary>
        /// AuthorizeAsync will use the given username and password to check if said credentials are authorized.
        /// If the user is authorized, the auth key will be saved.
        /// </summary>
        /// <param name="un"></param>
        /// <param name="pw"></param>
        /// <returns>True if user is authorized, false if that is not the case</returns>
        public static async Task<bool> AuthorizeAsync(string un, string pw)
        {
            try
            {
                ///in the httpContent we will choose the content that our webRequest sends via POST
                ///The first part of the StringContent is this case our JSON with the username and password
                ///The second part is the encoding, for which we use UTF8
                ///The last part is the Content-Type for which we are using "application/json" since we are posting a JSON
                var httpContent = new StringContent(
                    $"{{\"UserName\":\"{un}\"," +
                    $"\"password\":\"{pw}\"}}",
                    Encoding.UTF8, "application/json");

                ///Using PostAsync we will send a POST request to the API.
                var response = await client.PostAsync("https://digikabu.de/api/authenticate", httpContent);

                ///After sending our POST request we are awaiting a response from the API.
                ///As soon as we receive said response, we will save it as a String
                String responseString = await response.Content.ReadAsStringAsync();

                ///This checks if we are getting an Bearer authentication key as response.
                ///If we are not, then we will simply return false, since our authorization failed.
                if (responseString.Contains("Username or password is incorrect"))
                {
                    return false;
                }

                ///Since the API uses Bearer authentification, the auth key needs to be in following format:
                ///"Bearer [authkey]"
                auth = "Bearer " + responseString.Replace("\"", "");

                SetLoginData(un, pw);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static void SetLoginData(string un, string pw)
        {
            username = un;
            password = pw;
        }

        /// <summary>
        /// GetScheduleOfToday gets the Schedule of the Current Day using the Digikabu API
        /// </summary>
        /// <returns>The schedule of the Current Day as JSON string</returns>
        public static async Task<string> GetScheduleOfTodayAsync()
        {
            if (/*await AuthorizeAsync(username, password) == */true)
            {

                try
                {
                    RemoveAuthHeader();
                    client.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = await client.GetAsync("https://digikabu.de/api/stundenplan?datum=" + DateTime.Now.ToString("yyyy-MM-dd"));

                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;

                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return null;
        }

        /// <summary>
        /// RemoveAuthHeader is used to remove the Bearer Authorization Token from the Header of a Request
        /// </summary>
        public static void RemoveAuthHeader()
        {
            try
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }
            catch (Exception e)
            {

            }
        }
    }
}
