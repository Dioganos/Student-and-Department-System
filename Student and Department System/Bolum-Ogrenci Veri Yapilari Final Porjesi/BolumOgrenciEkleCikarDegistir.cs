using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bolum_Ogrenci_Veri_Yapilari_Final_Porjesi
{
    public partial class BolumOgrenciEkleCikarDegistir : Form
    {
        private List<string> yazdir = new List<string>();
        private static string dosya = @"data.txt";
        
        public BolumOgrenciEkleCikarDegistir()
        {
            InitializeComponent();
            BolumYukle();
        }
        private void BolumYukle()
        {
            foreach (var item in Data.ogrenciler.BolumGonder())
            {
                BolumBox.Items.Add(item);
            }

        }

        public void Yenile()
        {
            BolumBox.Items.Clear();
            BolumBox.Text = "";
            OgrenciBox.Items.Clear();
            OgrenciBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            BolumYukle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "" && OgrenciBox.Text != "" && BolumBox.Text != "")
            {
                if (!Data.ogrenciler.ogrencivar(OgrenciBox.Text) || !Data.ogrenciler.bolumvar(BolumBox.Text))
                    Data.ogrenciler.Ekle(textBox2.Text, textBox1.Text, OgrenciBox.Text, BolumBox.Text);

                else if (Data.ogrenciler.ogrencivar(OgrenciBox.Text))
                { 
                    Data.ogrenciler.OgrenciBul(OgrenciBox.Text).numara = textBox1.Text;
                    Data.ogrenciler.OgrenciBul(OgrenciBox.Text).sinif = textBox2.Text;
                }

                Yenile();
            }

            else 
            { 
                MessageBox.Show("Bütün verileri girilmeden ogrenci eklenemez !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BolumBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OgrenciBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            OgrenciBox.Items.Clear();

            foreach (var item in Data.ogrenciler.OgrenciGonder(BolumBox.Text)) 
            { 
                OgrenciBox.Items.Add(item);
            }
        }
        private void OgrenciBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text = Data.ogrenciler.OgrenciBul(OgrenciBox.Text).numara;
            textBox2.Text = "";
            textBox2.Text = Data.ogrenciler.OgrenciBul(OgrenciBox.Text).sinif;
        }

        
        

        private void BolumOgrenciEkleCikarDegistir_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string item in Data.ogrenciler.SendBack())
            { 
                yazdir.Add(item);
            }

            File.WriteAllLines(dosya, yazdir);
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            BolumOgrenci bo = new BolumOgrenci();
            bo.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (OgrenciBox.Text != "")
            {
                Data.ogrenciler.Cikar(OgrenciBox.Text);
            }

            else
            {
                foreach (var item in Data.ogrenciler.BolumNodeGonder(BolumBox.Text))
                {
                    Data.ogrenciler.Cikar(item.ogrenci);
                }
            }
            Yenile();
        }

        bool tutuyom;
        int FareX, FareY;


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tutuyom == true)
            {
                this.Left = Cursor.Position.X - FareX;
                this.Top = Cursor.Position.Y - FareY;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            tutuyom = true;
            FareX = Cursor.Position.X - this.Left;
            FareY = Cursor.Position.Y - this.Top;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            tutuyom = false;
            FareX = 0;
            FareY = 0;
        }


        private void button1_MouseHover(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            label7.Visible = true;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            label9.Visible = true;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1- Bir kisinin uzerinde numara ve sinif bilgisi degisikligi yapmak icin once bolum seciniz ardindan o bolume ait ogrenciler \"Ogrenci\" kutucugunda siralanacaktir\n" +
                "2- \"Ogrenci\" kutucugunda siralanan ogrencilerden birini sectiginizde eger o ogrenciye ait bir bilgi daha onceden eklendiyse ilgili kutularda belirecektir ve uzerinde degisiklik yapıp kalem simgesine tıklarsanız bilgiler guncellenecektir..\n" +
                "3- \"Ogrenci\" veya \"Bolum\" kutucuklari bos iken yazilacak yeni bilgiler dogrultusunda kalem simgesine tiklanmasi dahilinde sisteme kayit edilecektir\n" +
                "4- \"Bolum\" sekmesinde ilgili bolum secili iken \"Ogrenci\" sekmesi bossa ve yeni bir ogrenci ismi yazilir ardından kalem simgesine tiklanirsa ilgili bolume yeni ogrenci kayit edilmis olur.\n" +
                "5- Sadece \"Bolum\" sekmesi seciliyken cop kutusu simgesine tiklanmasi ilgili bolumun tamamini ve icindeki bilgileri siler. Sadece \"Ogrenci\" bilgisi silmek icin gerekli ogrenci de secilmelidir.");
        }

       
    }
}
