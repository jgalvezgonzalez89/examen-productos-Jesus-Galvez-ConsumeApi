using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using examen_productos_Jesus_Galvez_ConsumeApi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace examen_productos_Jesus_Galvez_ConsumeApi.Controllers
{

    public class ProductosController : Controller
    {
        // GET: Productos

        string Baseurl = "http://localhost:27495/";

        public async Task<ActionResult> Index()
        {
            List<Productos> prodInfo = new List<Productos>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Productos/");
                if (Res.IsSuccessStatusCode)
                {
                    var ProdResp = Res.Content.ReadAsStringAsync().Result;
                    prodInfo = JsonConvert.DeserializeObject<List<Productos>>(ProdResp);
                }
                return View(prodInfo);
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Productos productos)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27495/api/Productos");
                var postTask = client.PostAsJsonAsync<Productos>("Productos", productos);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacte al administrador");
            return View(productos);
        }

        public ActionResult Edit(int id)
        {
            Productos productos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27495/");
                var responseTask = client.GetAsync("api/Productos/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Productos>();
                    readTask.Wait();
                    productos = readTask.Result;
                }

            }
            return View(productos);
        }

        [HttpPost]
        public ActionResult Edit(Productos productos)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27495/");
                var putTask = client.PutAsJsonAsync($"api/Productos/{productos.id}", productos);
                putTask.Wait();
                var result = putTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(productos);
        }

        public ActionResult Delete (int id)
        {
            Productos productos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27495/");
                var responseTask = client.GetAsync("api/Productos/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Productos>();
                    readTask.Wait();
                    productos = readTask.Result;

                }
            }
            return View(productos);
        }


        [HttpPost]
        public ActionResult Delete(Productos productos, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:27495/");
                var deleteTask = client.DeleteAsync($"api/Productos/" + id.ToString());
                var result = deleteTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(productos);
        }

    }
}