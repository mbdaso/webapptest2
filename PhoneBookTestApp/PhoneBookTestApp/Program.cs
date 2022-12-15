using System;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    class Program
    {
        static PhoneBook phonebook = new PhoneBook();
        static void Main(string[] args)
        {
            try
            {
                DatabaseUtil.InitializeDatabase();
                /* TODO: create person objects and put them in the PhoneBook and database
                * John Smith, (248) 123-4567, 1234 Sand Hill Dr, Royal Oak, MI
                * Cynthia Smith, (824) 128-8758, 875 Main St, Ann Arbor, MI
                */

                phonebook.AddPerson(
                    new Person
                    {
                        name = "John Smith",
                        phoneNumber = "(248) 123-4567",
                        address = "1234 Sand Hill Dr, Royal Oak, MI"
                    });
                phonebook.AddPerson(
                    new Person { 
                        name = "Cynthia Smith", 
                        phoneNumber = "(824) 128-8758", 
                        address = "875 Main St, Ann Arbor, MI" }
                    );

                Console.WriteLine("print the phone book out to System.out");
                Console.WriteLine(phonebook);

                Console.WriteLine("find Cynthia Smith and print out just her entry    ");
                Console.WriteLine(phonebook.FindPerson(firstName: "Cynthia", lastName: "Smith"));

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                DatabaseUtil.CleanUp();
            }
            Console.ReadLine();
        }
    }
}
