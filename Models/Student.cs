using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {

        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;
            coveredExams = new List<int>();
        }
        private int id;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }


        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                firstName = value;
            }
        }


        private string lastName;

        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                lastName = value;
            }
        }

        private List<int> coveredExams;
        //We must initializе this PRIVATE LIST in ctor !!!
        public IReadOnlyCollection<int> CoveredExams 
        {
            get { return coveredExams.AsReadOnly(); } 
            // Here we make the List to ReadOnly collection.
        }

        private IUniversity university;

        public IUniversity University
        {
            get { return university; }
            // Deleted setter! Its in JoinUniversity method ! 
        }


        public void CoverExam(ISubject subject)
        {
            coveredExams.Add(subject.Id); 
        }

        public void JoinUniversity(IUniversity university)
        {
            //Here we set value of University and we must delete setter ! 
            this.university = university;
        }
    }
}
