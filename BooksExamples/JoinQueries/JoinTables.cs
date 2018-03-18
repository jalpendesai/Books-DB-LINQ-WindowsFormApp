using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BooksExamples;

namespace JoinQueries
{
    public partial class JoinTables : Form
    {
        BooksEntities bookDb = new BooksEntities();
        public JoinTables()
        {
            InitializeComponent();
        }

        private void JoinTables_Load(object sender, EventArgs e)
        {
            //get authors and ISBNs of each book they co-authored
            var authorAndISBNs = from author in bookDb.Authors
                                 from book in bookDb.Titles
                                 orderby author.LastName, author.FirstName
                                 select new { author.FirstName, author.LastName, book.ISBN };
            outputTextBox.AppendText("Authors and Books:");
            foreach (var item in authorAndISBNs)
            {
                outputTextBox.AppendText(String.Format("\r\n\t{0,-10} {1,-10} {2,-10}", item.FirstName,item.LastName, item.ISBN ));
            }

            //get authors and titiles of each book they co-authored
            var authorsAndTitles = from book in bookDb.Titles
                                   from author in bookDb.Authors
                                   orderby author.LastName, author.FirstName, book.Title1
                                   select new { author.FirstName, author.LastName, book.Title1 };
            outputTextBox.AppendText("\r\n\r\n Authors and titles:");
            foreach (var item in authorsAndTitles)
            {
                outputTextBox.AppendText(string.Format("\r\n\t{0,-10} {1,-10} {2}", item.FirstName, item.LastName, item.Title1));
            }

            //get authors and titles of each book
            var titleByAuthor = from author in bookDb.Authors
                                let Name = author.FirstName + " " + author.LastName
                                orderby author.LastName, author.FirstName
                                select new
                                {
                                    Name,
                                    Titles = from book in author.Titles
                                             orderby book.Title1
                                             select book.Title1
                                };

        }
    }
}
