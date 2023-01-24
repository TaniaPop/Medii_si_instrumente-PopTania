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
    public partial class Form4 : Form
    {
        List<Producatori> prod = new List<Producatori>();
        List<Bijuterii> bij = new List<Bijuterii>();
        float cos = 0;
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True"))
            {
                con.Open();
                string query = "Select * from clienti where nume = '"+textBox1.Text+"' and prenume='"+textBox2.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dta = new DataTable();
                sda.Fill(dta);


                if (dta.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into clienti ( nume, prenume, adresa,telefon,mail) values (@nume,@pren, @adr, @tele, @email)", con);
                    cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pren", textBox2.Text);
                    cmd.Parameters.AddWithValue("@adr", textBox3.Text);
                    cmd.Parameters.AddWithValue("@tele", textBox4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datele clinetului nou au fost introduse!", "Succesfully");
                }

                SqlCommand cmd1 = new SqlCommand("insert into tranzactii(data_tranz,id_cump) select @data,id_client from clienti where nume=@nume and prenume=@prenume ", con);
                cmd1.Parameters.AddWithValue("@nume", textBox1.Text);
                cmd1.Parameters.AddWithValue("@prenume", textBox2.Text);
                cmd1.Parameters.AddWithValue("@data", DateTime.Now);
                cmd1.ExecuteNonQuery();

                var command = new System.Data.SqlClient.SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.Text;
                command.CommandText = "select id_tranz from tranzactii t join clienti c on t.id_cump= c.id_client where c.nume=@nume and prenume=@prenume";
                command.Parameters.AddWithValue("@nume", textBox1.Text);
                command.Parameters.AddWithValue("@prenume", textBox2.Text);
                //command.Parameters.AddWithValue("@data", DateTime.Now.Date);
                var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                DataTable dtDataBase = dataset.Tables[0];
                string id_tranz = dataset.Tables[0].Rows[0][0].ToString();
                int id = int.Parse(id_tranz);

                SqlCommand cmd2 = new SqlCommand("insert into vanzari(id_tranz, id_bij, nr_buc) select '"+id+"', id_bij, @cant from bijuterii where denumire = @bij", con);
                SqlCommand cmd3 = new SqlCommand("update bijuterii set cantitate=cantitate - @cant where denumire=@bij", con);

                cmd2.Parameters.AddWithValue("@bij", comboBox1.Text);
                cmd2.Parameters.AddWithValue("@cant", int.Parse(textBox7.Text));
                cmd3.Parameters.AddWithValue("@cant", int.Parse(textBox7.Text));
                cmd3.Parameters.AddWithValue("@bij", comboBox1.Text);
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Produs introdus in cos!");

                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.Text;
                command.CommandText = "select price from bijuterii where denumire =@bij";
                command.Parameters.AddWithValue("@bij", comboBox1.Text);
                adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                dataset = new DataSet();
                adapter.Fill(dataset);
                DataTable dtDataBase1 = dataset.Tables[0];
                string id_price = dataset.Tables[0].Rows[0][0].ToString();
                cos=cos + (float.Parse(id_price)*int.Parse(textBox7.Text));
                textBox6.Text = cos.ToString();

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            int id = prod[comboBox2.SelectedIndex].id_prod;
            foreach (string name in GetBijByIdent(id))
            {
                this.comboBox1.Items.Add(name);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7ESVVP9\SQLEXPRESS;Initial Catalog=bij_shop;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from producatori", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["producator"]);
                prod.Add(new Producatori()
                {
                    id_prod = ((int)dr["id_prod"]),
                    producator = dr["producator"] as string
                });
            }
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from bijuterii", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                bij.Add(new Bijuterii()
                {
                    id_bij = ((int)dr1["id_bij"]),
                    denumire = dr1["denumire"] as string,
                    id_prod = ((int)dr1["id_prod"])
                });
            }

            con.Close();
        }

        private string[] GetBijByIdent(int id)
        {
            return bij.Where(line => line.id_prod == id).Select(l => l.denumire).ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Produsele adaugate in cos vor fi livrate la adresa dumneavoastra in decurs de 2-5 zile lucratoare!");
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";
        }
    }
    [Serializable]
        class Producatori
        {
            public int id_prod { get; set; }
            public string producator { get; set; }
        }
        [Serializable]
        class Bijuterii
        {
            public int id_bij { get; set; }
            public string denumire { get; set; }
            public int id_prod { get; set; }
        }
    
}
