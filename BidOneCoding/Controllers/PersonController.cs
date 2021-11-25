using BidOneCoding.Models;
using BidOneCoding.Respository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BidOneCoding.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository) => _personRepository = personRepository;

        public IActionResult Index()
        {
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
                var updatedPersonList = _personRepository.CreatePerson(person);
                return RedirectToAction("Index", updatedPersonList);
            }
            else
                return View();
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
                var personList = _personRepository.GetAll();

                var personToUpdate = _personRepository.GetPerson(firstName);

                if (personToUpdate == null)
                    personList.Add(person);
                else
                {
                    var updatedPersonList = _personRepository.UpdatePerson(person, firstName);
                    _personRepository.UpdatePersonList(updatedPersonList);
                }

                return RedirectToAction("Index", personList);
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string firstName)
        {
            List<Person> personList = _personRepository.GetAll();

            Person personToDelete = personList.Where(p => p.FirstName == firstName).FirstOrDefault();
            personList.Remove(personToDelete);

            _personRepository.UpdatePersonList(personList);

            return RedirectToAction("Index", personList);
        }
    }
}
