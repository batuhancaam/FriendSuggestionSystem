using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendProject.DatabaseModel
{
   public class OgrenciListesiModel//OgrenciListesi excel dosyanın değerlerinin tutulacağı model.
    {

        private string adi;

        public string Adi { 
            get { return adi; }
            set { adi = value; }
        }


        private string ogrNo;

        public string OgrNo
        {
            get { return ogrNo;  }
            set { ogrNo = value; }
        }

    }
}
