using BidOneCoding.Respository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidOneCoding.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public ValidationController(IPersonRepository personRepository) => _personRepository = personRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidateFirstName(string firstName)
        {
            if (_personRepository.GetPerson(firstName) != null)
                return Json(data: "First name already exists");

            return Json(data: true);
        }
    }
}
