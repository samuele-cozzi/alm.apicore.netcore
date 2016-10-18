using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace apicore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Orchestrate settings_orchestrate;
        private HttpClient client;

        public ValuesController(IOptions<Orchestrate> settings){
            settings_orchestrate = settings.Value;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(string.Format("{0}{1}?limit=100", 
                    settings_orchestrate.Server, 
                    settings_orchestrate.Collection))
            };
            client.DefaultRequestHeaders.Add("Authorization",settings_orchestrate.Authorization);

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
            client = new HttpClient
            {
                BaseAddress = new Uri(string.Format("{0}{1}/{2}", 
                    settings_orchestrate.Server, 
                    settings_orchestrate.Collection,
                    id))
            };
            client.DefaultRequestHeaders.Add("Authorization",settings_orchestrate.Authorization);

            var response = client.GetAsync("").Result;
            var value = response.Content.ReadAsAsync<Value>().Result;
            value.id = id;
            return value;
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Value value)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(string.Format("{0}{1}", 
                    settings_orchestrate.Server, 
                    settings_orchestrate.Collection))
            };
            client.DefaultRequestHeaders.Add("Authorization",settings_orchestrate.Authorization);

            HttpResponseMessage response = client.PostAsJsonAsync("", value).Result;
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(string id, [FromBody]Value value)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(string.Format("{0}{1}/{2}", 
                    settings_orchestrate.Server, 
                    settings_orchestrate.Collection,
                    id))
            };
            client.DefaultRequestHeaders.Add("Authorization",settings_orchestrate.Authorization);

            return client.PutAsJsonAsync("", value).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(string id)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(string.Format("{0}{1}/{2}", 
                    settings_orchestrate.Server, 
                    settings_orchestrate.Collection,
                    id))
            };
            client.DefaultRequestHeaders.Add("Authorization",settings_orchestrate.Authorization);

            return client.DeleteAsync("").Result;
        }

        protected override void Dispose(bool disposing){
            client.Dispose();
            base.Dispose(disposing);
        }
    }
}
