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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
            con.Open();
            string query="select * from bijuterii where denumire = @den";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dta = new DataTable();
            sda.Fill(dta);
            if (dta.Rows.Count == 0)
            {
                MessageBox.Show("Bijuteria introdusa nu exista!", "Incorect");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update bijuterii set cantitate=cantitate-@cant where denumire = @den", con);
                cmd.Parameters.AddWithValue("@den", textBox1.Text);
                cmd.Parameters.AddWithValue("@cant", int.Parse(textBox2.Text));
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("delete from bijuterii where denumire=@den and cantitate=0", con);
                cmd1.Parameters.AddWithValue("@den", textBox1.Text);
                cmd1.Parameters.AddWithValue("@cant", int.Parse(textBox2.Text));
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Produsele au fost sterse!");
            }

           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }
    }
}
