using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjectRepository;
        private StudentRepository studentRepository;
        private UniversityRepository universityRepository;

        private string[] allowedCategories = new string[] { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };

        public Controller()
        {
            subjectRepository = new SubjectRepository();
            studentRepository = new StudentRepository();
            universityRepository = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (!allowedCategories.Contains(subjectType))
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported , subjectType);
            }
            if (subjectRepository.FindByName(subjectName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            if (subjectType == "TechnicalSubject")
            {
                ISubject subject = new TechnicalSubject((subjectRepository.Models.Count + 1), subjectName);
                subjectRepository.AddModel(subject);
                return String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjectRepository.GetType().Name);
            }
            if (subjectType == "EconomicalSubject")
            {
                ISubject subject = new EconomicalSubject((subjectRepository.Models.Count + 1), subjectName);
                subjectRepository.AddModel(subject);
                return String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjectRepository.GetType().Name);
            }
            else
            {
                ISubject subject = new HumanitySubject((subjectRepository.Models.Count + 1), subjectName);
                subjectRepository.AddModel(subject);
                return String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjectRepository.GetType().Name);
            }
        }


        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universityRepository.FindByName(universityName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            List<int> requiredSubjectAsInt = new List<int>();
            foreach (var reqiredSubject in requiredSubjects)
            {
                requiredSubjectAsInt.Add(subjectRepository.FindByName(reqiredSubject).Id);
            }

            IUniversity university = new University(universityRepository.Models.Count() + 1, universityName, category, capacity, requiredSubjectAsInt);
            universityRepository.AddModel(university);
            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universityRepository.GetType().Name);
        }

        // One test crashed.


        public string AddStudent(string firstName, string lastName)
        {
            string studentName = firstName + " " + lastName;
            if (studentRepository.FindByName(studentName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }
            IStudent student = new Student(studentRepository.Models.Count() + 1, firstName, lastName);
            studentRepository.AddModel(student);
            return String.Format(OutputMessages.StudentAddedSuccessfully,firstName, lastName, studentRepository.GetType().Name);
        }


        public string TakeExam(int studentId, int subjectId)
        {
            if (studentRepository.FindById(studentId) == null)
            {
                return OutputMessages.InvalidStudentId;
            }
            if (subjectRepository.FindById(subjectId) == null)
            {
                return OutputMessages.InvalidSubjectId;
            }
            if (studentRepository.FindById(studentId).CoveredExams.Contains(subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, studentRepository.FindById(studentId).FirstName, studentRepository.FindById(studentId).LastName, subjectRepository.FindById(subjectId).Name);
            }
            studentRepository.FindById(studentId).CoverExam(subjectRepository.FindById(subjectId));
            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, studentRepository.FindById(studentId).FirstName, studentRepository.FindById(studentId).LastName, subjectRepository.FindById(subjectId).Name);
        }




        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] splittedName = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string studentFirstName = splittedName[0];
            string studentLastName = splittedName[1];

            if (studentRepository.FindByName(studentName) == null)
            {
                return String.Format(OutputMessages.StudentNotRegitered, studentFirstName, studentLastName);
            }
            if (universityRepository.FindByName(universityName) == null)
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
            List<int> reqiredExams = universityRepository.FindByName(universityName).RequiredSubjects.ToList();
            for (int i = 0; i < reqiredExams.Count; i++)
            {
                if (studentRepository.FindByName(studentName).CoveredExams.Contains(reqiredExams[i]) == false)
                {
                    return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }

            }
            
            if (studentRepository.FindByName(studentName).University == null)
            {
                studentRepository.FindByName(studentName).JoinUniversity(universityRepository.FindByName(universityName));
                return String.Format(OutputMessages.StudentSuccessfullyJoined, studentFirstName, studentLastName, universityName);
            }

            if (studentRepository.FindByName(studentName).University.Name == universityName)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, studentFirstName, studentLastName, universityName);

            }
            studentRepository.FindByName(studentName).JoinUniversity(universityRepository.FindByName(universityName));
            return String.Format(OutputMessages.StudentSuccessfullyJoined, studentFirstName, studentLastName, universityName);

        }



        public string UniversityReport(int universityId)
        {
            IUniversity university = universityRepository.FindById(universityId);
            StringBuilder sb = new StringBuilder();

            int admittedStudents = 0;
            for (int i = 1; i <= studentRepository.Models.Count; i++)
            {
                if(studentRepository.FindById(i).University == null)
                {

                }
                else if (studentRepository.FindById(i).University.Name == university.Name)
                {
                    admittedStudents++;
                }
            }
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {admittedStudents}");
            sb.AppendLine($"University vacancy: {university.Capacity - admittedStudents}");

            return sb.ToString().Trim();
        }
    }
}
