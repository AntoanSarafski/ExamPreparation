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
            if (subjectType == typeof(TechnicalSubject).Name)
            {
                subject = new TechnicalSubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }
            if (subjectType == typeof(EconomicalSubject).Name)
            {
                subject = new EconomicalSubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }
            if (subjectType == typeof(HumanitySubject).Name)
            {
                subject = new HumanitySubject(0, subjectName); // this 0 is Id , which we must added in the repository after that !!!
            }

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subject.GetType().Name, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            // Math, Physics ... we must find this subject in SubjectRepository and take the id.
            List<int> requiredSubjectsAsInts = requiredSubjects.Select(s => subjects.FindByName(s).Id).ToList();

            University university = new University(0, universityName, category, capacity, requiredSubjectsAsInts);

            universities.AddModel(university);
            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);

        }


        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName);
            }

            students.AddModel(new Student(0, firstName, lastName));

            return String.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            if (student == null)
            {
                return String.Format(OutputMessages.InvalidStudentId);
            }
            
            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return String.Format(OutputMessages.InvalidSubjectId);
            }
            if (student.CoveredExams.Contains(subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);
            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }
        

        public string ApplyToUniversity(string studentName, string universityName)
        {
            throw new NotImplementedException();
        }


        public string UniversityReport(int universityId)
        {
            throw new NotImplementedException();
        }
    }
}
