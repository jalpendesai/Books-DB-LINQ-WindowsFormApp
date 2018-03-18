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
            //1
        
            var authorsAndTitles = from book in bookDb.Titles
                                   from author in book.Authors
                                   orderby book.Title1
                                   select new { author.FirstName, author.LastName, book.Title1 };
            outputTextBox.AppendText("\r\n\r\n Sorted by Title:\n");
            foreach (var item in authorsAndTitles)
            {
                outputTextBox.AppendText(string.Format("\r\n\t{0,-10} {1,-10} {2}", item.FirstName, item.LastName, item.Title1));
            }

            //2
            
            var authorsAndTitlesSorted = from book in bookDb.Titles
                                   from author in book.Authors
                                   orderby book.Title1, author.LastName, author.FirstName
                                   select new { author.FirstName, author.LastName, book.Title1 };
            outputTextBox.AppendText("\r\n\r\n Sorted by Title and Author:\n");
            foreach (var item in authorsAndTitlesSorted)
            {
                outputTextBox.AppendText(string.Format("\r\n\t{0,-10} {1,-10} {2}", item.FirstName, item.LastName, item.Title1));
            }

            

            //3
            outputTextBox.AppendText("\r\n\r\n Grouped by Title, sorted by title and Author:\n");
            var allTitles = from book in bookDb.Titles
                            orderby book.Title1
                            select book;
            if (allTitles.Any())
            {
                foreach (Title title in allTitles)
                {
                    outputTextBox.AppendText(string.Format("\n{0}\r\n", title.Title1));
                    var titleAuthors = from author in title.Authors
                                       orderby author.LastName, author.FirstName
                                       select author;
                    if (titleAuthors.Any())
                    {
                        foreach(Author author in titleAuthors)
                        {
                            outputTextBox.AppendText(string.Format("\t\t\t{0}\r\n", author.FirstName + " " + author.LastName));
                        }
                    }
                }
            }

        }
    }
}
