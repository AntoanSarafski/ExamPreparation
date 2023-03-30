using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Repositories;
using UniversityCompetition.Models;
using System.Linq;
using UniversityCompetition.Utilities.Messages;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private string[] allowedCategories = new string[] { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };

        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;


        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }


        public string AddSubject(string subjectName, string subjectType)
        {
            if (!allowedCategories.Contains(subjectType))
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (subjects.FindByName(subjectName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectType);
            }

            Subject subject = null;
            if (subjectType == "Technical")
            {
                subject = new TechnicalSubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }
            if (subjectType == "Economical")
            {
                subject = new EconomicalSubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }
            if (subjectType == "Humanity")
            {
                subject = new HumanitySubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subject.GetType().Name, subjectName, subjects.GetType().Name);
        }


        public string AddStudent(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            List<int> requiredSubjectsAsInts = requiredSubjects.Select(s => int.Parse(s)).ToList();

            University university = new University(0, universityName, category, capacity, requiredSubjectsAsInts);

            universities.AddModel(university);
            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
            
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            throw new NotImplementedException();
        }

        public string TakeExam(int studentId, int subjectId)
        {
            throw new NotImplementedException();
        }

        public string UniversityReport(int universityId)
        {
            throw new NotImplementedException();
        }
    }
}
