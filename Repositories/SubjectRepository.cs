using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> models;

        public IReadOnlyCollection<ISubject> Models
        {
            get { return models.AsReadOnly(); } //Delete setter for better encapsulation!
        }

        public void AddModel(ISubject model)
        {
            throw new NotImplementedException();
        }

        public ISubject FindById(int id)
        {
            throw new NotImplementedException();
        }

        public ISubject FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
