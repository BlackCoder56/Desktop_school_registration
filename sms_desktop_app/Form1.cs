using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sms_desktop_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = 
            new SqlConnection("Data Source = DESKTOP-2R4RB09\\SQLEXPRESS; Initial Catalog = sms; User = sa; Password = sqlDb11");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;

        // function loading data into the data gride view
        public void load()
        {
            try
            {
                sql = "SELECT * FROM student";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);

                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Get id from grid view
        public void getId(string id)
        {
            sql = "SELECT * FROM student WHERE id = '" + id + "'";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                txt1.Text = read[1].ToString();
                txt2.Text = read[2].ToString();
                txt3.Text = read[3].ToString();
            }
            conn.Close();
        }
        /* If the Mode is true means add record to db else update record
         * Funtion to perform onclick fuction to save or edit data        
        */ 
        private void btn_save_Click(object sender, EventArgs e)
        {
            string name = txt1.Text;
            string course = txt2.Text;
            string fees = txt3.Text;

            if(Mode == true)
            {
                // sql query command
                sql = "INSERT INTO student(student_name,course,fee) VALUES(@student_name, @course, @fee)";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fees);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Successfully!");

                // Clear text boxes
                txt1.Clear();
                txt2.Clear();
                txt3.Clear();

                // Set txt1 focus
                txt1.Focus();
            }
            else
            {
                // id got from grid view click cell
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                // sql query command
                sql = "UPDATE student SET student_name = @student_name, course = @course, fee = @fee where id @id";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fees);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Edited Successfully!");

                // Clear text boxes
                txt1.Clear();
                txt2.Clear();
                txt3.Clear();

                // Set txt1 focus
                txt1.Focus();

                Mode = true;

                btn_save.Text = "Save";
            }
            conn.Close();
        }

        /*Data grid view cellcontentclick
         * Executed when user clicks the edit cell on grid view
        */
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getId(id);
                btn_save.Text = "Edit";
            }
        }
    }
}
