using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private string firstName;
        private string lastName;
        private int id;
        private IUniversity university;
        private readonly List<int> coveredExams;

        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;
            coveredExams = new List<int>();
        }
        public int Id
        {
            get => id;
            private set => id = value;
        }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                lastName = value;
            }
        }

        public IReadOnlyCollection<int> CoveredExams => coveredExams.AsReadOnly();

        public IUniversity University 
        { 
            get => university;
        }

        public void CoverExam(ISubject subject)
            => coveredExams.Add(subject.Id);

        public void JoinUniversity(IUniversity university)
            => this.university = university;
    }
}
