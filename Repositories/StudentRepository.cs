using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> students;

        public StudentRepository()
        {
            students = new List<IStudent>();    
        }
        public IReadOnlyCollection<IStudent> Models => students.AsReadOnly();

        public void AddModel(IStudent student)
            => students.Add(student);

        public IStudent FindById(int id)
            => students.FirstOrDefault(s => s.Id == id);
        
        public IStudent FindByName(string name)
        {
            string[] splittedName = name.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string firstName = splittedName[0];
            string lastName = splittedName[1];

            return students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
