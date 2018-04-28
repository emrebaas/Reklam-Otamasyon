using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reklam_otomasyon
{
    public partial class Form1 : Form
    {

        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-LSFJ4N5\\SQLEXPRESS;Initial Catalog=SerasReklam;Integrated Security=True");


        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into Musteri(Müsteri_tc,MüsteriTelefonu,MüsteriAdi,MüsteriSoyadi,MüsteriAdresi) values (@tcno,@tel,@ad,@soyad,@adres)";
                string kayit2 = "insert into Siparis(siparis_musteri_tc,SiparisAdi,SiparisTarihi) values (@tcno,@adsip,@tarih)";
                string kayit3 = "insert into Urun(urun_Musteri_tc,UrunAdi,Urun_Malzeme_adi) values (@tcno,@admal,@mal_adi)";
               
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglan);
                SqlCommand komut2 = new SqlCommand(kayit2, baglan);
                SqlCommand komut3 = new SqlCommand(kayit3, baglan);
                
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@tcno", mus_tc.Text);
                komut.Parameters.AddWithValue("@ad", mus_adi.Text);
                komut.Parameters.AddWithValue("@soyad", mus_soyad.Text);
                komut.Parameters.AddWithValue("@tel", mus_tel.Text);
                komut.Parameters.AddWithValue("@adres", mus_adres.Text);

                komut2.Parameters.AddWithValue("@tcno", mus_tc.Text);
                komut2.Parameters.AddWithValue("@adsip", sip_adi.Text);
                komut2.Parameters.AddWithValue("@tarih", sip_tarih.Text);

                komut3.Parameters.AddWithValue("@tcno", mus_tc.Text);
                komut3.Parameters.AddWithValue("@admal", urun_adi.Text);
                komut3.Parameters.AddWithValue("@mal_adi", urun_malzeme.Text);

               

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                komut3.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglan.Close();
                MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into Firma(FirmaAdi,FirmaAdresi,FirmaTelefonu,firma_müsteri_tc) values (@fir_adi,@fir_adres,@fir_tel,@fir_tc)";
                string kayit2 = "insert into Referans(ReferansAdi,ReferansAdresi,ReferansTelefonu,Referans_müşteri_tc) values (@ref_adi,@ref_adres,@ref_tel,@ref_tc)";
               
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglan);
                SqlCommand komut2 = new SqlCommand(kayit2, baglan);
               

                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@fir_adi", fir_adi.Text);
                komut.Parameters.AddWithValue("@fir_adres", fir_adres.Text);
                komut.Parameters.AddWithValue("@fir_tel", fir_tel.Text);
                komut.Parameters.AddWithValue("@fir_tc", fir_tc.Text);

                komut2.Parameters.AddWithValue("@ref_tc", ref_tc.Text);
                komut2.Parameters.AddWithValue("@ref_adi", ref_adi.Text);
                komut2.Parameters.AddWithValue("@ref_adres", ref_adres.Text);
                komut2.Parameters.AddWithValue("@ref_tel", ref_tel.Text);

                
               

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();

                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglan.Close();
                MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From Musteri,Siparis,Urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["Müsteri_tc"].ToString();
                ekle.SubItems.Add(oku["MüsteriAdi"].ToString());
                ekle.SubItems.Add(oku["MüsteriSoyadi"].ToString());
                ekle.SubItems.Add(oku["MüsteriAdresi"].ToString());
                ekle.SubItems.Add(oku["MüsteriTelefonu"].ToString());

                ekle.SubItems.Add(oku["Siparisid"].ToString());
                ekle.SubItems.Add(oku["SiparisAdi"].ToString());
                ekle.SubItems.Add(oku["SiparisTarihi"].ToString());

                ekle.SubItems.Add(oku["urunid"].ToString());
                ekle.SubItems.Add(oku["UrunAdi"].ToString());
                ekle.SubItems.Add(oku["Urun_Malzeme_adi"].ToString());


                listView1.Items.Add(ekle);


            }
            baglan.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From Musteri O, Siparis K , Urun U Where O.Müsteri_tc='" + (textBox1.Text) + "' And K.siparis_musteri_tc='" + (textBox1.Text) + "' And U.urun_Musteri_tc = '" + (textBox1.Text) + "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["Müsteri_tc"].ToString();
                ekle.SubItems.Add(oku["MüsteriAdi"].ToString());
                ekle.SubItems.Add(oku["MüsteriSoyadi"].ToString());
                ekle.SubItems.Add(oku["MüsteriAdresi"].ToString());
                ekle.SubItems.Add(oku["MüsteriTelefonu"].ToString());

                ekle.SubItems.Add(oku["Siparisid"].ToString());
                ekle.SubItems.Add(oku["SiparisAdi"].ToString());
                ekle.SubItems.Add(oku["SiparisTarihi"].ToString());

                ekle.SubItems.Add(oku["urunid"].ToString());
                ekle.SubItems.Add(oku["UrunAdi"].ToString());
                ekle.SubItems.Add(oku["Urun_Malzeme_adi"].ToString());

                listView1.Items.Add(ekle);


            }


            baglan.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sil = new SqlCommand("Delete from Musteri where Müsteri_tc='" + textBox1.Text.ToString() + "'", baglan);
            sil.ExecuteNonQuery();

            MessageBox.Show(" Tc numarası "+textBox1.Text.ToString()+" olan müşteri silindi.");



            baglan.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sil = new SqlCommand("Delete From Firma F,Referans R where F.firma_müsteri_tc='" + textBox2.Text.ToString() + "' And R.Referans_müşteri_tc='" + textBox2.Text.ToString() + "'", baglan);
           
            sil.ExecuteNonQuery();

            MessageBox.Show(" Tc numarası " + textBox1.Text.ToString() + " olan müşteri silindi.");



            baglan.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand komut = new SqlCommand("Select * From Firma O, Referans K  Where O.firma_müsteri_tc='" + (textBox2.Text) + "' And K.Referans_müşteri_tc='" + (textBox2.Text) +  "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["firma_müsteri_tc"].ToString();
                ekle.SubItems.Add(oku["FirmaAdi"].ToString());
                ekle.SubItems.Add(oku["FirmaAdresi"].ToString());
                ekle.SubItems.Add(oku["FirmaTelefonu"].ToString());
                ekle.SubItems.Add(oku["Referans_müşteri_tc"].ToString());
                ekle.SubItems.Add(oku["ReferansAdi"].ToString());
                ekle.SubItems.Add(oku["ReferansAdresi"].ToString());
                ekle.SubItems.Add(oku["ReferansTelefonu"].ToString());


                listView2.Items.Add(ekle);
            }
            baglan.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand sil = new SqlCommand("Delete from Firma where firma_müsteri_tc='" + textBox2.Text.ToString() + "'", baglan);
            sil.ExecuteNonQuery();

            MessageBox.Show(" Tc numarası " + textBox2.Text.ToString() + " olan müşteri silindi.");
        }
    }
}




