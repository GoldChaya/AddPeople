using Homework_March_27.Data;
using Homework_March_27.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_March_27.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString ="Data Source=.\\sqlexpress;Initial Catalog=PeopleCars;Integrated Security=True";
        public IActionResult Index()
        {
            Manager mgr = new(_connectionString);
            PeopleViewModel vm = new();
            vm.People = mgr.GetAll();
            if (TempData["success-message"]!=null)
            {
                vm.Message = (string)TempData["success-message"];
            }

            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(List<Person> people)
        {
            Manager mgr = new Manager(_connectionString);
            List<Person> peopleToAdd = new();
            foreach (var person in people)
            {
                if(person.FirstName !=null && person.LastName !=null)
                {
                    peopleToAdd.Add(person);
                }
            }
            mgr.AddPerson(peopleToAdd);
            TempData["success-message"] = $"{peopleToAdd.Count} Added Successfully";
            return Redirect("/Home/Index");
        }
        public IActionResult Add()
        {
            return View();
        }

    }
}