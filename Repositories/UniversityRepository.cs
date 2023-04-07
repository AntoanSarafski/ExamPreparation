using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        private List<IUniversity> universities;
        public UniversityRepository()
        {
            universities = new List<IUniversity>();
        }

        public IReadOnlyCollection<IUniversity> Models => universities.AsReadOnly();

        public void AddModel(IUniversity university)
            => universities.Add(university);

        public IUniversity FindById(int id)
            => universities.FirstOrDefault(u => u.Id == id);

        public IUniversity FindByName(string name)
            => universities.FirstOrDefault(u => u.Name == name);
    }
}
