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

namespace MesajPaneli
{
    public partial class MesajPaneli : Form
    {
        public MesajPaneli()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbMesajlasmaPaneli;Integrated Security=True");
        public string numara;

        void GelenKutusu()
        {
            try 
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from TblMesajlar where ALICI=" + numara, baglanti);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                dataGridView2.DataSource = dt1;

            }
            catch 
            { 
                MessageBox.Show("Mesaj Gelemedi Hatası..","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }   
        }
        void GidenKutusu()
        {
            try
            {
                SqlDataAdapter da2 = new SqlDataAdapter("select * from TblMesajlar where GONDEREN=" + numara, baglanti);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView1.DataSource = dt2;

            }
            catch
            {
                MessageBox.Show("Mesaj Gidemedi Hatası..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Temizle()
        {
            maskedTextBoxALICI.Text = "";
            textBoxBASLIK.Text = "";
            richTextBoxICERIK.Text = "";
            maskedTextBoxALICI.Focus();
        }


        private void MesajPaneli_Load(object sender, EventArgs e)
        {
            labelNUM.Text = numara;
            GelenKutusu();
            GidenKutusu();
            // Ad ve Soyad çekme
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select AD, SOYAD from TblKisiler where NUMARA = @p1", baglanti);
                komut.Parameters.AddWithValue("@p1", numara);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    labelAD.Text = dr["AD"].ToString();
                    labelSOYAD.Text = dr["SOYAD"].ToString();
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ad ve Soyad bilgisi çekilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GelenKutusu();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(" insert into  TblMesajlar (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBoxALICI.Text);
            komut.Parameters.AddWithValue("@p3", textBoxBASLIK.Text);
            komut.Parameters.AddWithValue("@p4", richTextBoxICERIK.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GidenKutusu();
            Temizle();

        }
    }
}
