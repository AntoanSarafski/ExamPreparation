using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        const double _subjectRate = 1.0;
        public EconomicalSubject(int subjectId, string subjectName) 
            : base(subjectId, subjectName, _subjectRate)
        {
        }
    }
}
