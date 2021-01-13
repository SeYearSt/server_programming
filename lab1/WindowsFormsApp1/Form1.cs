using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadBookRecords();
            loadSageRecords();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox5.Text.ToString());
            string name = textBox1.Text;
            string description = textBox2.Text;

            string[] sagesIdSt = textBox8.Text.Split(',');
            int[] sagesId = Array.ConvertAll(sagesIdSt, s => int.Parse(s));

            using (var cnt = new SageBookDbContext())
            {
                var dbBook = cnt.Books.First(b => b.id == id);
                dbBook.name = name;
                dbBook.description = description;
                var sages = cnt.Sages.Where(s => sagesId.Contains(s.id)).ToList();
                //dbBook.Sages = dbBook.Sages;
                //dbBook.Sages = sages;

                cnt.SaveChanges();

            }
            loadBookRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;

            loadBookRecords();
            loadSageRecords();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string age = textBox4.Text;
            var photo = pictureBox1.Image;
            string city = textBox7.Text;

            // TODO: check arguments

            using (var cnt = new SageBookDbContext())
            { 
                //var booksIds = cnt.Books.Where(b => b.Sages.);
                Sage sage = new Sage() { name = name, age = age, city = city, photo = Sage.ImageToByteArray(photo) };

                cnt.Sages.Add(sage);
                cnt.SaveChanges();
            }
            loadSageRecords();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string description = textBox2.Text;

            string[] sagesIdSt = textBox8.Text.Split(',');

            int[] sagesId = Array.ConvertAll(sagesIdSt, s => int.Parse(s));


            // TODO: check arguments


            using (var cnt = new SageBookDbContext())
            {

                var sages = cnt.Sages.Where(s => sagesId.Contains(s.id));

                Book book = new Book() { name = name, description = description, };
                foreach (var sage in sages)
                {
                    book.Sages.Add(sage);
                }

                cnt.Books.Add(book);
                cnt.SaveChanges();
            }
            loadBookRecords();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private string sagesIdToString(ICollection<Sage> sages)
        {
            var ids = sages.Select(s => s.id).ToList();

            return String.Join(",", ids);
        }

        private string booksIdToString(ICollection<Book> books)
        {
            var ids = books.Select(s => s.id).ToList();

            return String.Join(",", ids);
        }

        private void loadBookRecords()
        {
            using (var cnt = new SageBookDbContext())
            {
                var books = cnt.Books.ToList<Book>();

                List<object> booksFormated = new List<object>();


                DataTable table = new DataTable("BookTable");
                // Declare variables for DataColumn and DataRow objects.
                DataColumn column;
                DataRow row;

                // Create new DataColumn, set DataType,
                // ColumnName and add to DataTable.
                column = new DataColumn();
                column.DataType = Type.GetType("System.Int32");
                column.ColumnName = "id";
                column.ReadOnly = true;
                column.Unique = true;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Name";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Description";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "SageId";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                foreach (var book in books)
                { 
                    row = table.NewRow();
                    row["id"] = book.id;
                    row["Name"] = book.name;
                    row["Description"] = book.description;
                    row["SageId"] = sagesIdToString(book.Sages);
                    table.Rows.Add(row);
                }

                dataGridView2.DataSource = table;

            }
        }

        private void loadSageRecords()
        {
            using (var cnt = new SageBookDbContext())
            {
                var sages = cnt.Sages.ToList<Sage>();

                DataTable table = new DataTable("SageTable");
                // Declare variables for DataColumn and DataRow objects.
                DataColumn column;
                DataRow row;

                // Create new DataColumn, set DataType,
                // ColumnName and add to DataTable.
                column = new DataColumn();
                column.DataType = Type.GetType("System.Int32");
                column.ColumnName = "id";
                column.ReadOnly = true;
                column.Unique = true;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "name";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.Int32");
                column.ColumnName = "age";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Byte[]");
                column.ColumnName = "photo";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "city";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "books";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);


                foreach (var sage in sages)
                {
                    row = table.NewRow();
                    row["id"] = sage.id;
                    row["name"] = sage.name;
                    row["age"] = sage.age;
                    row["photo"] = sage.photo;
                    row["city"] = sage.city;
                    row["books"] = booksIdToString(sage.Books);
                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;

                //dataGridView1.DataSource = sages;
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                //int sageId = int.Parse(dataGridView1.CurrentRow);
                int id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                string name = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
                int age = int.Parse(dataGridView1.CurrentRow.Cells["age"].Value.ToString());
                string city = dataGridView1.CurrentRow.Cells["city"].Value.ToString();
                byte[] photo = (byte[])dataGridView1.CurrentRow.Cells["photo"].Value;

                updateSageInfo(id, name, age, photo, city);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadBookRecords();
            loadSageRecords();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                int id = int.Parse(dataGridView2.CurrentRow.Cells["id"].Value.ToString());
                string name = dataGridView2.CurrentRow.Cells["name"].Value.ToString();
                string description = dataGridView2.CurrentRow.Cells["description"].Value.ToString();
                List<int> sagesId = dataGridView2.CurrentRow.Cells["sageId"].Value.ToString().Split(',').Select(Int32.Parse).ToList();

                updateBookInfo(id, name, description, sagesId);
            }
        }
        private void updateBookInfo(int id, string name, string description, List<int> sagesId)
        {
            textBox5.Text = id.ToString();
            textBox1.Text = name;
            textBox2.Text = description;
            textBox8.Text = String.Join(",", sagesId);


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                using (var cnt = new SageBookDbContext())
                {
                    int id = int.Parse(dataGridView2.CurrentRow.Cells["id"].Value.ToString());
                    var book = cnt.Books.First(b => b.id == id);
                    //var entry = cnt.Entry(book);
                    //if (entry.State == EntityState.Detached)
                    //    cnt.Books.Attach(book);
                    cnt.Books.Remove(book);
                    cnt.SaveChanges();
                }
            }
            loadBookRecords();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                using (var cnt = new SageBookDbContext())
                {
                    int id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    var book = cnt.Sages.First(s => s.id == id);
                    cnt.Sages.Remove(book);
                    cnt.SaveChanges();
                }
            }
            loadSageRecords();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow.Index != -1)
            {

                using (var cnt = new SageBookDbContext())
                {
                    int id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    var sage = cnt.Sages.Where(s => s.id == id);
                    string name = textBox3.Text;
                    int age = Int32.Parse(textBox4.Text);
                    byte[] photo = Sage.ImageToByteArray(pictureBox1.Image);
                    string city = textBox7.Text;

                    updateSageInfo(id, name, age, photo, city);

                    //var booksIds = cnt.Books.Where(b => b.Sages.);
                    //Sage sage = new Sage() { name = name, age = age, city = city, photo = Sage.ImageToByteArray(photo) };
                    var dbSage = cnt.Sages.First(b => b.id == id);
                    dbSage.name = name;
                    dbSage.age = age.ToString();
                    dbSage.photo = photo;
                    dbSage.city = city;
                    //var sages = cnt.Sages.Where(s => sagesId.Contains(s.id)).ToList();
                    //dbBook.Sages = dbBook.Sages;
                    //dbBook.Sages = sages;

                    cnt.SaveChanges();
                }
            }

            loadSageRecords();
        }

        private void updateSageInfo(int id, string name, int age, byte[] photo, string city) {
            textBox6.Text = id.ToString();
            textBox3.Text = name;
            textBox4.Text = age.ToString();
            pictureBox1.Image = Sage.ByteArrayToImage(photo);
            textBox7.Text = city.ToString();
        }

        private void dataGridView1_DockChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
