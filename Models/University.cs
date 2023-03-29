using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models.Contracts
{
    public class University : IUniversity
    {
        private string[] allowedCategories = new string[] { "Technical", "Economical", "Humanity" };
        private int id;
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }


        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        private string category;
        public string Category
        {
            get { return category; }
            private set
            {
                if (allowedCategories.Contains(value))
                {
                    category = value;
                }
                throw new ArgumentException(String.Format(ExceptionMessages.CategoryNotAllowed, value));
                //ExceptionMessages.CategoryNotAllowed ->
                //"University category {0} !!! THIS {0} = value in string.FORMAT !!! is not allowed in the application!"
            }
        }

        public int Capacity => throw new NotImplementedException();

        public IReadOnlyCollection<int> RequiredSubjects => throw new NotImplementedException();
    }
}
