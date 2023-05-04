using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalFacultateBackend2.Controllers
{
    [Route("api/[controller]")]
    public class SpecialtiesController : Controller
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KmwmiyQBBEcMB2TQuqh8d6xvaOWBzQiBvczF1fIA",
            BasePath = "https://medicalapp-d0eed-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        // GET: api/values
        [HttpGet]
        public List<Models.Specialty> Get()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("specialitati");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Models.Specialty>();
            foreach(var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Models.Specialty>(((JProperty)item).Value.ToString()));
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

        [HttpPost]
        public ActionResult Create(Models.Specialty specialty)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = specialty;
                PushResponse response = client.Push("specialitati/", data);
                data.id = response.Result.name;
                SetResponse setResponse = client.Set("specialitati/" + data.id, data);
                ModelState.AddModelError(String.Empty, "Added Successfully");
            }
            catch (Exception ex) {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View();
        }

    }
}

