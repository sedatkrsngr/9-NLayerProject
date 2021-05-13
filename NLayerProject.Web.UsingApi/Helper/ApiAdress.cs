using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Helper
{
    public class ApiAdress
    {
        public HttpClient Initial()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/");
            client.Timeout = TimeSpan.FromSeconds(5);
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer K313yic05wwpc6le0MMnm0zVRr1f2uiJORchBxIXiNOU9juRgw34yJJzxzMr");
           //Yukardaki şekilde de veri gönderimi yapılır
            return client;
        }
    }
}
