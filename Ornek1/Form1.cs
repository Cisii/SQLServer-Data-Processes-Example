using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ornek1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into ogrenciBilgileri(kayitTarihi,adSoyad,gsmNo) VALUES(@kayitTarihi,@adSoyad,@gsmNo)";

            cmd.Parameters.AddWithValue("@kayitTarihi", dtTarih.Value.Date);
            cmd.Parameters.AddWithValue("@adSoyad", txtadSoyad.Text);
            cmd.Parameters.AddWithValue("@gsmNo", txtgsmNo.Text);

            if (cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Ekleme Başarılı");
                listele();
                idListele();
            }

            conn.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update ogrenciBilgileri set kayitTarihi=@kayitTarihi, adSoyad=@adSoyad, gsmNo=@gsmNo where Id=@Id";

            cmd.Parameters.AddWithValue("@kayitTarihi", dtTarih.Value.Date);
            cmd.Parameters.AddWithValue("@adSoyad", txtadSoyad.Text);
            cmd.Parameters.AddWithValue("@gsmNo", txtgsmNo.Text);
            cmd.Parameters.AddWithValue("@Id", cmbxId.Text);


            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Güncelleme Başarılı");
                listele();
                idListele();
            }

            conn.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from ogrenciBilgileri where Id=@Id";

            cmd.Parameters.AddWithValue("@Id", cmbxId.Text);

            if (cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Silme işlemi tamamlandı");
                listele();
                idListele();
            }
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            idListele();
        }

        void idListele()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from ogrenciBilgileri";

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            cmbxId.Items.Clear();
            while (dr.Read())
            {
                cmbxId.Items.Add(dr["Id"].ToString());
            }
            conn.Close();
        }

        private void cmbxId_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from ogrenciBilgileri where Id=@Id";

            cmd.Parameters.AddWithValue("@Id", cmbxId.Text);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                txtadSoyad.Text = dr["adSoyad"].ToString();
                txtgsmNo.Text = dr["gsmNo"].ToString();
                dtTarih.Value = Convert.ToDateTime(dr["kayitTarihi"].ToString()).Date;
            }
            conn.Close();


        }

        void listele()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=BILCE;Initial Catalog=Okul;Integrated Security=True";
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from ogrenciBilgileri", conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }
    }
}
