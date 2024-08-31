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


 namespace MesajPaneli
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbMesajlasmaPaneli;Integrated Security=True");

        private void buttonGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from TblKisiler where NUMARA=@p1 and SIFRE=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", maskedTextBoxNUM.Text);
            komut.Parameters.AddWithValue("p2", textBoxSIFRE.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MesajPaneli frm= new MesajPaneli();
                frm.numara=maskedTextBoxNUM.Text;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi");
            }
            baglanti.Close();
           
        }
    }
}
