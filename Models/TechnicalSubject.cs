using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCompetition.Models
{
    public class TechnicalSubject : Subject
    {
        const double _subjectRate = 1.3;
        public TechnicalSubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, _subjectRate)
        {
        }
    }
}
