using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.DataAccess;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.Business
{
    public class User : Employee
    {
        private int userId;
        private string password;
        private string status;

        public int UserId { get => userId; set => userId = value; }
        public string Password { get => password; set => password = value; }
        public string Status { get => status; set => status = value; }

        public override string DisplayInfo()
        {
            return (base.DisplayInfo() + "," + password);
        }

        public bool LogIn (User user)
        {
            return UserDA.VerifyLogIn(user);
        }

        public void SaveUser (User usr)
        {
            UserDA.SaveUser(usr);
        }

        public List<User> GetUserList()
        {
            return UserDA.GetAllUserRecords();
        }

        public void DisplayUserList (ListView listviewU, List<User> listU)
        {
            listviewU.Items.Clear();
            foreach (User usrItem in listU)
            {
                ListViewItem itemU = new ListViewItem(usrItem.UserId.ToString());
                itemU.SubItems.Add(usrItem.Password);
                itemU.SubItems.Add(usrItem.Status);
                listviewU.Items.Add(itemU);
            }
        }

        public List<User> searchUser (string input)
        {
            return UserDA.SearchUser(input);
        }

        public User searchUserId(int usrId)
        {
            return UserDA.SearchUserId(usrId);
        }

        public void UpdateUser(User usr)
        {
            UserDA.UpdateUserRecord(usr);
        }

        public void DeleteUserRecord(int usrid)
        {
            UserDA.DeleteUserRecord(usrid);
        }

        public bool UserDuplicated (int usrid)
        {
            return UserDA.IsUserDuplicated(usrid);
        }
    }
}
