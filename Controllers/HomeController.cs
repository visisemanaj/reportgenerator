using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReportGenerator.DTOs;
using ReportGenerator.Validation;

namespace ReportGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var budget = JsonConvert.DeserializeObject<Budget>(System.IO.File.ReadAllText(Server.MapPath("~/App_Data/BudgetConfiguration.json")));

            var timeTrackerUserLogins = new[] {"John", "Kyle", "Melissa", "Rose"}.ToList();
            var timeTrackerClients = new[] {"Client1", "Client2"}.ToList();
            var validator = new ConfigurationValidator(timeTrackerClients, timeTrackerUserLogins);

            List<string> errors;
            if (validator.Validate(budget, out errors) == false)
            {
                Debug.WriteLine($"JSON file validation failed!");

                foreach (var i in errors)
                {
                    Debug.WriteLine($"Error! {i}");
                }
            }

            return View(budget);
        }
    }
}