using BidOneCoding.Models;
using System.Collections.Generic;

namespace BidOneCoding.Respository
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person GetPerson(string firstName);
        List<Person> CreatePerson(Person person);
        List<Person> UpdatePerson(Person person, string firstName);
        void UpdatePersonList(List<Person> person);
    }
}