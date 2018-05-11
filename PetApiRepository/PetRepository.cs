using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PetApiRepository
{
    public class PetRepository<T> : IRepository<T> where T:class
    {
        public List<T> GetAll()
        {
            List<T> objData = new List<T>();

            string url = ConfigurationManager.AppSettings["url"];

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                objData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(responseData);
            }   
            
            return objData;

        }
    }
}
