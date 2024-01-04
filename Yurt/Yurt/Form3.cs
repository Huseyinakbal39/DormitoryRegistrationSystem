using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Yurt
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port = 5432;Database=DbYurtKayitSistem;user ID=postgres;password=aa");
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from personelListe";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from personel_tur", baglanti);
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("Select * from cinsiyet", baglanti);
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("Select * from il", baglanti);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            da.Fill(dt);
            da1.Fill(dt1);
            da2.Fill(dt2);
            comboBox3.DisplayMember = "il_isim";
            comboBox3.ValueMember = "il_id";
            comboBox3.DataSource = dt2;
            comboBox1.DisplayMember = "cinsiyet_ad";
            comboBox1.ValueMember = "cinsiyet_id";
            comboBox1.DataSource = dt1;
            comboBox2.DisplayMember = "tur_ad";
            comboBox2.ValueMember = "tur_id";
            comboBox2.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("call personel_ekle (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut2.Parameters.AddWithValue("@p1", long.Parse(textBox1.Text));
            komut2.Parameters.AddWithValue("@p2", textBox2.Text);
            komut2.Parameters.AddWithValue("@p3", textBox3.Text);
            komut2.Parameters.AddWithValue("@p4", comboBox1.SelectedValue);
            komut2.Parameters.AddWithValue("@p5", comboBox2.SelectedValue);
            komut2.Parameters.AddWithValue("@p6", comboBox3.SelectedValue);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel başarıyla eklendi!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("call personel_sil(@p1)", baglanti);
            komut3.Parameters.AddWithValue("@p1", long.Parse(textBox1.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("call personel_guncelle (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut2.Parameters.AddWithValue("@p1", long.Parse(textBox1.Text));
            komut2.Parameters.AddWithValue("@p2", textBox2.Text);
            komut2.Parameters.AddWithValue("@p3", textBox3.Text);
            komut2.Parameters.AddWithValue("@p4", comboBox2.SelectedValue);
            komut2.Parameters.AddWithValue("@p5", comboBox3.SelectedValue);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel başarıyla güncellendi!");
        }
    }
}
