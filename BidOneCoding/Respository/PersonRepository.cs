using BidOneCoding.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BidOneCoding.Respository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IFileOperations _fileOperations;

        public PersonRepository(IFileOperations fileOperations) => _fileOperations = fileOperations;

        public List<Person> GetAll() =>
            JsonSerializer.Deserialize<List<Person>>(_fileOperations.Read());

        public Person GetPerson(string firstName) => 
            GetAll().FirstOrDefault(x => x.FirstName.ToLower() == firstName.ToLower());

        public void UpdatePersonList(List<Person> person)
        {
            var jsonOptions = new JsonSerializerOptions(){ WriteIndented= true};
            _fileOperations.Write(JsonSerializer.Serialize(person, jsonOptions));
        }

        public List<Person> CreatePerson(Person person)
        {
            var personList = GetAll();
            personList.Add(person);
            UpdatePersonList(personList);

            return personList;
        }

        public List<Person> UpdatePerson(Person person, string firstName)
        {
            var personList = GetAll();
            personList.Where(p => p.FirstName == firstName).FirstOrDefault().LastName = person.LastName;
            UpdatePersonList(personList);

            return personList;
        }
    }
}
