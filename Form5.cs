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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
            con.Open();

            if(comboBox1.Text=="alfabetic")
            {
                SqlDataAdapter dta = new SqlDataAdapter("select b.id_bij,b.denumire,p.producator, c.categorie, b.price, b.cantitate, b.detalii from bijuterii b join categorii c on b.id_cat=c.id_cat " +
                    "join producatori p on p.id_prod=b.id_prod order by b.denumire",con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;

            }else if(comboBox1.Text=="pret crescator")
            {
                SqlDataAdapter dta = new SqlDataAdapter("select b.id_bij,b.denumire,p.producator, c.categorie, b.price, b.cantitate, b.detalii from bijuterii b join categorii c on b.id_cat=c.id_cat " +
                 "join producatori p on p.id_prod=b.id_prod order by b.price", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            else if(comboBox1.Text=="pret descrescator")
            {
                SqlDataAdapter dta = new SqlDataAdapter("select b.id_bij,b.denumire,p.producator, c.categorie, b.price, b.cantitate, b.detalii from bijuterii b join categorii c on b.id_cat=c.id_cat " +
                   "join producatori p on p.id_prod=b.id_prod order by b.price DESC", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }
    }
}
