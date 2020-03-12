using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Eklenenler
using mshtml;
using System.Collections;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Değişkenler
        string site = "http://www.lidermafya.com/securimage/securimage_show.php";
        Bitmap bmp1, bmp2;
        ArrayList arr = new ArrayList();
        ArrayList arr2 = new ArrayList();
        DataTable tablo = new DataTable();
        OleDbDataAdapter veri = new OleDbDataAdapter();
        int veriTop = 0;
        int ii = 0;

        #region Veritabanı Güncelleme
        void listele()
        {
            OleDbDataAdapter adaptor = new OleDbDataAdapter("SELECT * FROM veriler", db.baglanti);
            DataSet ds = new DataSet();
            ds.Clear();
            adaptor.Fill(ds, "veriler");
            dataGridView1.DataSource = ds.Tables["veriler"];
            adaptor.Dispose();
            veriTop = dataGridView1.RowCount;
            timer1Calis = true;
        }
        #endregion 

        #region FORM_LOAD
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        #endregion

        #region WebBrowser_DocumentCompleted
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            #region Güvenlik Kodu Resmi Çekme
            IHTMLDocument2 doc = (IHTMLDocument2)webBrowser1.Document.DomDocument; // unmanaged document nesnesini alıyoruz
            IHTMLControlRange imgler = (IHTMLControlRange)((HTMLBody)doc.body).createControlRange(); // controlRange ile Html nesne dizisi oluşturuyoruz

            foreach (IHTMLImgElement img in doc.images) // Tüm img elementleri için
            {
                imgler.add((IHTMLControlElement)img); // Koleksiyona elementi ekliyoruz
                imgler.execCommand("copy", false, null); // Koleksiyonu Clipboard a kopyalıyoruz

                using (Bitmap bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap)) // Clipboard daki resmi bitmap olarak alıyoruz
                {
                    arr.Clear();
                    bmp1 = Image.FromHbitmap(bmp.GetHbitmap());
                    pictureBox1.Image = bmp1;
                    for (int i = 0; i < bmp1.Width; i++)
                    {
                        for (int k = 0; k < bmp1.Height; k++)
                        {
                            arr.Add(bmp1.GetPixel(i, k).Name);
                        }
                    }
                }
            }
            #endregion

            //Veritabanı Güncelleme
            listele();

            timer2.Enabled = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1Calis)
            {
                webBrowser1.Navigate(site);

            }
            
        }

        #region Resim Kaydetme
        void resimKayit()
        {
            db.baglan();
            Random rnd = new Random();
            int sayi = rnd.Next(10000);
            try
            {
                string yol = Application.StartupPath + @"\GuvenlikKodu\" + sayi.ToString() + ".jpg";
                string query = string.Format("INSERT INTO veriler(yol,sonuc)VALUES ('{0}','{1}')", yol, textBox1.Text);
                new OleDbCommand(query, db.baglanti).ExecuteNonQuery();
                pictureBox1.Image.Save(yol);
                label5.Text = sayi.ToString() + ".jpg Resim kaydedildi.";
                listele();
                timer1Calis = false;
                timer2Calis = false;
                groupBox1.Enabled = false;
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                label3.Text = "Bekleniyor";
                textBox2.Clear();
                textBox3.Clear();
                MessageBox.Show(sayi.ToString() + ".jpg Resim kaydedildi.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Threading.Thread.Sleep(100);
                timer1Calis = true;
            }
            catch
            {
                label5.Text = sayi.ToString() + ".jpg Resim Kaydedilemedi.";
                MessageBox.Show(sayi.ToString() + ".jpg Resim katdedilemedi.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            db.kes();
        }
        #endregion
        public static bool timer1Calis = false;
        public static bool timer2Calis = false;
        #region Resim Karşılaştırma
        void resimKarsilastir()
        {
            double esit = 0;
            double esitdeyil = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].ToString() == arr2[i].ToString())
                {
                    esit++;
                }
                else
                {
                    esitdeyil++;
                }
            }

            if (esitdeyil == 0)
            {
                timer1Calis = false;
                timer2Calis = false;
                label3.Text = "%100";
                MessageBox.Show("Resimler Eşleşti!","İşlem",MessageBoxButtons.OK,MessageBoxIcon.Information);
                listBox1.Items.Add(textBox2.Text);
                listele();
                groupBox1.Enabled = false;
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                label3.Text = "Bekleniyor";
                textBox2.Clear();
                textBox3.Clear();
                System.Threading.Thread.Sleep(100);
                timer1Calis = true;
            }
            else
            {
                timer1Calis = false;
                groupBox1.Enabled = true;

                try
                {
                    if (esit > esitdeyil)
                    {
                        label3.Text = "Eşleşmedi % " + Convert.ToString(100 - ((esitdeyil * 100) / esit)).Substring(0, 5);
                    }
                    else
                    {
                        label3.Text = "Eşleşmedi % " + Convert.ToString((esit * 100) / esitdeyil).Substring(0, 5);
                    }
                }
                catch
                {
                    label3.Text = "Eşleşmedi";
                }
            }
        }
        #endregion


        

        private void button1_Click(object sender, EventArgs e)
        {
            resimKayit();
        }

        private void pictureBox2_LocationChanged(object sender, EventArgs e)
        {
            resimKarsilastir();
        }

        
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2Calis) {
                ii = ii + 1;

                if (veriTop == ii)
                {
                    ii = 0;
                    
                    label3.Text = "Hiç biri eşleşmedi!";
                    MessageBox.Show("PictureBox1'deki resim eşleşmiyor. Resmi Kaydedebilirsiniz.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timer2Calis = false;

                }
                else
                {
                    string yol = dataGridView1.Rows[ii].Cells["yol"].Value.ToString();
                    string karsiligi = dataGridView1.Rows[ii].Cells["sonuc"].Value.ToString();
                    textBox2.Text = yol;
                    textBox3.Text = karsiligi;

                    arr2.Clear();
                    bmp2 = (Bitmap)Bitmap.FromFile(yol);
                    pictureBox2.Image = bmp2;
                    for (int i = 0; i < bmp2.Width; i++)
                    {
                        for (int k = 0; k < bmp2.Height; k++)
                        {
                            arr2.Add(bmp2.GetPixel(i, k).Name);
                        }
                    }
                    resimKarsilastir();
                }
            }
            

        }

    }
}
