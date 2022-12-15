using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace PhoneBookTestApp
{
    public class DatabaseUtil
    {
        public static void InitializeDatabase()
        {
            var dbConnection = OpenConnection();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "create table PHONEBOOK (NAME varchar(255), PHONENUMBER varchar(255), ADDRESS varchar(255))",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Chris Johnson','(321) 231-7876', '452 Freeman Drive, Algonac, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Dave Williams','(231) 502-1236', '285 Huron St, Port Austin, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Person> List()
        {
            var result = new List<Person>();

            using (SQLiteConnection connect = OpenConnection())
            {
                using (SQLiteCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM PHONEBOOK";
                    cmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        result.Add(
                            new Person
                            {
                                name = r["NAME"].ToString(),
                                phoneNumber = r["PHONENUMBER"].ToString(),
                                address = r["ADDRESS"].ToString(),
                            });
                    }
                }
            }
            return result;
        }

        public static Person Find(string firstName, string lastName)
        {
            Console.WriteLine($"Query for {firstName} {lastName}");
            var result = new Person();
            using (SQLiteConnection conn = OpenConnection())
            {
                var cmd = new SQLiteCommand(
                    "SELECT * FROM PHONEBOOK WHERE NAME LIKE @name",
                    conn
                    );
                cmd.Parameters.AddWithValue("@name", firstName + " " + lastName);
                SQLiteDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    result = new Person
                    {
                        name = r["NAME"].ToString(),
                        phoneNumber = r["PHONENUMBER"].ToString(),
                        address = r["ADDRESS"].ToString(),
                    };
                }
            }
            return result;
        }

        public static void Add(Person newPerson)
        {
            var result = new List<Person>();

            using (SQLiteConnection conn = OpenConnection())
            {
                var cmd = new SQLiteCommand(
                           "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES(@name, @phonenumber, @address)",
                           conn);
                cmd.Parameters.AddWithValue("@name", newPerson.name);
                cmd.Parameters.AddWithValue("@phonenumber", newPerson.phoneNumber);
                cmd.Parameters.AddWithValue("@address", newPerson.address);
                cmd.ExecuteNonQuery();
                Console.WriteLine("row inserted");
            }
        }

        public static SQLiteConnection OpenConnection()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            return dbConnection;
        }

        public static void CleanUp()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "drop table PHONEBOOK",
                        dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}