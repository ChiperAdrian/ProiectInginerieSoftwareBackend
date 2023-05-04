using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MedicalFacultateBackend2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalFacultateBackend2.Controllers
{
    [Route("api/[controller]")]
    public class ConsultatiiController : Controller
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KmwmiyQBBEcMB2TQuqh8d6xvaOWBzQiBvczF1fIA",
            BasePath = "https://medicalapp-d0eed-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        // GET: api/values
        [HttpGet]
        public List<Models.Consultatie> Get()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("consultatii");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Models.Consultatie>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Models.Consultatie>(((JProperty)item).Value.ToString()));
            }
            //return new string[] { "value1", "value2" };
            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Create([FromBody] Models.Consultatie consultatie)
        {
            Console.WriteLine(consultatie);
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = consultatie;
                
                PushResponse response = client.Push("consultatii/", data);
                data.id = response.Result.name;
                SetResponse setResponse = client.Set("consultatii/" + data.id, data);
                ModelState.AddModelError(String.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] Consultatie consultatie)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("consultatii/" + consultatie.id, consultatie);

        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete([FromBody] Consultatie consultatie)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("consultatii/" + consultatie.id);
        }
    }
}

