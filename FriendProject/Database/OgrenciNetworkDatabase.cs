using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendProject.DatabaseModel;

namespace FriendProject.Database
{
    public class OgrenciNetworkDatabase
    {

        public void AddNetworkStudent(OgrenciNetworkDBModel ogrenci)//Button1 click olayında cağrılır ve click olayında okunan ogrencinetwork dosyasının içindeki veriler Veritabanındaki OgrenciNetwork tablosuna atılır.
        {
            string path = @"C:\Users\User\AllFriendDatabase.db";
            SQLiteConnection con = new SQLiteConnection("DataSource =" + path);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("insert into OgrenciNetwork(OgrKendisi,Friend1,Friend2,Friend3,Friend4,Friend5,Friend6,Friend7,Friend8,Friend9,Friend10) values(@no,@frd1,@frd2,@frd3,@frd4,@frd5,@frd6,@frd7,@frd8,@frd9,@frd10)", con);
            cmd.Parameters.AddWithValue("@no", ogrenci.Numara);
            cmd.Parameters.AddWithValue("@frd1", ogrenci.Friend1);
            cmd.Parameters.AddWithValue("@frd2", ogrenci.Friend2);
            cmd.Parameters.AddWithValue("@frd3", ogrenci.Friend3);
            cmd.Parameters.AddWithValue("@frd4", ogrenci.Friend4);
            cmd.Parameters.AddWithValue("@frd5", ogrenci.Friend5);
            cmd.Parameters.AddWithValue("@frd6", ogrenci.Friend6);
            cmd.Parameters.AddWithValue("@frd7", ogrenci.Friend7);
            cmd.Parameters.AddWithValue("@frd8", ogrenci.Friend8);
            cmd.Parameters.AddWithValue("@frd9", ogrenci.Friend9);
            cmd.Parameters.AddWithValue("@frd10", ogrenci.Friend10);
            cmd.ExecuteNonQuery();
            con.Close();



        }
        public List<OgrenciNetworkDBModel> GetNetworkStudent()//Sonrasında gridview'e basmak amacıyla bu verilerin geri getirildiği fonksiyon.
        {
            List<OgrenciNetworkDBModel> Ogrenciler = new List<OgrenciNetworkDBModel>();
            string path = @"C:\Users\User\AllFriendDatabase.db";
            SQLiteConnection con = new SQLiteConnection("DataSource =" + path);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from OgrenciNetwork", con);

            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OgrenciNetworkDBModel OgrenciNetworkModel = new OgrenciNetworkDBModel();
                OgrenciNetworkModel.Numara = dr.GetString(dr.GetOrdinal("OgrKendisi"));
                OgrenciNetworkModel.Friend1 = dr.GetString(dr.GetOrdinal("Friend1"));
                OgrenciNetworkModel.Friend2= dr.GetString(dr.GetOrdinal("Friend2"));
                OgrenciNetworkModel.Friend3 = dr.GetString(dr.GetOrdinal("Friend3"));
                OgrenciNetworkModel.Friend4 = dr.GetString(dr.GetOrdinal("Friend4"));
                OgrenciNetworkModel.Friend5 = dr.GetString(dr.GetOrdinal("Friend5"));
                OgrenciNetworkModel.Friend6 = dr.GetString(dr.GetOrdinal("Friend6"));
                OgrenciNetworkModel.Friend7 = dr.GetString(dr.GetOrdinal("Friend7"));
                OgrenciNetworkModel.Friend8 = dr.GetString(dr.GetOrdinal("Friend8"));
                OgrenciNetworkModel.Friend9 = dr.GetString(dr.GetOrdinal("Friend9"));
                OgrenciNetworkModel.Friend10 = dr.GetString(dr.GetOrdinal("Friend10"));
                Ogrenciler.Add(OgrenciNetworkModel);


            }
            con.Close();
            return Ogrenciler;

        }




    }
}
