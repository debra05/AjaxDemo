using AjaxDemo.Data;
using AjaxDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AjaxDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
            "Data Source=.\\sqlexpress;Initial Catalog=People;Integrated Security=True;Trust Server Certificate=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            //Thread.Sleep(2000);
            var repo = new PeopleRepo(_connectionString);
            return Json(repo.GetAll());
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Add(person);
        }
        [HttpPost]
        public void EditPerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Edit(person);
        }
        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Delete(id);
        }
    }
}
