using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {

        public SubjectRepository()
        {
            models = new List<ISubject>();
        }
        private List<ISubject> models;

        public IReadOnlyCollection<ISubject> Models
        {
            get { return models.AsReadOnly(); } //Delete setter for better encapsulation!
        }

        public void AddModel(ISubject model)
        {
            models.Add(model);
        }

        public ISubject FindById(int id)
        {
            return models.FirstOrDefault(m => m.Id == id);
        }

        public ISubject FindByName(string name)
        {
            return models.FirstOrDefault(n => n.Name == name);
        }
    }
}
