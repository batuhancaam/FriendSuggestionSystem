using FriendProject.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
namespace FriendProject
{
    public class OgrenciListesiOku//ogrenciListesi excel dosyanın okunduğu sınıftır.
    {

        public List<OgrenciListesiModel> getStudenList()//Bu fonksiyon ogrenciListesi excelini okur ve ogrenciListesi modelinin içerisine atar.
        {
            List<OgrenciListesiModel> students = new List<OgrenciListesiModel>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\User\Desktop\ogrencilistesi.xlsx");
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            for (int i = 2; i <= xlWorksheet.UsedRange.Rows.Count; i++)
            {
                OgrenciListesiModel student = new OgrenciListesiModel();
                student.OgrNo = (xlWorksheet.Cells[i, 1] as Range)?.Value2?.ToString();
                student.Adi = (xlWorksheet.Cells[i, 2] as Range)?.Value2?.ToString();
                students.Add(student);
            }
            xlWorkbook.Close();
            return students;
        }
    }
}
