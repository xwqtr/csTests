namespace CommonApiAccessProvider.ApiAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.IO;
    using CurrencyService.Common.Interfaces;
    using Newtonsoft.Json;
    public class WebRequestBasedApiAccess : IApiAccessProvider
    {

        public async Task<T> GetData<T>(string uri,string[] headers =null)
        {
            var requestUri = new Uri(uri);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            if (headers != null)
            {
                foreach (var h in headers)
                {
                    request.Headers.Add(h);
                }
            }


            var response = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream());
            var responseString = await sr.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
