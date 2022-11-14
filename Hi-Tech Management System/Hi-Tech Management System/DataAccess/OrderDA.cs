using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.DataAccess
{
    class OrderDA
    {
        static string filePath = Application.StartupPath + @"\Orders.dat";
        static string filePath2 = Application.StartupPath + @"\OrdersTemp.dat";        

        public static void Save (Order odr)
        {
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(odr.ClientInfo + "," + odr.ProductInfo + "," + odr.ContactMethod + "," + 
                odr.ContactNumber + "," + odr.OrderID + "," + odr.OrderQuantity + "," + odr.RequireDate + "," + odr.ShipDate + "," + odr.Status);
            MessageBox.Show("Order info has been saved successfully.", "Confirmation");
            sw.Close();
        }

        public static List<Order> GetAllRecords()
        {
            List<Order> listAll = new List<Order>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        Order odr = new Order();
                        odr.ClientInfo = fields[0];
                        odr.ProductInfo = fields[1];
                        odr.ContactMethod = fields[2];
                        odr.ContactNumber = fields[3];
                        odr.OrderID = Convert.ToInt32(fields[4]);
                        odr.OrderQuantity = Convert.ToInt32(fields[5]);
                        odr.RequireDate = Convert.ToDateTime(fields[6]);
                        odr.ShipDate = Convert.ToDateTime(fields[7]);
                        odr.Status = fields[8];
                        listAll.Add(odr);
                        line = sr.ReadLine();
                    }
                }
            }
            else
            {
                listAll = null;
                MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listAll;
        }

        public static Order Search(int odrId)
        {
            Order odr = new Order();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (odrId == Convert.ToInt32(fields[4]))
                    {
                        odr.ClientInfo = fields[0];
                        odr.ProductInfo = fields[1];
                        odr.ContactMethod = fields[2];
                        odr.ContactNumber = fields[3];
                        odr.OrderID = Convert.ToInt32(fields[4]);
                        odr.OrderQuantity = Convert.ToInt32(fields[5]);
                        odr.RequireDate = Convert.ToDateTime(fields[6]);
                        sr.Close();
                        return odr;
                    }
                    line = sr.ReadLine();
                }
            }
            else
            {
                MessageBox.Show("Order Information not Found!", "Error");
            }
            odr = null;
            sr.Close();
            return odr;
        }

        public static List<Order> SearchOption(string input)
        {
            List<Order> listO = new List<Order>();
            StreamReader sr = new StreamReader(filePath);
            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line !=null)
                {
                    string[] fields = line.Split(',');
                    if (fields[0].ToUpper() == input.ToUpper() || fields[1].ToUpper() == input.ToUpper() || fields[2].ToUpper() == input.ToUpper())
                    {
                        Order odr = new Order();
                        odr.ClientInfo = fields[0];
                        odr.ProductInfo = fields[1];
                        odr.ContactMethod = fields[2];
                        odr.ContactNumber = fields[3];
                        odr.OrderID = Convert.ToInt32(fields[4]);
                        odr.OrderQuantity = Convert.ToInt32(fields[5]);
                        odr.RequireDate = Convert.ToDateTime(fields[6]);
                        odr.ShipDate = Convert.ToDateTime(fields[7]);
                        odr.Status = fields[8];
                        listO.Add(odr);
                        line = sr.ReadLine();
                    }
                    else
                    {
                        line = sr.ReadLine();
                    }
                }
                sr.Close();
            }
            else
            {
                MessageBox.Show("File not found!", "Missing Employee File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listO;
        }

        public static void UpdateRecord(Order odr)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[4]) != odr.OrderID)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + 
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(odr.ClientInfo + "," + odr.ProductInfo + "," + odr.ContactMethod + "," + 
                    odr.ContactNumber + "," + odr.OrderID + "," + odr.OrderQuantity + "," + 
                    odr.RequireDate.ToShortDateString() + "," + odr.ShipDate.ToShortDateString() + "," + odr.Status);
                sr.Close();
                sw.Close();
            } 
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static void CancelRecord(Order odr)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[4]) != odr.OrderID)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," +
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(odr.ClientInfo + "," + odr.ProductInfo + "," + odr.ContactMethod + "," +
                    odr.ContactNumber + "," + odr.OrderID + "," + odr.OrderQuantity + "," +
                    odr.RequireDate.ToShortDateString() + "," + odr.ShipDate.ToShortDateString() + "," + odr.Status);
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static bool IsDuplicated(int odrid)
        {
            Order odr = new Order();
            StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            if (File.Exists(filePath))
            {
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (odrid == Convert.ToInt32(fields[4]))
                    {
                        sr.Close();
                        return true;
                    }
                    line = sr.ReadLine();
                }
            }
            sr.Close();
            return false;

        }
    }
}
