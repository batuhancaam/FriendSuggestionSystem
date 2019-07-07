using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendProject.DatabaseModel;
using System.Data.SQLite;

namespace FriendProject.Database
{
    public class OgrenciProfilDatabase//SQLite veritabanı kullanıldığında SQLite command kullanılmıştır.Projedeki bütün veritabanı işlemleri SQLite ile yapılır
    {

        public void AddProfileStudent(OgrenciProfilDBModel ogrenci)//Bu fonksiyonda ogrenciprofil tablosuna button1 click olayında açılan profil dosyasından gelen verilerin ekleme işlemleri yapılır
        {
            string path = @"C:\Users\User\AllFriendDatabase.db";
            SQLiteConnection con = new SQLiteConnection("DataSource =" + path);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("insert into OgrenciProfil(OgrKendisi,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15) values(@no,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)", con);
            cmd.Parameters.AddWithValue("@no", ogrenci.Numara);
            cmd.Parameters.AddWithValue("@p1", ogrenci.P1);
            cmd.Parameters.AddWithValue("@p2", ogrenci.P2);
            cmd.Parameters.AddWithValue("@p3", ogrenci.P3);
            cmd.Parameters.AddWithValue("@p4", ogrenci.P4);
            cmd.Parameters.AddWithValue("@p5", ogrenci.P5);
            cmd.Parameters.AddWithValue("@p6", ogrenci.P6);
            cmd.Parameters.AddWithValue("@p7", ogrenci.P7);
            cmd.Parameters.AddWithValue("@p8", ogrenci.P8);
            cmd.Parameters.AddWithValue("@p9", ogrenci.P9);
            cmd.Parameters.AddWithValue("@p10", ogrenci.P10);
            cmd.Parameters.AddWithValue("@p11", ogrenci.P11);
            cmd.Parameters.AddWithValue("@p12", ogrenci.P12);
            cmd.Parameters.AddWithValue("@p13", ogrenci.P13);
            cmd.Parameters.AddWithValue("@p14", ogrenci.P14);
            cmd.Parameters.AddWithValue("@p15", ogrenci.P15);

            cmd.ExecuteNonQuery();
            con.Close();



        }

        public List<OgrenciProfilDBModel> GetProfileStudent() //Bu fonksiyonda ise eklenen veriler tablodan okunur(Gridviewe basmak için)
        {
            List<OgrenciProfilDBModel> Ogrenciler = new List<OgrenciProfilDBModel>();
            string path = @"C:\Users\User\AllFriendDatabase.db";
            SQLiteConnection con = new SQLiteConnection("DataSource =" + path);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from OgrenciProfil", con);

            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OgrenciProfilDBModel OgrenciProfilModel = new OgrenciProfilDBModel();
                OgrenciProfilModel.Numara = dr.GetString(dr.GetOrdinal("OgrKendisi"));
                OgrenciProfilModel.P1 = dr.GetInt32(dr.GetOrdinal("P1"));
                OgrenciProfilModel.P2 = dr.GetInt32(dr.GetOrdinal("P2"));
                OgrenciProfilModel.P3 = dr.GetInt32(dr.GetOrdinal("P3"));
                OgrenciProfilModel.P4 = dr.GetInt32(dr.GetOrdinal("P4"));
                OgrenciProfilModel.P5 = dr.GetInt32(dr.GetOrdinal("P5"));
                OgrenciProfilModel.P6 = dr.GetInt32(dr.GetOrdinal("P6"));
                OgrenciProfilModel.P7 = dr.GetInt32(dr.GetOrdinal("P7"));
                OgrenciProfilModel.P8 = dr.GetInt32(dr.GetOrdinal("P8"));
                OgrenciProfilModel.P9 = dr.GetInt32(dr.GetOrdinal("P9"));
                OgrenciProfilModel.P10 = dr.GetInt32(dr.GetOrdinal("P10"));
                OgrenciProfilModel.P11 = dr.GetInt32(dr.GetOrdinal("P11"));
                OgrenciProfilModel.P12 = dr.GetInt32(dr.GetOrdinal("P12"));
                OgrenciProfilModel.P13 = dr.GetInt32(dr.GetOrdinal("P13"));
                OgrenciProfilModel.P14 = dr.GetInt32(dr.GetOrdinal("P14"));
                OgrenciProfilModel.P15 = dr.GetInt32(dr.GetOrdinal("P15"));
                Ogrenciler.Add(OgrenciProfilModel);


            }
            con.Close();
            return Ogrenciler;

        }



    }
}
