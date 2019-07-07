using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using FriendProject.DatabaseModel;
using FriendProject.Database;
using System.Collections.ObjectModel;


namespace FriendProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Dosya aç butonu; bu butonda open file dialog ile kullanıcıya gerekli dosyalar açtırılır 
        {

            OgrenciNetworkDatabase OgrenciNetwork = new OgrenciNetworkDatabase();//OgrenciProfil ve network dosyalarının içindeki verilerin veritabanı tablolarına eklenmesi veya geri istenmesi için 
            OgrenciProfilDatabase OgrenciProfil = new OgrenciProfilDatabase();//Gerekli nesneler oluşturuluyor.
            

            OpenFileDialog openFile = new OpenFileDialog();//Dialog açtırılıyor...
            openFile.Filter = @"CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";
            openFile.DefaultExt = ".csv";
            openFile.ShowDialog();
            var SafeFileName = openFile.SafeFileName;
            var ExcelFileName = openFile.FileName;
            if (SafeFileName == "ogrenciNetwork.csv")//eğer dialogda seçilen dosyanın adı ogrenciNetwork ise
            {
                string[] NetworkRow = File.ReadAllLines(ExcelFileName);//Bu dosyanın bütün satırları okunuyor

                for (int i = 0; i < NetworkRow.Length; i++)//satırların uzunluguna kadar dönecek for yani 90 kez.
                {
                    OgrenciNetworkDBModel networkModel = new OgrenciNetworkDBModel();//Network modeli oluşturuluyor gelen veriler bu modelin içine atılıp ordan tabloya gidecek.
                    string[] AllStudents = NetworkRow[i].Split(',');//her bir satır virgüllere göre ayrılıp okunuyor.

                    networkModel.Numara = AllStudents[0];//satırın 0. elemanı yani ögrencinin kendisi geri kalan alanlar ise arkadasları bu işlem tüm satırlara uygulanıyor.
                    networkModel.Friend1 = AllStudents[1];
                    networkModel.Friend2 = AllStudents[2];
                    networkModel.Friend3 = AllStudents[3];
                    networkModel.Friend4 = AllStudents[4];
                    networkModel.Friend5 = AllStudents[5];
                    networkModel.Friend6 = AllStudents[6];
                    networkModel.Friend7 = AllStudents[7];
                    networkModel.Friend8 = AllStudents[8];
                    networkModel.Friend9 = AllStudents[9];
                    networkModel.Friend10 = AllStudents[10];
                    OgrenciNetwork.AddNetworkStudent(networkModel);//ardından dolan model network dosyalarını ekleyeceğimiz tabloya parametre olarak geçiliyor.
                    //yani OgrenciNetwork tablosu dosyadan okunan bütün verilerle düzenli bir şekilde doldu.





                }
                dataGridView1.DataSource = OgrenciNetwork.GetNetworkStudent();//ardından veritabanındaki kayıtlar grid view'e basılıyor.

            }

            if (SafeFileName == "ogrenciProfil.csv")//Ogrenci network için gerçekleşen tüm işlemlerin aynısı Ogrenci Profil içiinde gerçekleştiriliyor.
            {

                string[] ProfileRow = File.ReadAllLines(ExcelFileName);
                for (int i = 0; i < ProfileRow.Length; i++)
                {
                    OgrenciProfilDBModel ogrenciProfil = new OgrenciProfilDBModel();
                    string[] AllProfile = ProfileRow[i].Split(',');
                    ogrenciProfil.Numara = AllProfile[0];
                    ogrenciProfil.P1 = Convert.ToInt32(AllProfile[1]);
                    ogrenciProfil.P2 = Convert.ToInt32(AllProfile[2]);
                    ogrenciProfil.P3 = Convert.ToInt32(AllProfile[3]);
                    ogrenciProfil.P4 = Convert.ToInt32(AllProfile[4]);
                    ogrenciProfil.P5 = Convert.ToInt32(AllProfile[5]);
                    ogrenciProfil.P6 = Convert.ToInt32(AllProfile[6]);
                    ogrenciProfil.P7 = Convert.ToInt32(AllProfile[7]); ;
                    ogrenciProfil.P8 = Convert.ToInt32(AllProfile[8]);
                    ogrenciProfil.P9 = Convert.ToInt32(AllProfile[9]);
                    ogrenciProfil.P10 = Convert.ToInt32(AllProfile[10]);
                    ogrenciProfil.P11 = Convert.ToInt32(AllProfile[11]);
                    ogrenciProfil.P12 = Convert.ToInt32(AllProfile[12]);
                    ogrenciProfil.P13 = Convert.ToInt32(AllProfile[13]);
                    ogrenciProfil.P14 = Convert.ToInt32(AllProfile[14]);
                    ogrenciProfil.P15 = Convert.ToInt32(AllProfile[15]);
                    OgrenciProfil.AddProfileStudent(ogrenciProfil);

                }
                dataGridView2.DataSource = OgrenciProfil.GetProfileStudent();

            }










        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Bu button asıl işlemlerin gerçekleştiği buttondur.Arkadaş öneri buttonu
        {
            //Gerekli olan bütün sınıfların nesneleri üretiliyor
            OgrenciListesiOku OgrenciListOku = new OgrenciListesiOku();
            OgrenciNetworkDatabase OgrenciNetwork = new OgrenciNetworkDatabase();
            OgrenciProfilDatabase OgrenciProfil = new OgrenciProfilDatabase();
            ArkadaslarıBulma arkadaslar = new ArkadaslarıBulma();
            var ArananNumara = textBox1.Text;//Textboxdaki numara değeri alınıyor.

            List<OgrenciNetworkDBModel> NetworkList = new List<OgrenciNetworkDBModel>(OgrenciNetwork.GetNetworkStudent());//Network tablosundaki bütün veriler Network model tipinde bir listeye alınıyor
            List<OgrenciProfilDBModel> ProfilList = new List<OgrenciProfilDBModel>(OgrenciProfil.GetProfileStudent());//Profil tablosundaki bütün veriler Profil model tipinde bir listeye alınıyor


            if (ArananNumara.Length != 10)//eğer numara 10 haneden küçükse hata gösteriliyor.
            {
                MessageBox.Show("10 haneli numara giriniz");

            }

            else
            {
                var Arkadaslar = arkadaslar.FindFriend(ArananNumara);//eğer girilen numara hatasız ise oluşturduğumuş arkadaslar nesnesi yani veritabanında girilen numaraya göre arama yapan sınıfın nesnesi
                                                                     //ve sınıfın içindeki arama fonksiyonu çağırılıyor textboxdaki arananNumara değeri parametre olarak gönderildi ve o numaraya göre arama yapıldı.
                                                                     //Aranan numaranın arkadaşları da bulundu ve Arkadaslar adlı değişkene alındı.
                if (string.IsNullOrEmpty(Arkadaslar[0]))//Eğer ilk değer yani numaranın kendisi boş geliyorsa böyle bir numara yok demektir bunun kontrolü sağlanıyor.
                    MessageBox.Show("Bu numarada öğrenci bulunmamaktadır");

                else
                {

                    List<OgrenciAnketModel> AnketList = new List<OgrenciAnketModel>();//Girilen numaranın arkadaş bilgilerinin alınacağı liste
                    foreach (var item in ProfilList)//Tüm profil bilgilerinin içerisinde sadece girilen numaranın arkadaşlarının profli bilgilerinin alınacağı döngü
                    {
                        for (int i = 1; i < 10; i++)//Girilen numaranın arkadaşlarının profil bilgilerinin alındığı döndü
                        {
                            if (!string.IsNullOrEmpty(Arkadaslar[i]))
                            {
                                if (Arkadaslar[i] == item.Numara)//Eğer arkadaslarının numarası ile profil listesindeki numaralar eşit ise
                                {
                                    OgrenciAnketModel AnketModel = new OgrenciAnketModel();
                                    AnketModel.Arkadaslik = 1;//Arkadaslık 1 olarak belirlendi çünkü arkadaşlar.
                                    AnketModel.ArkadaslikDegeri = 0;
                                    AnketModel.Arkadaslar = item.Numara;
                                    AnketModel.P1 = item.P1;
                                    AnketModel.P2 = item.P2;
                                    AnketModel.P3 = item.P3;
                                    AnketModel.P4 = item.P4;
                                    AnketModel.P5 = item.P5;
                                    AnketModel.P6 = item.P6;
                                    AnketModel.P7 = item.P7;
                                    AnketModel.P8 = item.P8;
                                    AnketModel.P9 = item.P9;
                                    AnketModel.P10 = item.P10;
                                    AnketModel.P11 = item.P11;
                                    AnketModel.P12 = item.P12;
                                    AnketModel.P13 = item.P13;
                                    AnketModel.P14 = item.P14;
                                    AnketModel.P15 = item.P15;
                                    AnketList.Add(AnketModel);//tüm arkadaslarının profil bilgilerini alır ve Anketlist'e atar




                                }


                            }

                        }

                    }
                    List<OgrenciProfilDBModel> Liste = new List<OgrenciProfilDBModel>();//ardından tekrar bütün profil bilgileri alınıyor

                    Liste = OgrenciProfil.GetProfileStudent();//Ve listenin içine atılıyor
                    OgrenciProfilDBModel ogrenciProfil = new OgrenciProfilDBModel();
                    for (int i = 0; i < Arkadaslar.Length; i++)//Numarası girilen kişinin arkadaşlarını bütün profil bilgilerinden çıkarır.
                    {
                        if (!string.IsNullOrEmpty(Arkadaslar[i]))
                        {
                            ogrenciProfil = Liste.FirstOrDefault(x => x.Numara == Arkadaslar[i]);
                            Liste.Remove(ogrenciProfil);
                        }

                    }
                 
                    for (int i = 0; i < Liste.Count / 2; i++)//kalan profil bilglerinden ilk 40 kişi regresyon için alınır.
                    {

                        OgrenciAnketModel Anket = new OgrenciAnketModel();
                        Anket.ArkadaslikDegeri = 0;
                        Anket.Arkadaslik = 0;
                        Anket.Arkadaslar = Liste[i].Numara;
                        Anket.P1 = Liste[i].P1;
                        Anket.P2 = Liste[i].P2;
                        Anket.P3 = Liste[i].P3;
                        Anket.P4 = Liste[i].P4;
                        Anket.P5 = Liste[i].P5;
                        Anket.P6 = Liste[i].P6;
                        Anket.P7= Liste[i].P7;
                        Anket.P8 = Liste[i].P8;
                        Anket.P9 = Liste[i].P9;
                        Anket.P10 = Liste[i].P10;
                        Anket.P11 = Liste[i].P11;
                        Anket.P12 = Liste[i].P12;
                        Anket.P13 = Liste[i].P14;
                        Anket.P15 = Liste[i].P15;
                        AnketList.Add(Anket);


                    }

                    for (int i = 0; i < AnketList.Count; i++)//Regresyon için alınan 40 kişi de çıkarılır.
                    {


                        var AnketSil = Liste.FirstOrDefault(x => x.Numara == AnketList[i].Arkadaslar);
                        Liste.Remove(AnketSil);
                        

                    }
                    List<OgrenciAnketModel> AranacakOgrenciListe = new List<OgrenciAnketModel>();
                    for (int i = 0; i < Liste.Count; i++)//Ardından hiç dokunulmamış 40 kişi karşılaştırma yapılmak için bir listeye alınır
                    {

                        OgrenciAnketModel Anket = new OgrenciAnketModel();
                        Anket.ArkadaslikDegeri = 0;
                        Anket.Arkadaslik = 0;
                        Anket.Arkadaslar = Liste[i].Numara;
                        Anket.P1 = Liste[i].P1;
                        Anket.P2 = Liste[i].P2;
                        Anket.P3 = Liste[i].P3;
                        Anket.P4 = Liste[i].P4;
                        Anket.P5 = Liste[i].P5;
                        Anket.P6 = Liste[i].P6;
                        Anket.P7 = Liste[i].P7;
                        Anket.P8 = Liste[i].P8;
                        Anket.P9 = Liste[i].P9;
                        Anket.P10 = Liste[i].P10;
                        Anket.P11 = Liste[i].P11;
                        Anket.P12 = Liste[i].P12;
                        Anket.P13 = Liste[i].P14;
                        Anket.P15 = Liste[i].P15;
                        AranacakOgrenciListe.Add(Anket);


                    }

                    int N = AnketList.Count;//Regresyon için alınan 40 kişi
                    int Loop = 100;//iterasyon sayısı
                    double stepSize = 0.001;
                    double[] beta = new double[16];//Betaların değerlerinin tutulduğu dizi

                    for (int i = 0; i < beta.Length; i++)
                    {
                        beta[i] = 1;

                    }
                    double[] newBeta = new double[16];
                    double toplam1 = 0.0;
                    double toplam2 = 0.0;
                    for (int k = 1; k <= Loop; k++)
                    {
                        toplam1 = 0.0;
                        for (int i = 0; i <N; i++)
                        {
                            int[] Degerler = Deger(AnketList[i]);//regresyon için alınan 40 kişinin profil bilgileri bu dizide tutuluyor.
                            double hx = Math.Exp(-(beta[0] + beta[1] * Degerler[1] + beta[2] * Degerler[2] + beta[3] * Degerler[3] + beta[4] * Degerler[4] + beta[5] * Degerler[5] +
                         beta[6] * Degerler[6] + beta[7] * Degerler[7] + beta[8] * Degerler[8] + beta[9] * Degerler[9] + beta[10] * Degerler[10] + beta[11] * Degerler[11] + beta[12] * Degerler[12] +
                         beta[13] * Degerler[13] + beta[14] * Degerler[14] + beta[15] * Degerler[15]));//Ardından bu değerlere göre regresyon uygulanır
                            double hb = 1.0 / (1.0 + hx);
                            double y = AnketList[i].Arkadaslik;//Arkadas olup olmadıkları durumu
                            toplam1 += hb - y;
                        }
                        newBeta[0] = beta[0] - (stepSize * toplam1 / N);//Beta 0'ın yeni değerlerini bu dizide tutuyoruz
                        for (int t= 0; t < 15; t++)
                        {
                            toplam2 = 0.0;
                            for (int i = 0; i < N; i++)
                            {
                                int[] Degerler = Deger(AnketList[i]);//tekrardan regresyon için alınan ilk 40 kişinin profil değerleri alınıyor
                                double hb = 1.0 / (1.0 + Math.Exp(-(beta[0] + beta[1] * Degerler[1] + beta[2] * Degerler[2] + beta[3] * Degerler[3] + beta[4] * Degerler[4] + beta[5] * Degerler[5] +
                                beta[6] * Degerler[6] + beta[7] * Degerler[7] + beta[8] * Degerler[8] + beta[9] * Degerler[9] + beta[10] * Degerler[10] + beta[11] * Degerler[11] + beta[12] * Degerler[12] +
                                beta[13] * Degerler[13] + beta[14] * Degerler[14] + beta[15] * Degerler[15])));
                                double y = AnketList[i].Arkadaslik;
                                toplam2 += (hb - y) * Degerler[t];

                            }
                            newBeta[t] = beta[t] - (stepSize * toplam2 / N);//Beta0 hariç diğer tüm betaların yeni değerlerini burda tutuyoruz

                        }
                        for (int i = 0; i < beta.Length; i++)
                        {
                            beta[i] = newBeta[i];//Betalara yeni değerlerini atıyoruz
                        }

                    }

                    for (int i = 0; i < AranacakOgrenciListe.Count ; i++)//Ardından hiç dokunmadığımız 40 kişiye ait döngüye geliyoruz



                    {   //Regresyona aldığımız kişilerden çıkan beta değerlerine göre dokunmadığımız 40 kişinin de değerlerini buluyoruz
                        int[] Degerler = Deger(AranacakOgrenciListe[i]);
                        double hb = 1.0 / (1.0 + Math.Exp(-(beta[0] + beta[1] * Degerler[1] + beta[2] * Degerler[2] + beta[3] * Degerler[3] + beta[4] * Degerler[4] + beta[5] * Degerler[5] +
                        beta[6] * Degerler[6] + beta[7] * Degerler[7] + beta[8] * Degerler[8] + beta[9] * Degerler[9] + beta[10] * Degerler[10] + beta[11] * Degerler[11] + beta[12] * Degerler[12] +
                         beta[13] * Degerler[13] + beta[14] * Degerler[14] + beta[15] * Degerler[15])));
                     
                            AranacakOgrenciListe[i].ArkadaslikDegeri = hb;//Bu değerleri hiç dokunmadığımız 40 kişinin arkadaşlık değerlerine atıyoruz
                    }
                    List<OgrenciAnketModel> BulunanArkadaslar = (from x in AranacakOgrenciListe//Ardından arkadaslık değeri en yüksek olan kişiler bulunuyor.
                                                       orderby x.ArkadaslikDegeri descending
                                                       select x).ToList();

                    var OgrenciListesi = OgrenciListOku.getStudenList();//Önerilen arkadasların adlarını da görüntülemek için Ogrencilistesi excelini okudugumuz Fonksiyonu cagırıyoruz
                    List<OgrenciListesiModel> OnerilenArkadasListe = new List<OgrenciListesiModel>();//Ogrencilistesi modelimizin tipinde bir liste olusturuyoruz 10 arkadas bu listeye alınacak.

                    for (int i = 0; i < BulunanArkadaslar.Count; i++)//Bulunan tüm arkadaşların sayısı kadar bir for
                    {
                        for (int j = 0; j < OgrenciListesi.Count; j++)//Ardından excel dosyasındaki tüm ögrenciler kadar dönecek bir for
                        {
                            if (BulunanArkadaslar[i].Arkadaslar == OgrenciListesi[j].OgrNo)//Eğer bulunan arkadaşların numaraları ile ögrenci listesindeki ögrencilerin numaraları eşit ise
                            {
                                if (OnerilenArkadasListe.Count < 10)//o eşit olanların ilk 10 tanesini alıyor.
                                {
                                    OnerilenArkadasListe.Add(OgrenciListesi[j]);//ve bu ilk 10 kişieye OnerilenArkadas listesine atıyor.
                                }

                            }
                        }
                    }


                    dataGridView3.DataSource = OnerilenArkadasListe;//Ardından önerilen arkadaşlar grid view'e basılıyor.

                }

               
            }

           
            }
        private int[] Deger(OgrenciAnketModel anketDegeri)//Bu fonksiyon AnketModelin içinden sadece profil bilgilerini çekmemizi sağlar.
        {
            int[] parseDeger = new int[16];
            parseDeger[0] = 0;
            parseDeger[1] = anketDegeri.P1;
            parseDeger[2] = anketDegeri.P2;
            parseDeger[3] = anketDegeri.P3;
            parseDeger[4] = anketDegeri.P4;
            parseDeger[5] = anketDegeri.P5;
            parseDeger[6] = anketDegeri.P6;
            parseDeger[7] = anketDegeri.P7;
            parseDeger[8] = anketDegeri.P8;
            parseDeger[9] = anketDegeri.P9;
            parseDeger[10] = anketDegeri.P10;
            parseDeger[11] = anketDegeri.P11;
            parseDeger[12] = anketDegeri.P12;
            parseDeger[13] = anketDegeri.P13;
            parseDeger[14] = anketDegeri.P14;
            parseDeger[15] = anketDegeri.P15;

            return parseDeger;
        }
    }


    }



