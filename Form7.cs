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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
            con.Open();
            SqlDataAdapter dta = new SqlDataAdapter("select convert(time(0), t.data_tranz) as Ora, t.id_tranz, c.nume, c.prenume, b.denumire, v.nr_buc from bijuterii b join vanzari v on b.id_bij=v.id_bij join tranzactii t on v.id_tranz=t.id_tranz join clienti c on c.id_client=t.id_cump where convert(date,t.data_tranz)='" + dateTimePicker1.Value + "'", con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
