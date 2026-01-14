using System;
using System.Net.Http;
using System.Web;

namespace ProdContact
{
    public class SearchProjectProxy : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.QueryString["name"];

            if (string.IsNullOrWhiteSpace(name))
            {
                context.Response.StatusCode = 400;
                context.Response.Write("{\"error\":\"Missing name parameter\"}");
                return;
            }

            using (var client = new HttpClient())
            {
                var apiUrl = "http://172.17.0.16:90/api/project/searchproject?name=" + HttpUtility.UrlEncode(name);
                var task = client.GetAsync(apiUrl);
                task.Wait();
                var response = task.Result;

                context.Response.ContentType = "application/json";
                context.Response.Write(response.Content.ReadAsStringAsync().Result);
            }
        }

        public bool IsReusable => false;
    }
}
