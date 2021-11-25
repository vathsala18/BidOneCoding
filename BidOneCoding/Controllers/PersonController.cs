using BidOneCoding.Models;
using BidOneCoding.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BidOneCoding.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonRepository personRepository, ILogger<PersonController> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("PersonController: Index => page has been accessed");
            return View(_personRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"PersonController:Create => First name: {person.FirstName} and Last name: {person.LastName}");
                var updatedPersonList = _personRepository.CreatePerson(person);
                return RedirectToAction("Index", updatedPersonList);
            }
            else
            {
                _logger.LogWarning("PersonController:Create => ModelState is invalid");
                return View();
            }
        }

        public IActionResult Update(string firstName)
        {
            var personToUpdate = _personRepository.GetPerson(firstName);
            return View(personToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Person person, string firstName)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"PersonController:Update => First name of the person to be updated: {firstName} ");
                var personList = _personRepository.GetAll();

                var personToUpdate = _personRepository.GetPerson(firstName);

                if (personToUpdate == null)
                    personList.Add(person);
                else
                {
                    var updatedPersonList = _personRepository.UpdatePerson(person, firstName);
                    _logger.LogInformation($"PersonController:Update => Person updated with First name: {person.FirstName} and Last name: {person.LastName}");
                    _personRepository.UpdatePersonList(updatedPersonList);
                }

                return RedirectToAction("Index", personList);
            }
            else
            {
                _logger.LogWarning("PersonController:Update => ModelState is invalid");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string firstName)
        {
            _logger.LogInformation($"PersonController:Delete=> First name: {firstName}");
            List<Person> personList = _personRepository.GetAll();
            _logger.LogInformation($"PersonController:Delete => No of persons before deleting: {personList.Count}");

            Person personToDelete = personList.Where(p => p.FirstName == firstName).FirstOrDefault();
            personList.Remove(personToDelete);
            _logger.LogInformation("PersonController:Delete => Person deleted");
            _logger.LogInformation($"PersonController:Delete => No of persons after deleting: {personList.Count}");

            _personRepository.UpdatePersonList(personList);

            return RedirectToAction("Index", personList);
        }
    }
}
