using System;
using System.Net.Http;
using System.Web;

namespace AMR
{
    public class SearchBrandProxy : IHttpHandler
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
                var apiUrl = "http://172.17.0.16:89/api/brand/searchbrand?name=" + HttpUtility.UrlEncode(name);
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
