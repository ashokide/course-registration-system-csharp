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

namespace CourseRegistrationSystem
{
    public partial class admin : Form
    {
        home homeObj;
        public admin(home o)
        {
            InitializeComponent();
            homeObj = o;
        }

        private void admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cRSDataSet1.new_courses' table. You can move, or remove it, as needed.
            this.new_coursesTableAdapter.Fill(this.cRSDataSet1.new_courses);
            // TODO: This line of code loads data into the 'cRSDataSet2.courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.cRSDataSet2.courses);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logged Out Successfully", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            homeObj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string CONNECTION_URL = "Data Source=ASHOK-LAPTOP\\SQLEXPRESS;Initial Catalog=CRS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(CONNECTION_URL);
            SqlCommand cmd = null;
            SqlDataAdapter sda;
            SqlDataReader sdr;
            try
            {
                connection.Open();
                string sql = "SELECT * FROM NEW_COURSES WHERE COURSES=" + "\'" + textBox1.Text + "\';";
                cmd = new SqlCommand(sql, connection);
                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    MessageBox.Show("Course Already Available :" + textBox1.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sdr.Close();
                    cmd.Dispose();
                }
                else
                {
                    sdr.Close();
                    sql = "INSERT INTO NEW_COURSES (COURSES) VALUES(" + "\'" + textBox1.Text + "\'" + ");";
                    sda = new SqlDataAdapter();
                    sda.InsertCommand = new SqlCommand(sql, connection);
                    sda.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Course Added :" + textBox1.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.admin_Load(sender, e);
                    textBox1.Text = "";
                    sda.Dispose();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("DB Connection Error" + err.Message, "Error");
            }
            finally
            {
                connection.Close();
            }
        }

        string selectedValue;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView2.Rows[index];
            textBox1.Text = selectedRow.Cells[0].Value.ToString();
            selectedValue = selectedRow.Cells[0].Value.ToString(); ;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string CONNECTION_URL = "Data Source=ASHOK-LAPTOP\\SQLEXPRESS;Initial Catalog=CRS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(CONNECTION_URL);
            SqlDataAdapter sda;
            try
            {
                connection.Open();
                string sql = "UPDATE NEW_COURSES SET COURSES= "+ "\'" + textBox1.Text + "\' "+"WHERE COURSES=" + "\'" + selectedValue + "\';";

                if (textBox1.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Course Field Cannot Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sda = new SqlDataAdapter();
                    sda.UpdateCommand = new SqlCommand(sql, connection);
                    sda.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show("Course Updated :" + textBox1.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.admin_Load(sender, e);
                    textBox1.Text = "";
                    sda.Dispose();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("DB Connection Error" + err.Message, "Error");
            }
            finally
            {
                connection.Close();
            }

            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            textBox1.Text = "";
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
        }
    }
}
