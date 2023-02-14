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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        SinavOgrenciEntities db = new SinavOgrenciEntities();//veri tabanı tanımlama
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ogrenci o = new Ogrenci();
            o.ad = txtOgrenciAd.Text;
            o.soyad = txtOgrenciSoyad.Text;
            db.Ogrenci.Add(o);
            db.SaveChanges();
            MessageBox.Show("Öğrenci eklendi ");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-V96KRV1T;Initial Catalog=SinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("SELECT * FROM Ders", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {


            //veritabanı listeleme
            dataGridView1.DataSource = db.Ogrenci.ToList();

            //istenmeyen sütunları gizleme
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnNotListesi_Click(object sender, EventArgs e)
        {
            //istenilen sütunları yazdırıyoruz burada foreach benzeri bir yapıyla
            var query = from item in db.Notlar
                        select new
                        {
                            item.notID,
                            item.Ogrenci.ad,
                            item.Ogrenci.soyad,
                            item.Ders.dersAdi,
                            item.sinav1,
                            item.sinav2,
                            item.sinav3,
                            item.ortalama,
                            item.durum
                        };
            dataGridView1.DataSource = query.ToList();



            //dataGridView1.DataSource=db.Notlar.ToList();
        }

        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            Ders d = new Ders();
            d.dersAdi = txtDersAd.Text;
            db.Ders.Add(d);
            db.SaveChanges();
            MessageBox.Show("Ders Eklendi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciID.Text);
            var x = db.Ogrenci.Find(id);
            db.Ogrenci.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciID.Text);
            var x = db.Ogrenci.Find(id);
            x.ad = txtOgrenciAd.Text;
            x.soyad = txtOgrenciSoyad.Text;
            x.fotograf = txtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Değiştirildi");

        }

        private void btnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotListesi();//prosedür yazıp prosedürü çağırdık 
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
                db.Ogrenci.Where(x => x.ad == txtOgrenciAd.Text | x.soyad == txtOgrenciSoyad.Text).ToList();
        }

        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {
            // textboxa yazarken yazımız ne ise onu içeren metodlar datagridview içerisinde listeleniyor
            string aranan = txtOgrenciAd.Text;
            var degerler =
                from item in db.Ogrenci where item.ad.Contains(aranan) select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            //ascending a'dan z ye sıralama
            if (radioButton1.Checked == true)
            {
                List<Ogrenci> liste1 = db.Ogrenci.OrderBy(p => p.ad).ToList();
                dataGridView1.DataSource = liste1;
            }
            //descending sıralama
            if (radioButton4.Checked == true)
            {
                List<Ogrenci> liste2 = db.Ogrenci.OrderByDescending(p => p.ad).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<Ogrenci> liste3 = db.Ogrenci.OrderBy(p => p.ad).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton2.Checked == true)
            {
                List<Ogrenci> liste4 = db.Ogrenci.Where(x => x.ogrenciID.ToString() == txtOgrenciID.Text).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {

                List<Ogrenci> liste5 = db.Ogrenci.Where(x => x.ad.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked == true)
            {
                List<Ogrenci> liste6 = db.Ogrenci.Where(x => x.ad.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }

            if (radioButton7.Checked == true)
            {
                bool deger = db.Ogrenci.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton8.Checked == true)
            {
                int ogrenciSayisi = db.Ogrenci.Count();
                MessageBox.Show(ogrenciSayisi.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton9.Checked == true)
            {
                var toplamPuan = db.Notlar.Sum(x => x.sinav1);
                MessageBox.Show("1.Sınav Toplam Puanları: " + toplamPuan, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton10.Checked == true)
            {
                var ortalamaPuan = db.Notlar.Average(x => x.sinav1);
                MessageBox.Show("1.Sınav Ortalama Puanları: " + ortalamaPuan, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton11.Checked == true)
            {
                var ortalamaPuan = db.Notlar.Average(x => x.sinav1);
                List<Notlar> liste7 = db.Notlar.Where(x => x.sinav1 > ortalamaPuan).ToList();
                dataGridView1.DataSource = liste7;
            }
            if (radioButton12.Checked == true)
            {
                var enYuksekPuan = db.Notlar.Max(x => x.sinav1);
                var enDusukPuan = db.Notlar.Min(x => x.sinav1);
                MessageBox.Show("1.Sınav En Yüksek Puan: " + enYuksekPuan + " En Düşük puan ise : " + enDusukPuan, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.Notlar
                        join d2 in db.Ogrenci
                        on d1.ogrenciID equals d2.ogrenciID
                        join d3 in db.Ders
                        on d1.dersID equals d3.dersID
                        select new
                        {
                            Ogrenci = d2.ad + " " + d2.soyad,
                            Soyad = d2.soyad,
                            Ders = d3.dersAdi,
                            Sinav1 = d1.sinav1,
                            Sinav2 = d1.sinav2,
                            Sinav3 = d1.sinav3,
                            Ortalama = d1.ortalama

                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
