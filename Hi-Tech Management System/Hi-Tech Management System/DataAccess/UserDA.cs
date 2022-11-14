using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.Business;
using System.Windows.Forms;
using System.IO;

namespace Hi_Tech_Management_System.DataAccess
{
    public static class UserDA
    {
        static string filePathUser = Application.StartupPath + @"\Users.dat";
        static string filePathUser2 = Application.StartupPath + @"\UsersTemp.dat";

        public static bool VerifyLogIn(User aUser)
        {
            List<User> usr = new List<User>();
            StreamReader sr = new StreamReader(filePathUser, true);
            if (File.Exists(filePathUser))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] files = line.Split(',');
                    if (aUser.UserId == Convert.ToInt32(files[0]) && aUser.Password == files[1])
                    {
                        sr.Close();
                        return true;
                    }
                    line = sr.ReadLine();
                }            
            }
            return false;
        }

        public static void SaveUser(User usr)
        {
            StreamWriter sw = new StreamWriter(filePathUser, true);
            sw.WriteLine(usr.UserId + "," + usr.Password + "," + usr.Status);
            MessageBox.Show("User info has been saved succesffuly.", "Confirmation");
            sw.Close();
        }

        public static List<User> GetAllUserRecords()
        {
            List<User> listUser = new List<User>();
            if (File.Exists(filePathUser))
            {
                using (StreamReader sr = new StreamReader(filePathUser))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        User usr = new User();
                        usr.UserId = Convert.ToInt32(fields[0]);
                        usr.Password = fields[1];
                        usr.Status = fields[2];
                        listUser.Add(usr);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }                
            }
            else
            {
                listUser = null;
                MessageBox.Show("File not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return listUser;
        }

        public static List<User> SearchUser(string input)
        {
            List<User> listU = new List<User>();
            StreamReader sr = new StreamReader(filePathUser);

            if (File.Exists(filePathUser))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (fields[0] == input)
                    {
                        User usr = new User();
                        usr.UserId = Convert.ToInt32(fields[0]);
                        usr.Password = fields[1];
                        usr.Status = fields[2];
                        listU.Add(usr);
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
                MessageBox.Show("User Information not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listU;
        }

        public static User SearchUserId(int userid)
        {
            User usr = new User();
            StreamReader sr = new StreamReader(filePathUser);
            List<User> listU = new List<User>();

            if (File.Exists(filePathUser))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (userid == Convert.ToInt32(fields[0]))
                    {
                        usr.UserId = Convert.ToInt32(fields[0]);
                        usr.Password = fields[1];
                        usr.Status = fields[2];
                        listU.Add(usr);
                        sr.Close();
                        return usr;
                    }
                    line = sr.ReadLine();
                }
            }
            else
            {
                MessageBox.Show("User Information not Found!", "Error");
            }
            usr = null;
            sr.Close();
            return usr;
        }

        public static void UpdateUserRecord(User usr)
        {
            if (File.Exists(filePathUser))
            {
                StreamReader sr = new StreamReader(filePathUser);
                StreamWriter sw = new StreamWriter(filePathUser2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[0]) != usr.UserId)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2]);
                    }
                    line = sr.ReadLine();
                }
                sw.WriteLine(usr.UserId + "," + usr.Password + "," + usr.Status);
                sr.Close();
                sw.Close();
            }
            File.Delete(filePathUser);
            File.Move(filePathUser2, filePathUser);
        }

        public static void DeleteUserRecord(int usrid)
        {
            if (File.Exists(filePathUser))
            {
                StreamReader sr = new StreamReader(filePathUser);
                StreamWriter sw = new StreamWriter(filePathUser2, true);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (Convert.ToInt32(fields[0]) != usrid)
                    {
                        sw.WriteLine(fields[0] + "," + fields[1] + "," + fields[2]);
                    }

                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }
            File.Delete(filePathUser);
            File.Move(filePathUser2, filePathUser);
        }

        //public static int Comboboxcheck (int usrid)
        //{
        //    User usr = new User();
        //    bool found = false;
        //    int comboxIndex = 0;

        //    string usridstring = Convert.ToString(usrid);

        //    if (File.Exists(filePathUser))
        //    {
        //        using (StreamReader sr = new StreamReader(filePathUser))
        //        {
        //            string line = sr.ReadLine();
                   
        //            while (line != null && !found)
        //            {
        //                string[] fields = line.Split(',');
        //                if (Convert.ToInt32(fields[0]) == usrid)
        //                {
        //                    if (fields[2] == "Active")
        //                    {
        //                        comboxIndex = 0;
        //                    }
        //                    if (fields[2] == "Inactive")
        //                    {
        //                        comboxIndex = 1;
        //                    }
        //                }
        //                line = sr.ReadLine();
        //            }
        //            sr.Close();
        //        }
        //    }
        //    return comboxIndex;
        //}

        public static bool IsUserDuplicated(int usrid)
        {
            User usr = new User();
            StreamReader sr = new StreamReader(filePathUser);
            string line = sr.ReadLine();
            if (File.Exists(filePathUser))
            {
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (usrid == Convert.ToInt32(fields[0]))
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
