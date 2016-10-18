using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace apicore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.orchestrate.io/v0/Values?limit=100")
            };

            client.DefaultRequestHeaders.Add("Authorization","Basic OTI2MGQzNzMtYjEyMC00YzY2LThlNTQtNTZjMmNlNjNhYjk2Og==");
            
            var response = client.GetAsync("").Result;
            var root = response.Content.ReadAsAsync<RootObject>().Result;

            List<Value> values = new List<Value>();

            foreach (var result in root.results){
                Value value = new Value(){
                    id = result.path.key,
                    name = result.value.name
                };
                values.Add(value);
            }

            return values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Value Get(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.orchestrate.io/v0/Values/" + id)
            };

            client.DefaultRequestHeaders.Add("Authorization","Basic OTI2MGQzNzMtYjEyMC00YzY2LThlNTQtNTZjMmNlNjNhYjk2Og==");
            
            var response = client.GetAsync("").Result;
            var value = response.Content.ReadAsAsync<Value>().Result;
            value.id = id;
            return value;
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Value value)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.orchestrate.io/v0/Values")
            };

            client.DefaultRequestHeaders.Add("Authorization","Basic OTI2MGQzNzMtYjEyMC00YzY2LThlNTQtNTZjMmNlNjNhYjk2Og==");

            HttpResponseMessage response = client.PostAsJsonAsync("", value).Result;
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(string id, [FromBody]Value value)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.orchestrate.io/v0/Values/" + id)
            };

            client.DefaultRequestHeaders.Add("Authorization","Basic OTI2MGQzNzMtYjEyMC00YzY2LThlNTQtNTZjMmNlNjNhYjk2Og==");

            return client.PutAsJsonAsync("", value).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.orchestrate.io/v0/Values/" + id)
            };

            client.DefaultRequestHeaders.Add("Authorization","Basic OTI2MGQzNzMtYjEyMC00YzY2LThlNTQtNTZjMmNlNjNhYjk2Og==");

            return client.DeleteAsync("").Result;
        }
    }
}
