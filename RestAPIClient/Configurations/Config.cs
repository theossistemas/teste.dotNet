using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestAPIClient.Configurations
{
    public class Config
    {
        private Config() { }

        public static IDictionary<String, String> Urls { get; private set; } = new Dictionary<String, String>();

        public static void Initializer()
        {
            if (Urls.Count == 0)
            {
                String configFilePath = Path.Combine(AppContext.BaseDirectory, "urls.json");

                RestUrl[] urls = null;

                using (StreamReader reader = new StreamReader(configFilePath))
                    urls = JsonConvert.DeserializeObject<RestUrl[]>(reader.ReadToEnd());

                foreach (RestUrl url in urls)
                    Urls.Add(url.Nome, url.Url);
            }
        }

        public static String GetUrl(String nome)
        {
            Initializer();

            return Urls[nome];
        }
    }

    public class RestUrl
    {
        public String Nome { get; set; }

        public String Url { get; set; }
    }
}
