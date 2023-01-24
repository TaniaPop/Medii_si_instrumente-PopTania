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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
                con.Open();
                var command = new System.Data.SqlClient.SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.Text;
                command.CommandText = "select producator from producatori";
                var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                DataTable dtDataBase = dataset.Tables[0];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    comboBox2.Items.Add(dataset.Tables[0].Rows[i][0].ToString());
                }

                command.CommandText = "select categorie from categorii";
                adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                dataset = new DataSet();
                adapter.Fill(dataset);
                dtDataBase = dataset.Tables[0];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(dataset.Tables[0].Rows[i][0].ToString());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into bijuterii ( denumire, price, cantitate,detalii) values (@den, @price, @cant,@det)",con);
            SqlCommand cmd1 = new SqlCommand("update bijuterii set id_prod=(select id_prod from producatori where producator=@prod)",con);
            SqlCommand cmd2 = new SqlCommand("update bijuterii set id_cat=(select id_cat from categorii where categorie=@cat)",con);
            cmd2.Parameters.AddWithValue("@cat", comboBox1.Text);
            cmd1.Parameters.AddWithValue("@prod", comboBox2.Text);
            cmd.Parameters.AddWithValue("@den",textBox1.Text);
            cmd.Parameters.AddWithValue("@price", float.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@cant", int.Parse(textBox3.Text));
            cmd.Parameters.AddWithValue("@det", textBox4.Text);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Date introduse cu succes!");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
