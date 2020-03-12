/*
 * 
 * 
 * 
	* mMafya Bot programının tasarımı ve kodlaması "Osman Yavuz" tarafından yapılmıştır.
	* https://twitter.com/omnyvz
    * https://www.facebook.com/omnyvz
    * http://hatosbilisim.com/
    * http://blog.hatosbilisim.com
 * 
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Eklenen
//using HtmlAgilityPack;

namespace mMafya_Bot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Veri Ayıklama Fonksiyon
        public string veri;
        void veriAyiklama(string kaynakKod, string ilkVeri, int ilkVeriKS, string sonVeri)
        {
            try
            {
                string gelen = kaynakKod;
                int titleIndexBaslangici = gelen.IndexOf(ilkVeri) + ilkVeriKS;
                int titleIndexBitisi = gelen.Substring(titleIndexBaslangici).IndexOf(sonVeri);
                veri = gelen.Substring(titleIndexBaslangici, titleIndexBitisi);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Hata: " + ex.Message, "Hata;", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Kodlar 
        /*
         * try
            {
                webBrowser1.Document.GetElementById("hunt_username").InnerText = "YILAN";
                webBrowser1.Document.GetElementById("hunt_time").InnerText = "10";

                HtmlElementCollection elc2 = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el2 in elc2)
                {
                    if (el2.GetAttribute("value").Equals("Hepsini Seç"))
                    {
                        el2.InvokeMember("onclick");
                    }

                    if (el2.GetAttribute("name").Equals("hunt_button"))
                    {
                        el2.InvokeMember("Click");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         */
        #endregion

        string kaynakSite = null;
        string refSite = null;
        string htmlKOD = null;
        int sure = 0;
        int toplamSaldiri = 0;
        string saat = null;
        bool islem = false;


        #region FORM_LOAD
        private void Form1_Load(object sender, EventArgs e)
        {
            //Kaynak Site
            //webBrowser1.Navigate(kaynakSite);
        }
        #endregion

        #region HTML Kodları
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //Site kaynak kodları
                htmlKOD = webBrowser1.Document.Body.InnerHtml.ToString();
                richTextBox1.Text = htmlKOD;
                //Site url
                string siteURL = webBrowser1.Url.ToString();

                //Tablo
                #region Toblo
                /* try
            {
                HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
                dokuman.LoadHtml(htmlKOD);
                HtmlNodeCollection XPath = dokuman.DocumentNode.SelectNodes("//table[@class='subTitle']/tbody/tr");
                foreach (var veri2 in XPath)
                {

                    richTextBox2.Text = veri2.InnerHtml;

                    string aranilan = null;
                    string sureDurum = null;
                    string gecerlilik = null;
                    string ulke = null;

                    //Aranılan
                    try
                    {
                        HtmlAgilityPack.HtmlDocument dokuman2 = new HtmlAgilityPack.HtmlDocument();
                        dokuman2.LoadHtml(veri2.InnerHtml);
                        HtmlNodeCollection XPath2 = dokuman.DocumentNode.SelectNodes("//table[@class='subTitle']/tbody/td[1]");
                        foreach (var veri3 in XPath2)
                        {
                            aranilan = veri3.InnerText;
                        }
                    }
                    catch { }
                    //Süre/Durum
                    try
                    {
                        HtmlAgilityPack.HtmlDocument dokuman2 = new HtmlAgilityPack.HtmlDocument();
                        dokuman2.LoadHtml(veri2.InnerHtml);
                        HtmlNodeCollection XPath2 = dokuman.DocumentNode.SelectNodes("//table[@class='subTitle']/tbody/td[2]");
                        foreach (var veri3 in XPath2)
                        {
                            sureDurum = veri3.InnerText;
                        }
                    }
                    catch { }
                    //Geçerlilik
                    try
                    {
                        HtmlAgilityPack.HtmlDocument dokuman2 = new HtmlAgilityPack.HtmlDocument();
                        dokuman2.LoadHtml(veri2.InnerHtml);
                        HtmlNodeCollection XPath2 = dokuman.DocumentNode.SelectNodes("//table[@class='subTitle']/tbody/td[3]");
                        foreach (var veri3 in XPath2)
                        {
                            gecerlilik = veri3.InnerText;
                        }
                    }
                    catch { }
                    //Ülke
                    try
                    {
                        HtmlAgilityPack.HtmlDocument dokuman2 = new HtmlAgilityPack.HtmlDocument();
                        dokuman2.LoadHtml(veri2.InnerHtml);
                        HtmlNodeCollection XPath2 = dokuman.DocumentNode.SelectNodes("//table[@class='subTitle']/tbody/td[4]");
                        foreach (var veri3 in XPath2)
                        {
                            ulke = veri3.InnerText;
                        }
                    }
                    catch { }

                    if (aranilan == "")
                    {
                        //Listview ekleme
                        int sira = listView1.Items.Count;
                        listView1.Items.Add(aranilan);
                        listView1.Items[sira].SubItems.Add(sureDurum);
                        listView1.Items[sira].SubItems.Add(gecerlilik);
                        listView1.Items[sira].SubItems.Add(ulke);
                    }
                }
            }
            catch { }*/
                #endregion

                /* ## KONTROLlER ## */
                #region Giriş ve Saldırı Kontrolleri
                /* Giriş Kontolü */
                if (htmlKOD.IndexOf("Üyeliğime Gir") != -1)
                {
                    //Durum Mesajları
                    label_durum.ForeColor = Color.DarkGreen;
                    label_durum.Text = "Site yüklendi! Giriş yapabilirsiniz..";
                    StatusLabel1.ForeColor = Color.DarkGreen;
                    StatusLabel1.Text = "Site yüklendi! Giriş yapabilirsiniz..";

                    //İşlemler
                    groupBox4.Enabled = false;
                    groupBox1.Enabled = true;
                    tabControl1.Enabled = false;
                    linkLabel_Yenile.Visible = false;

                }
                else if (htmlKOD.IndexOf("Hoşgeldin") != -1)
                {
                    //Durum Mesajları
                    label_durum.ForeColor = Color.DarkGreen;
                    label_durum.Text = textBox_Kadi.Text + " kullanıcısı ile giriş yaptınız!";
                    StatusLabel1.ForeColor = Color.DarkGreen;
                    StatusLabel1.Text = textBox_Kadi.Text + " kullanıcısı ile giriş yaptınız!";

                    //İşlemler
                    groupBox1.Enabled = false;
                    tabControl1.Enabled = true;
                    linkLabel_Yenile.Visible = false;

                    //Sayfa Yönlendirme
                    //webBrowser1.Navigate(refSite);

                }
                else
                {
                    // Saldırı Kontrolü
                    if (htmlKOD.IndexOf("Seçtiğiniz ülkelerdeki onaylanan aramalar başlamıştır") != -1)
                    {
                        //Toplam Saldırı Bir Attır
                        toplamSaldiri = toplamSaldiri + 1;
                        label5.Text = textBox_Hedef.Text + " kullanıcısına " + toplamSaldiri.ToString() + " saldırı gönderildi.";
                        //Durum Mesajları
                        StatusLabel1.ForeColor = Color.DarkGreen;
                        StatusLabel1.Text = "Saldırı gönderildi. Toplam Saldırı: " + toplamSaldiri.ToString();

                        //Sayfa Yönlendirme
                        sure = 0;

                    }
                    else if (htmlKOD.IndexOf("Aratacağınız kişinin nickini doğru yazınız.") != -1)
                    {
                        //Durum Mesajları
                        StatusLabel1.ForeColor = Color.DarkRed;
                        StatusLabel1.Text = "Aratacağınız kişinin nickini doğru yazınız.";

                        //İşlemler
                        button_Durdur.Enabled = false;
                        button_Baslat.Enabled = true;
                        tabControl2.Enabled = true;
                        sure = 0;
                        timer1.Enabled = false;
                        label5.Text = "Bekleniyor..";
                        toplamSaldiri = 0;

                        MessageBox.Show("Aratacağınız kişinin nickini doğru yazınız.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (htmlKOD.IndexOf("Lütfen formdaki tüm alanları doldurunuz.") != -1)
                    {
                        //Durum Mesajları
                        StatusLabel1.ForeColor = Color.DarkRed;
                        StatusLabel1.Text = "Lütfen formdaki tüm alanları doldurunuz.";

                        //İşlemler
                        button_Durdur.Enabled = false;
                        button_Baslat.Enabled = true;
                        tabControl2.Enabled = true;
                        sure = 0;
                        timer1.Enabled = false;
                        label5.Text = "Bekleniyor..";
                        toplamSaldiri = 0;
                        MessageBox.Show("Lütfen formdaki tüm alanları doldurunuz.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else if (htmlKOD.IndexOf("Böyle Biri Yok") != -1)
                    {
                        //Durum Mesajları
                        StatusLabel1.ForeColor = Color.DarkRed;
                        StatusLabel1.Text = "Böyle Biri Yok veya kişinin nickini yanlış yazdınız.";

                        //İşlemler
                        button_Durdur.Enabled = false;
                        button_Baslat.Enabled = true;
                        tabControl2.Enabled = true;
                        sure = 0;
                        timer1.Enabled = false;
                        label5.Text = "Bekleniyor..";
                        toplamSaldiri = 0;
                        MessageBox.Show("Böyle Biri Yok veya kişinin nickini yanlış yazdınız.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        if (htmlKOD.IndexOf("hunt_") != -1)
                        {
                            try
                            {
                                HtmlElementCollection elc1 = webBrowser1.Document.GetElementsByTagName("input");
                                foreach (HtmlElement el1 in elc1)
                                {
                                    if (el1.GetAttribute("value").Equals("Nick gir"))
                                    {
                                        el1.InnerText = textBox_Hedef.Text;
                                    }
                                }

                                HtmlElementCollection elc4 = webBrowser1.Document.GetElementsByTagName("input");
                                foreach (HtmlElement el4 in elc4)
                                {
                                    if (el4.GetAttribute("value").Equals(""))
                                    {
                                        el4.InnerText = textBox_Zaman.Text;
                                    }
                                }

                                HtmlElementCollection elc2 = webBrowser1.Document.GetElementsByTagName("input");
                                foreach (HtmlElement el2 in elc2)
                                {
                                    if (el2.GetAttribute("value").Equals("Hepsini Seç"))
                                    {
                                        el2.InvokeMember("onclick");
                                    }
                                }

                                HtmlElementCollection elc3 = webBrowser1.Document.GetElementsByTagName("input");
                                foreach (HtmlElement el3 in elc3)
                                {
                                    if (el3.GetAttribute("name").Equals("hunt_button"))
                                    {
                                        el3.InvokeMember("Click");
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                #endregion

            }
            catch { }
        }
        #endregion

        #region Sayfayı Yenile
        private void linkLabel_Yenile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Kaynak site yenile
            webBrowser1.Navigate(kaynakSite);

            //Durum Mesajları
            label_durum.ForeColor = Color.DarkBlue;
            label_durum.Text = "Sayfa yenileniyor. Lütfen bekleyiniz.";
            StatusLabel1.ForeColor = Color.DarkBlue;
            StatusLabel1.Text = "Sayfa yenileniyor. Lütfen bekleyiniz.";
        }
        #endregion

        #region Giriş Yap Butonu
        private void button_Giris_Click(object sender, EventArgs e)
        {
            try
            {
                //Kullanıcı adı
                webBrowser1.Document.GetElementById("username").InnerText = textBox_Kadi.Text;
                //Parola
                webBrowser1.Document.GetElementById("password").InnerText = textBox_Parola.Text;
                //Giriş Eventi
                HtmlElementCollection elc2 = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el2 in elc2)
                {
                    if (el2.GetAttribute("name").Equals("Submit2"))
                    {
                        el2.InvokeMember("Click");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Başlat Butonu
        private void button_Baslat_Click(object sender, EventArgs e)
        {
            if (textBox_Hedef.TextLength < 0 || textBox_Zaman.TextLength < 0)
            {
                //Durum Mesajları
                StatusLabel1.ForeColor = Color.DarkRed;
                StatusLabel1.Text = "Lütfen formdaki tüm alanları doldurunuz.";
                MessageBox.Show("Lütfen formdaki tüm alanları doldurunuz.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //İşlemler
                button_Durdur.Enabled = false;
                button_Baslat.Enabled = true;
                textBox_Hedef.ReadOnly = true;
                textBox_Zaman.ReadOnly = true;
                textBox_ZamanAsimi.ReadOnly = true;
                sure = 0;
                timer1.Enabled = false;
                label5.Text = "Bekleniyor..";
                toplamSaldiri = 0;
            }
            else
            {
                //İşlemler
                button_Baslat.Enabled = false;
                button_Durdur.Enabled = true;

                textBox_Hedef.ReadOnly = false;
                textBox_Zaman.ReadOnly = false;
                textBox_ZamanAsimi.ReadOnly = false;

                sure = 0;
                timer1.Enabled = true;

                //Durum Mesajları
                StatusLabel1.ForeColor = Color.DarkGreen;
                StatusLabel1.Text = textBox_Hedef.Text + " kullanıcısına saldırı başlatıldı!";
            }
        }
        #endregion

        #region Durdur Butonu
        private void button_Durdur_Click(object sender, EventArgs e)
        {
            //İşlemler
            button_Durdur.Enabled = false;
            button_Baslat.Enabled = true;
            textBox_Hedef.ReadOnly = true;
            textBox_Zaman.ReadOnly = true;
            textBox_ZamanAsimi.ReadOnly = true;
            timer1.Enabled = false;
            sure = 0;

            //Durum Mesajları
            StatusLabel1.ForeColor = Color.DarkRed;
            StatusLabel1.Text = textBox_Hedef.Text + " kullanıcısına saldırı durduruldu!";
            MessageBox.Show(textBox_Hedef.Text + " kullanıcısına saldırı durduruldu!\nToplam Saldırı: " + toplamSaldiri.ToString(), "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label5.Text = "Bekleniyor..";
            toplamSaldiri = 0;
        }
        #endregion

        #region Timer 
        private void timer1_Tick(object sender, EventArgs e)
        {
            int zamanAsimi = int.Parse(textBox_ZamanAsimi.Text); 

            sure = sure + 1;
            label14.Text = sure.ToString();

            if (sure == zamanAsimi)
            {
                //Sayfa Yönlendirme
                webBrowser1.Navigate(refSite);
            }
            else if (sure == zamanAsimi + 10)
            {
                //Sayfa Yönlendirme
                webBrowser1.Navigate(refSite);
            }
        }
        #endregion

        #region Özel Ayar
        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            saat = String.Format("{0:HH:mm}", dt);
            maskedTextBox1.Text = saat;

            string kalan = maskedTextBox2.Text;

            if (islem == true)
            {
                if (radioButton1.Checked == true)
                {
                    if (saat == kalan)
                    {
                        //Bilgisayar Kapatma
                        System.Diagnostics.Process.Start("shutdown", "-f -s");
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    if (saat == kalan)
                    {
                        //Programı Kapatma
                        Application.Exit();
                    }
                }
                else
                {
                    islem = false;
                    MessageBox.Show("Bir işlem seçmelisiniz", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                islem = true;
                MessageBox.Show("Seçimler uygulandı..", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                islem = false;
                MessageBox.Show("Seçimler iptal edildi..", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Program KApatma
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Site seç
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "http://www.lidermafya.com/")
            {
                kaynakSite = "http://www.lidermafya.com/";
                refSite = kaynakSite + "dedektif.php";
                label17.Text = "Seçili site: " + kaynakSite;
                webBrowser1.Navigate(kaynakSite);
            }
            else if (comboBox1.Text == "http://www.mmafya.com/")
            {
                kaynakSite = "http://www.mmafya.com/";
                refSite = kaynakSite + "dedektif.php";
                label17.Text = "Seçili site: " + kaynakSite;
                webBrowser1.Navigate(kaynakSite);
            }
            else
            {
                MessageBox.Show("Bir site seçmelisiniz..","İşlem",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "http://www.lidermafya.com/")
            {
                kaynakSite = "http://www.lidermafya.com/";
                refSite = kaynakSite + "dedektif.php";
                label17.Text = "Seçili site: " + kaynakSite;
            }
            else if (comboBox1.Text == "http://www.mmafya.com/")
            {
                kaynakSite = "http://www.mmafya.com/";
                refSite = kaynakSite + "dedektif.php";
                label17.Text = "Seçili site: " + kaynakSite;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);

        }

        #endregion
    }
}
