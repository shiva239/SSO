using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Library.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLocation { get; set; }
        public string AuthorDesc { get; set; }

    }


    public class AuthorModel
    {
        SqlConnection connection = new SqlConnection("Data Source=5CG6422T5C-LA;Initial Catalog=MVC;Integrated Security=true;");

        public List<Author> GetAuthorsList()
        {
            List<Author> authorsListObj = new List<Author>();
            SqlCommand command = new SqlCommand("sp_getAuthor", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach(DataRow dataRow in dataTable.Rows)
            {
                Author authorObj = new Author();
                authorObj.AuthorId = (int)dataRow[0];
                authorObj.AuthorName = (string)dataRow[1];
                authorObj.AuthorLocation = (string)dataRow[2];
                authorObj.AuthorDesc = (string)dataRow[3];

                authorsListObj.Add(authorObj);
            }
            connection.Close();
            return authorsListObj;
        }

        public int SaveAuthor(Author authorObj)
        {
            SqlCommand command = new SqlCommand("sp_insertAuthor", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vAuthorName", authorObj.AuthorName);
            command.Parameters.AddWithValue("@vAuthorLocation", authorObj.AuthorLocation);
            command.Parameters.AddWithValue("@vAuthorDesc", authorObj.AuthorDesc);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        public Author GetAuthorById(int? AuthId)
        {
            Author authorObj = new Author();
            SqlCommand command = new SqlCommand("sp_getAuthorbyID", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vAuthorId", AuthId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                authorObj.AuthorId = (int)dataRow[0];
                authorObj.AuthorName = (string)dataRow[1];
                authorObj.AuthorLocation = (string)dataRow[2];
                authorObj.AuthorDesc = (string)dataRow[3];

            }
            connection.Close();
            return authorObj;
        }

        public int EditAuthorById(Author authorObj)
        {
            SqlCommand command = new SqlCommand("sp_updateAuthor", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vAuthorId", authorObj.AuthorId);
            command.Parameters.AddWithValue("@vAuthorName", authorObj.AuthorName);
            command.Parameters.AddWithValue("@vAuthorLocation", authorObj.AuthorLocation);
            command.Parameters.AddWithValue("@vAuthorDesc", authorObj.AuthorDesc);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        public int DeleteAuthorById(int? AuthId)
        {
            SqlCommand command = new SqlCommand("sp_deleteAuthorbyID", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vAuthorId", AuthId);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

    }
}