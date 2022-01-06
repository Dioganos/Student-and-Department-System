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
    public partial class BolumOgrenci : Form
    {
        private List<string> satirlar = new List<string>();
        private List<string> cikti = new List<string>();
        private static string dosya = @"data.txt";
        
        public BolumOgrenci()
        {
            InitializeComponent();

            if (!File.Exists(dosya)) 
            {
                using (FileStream fs = File.Create(dosya)) { } 
            }

            else
            { 
                satirlar = File.ReadAllLines(dosya).ToList();
            }

            foreach (string line in satirlar) 
            {
                string[] entries = line.Split(',');      
                if (!Data.ogrenciler.ogrencivar(entries[2]))   
                    Data.ogrenciler.Ekle(entries[0], entries[1], entries[2], entries[3]); 
            }
            BolumYukle();
            
            
        }

        private void BolumYukle()
        {
            foreach (var item in Data.ogrenciler.BolumGonder())
            {
                BolumBox.Items.Add(item);
            }
                
        }

        public void Reload()
        {
            BolumBox.Items.Clear();
            BolumBox.Text = "";
            OgrenciBox.Items.Clear();
            OgrenciBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            BolumYukle();
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
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BolumOgrenciEkleCikarDegistir becd = new BolumOgrenciEkleCikarDegistir();
            becd.Show();
        }

        private void BolumOgrenci_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string item in Data.ogrenciler.SendBack())
            {
                cikti.Add(item);
            }

            File.WriteAllLines(dosya, cikti);
        }


        bool tutuyom;
        int FareX, FareY;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            tutuyom = true;
            FareX = Cursor.Position.X - this.Left;
            FareY = Cursor.Position.Y - this.Top;
        }
        

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (tutuyom == true)
            {
                this.Left = Cursor.Position.X - FareX;
                this.Top = Cursor.Position.Y - FareY;
            }

        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            tutuyom = false;
            FareX = 0;
            FareY = 0;
        }
        
        private void button1_MouseHover(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
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
            label7.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1- Bu sekmede herhangi bir degisiklik yapilamaz!\n" +
                "2- Bir kisinin numara ve sinif bilgisini gormek icin once \"Bolum\", Sonra o bolumdeki \"Ogrenci\" secilmelidir!\n" +
                "3- Kisinin bilgileri uzerinde degisiklik yapmak icin bu sekmedeki kalem simgesine tiklayiniz!");
        }

       
    }
}
