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
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port = 5432;Database=DbYurtKayitSistem;user ID=postgres;password=aa");
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("call ogrencii_sil(@p1,@p2)", baglanti);
            komut3.Parameters.AddWithValue("@p1", long.Parse(textBox3.Text));
            komut3.Parameters.AddWithValue("@p2", comboBox5.SelectedValue);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla silindi!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }
        
        private void button1_Click(object sender, EventArgs e)//listele
        {
            string sorgu = "select * from ogrenciListe";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }
        //call ogrenci_ekle(kisi_ad, kisi_soyad, kisi_cinsiyet, universite_id, bolum_id) values(@p1, @p2, @p3, @p4, @p5)
        private void button2_Click(object sender, EventArgs e)//kaydet
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("call ogrenci_ekle (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglanti);
            komut2.Parameters.AddWithValue("@p1", textBox1.Text);
            komut2.Parameters.AddWithValue("@p2", textBox2.Text);
            komut2.Parameters.AddWithValue("@p3", long.Parse(textBox3.Text));
            komut2.Parameters.AddWithValue("@p4", comboBox1.SelectedValue);
            komut2.Parameters.AddWithValue("@p5", comboBox2.SelectedValue);
            komut2.Parameters.AddWithValue("@p6", comboBox3.SelectedValue);
            komut2.Parameters.AddWithValue("@p7", comboBox6.SelectedValue);
            komut2.Parameters.AddWithValue("@p8", comboBox4.SelectedValue);
            komut2.Parameters.AddWithValue("@p9", comboBox5.SelectedValue);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci başarıyla eklendi!");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from bolum", baglanti);
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("Select * from cinsiyet", baglanti);
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("Select * from universite", baglanti);
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("Select * from il", baglanti);
            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("Select * from oda", baglanti);
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("Select * from yatak", baglanti);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2= new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            da.Fill(dt);
            da1.Fill(dt1);
            da2.Fill(dt2);
            da3.Fill(dt3);
            da4.Fill(dt4);
            da5.Fill(dt5);
            comboBox3.DisplayMember = "bolum_isim";
            comboBox3.ValueMember = "bolum_id";
            comboBox3.DataSource = dt;
            comboBox1.DisplayMember = "cinsiyet_ad";
            comboBox1.ValueMember = "cinsiyet_id";
            comboBox1.DataSource = dt1;
            comboBox2.DisplayMember = "universite_ad";
            comboBox2.ValueMember = "universite_id";
            comboBox2.DataSource = dt2;
            comboBox4.DisplayMember = "oda_id";
            comboBox4.ValueMember = "oda_id";
            comboBox4.DataSource = dt4;
            comboBox5.DisplayMember = "yatak_id";
            comboBox5.ValueMember = "yatak_id";
            comboBox5.DataSource = dt5;
            comboBox6.DisplayMember = "il_isim";
            comboBox6.ValueMember = "il_id";
            comboBox6.DataSource = dt3;
            baglanti.Close();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("call ogrenci_guncelle (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            komut2.Parameters.AddWithValue("@p3", long.Parse(textBox3.Text));
            komut2.Parameters.AddWithValue("@p1", textBox1.Text);
            komut2.Parameters.AddWithValue("@p2", textBox2.Text);
            komut2.Parameters.AddWithValue("@p4", comboBox2.SelectedValue);
            komut2.Parameters.AddWithValue("@p5", comboBox3.SelectedValue);
            komut2.Parameters.AddWithValue("@p6", comboBox6.SelectedValue);
            komut2.Parameters.AddWithValue("@p7", comboBox4.SelectedValue);
            komut2.Parameters.AddWithValue("@p8", comboBox5.SelectedValue);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci başarıyla güncellendi!");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
