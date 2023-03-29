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

        public StudentRepository()
        {
            models = new List<IStudent>();
        }
        private List<IStudent> models;

        public IReadOnlyCollection<IStudent> Models
        {
            get { return models.AsReadOnly(); } //Delete setter for better encapsulation!
        }

        public void AddModel(IStudent model)
        {
            models.Add(model);
        }

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(m => m.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] splitted = name.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string firstName = splitted[0];
            string lastName = splitted[1];
            return models.FirstOrDefault(n => n.FirstName == firstName && n.LastName == lastName);
        }
    }
}
