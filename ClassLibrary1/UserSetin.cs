using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
   internal  class UserSetin
    {
        public string path { get; private set; }
        public UserSetin(string path)
        {
            this.path = path;
        }


        public person_date GetUserInfo(string iin)
        {
            person_date data = new person_date();

            return data;
        }

        public async Task<(bool, string)> GetUserDataAsync(person_date date)
        {
            var client1 = new RestClient(path);


            string query = string.Format("/api-form/load-info-by-iin/?iin={0}&params[city_check]=true", date.Lin);

            var request = new RestRequest(query);

            RestResponse result = await client1.ExecuteAsync(request);

            if (result.IsSuccessful)
            {
                string json = result.Content;

                if (!string.IsNullOrWhiteSpace(json))
                {
                    return (false, "Contents is Empty");
                }
                else
                {
                    date = JsonConvert.DeserializeObject<person_date>(json);
                    return (result.IsSuccessful, result.StatusDescription);
                }
            }
            else
            {
                return (result.IsSuccessful, result?.ErrorException.Message);
            }
        }
    }
}
