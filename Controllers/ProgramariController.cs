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
    public class ProgramariController : Controller
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KmwmiyQBBEcMB2TQuqh8d6xvaOWBzQiBvczF1fIA",
            BasePath = "https://medicalapp-d0eed-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        // GET: api/values
        [HttpGet]
        public List<Models.Programare> Get()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("programari");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Models.Programare>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Models.Programare>(((JProperty)item).Value.ToString()));
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
        public void Create([FromBody] Models.Programare programare)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = programare;
                PushResponse response = client.Push("programari/", data);
                data.id = response.Result.name;
                SetResponse setResponse = client.Set("programari/" + data.id, data);
                ModelState.AddModelError(String.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete([FromBody] Models.Programare programare)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("programari/" + programare.id);
        }
    }
}

