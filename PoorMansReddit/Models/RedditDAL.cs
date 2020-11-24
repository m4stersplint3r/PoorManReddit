using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace PoorMansReddit.Models
{
    public class RedditDAL
    {
        public string GetPage(string sub)
        {
            string endpoint = $"https://www.reddit.com/r/{sub}/.json?limit=100";
            string output;

            HttpWebRequest request = WebRequest.CreateHttp(endpoint);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                output = rd.ReadToEnd();
            }
            catch (Exception)
            {
                output = "No such sub";
            }
            return output;
        }

        public Rootobject ConvertToPage(string sub)
        {
            string pageData = GetPage(sub);
            if(pageData == "No such sub")
            {
                return new Rootobject();
            }
            Rootobject r = JsonConvert.DeserializeObject<Rootobject>(pageData);

            return r;
        }
        
    }
}
