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
    public class DoctorsController : Controller
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KmwmiyQBBEcMB2TQuqh8d6xvaOWBzQiBvczF1fIA",
            BasePath = "https://medicalapp-d0eed-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        // GET: api/values

        [HttpGet]
        public List<Models.Doctor> Get()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("doctori");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Models.Doctor>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Models.Doctor>(((JProperty)item).Value.ToString()));
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
        public void Create(Models.Doctor specialty)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = specialty;
                PushResponse response = client.Push("doctori/", data);
                data.id = response.Result.name;
                SetResponse setResponse = client.Set("doctori/" + data.id, data);
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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
// DOCOTRI
// j3NSuUC6u0fXNlEaEELx
// VJV6v8GBzasNTOWgGEeN
// gxkPzGANUfGrEwPt8hg8
// fvacP1tjJxOVSBz9At1G
// kpTPMxZzNxbjPIFd6Gu8
// 6RMpTjyKL3v8Ndb8qfyS

// SERVICII
// C3E6C99B-9A23-4B3B-B3AF-EB81BB8097F5
// 4E24F480-0707-4A99-82BC-8EE9E45E609B
// BBF3D149-F356-4575-9959-8480E0C6B88A
// 65D330E4-F11A-4AA1-B69B-1C34931003EB
// EA532BFE-AB14-4C0E-B5E5-E4543275D10C
// A0CB0339-F800-441C-838C-89DE59262160
// 163C8C17-536A-4838-BF11-6848BA189B0C


