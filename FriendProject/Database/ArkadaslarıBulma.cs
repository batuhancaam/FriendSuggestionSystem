using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendProject.Database
{
    public class ArkadaslarıBulma
    {
        public string[] FindFriend(string number)//Bu fonksiyon textbox'a girilen  numaraya göre OgrenciNetwork tablosunda arama yapar o numaranın arkadaşları bulur ve geri döndürür.
        {
            string path = @"C:\Users\User\AllFriendDatabase.db";
            string[] Friends = new string[11];
            SQLiteConnection con = new SQLiteConnection("DataSource =" + path);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from OgrenciNetwork WHERE OgrKendisi = @no", con);
            cmd.Parameters.AddWithValue("@no", number);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                Friends[0] = dr.GetString(dr.GetOrdinal("OgrKendisi"));
                Friends[1] = dr.GetString(dr.GetOrdinal("Friend1"));
                Friends[2] = dr.GetString(dr.GetOrdinal("Friend2"));
                Friends[3] = dr.GetString(dr.GetOrdinal("Friend3"));
                Friends[4] = dr.GetString(dr.GetOrdinal("Friend4"));
                Friends[5] = dr.GetString(dr.GetOrdinal("Friend5"));
                Friends[6] = dr.GetString(dr.GetOrdinal("Friend6"));
                Friends[7] = dr.GetString(dr.GetOrdinal("Friend7"));
                Friends[8] = dr.GetString(dr.GetOrdinal("Friend8"));
                Friends[9] = dr.GetString(dr.GetOrdinal("Friend9"));
                Friends[10] = dr.GetString(dr.GetOrdinal("Friend10"));
                


            }

            con.Close();

            return Friends;

        }


    }
}
