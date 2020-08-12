using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Library.Models
{

    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookDescr { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string BookCategory { get; set; }

        [Remote("IsDateValid","Book", ErrorMessage ="Valid date range is from the year 1900 to 2020")]
        public DateTime BookPublishDate { get; set; }
        public string FilePath { get; set; }
    }
    public class BookModel
    {
        SqlConnection connection = new SqlConnection("Data Source=5CG6422T5C-LA;Initial Catalog=MVC;Integrated Security=true;");

        public List<Book> GetBooksList()
        {
            List<Book> booksListObj = new List<Book>();
            SqlCommand command = new SqlCommand("sp_getbook", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Book bookObj = new Book();
                bookObj.BookId = Convert.ToInt32(dataRow[0]);
                bookObj.BookTitle = Convert.ToString(dataRow[1]);
                bookObj.BookDescr =  Convert.ToString(dataRow[2]);
                bookObj.AuthorId = Convert.ToInt32(dataRow[3]);
                bookObj.AuthorName = Convert.ToString(dataRow[4]);
                bookObj.BookCategory = Convert.ToString(dataRow[5]);
                bookObj.BookPublishDate = Convert.ToDateTime(dataRow[6]);
                bookObj.FilePath = Convert.ToString(dataRow[7]);


                booksListObj.Add(bookObj);
            }
            connection.Close();
            return booksListObj;
        }

        public int SaveBook(Book bookObj)
        {
            SqlCommand command = new SqlCommand("sp_insertBook", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vBookTitle", bookObj.BookTitle);
            command.Parameters.AddWithValue("@vBookDescr", bookObj.BookDescr);
            command.Parameters.AddWithValue("@vAuthorId", bookObj.AuthorId);
            command.Parameters.AddWithValue("@vBookCategory", bookObj.BookCategory);
            command.Parameters.AddWithValue("@vBookPublishDate", bookObj.BookPublishDate);
            command.Parameters.AddWithValue("@vFilePath", bookObj.FilePath);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        public Book GetBookById(int? bookId)
        {
            Book bookObj = new Book();
            SqlCommand command = new SqlCommand("sp_getbookbyID", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vBookId", bookId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                bookObj.BookId = Convert.ToInt32(dataRow[0]);
                bookObj.BookTitle = Convert.ToString(dataRow[1]);
                bookObj.BookDescr = Convert.ToString(dataRow[2]);
                bookObj.AuthorId = Convert.ToInt32(dataRow[3]);
                bookObj.AuthorName = Convert.ToString(dataRow[4]);
                bookObj.BookCategory = Convert.ToString(dataRow[5]);
                bookObj.BookPublishDate = Convert.ToDateTime(dataRow[6]);
                bookObj.FilePath = Convert.ToString(dataRow[7]);
            }
            connection.Close();
            return bookObj;
        }

        public int EditBookById(Book bookObj)
        {
            SqlCommand command = new SqlCommand("sp_updateBook", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vBookId", bookObj.BookId);
            command.Parameters.AddWithValue("@vBookTitle", bookObj.BookTitle);
            command.Parameters.AddWithValue("@vBookDescr", bookObj.BookDescr);
            command.Parameters.AddWithValue("@vAuthorId", bookObj.AuthorId);
            command.Parameters.AddWithValue("@vBookCategory", bookObj.BookCategory);
            command.Parameters.AddWithValue("@vBookPublishDate", bookObj.BookPublishDate);
            command.Parameters.AddWithValue("@vFilePath", bookObj.FilePath);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        public int DeleteBookById(int? bookId)
        {
            SqlCommand command = new SqlCommand("sp_deletebookbyID", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@vBookId", bookId);
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }
    }
}