using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class PhoneBook : IPhoneBook
    {
        public void AddPerson(Person newPerson)
        {
            DatabaseUtil.Add(newPerson);
        }
        public Person FindPerson(string firstName, string lastName)
        {
            return DatabaseUtil.Find(firstName, lastName);
        }

        public override string ToString()
        {
            var personlist = DatabaseUtil.List();
            return $"{string.Join(",\n", personlist)}";
        }
    }
}