using CunsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CunsumeWebAPI.Controllers
{
    public class ConsumeExController : Controller
    {
        public async Task<IActionResult> List()
        {
            IEnumerable<data> data = await GetData();
            return View(data);
         
        }

        public async Task<IEnumerable<data>> GetData()
        {
            IEnumerable<data> obj = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responce = client.GetAsync("posts");
                responce.Wait();

                var data = responce.Result;
                if (data.IsSuccessStatusCode)
                {
                    var read = data.Content.ReadAsAsync<List<data>>();
                    read.Wait();
                    obj = read.Result;

                }
            }
            return obj;
        }
        public async Task<IEnumerable<student>>GetAllData()
        {
            IEnumerable<student> obj = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7287/api/");
                var responce = client.GetAsync("Curd/GetAllData");
                responce.Wait();

                var data = responce.Result;
                if (data.IsSuccessStatusCode)
                {
                    var read = data.Content.ReadAsAsync<List<student>>();
                    read.Wait();
                    obj = read.Result;

                }
            }
            return obj;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<student> obj = await GetAllData();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(student obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7287/api/");
                var postdata = client.PostAsJsonAsync<student>("Curd/Insert", obj);
                var res = postdata.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ConsumeEx");
                }
                else
                {
                    ModelState.AddModelError("", "Something Went Wrong");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            student obj = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7287/api/");
                var responce = client.GetAsync("Curd/Details?Id=" + Id.ToString());
                var res = responce.Result;
                if (res.IsSuccessStatusCode)
                {
                    var data= res.Content.ReadAsAsync<student>();
                    obj = data.Result;
                }
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(int id, student obj )
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7287/api/");
                var responce = client.PutAsJsonAsync<student>("Curd/Update", obj);
                var res = responce.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ConsumeEx");
                }
                else
                {
                    ModelState.AddModelError("", "Something Went Wrong");
                }
            }
            return View();
        }
    }
}
