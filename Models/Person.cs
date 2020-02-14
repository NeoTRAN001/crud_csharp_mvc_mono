using Npgsql;
using System.Data;
using System.Collections.Generic;

namespace CRUD.Models
{
    public class Person : DataAccess
    {
        /// <summary>
        /// Default builder
        /// </summary>
        public Person() {}

        /// <summary>
        /// Builder overloaded
        /// </summary>
        /// <param name="id">Person identifier</param>
        public Person(int id) 
        {
            NpgsqlParameter paramKeyPeron = new NpgsqlParameter("@id", id);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter> 
            { 
                paramKeyPeron 
            };

            const string SQL = "SELECT * FROM person WHERE id = @id;";

            DataTable table = GetQuery(SQL, lstParameters);

            if (table.Rows.Count <= 0) return;

            DataRow row = table.Rows[0];
            Id = (int)row["id"];
            Email = (string)row["email"];
            Age = (int)row["age"];
            Message = (string)row["message"];
        }

        /// <summary>
        /// Person identifier
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Person's email
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Person's age
        /// </summary>
        /// <value>The age.</value>
        public int Age { get; set; }
        /// <summary>
        /// Person's message
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Get the data of the person
        /// </summary>
        /// <returns>The person.</returns>
        /// <param name="id">Person's identifier</param>
        /// <param name="email">Person's email</param>
        public List<Person> GetPerson(int id, string email)
        {
            NpgsqlParameter paramKeyPerson = new NpgsqlParameter("@id", id);
            NpgsqlParameter paramEmailPerson = new NpgsqlParameter("@email", email);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>();
            List<string> arguments = new List<string>();

            string condition = string.Empty;

            if(id != 0)
            {
                arguments.Add("id = @id");
                lstParameters.Add(paramKeyPerson);
            }

            if(!string.IsNullOrEmpty(email))
            {
                arguments.Add("email ILIKE '%' || @email || '%'");
                lstParameters.Add(paramEmailPerson);
            }

            switch(arguments.Count)
            {
                case 1: condition = string.Format("WHERE {0}", arguments[0]); break;
                case 2: 
                    condition = string.Format("WHERE {0} AND {1}", arguments[0], arguments[1]);
                    break;
            }

            string sql = "SELECT * FROM person " + condition;

            DataTable table = GetQuery(sql, lstParameters);
            List<Person> lstPerson = ToList(table);

            return lstPerson;
        }

        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <returns><c>true</c>, if person was added, <c>false</c> otherwise.</returns>
        /// <param name="person">Person.</param>
        public bool AddPerson(Person person)
        {
            NpgsqlParameter paramKeyPerson = new NpgsqlParameter("@id", person.Id);
            NpgsqlParameter paramEmailPerson = new NpgsqlParameter("@email", person.Email);
            NpgsqlParameter paramAgePerson = new NpgsqlParameter("@age", person.Age);
            NpgsqlParameter paramMessagePerson = new NpgsqlParameter("@message", person.Message);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>()
            {
                paramKeyPerson, paramEmailPerson, paramAgePerson, paramMessagePerson
            };

            const string SQL = "INSERT INTO person(id, email, age, message) " +
            	               "VALUES (@id, @email, @age, @message)";

            int affected = ExecuteQuery(SQL, lstParameters);

            return affected > 0;
        }

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <returns><c>true</c>, if person was updated, <c>false</c> otherwise.</returns>
        /// <param name="person">Person.</param>
        public bool UpdatePerson(Person person)
        {
            NpgsqlParameter paramKeyPerson = new NpgsqlParameter("@id", person.Id);
            NpgsqlParameter paramEmailPerson = new NpgsqlParameter("@email", person.Email);
            NpgsqlParameter paramAgePerson = new NpgsqlParameter("@age", person.Age);
            NpgsqlParameter paramMessagePerson = new NpgsqlParameter("@message", person.Message);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>()
            {
                paramKeyPerson, paramEmailPerson, paramAgePerson, paramMessagePerson
            };

            const string SQL = "UPDATE person SET email = @email, age = @age, message = @message" +
            	" WHERE id = @id";

            int affected = ExecuteQuery(SQL, lstParameters);

            return affected > 0;
        }

        /// <summary>
        /// Deletes the person.
        /// </summary>
        /// <returns><c>true</c>, if person was deleted, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        public bool DeletePerson(int id)
        {
            NpgsqlParameter paramKeyPerson = new NpgsqlParameter("@id", id);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>()
            {
                paramKeyPerson
            };

            const string SQL = "DELETE FROM person WHERE id = @id;";

            int affected = ExecuteQuery(SQL, lstParameters);

            return affected > 0;
        }

        /// <summary>
        /// A list of the person object is created
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="table">Table.</param>
        private List<Person> ToList(DataTable table)
        {
            List<Person> lstPeriodos = new List<Person>();
            foreach (DataRow row in table.Rows)
            {
                Person periodo = new Person() 
                {
                    Id = (int)row["id"],
                    Email = (string)row["email"],
                    Age = (int)row["age"],
                    Message = (string)row["message"]
                };
                lstPeriodos.Add(periodo);
            }

            return lstPeriodos;
        }

    }
}
