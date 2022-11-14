using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Hi_Tech_Management_System.DataAccess
{
    public static class EmployeeDA
    {
        static string filePath = Application.StartupPath + @"\Employees.dat";
        static string filePath2 = Application.StartupPath + @"\EmployeeTemp.dat";

        public static void Save (Employee emp)
        {
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(emp.EmployeeId + "," + emp.FirstName + "," + emp.LastName + "," + 
                emp.HireDate + "," + emp.PhoneNumber + "," + emp.Email + "," + emp.JobTitle);
            MessageBox.Show("Employee info has been saved successfully.", "Confirmation");
            sw.Close();
        }

        public static Employee Search(int empId)
        {
            Employee emp = new Employee();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (empId == Convert.ToInt32(fields[0]))
                    {
                        emp.EmployeeId = Convert.ToInt32(fields[0]);
                        emp.FirstName = fields[1];
                        emp.LastName = fields[2];
                        emp.HireDate = Convert.ToDateTime(fields[3]);
                        emp.PhoneNumber = fields[4];
                        emp.Email = fields[5];
                        emp.JobTitle = fields[6];
                        sr.Close();
                        return emp;
                    }                    
                    line = sr.ReadLine();                
                   
                }
            }
            else
            {
                MessageBox.Show("Employee Infomation not Found!", "Error");
            }
            emp = null;
            sr.Close();
            return emp;
        }

        public static List<Employee> Searchname(string input)
        {
            List<Employee> listE = new List<Employee>();
            StreamReader sr = new StreamReader(filePath);

            if (File.Exists(filePath))
            {

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[1].ToUpper() == input.ToUpper() || fields[2].ToUpper() == input.ToUpper())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(fields[0]);
                        emp.FirstName = fields[1];
                        emp.LastName = fields[2];
                        emp.HireDate = Convert.ToDateTime(fields[3]);
                        emp.PhoneNumber = fields[4];
                        emp.Email = fields[5];
                        emp.JobTitle = fields[6];
                        listE.Add(emp);
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

            return listE;
        }

        public static List<Employee> GetAllRecords()
        {
            List<Employee> listAll = new List<Employee>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(fields[0]);
                        emp.FirstName = fields[1];
                        emp.LastName = fields[2];
                        emp.HireDate = Convert.ToDateTime(fields[3]);
                        emp.PhoneNumber = fields[4];
                        emp.Email = fields[5];
                        emp.JobTitle = fields[6];
                        listAll.Add(emp);
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
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                    }

                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static void UpdateRecord(Employee emp)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                StreamWriter sw = new StreamWriter(filePath2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[0]) != emp.EmployeeId)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(emp.EmployeeId + "," + emp.FirstName + "," + emp.LastName + "," + emp.HireDate.ToShortDateString() + "," + emp.PhoneNumber + "," + emp.Email + "," + emp.JobTitle);
                sr.Close();
                sw.Close();
            }
            File.Delete(filePath);
            File.Move(filePath2, filePath);
        }

        public static bool IsDuplicated(int empid)
        {
            Employee emp = new Employee();
            StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            if (File.Exists(filePath))
            {
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (empid == Convert.ToInt32(fields[0]))
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

        public static int ComboboxJobTitleCheck (int empid)
        {
            Employee emp = new Employee();
            bool found = false;
            int comboIndex = 0;

            //string empidstring = Convert.ToString(empid);

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null && !found)
                    {
                        string[] fields = line.Split(',');
                        if (Convert.ToInt32(fields[0]) == empid)
                        {
                            if (fields[6] == "MIS Manager")
                            {
                                comboIndex = 0;
                            }
                            if (fields[6] == "Sales Manager")
                            {
                                comboIndex = 1;
                            }
                            if (fields[6] == "Inventory Controller")
                            {
                                comboIndex = 2;
                            }
                            if (fields[6] == "Order Clerks")
                            {
                                comboIndex = 3;
                            }
                        }
                        line = sr.ReadLine();
                    }
                }
            }
            return comboIndex;
        }

        public static bool empJobTitleCheck (int empid)
        {
            Employee emp = new Employee();
            StreamReader sr = new StreamReader(filePath, true);
            string line = sr.ReadLine();
            if (File.Exists(filePath))
            {                
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (empid == Convert.ToInt32(fields[0]))
                    {
                        emp.JobTitle = fields[6];
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