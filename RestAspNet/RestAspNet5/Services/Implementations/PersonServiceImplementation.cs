using RestAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestAspNet5.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
          
        }

        public List<Person> FindAll()
        {

            List<Person> persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                persons.Add(CriarPerson(i));
            }
            return persons;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Thiago",
                LastName = "Teixeira",
                Adress = "Taubaté / SP",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        private Person CriarPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Thiago " + i ,
                LastName = "Teixeira " ,
                Adress = "Taubaté / SP " ,
                Gender = "Male " 
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
