using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string _url = "http://agl-developer-test.azurewebsites.net/people.json";
            List<Owners> objData = new List<Owners>();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(_url);



            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            HttpResponseMessage response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                objData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Owners>>(responseData);
            }

            DataTable dt = new DataTable();
            DataColumn dcGender = new DataColumn("Gender");
            dt.Columns.Add(dcGender);
            DataColumn dcCatName = new DataColumn("CatName");
            dt.Columns.Add(dcCatName);

            var maleOwners = objData.Where(m => m.Gender == "Male");         

            foreach (var male in maleOwners)
            {
                if (male.Pets != null)
                {
                    var cats = male.Pets.Where(c => c.Type == "Cat");

                    foreach (var cat in cats)
                    {
                        DataRow dr= dt.Rows.Add();
                        dr["Gender"] = "Male";
                        dr["CatName"] = cat.Name;
                    }
                }

            }

            var femaleOwners = objData.Where(m => m.Gender == "Female");

            foreach (var female in femaleOwners)
            {
                if (female.Pets != null)
                {
                    var cats = female.Pets.Where(c => c.Type == "Cat");

                    foreach (var cat in cats)
                    {
                        DataRow dr = dt.Rows.Add();
                        dr["Gender"] = "Female";
                        dr["CatName"] = cat.Name;
                    }
                }

            }

            Console.ReadKey();
        }
    }
}
