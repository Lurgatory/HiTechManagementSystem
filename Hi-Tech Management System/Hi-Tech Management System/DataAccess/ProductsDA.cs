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
    class ProductsDA
    {
        static string filePath = Application.StartupPath + @"\Products.dat";
        static string filePath2 = Application.StartupPath + @"\ProductsTemp.dat";

        public static void Save (Products pdt)
        {
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(pdt.Category + "," + pdt.AuthorId + "," + pdt.AuthorFirstName + "," + pdt.AuthorLastName + "," 
                + pdt.ProductId + "," + pdt.ProductTitle + "," + pdt.UnitPrice + "," + pdt.Publisher + "," + pdt.YearPublished + "," + pdt.Qoh);
            MessageBox.Show("Product info has been saved successfully!", "Confirmation");
            sw.Close();
        }

        public static List<Products> GetAllRecords()
        {
            List<Products> listP = new List<Products>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        Products pdt = new Products();
                        pdt.Category = fields[0];
                        pdt.AuthorId = Convert.ToInt32(fields[1]);
                        pdt.AuthorFirstName = fields[2];
                        pdt.AuthorLastName = fields[3];
                        pdt.ProductId = fields[4];
                        pdt.ProductTitle = fields[5];
                        pdt.UnitPrice = Convert.ToSingle(fields[6]);
                        pdt.Publisher = fields[7];
                        pdt.YearPublished = Convert.ToInt32(fields[8]);
                        pdt.Qoh = Convert.ToInt32(fields[9]);
                        listP.Add(pdt);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            else
            {
                listP = null;
                MessageBox.Show("File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listP;
        }

        public static Products SearchProductId(string pdtId)
        {
            Products pdt = new Products();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (pdtId == fields[4])
                    {
                        pdt.Category = fields[0];
                        pdt.AuthorId = Convert.ToInt32(fields[1]);
                        pdt.AuthorFirstName = fields[2];
                        pdt.AuthorLastName = fields[3];
                        pdt.ProductId = fields[4];
                        pdt.ProductTitle = fields[5];
                        pdt.UnitPrice = Convert.ToSingle(fields[6]);
                        pdt.Publisher = fields[7];
                        pdt.YearPublished = Convert.ToInt32(fields[8]);
                        pdt.Qoh = Convert.ToInt32(fields[9]);
                        sr.Close();
                        return pdt;
                    }
                    line = sr.ReadLine();
                }
            }
            else
            {
                MessageBox.Show("Product information not Found!", "Error");
            }
            pdt = null;
            sr.Close();
            return pdt;
        }

        public static List<Products> SearchProduct(string input)
        {
            // * OLD WAY 
            List<Products> listP = new List<Products>();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[0].ToUpper() == input.ToUpper() || fields[1] == input || fields[2].ToUpper() == input.ToUpper() || 
                        fields[3].ToUpper() == input.ToUpper() || fields[5].ToUpper() == input.ToUpper() || fields[7].ToUpper() == input.ToUpper())
                    {
                        Products pdt = new Products();
                        pdt.Category = fields[0];
                        pdt.AuthorId = Convert.ToInt32(fields[1]);
                        pdt.AuthorFirstName = fields[2];
                        pdt.AuthorLastName = fields[3];
                        pdt.ProductId = fields[4];
                        pdt.ProductTitle = fields[5];
                        pdt.UnitPrice = Convert.ToSingle(fields[6]);
                        pdt.Publisher = fields[7];
                        pdt.YearPublished = Convert.ToInt32(fields[8]);
                        pdt.Qoh = Convert.ToInt32(fields[9]);
                        listP.Add(pdt);
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
                MessageBox.Show("File not found!", "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listP;

            // * NEW WAY
            //List<Products> listP = new List<Products>();
            //if (File.Exists(filePath))
            //{
            //    using (StreamReader sr = new StreamReader(filePath))
            //    {
            //        string line = sr.ReadLine();
            //        while (line != null)
            //        {
            //            string[] fields = line.Split(',');
            //            if (fields[0].ToUpper() == input.ToUpper() || fields[1] == input || fields[2].ToUpper() == input.ToUpper() || fields[3].ToUpper() == input.ToUpper() || fields[5].ToUpper() == input.ToUpper())
            //            {
            //                Products pdt = new Products();
            //                pdt.Category = fields[0];
            //                pdt.AuthorId = Convert.ToInt32(fields[1]);
            //                pdt.AuthorFirstName = fields[2];
            //                pdt.AuthorLastName = fields[3];
            //                pdt.ProductId = fields[4];
            //                pdt.ProductTitle = fields[5];
            //                pdt.UnitPrice = Convert.ToSingle(fields[6]);
            //                pdt.YearPublished = Convert.ToInt32(fields[7]);
            //                pdt.Qoh = Convert.ToInt32(fields[8]);
            //                listP.Add(pdt);
            //                line = sr.ReadLine();
            //            }
            //            else
            //            {
            //                line = sr.ReadLine();
            //            }
            //        }
            //        sr.Close();
            //    }
            //}
            //else
            //{
            //    listP = null;
            //    MessageBox.Show("File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //return listP;
        }

        public static void UpdateRecord(Products pdt)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[4] != pdt.ProductId )
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + 
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8] + "," + fields[9]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(pdt.Category + "," + pdt.AuthorId + "," + pdt.AuthorFirstName + "," + pdt.AuthorLastName + "," + 
                    pdt.ProductId + "," + pdt.ProductTitle + "," + pdt.UnitPrice + "," + pdt.Publisher + "," + pdt.YearPublished + "," + pdt.Qoh);
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static void DeletRecord(string pdtId)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[4] != pdtId)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + 
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8] + "," + fields[9]);
                    }
                    line = sr.ReadLine();
                }               
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static bool IsProductIDDuplicated(string pdtid)
        {
            Products pdt = new Products();
            StreamReader sr = new StreamReader(filePath,true);
            string line = sr.ReadLine();
            if (File.Exists(filePath))
            {
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (pdtid == fields[4])
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
