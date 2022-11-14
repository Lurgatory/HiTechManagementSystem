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
    class ClientDA
    {
        static string filePath = Application.StartupPath + @"\Client.dat";
        static string filePath2 = Application.StartupPath + @"\ClientTemp.dat";

        public static void Save(Client clt)
        {
            StreamWriter sw = new StreamWriter(filePath,true);
            sw.WriteLine(clt.ClientId + "," + clt.ClientName + "," + clt.ClientAddress + "," + clt.ClientCity + "," + 
                clt.ClientPost + "," + clt.ClientPhoneNumber + "," + clt.FaxNumber + "," + clt.BankAccount + "," + clt.CreditLimit);
            MessageBox.Show("Client Info has been saved successfully!", "Confiremation");
            sw.Close();
        }

        public static List<Client> GetAllClientRecord()
        {
            List<Client> listAll = new List<Client>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        Client clt = new Client();
                        clt.ClientId = Convert.ToInt32(fields[0]);
                        clt.ClientName = fields[1];
                        clt.ClientAddress = fields[2];
                        clt.ClientCity = fields[3];
                        clt.ClientPost = fields[4];
                        clt.ClientPhoneNumber = fields[5];
                        clt.FaxNumber = fields[6];
                        clt.BankAccount = Convert.ToInt32(fields[7]);
                        clt.CreditLimit = fields[8];
                        listAll.Add(clt);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            else
            {
                listAll = null;
                MessageBox.Show("File not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return listAll;
        }

        public static void UpdateRecord(Client clt)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null) 
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[0]) != clt.ClientId)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + 
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(clt.ClientId + "," + clt.ClientName + "," + clt.ClientAddress + "," + 
                    clt.ClientCity + "," + clt.ClientPost + "," + clt.ClientPhoneNumber + "," + clt.FaxNumber + "," + clt.BankAccount + "," + clt.CreditLimit);
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static Client Search(int cltId)
        {
            Client clt = new Client();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (cltId == Convert.ToInt32(fields[0]))
                    {
                        clt.ClientId = Convert.ToInt32(fields[0]);
                        clt.ClientName = fields[1];
                        clt.ClientAddress = fields[2];
                        clt.ClientCity = fields[3];
                        clt.ClientPost = fields[4];
                        clt.ClientPhoneNumber = fields[5];
                        clt.FaxNumber = fields[6];
                        clt.BankAccount = Convert.ToInt32(fields[7]);
                        clt.CreditLimit = fields[8];
                        sr.Close();
                        return clt;
                    }
                    line = sr.ReadLine();
                }
            }
            else
            {
                MessageBox.Show("Client Information not Found!", "Error");
            }
            clt = null;
            sr.Close();
            return clt;
        }

        public static List<Client> Searchname(string input)
        {
            List<Client> listC = new List<Client>();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[1].ToUpper() == input.ToUpper())
                    {
                        Client clt = new Client();
                        clt.ClientId = Convert.ToInt32(fields[0]);
                        clt.ClientName = fields[1];
                        clt.ClientAddress = fields[2];
                        clt.ClientCity = fields[3];
                        clt.ClientPost = fields[4];
                        clt.ClientPhoneNumber = fields[5];
                        clt.FaxNumber = fields[6];
                        clt.BankAccount = Convert.ToInt32(fields[7]);
                        clt.CreditLimit = fields[8];
                        listC.Add(clt);
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
            return listC;
        }

        public static void DeleteRecord(int id)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[0]) != id)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + 
                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static bool IsDuplicated(int cltid)
        {
            Client clt = new Client();
            StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            if (File.Exists(filePath))
            {
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (cltid == Convert.ToInt32(fields[0]))
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
