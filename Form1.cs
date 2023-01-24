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

namespace Medii_si_instrumente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True"))
                {
                    string query = "Select * from useri where username='" + username.Text.Trim() +
                        "' and parola= '" + password.Text.Trim() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        Form2 form2 = new Form2();
                        this.Hide();
                        form2.Show();
                    }
                    else
                    {
                        MessageBox.Show("Datele intrduse nu sunt corecte!", "Incorect");
                    }
                }
            }
        }

        private bool isValid()
        {
            if (username.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Username gresit!", "Error");
                return false;
            }
            else if (password.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Parola invalida!", "Error");
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
