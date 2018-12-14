using CommonApiAccessProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CurrencyService.Common.Interfaces;
using Newtonsoft.Json;

namespace CommonApiAccessProvider.ApiAccess
{
    public class WebClientBasedApiAccess : IApiAccessProvider
    {
        public T GetData<T>(string uri, string[] headers = null)
        {
            using (WebClient wc = new WebClient())
            {
                if (headers != null)
                {
                    foreach (var h in headers)
                    {
                        wc.Headers.Add(h);
                    }
                }
                Stream data = wc.OpenRead(uri);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return JsonConvert.DeserializeObject<T>(s);
            }
               
        }
    }
}
