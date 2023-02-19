using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        SinavOgrenciEntities db = new SinavOgrenciEntities();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.Notlar.Where(x => x.sinav1 < 50);
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton2.Checked == true)
            {
                var degerler = db.Ogrenci.Where(x => x.ad == "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton3.Checked == true)
            {
                var degerler = db.Ogrenci.Where(x => x.ad == textBox1.Text || x.soyad == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton4.Checked == true)
            {
                //sadece soyadlarını getirmek için anonymus type
                var degerler = db.Ogrenci.Select(x => new { soyadi = x.soyad });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton5.Checked == true)
            {
                //isim büyük soyisim küçük anonymus type 
                //anonymus type'ın ismi ne ise sütun başlığına da o yazılır
                var degerler = db.Ogrenci.Select(x => new { ad = x.ad.ToUpper(), soyad = x.soyad.ToLower() });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton6.Checked == true)
            {
                //şartlı seçim
                var degerler = db.Ogrenci.Select(x => new
                {
                    Ad = x.ad.ToUpper(),
                    Soyad = x.soyad.ToLower()
                }).Where(x => x.Ad != "Ali");

                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton7.Checked == true)
            {
                var degerler = db.Notlar.Select(x => new
                {
                    //İD = x.ogrenciID,
                    Adı = x.Ogrenci.ad.ToUpper(),
                    Soyadı = x.Ogrenci.soyad.ToLower(),
                    Ortalaması = x.ortalama,
                    Durumu = x.durum == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton8.Checked)
            {
                var degerler = db.Notlar.SelectMany(x => db.Ogrenci.Where(y => y.ogrenciID == x.ogrenciID), (x, y) => new
                {
                    Ad = y.ad,
                    Soyad = y.soyad,
                    Ortalama = x.ortalama,
                    Durum = x.durum == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton10.Checked)
            {
                //ilk 3 değeri getir
                //db.Ogrenci.OrderBy(x => x.ogrenciID).Take(3);
                var degerler = db.Ogrenci.OrderBy(x => x.ogrenciID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton9.Checked)
            {
                //son 3 değeri getir
                var degerler = db.Ogrenci.OrderByDescending(x => x.ogrenciID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton11.Checked)
            {
                //ada göre sıralama
                //var degerler = db.Ogrenci.GroupBy(x => x.ad);
                var degerler = db.Ogrenci.OrderBy(x => x.ad);
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton12.Checked)
            {
                //ilk 5 değeri atla
                var degerler = db.Ogrenci.OrderBy(x => x.ogrenciID).Skip(5);
                dataGridView1.DataSource = degerler.ToList();

            }



        }

    }
}
