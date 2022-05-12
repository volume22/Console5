using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibrary.bll
{
    class UserSetting
    {
        public string path { get; private set; }
        public UserSetting(string path)
        {
            this.path = path;
        }


        public PersonalData GetUserInfo(string iin)
        {
            PersonalData data = new PersonalData();

            return data;
        }

        public async Task<(bool, string)> GetUserDataAsync(PersonalData date)
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
                    date = JsonConvert.DeserializeObject<PersonalData>(json);
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

