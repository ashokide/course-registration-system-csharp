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
    public partial class register : Form
    {
        home homeObj;
        public register(home o)
        {
            InitializeComponent();
            homeObj = o;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            homeObj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals("") || textBox3.Text.Trim().Equals(""))
            {
                MessageBox.Show("Register Fields Cannot Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string CONNECTION_URL = "Data Source=ASHOK-LAPTOP\\SQLEXPRESS;Initial Catalog=CRS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(CONNECTION_URL);
            SqlCommand cmd = null;
            string sql = "SELECT * FROM AUTH WHERE USERNAME=" + "\'" + textBox1.Text + "\'";
            SqlDataReader sdr;
            SqlDataAdapter sda;

            try
            {
                connection.Open();
                cmd = new SqlCommand(sql, connection);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    MessageBox.Show("User Exists With Name : "+sdr.GetString(0), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = "";
                    textBox3.Text = "";
                    sdr.Close();
                }
                else
                {
                    cmd.Dispose();
                    sdr.Close();
                    sql = "INSERT INTO AUTH (USERNAME,PASSWORD) VALUES("+ "\'" + textBox1.Text+"\'"+","+ "\'" + textBox3.Text + "\'" + ");";
                    sda = new SqlDataAdapter();
                    sda.InsertCommand = new SqlCommand(sql, connection);
                    sda.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("User Account Created With Name :"+textBox1.Text , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox3.Text = "";
                    this.Close();
                    new login(homeObj).Show();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("DB Connection Error"+err.Message, "Error");
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Really Want to Close The Application", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                homeObj.Close();
            }
        }
    }
}
