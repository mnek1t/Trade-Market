using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Person : BaseEntity
    {
        public Person() : base() { }
        public Person(int id, string name, string surname, DateTime birthDate) : base(id)
        {
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        
        
    }
}
