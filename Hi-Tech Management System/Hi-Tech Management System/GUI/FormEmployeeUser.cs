using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_Tech_Management_System.Business;
using Hi_Tech_Management_System.Validation;
using System.Xml;
using Hi_Tech_Management_System.DataAccess;

namespace Hi_Tech_Management_System.GUI
{
    public partial class FormEmployeeUser : Form
    {
        public FormEmployeeUser()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUserId.Text = textBoxEmployeeId.Text;
            textBoxUserId.Enabled = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            int selectIndex = comboBoxJobtitle.SelectedIndex;
            if (!Validator.IsValidedId(textBoxEmployeeId.Text.ToString()))
            {
                MessageBox.Show("Employee number must be a 4-digit number.", "Invalid Employee Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Text = "";
                textBoxEmployeeId.Focus();
                return;
            }

            if (emp.DuplicatedId(Convert.ToInt32(textBoxEmployeeId.Text)))
            {
                MessageBox.Show("Employee number already inserted.", "Duplicated Employee Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmployeeId.Text = "";
                textBoxEmployeeId.Focus();
                return;
            }

            if (!Validator.IsValidedName(textBoxFirstName.Text))
            {
                MessageBox.Show("Employee first name must be letters.", "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            if (!Validator.IsValidedName(textBoxLastName.Text))
            {
                MessageBox.Show("Employee last name must be letters.", "Invalid Last Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            if (!Validator.IsValideDate(maskedTextBoxHiredDate.Text))
            {
                MessageBox.Show("Must insert a hired date.", "Invalid Hired Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxHiredDate.Clear();
                maskedTextBoxHiredDate.Focus();
                return;
            }

            if (!Validator.IsValidPhoneNumber(maskedTextBoxPhoneNumber.Text.Trim()))
            {
                MessageBox.Show("Must insert a phone number.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxPhoneNumber.Clear();
                maskedTextBoxPhoneNumber.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Must insert an Email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                textBoxEmail.Focus();
                return;
            }

            if (selectIndex == -1)
            {
                MessageBox.Show("Must choose an Jot Title.", "Invalid Job Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxJobtitle.Focus();
                return;
            }

            emp.EmployeeId = Convert.ToInt32(textBoxEmployeeId.Text);
            emp.FirstName = textBoxFirstName.Text;
            emp.LastName = textBoxLastName.Text;
            emp.HireDate = Convert.ToDateTime(maskedTextBoxHiredDate.Text);
            emp.PhoneNumber = maskedTextBoxPhoneNumber.Text;
            emp.Email = textBoxEmail.Text;
            emp.JobTitle = comboBoxJobtitle.Text;
            emp.SaveEmployee(emp);
            ClearAllEmployeeInformation();
        }

        private void ClearAllEmployeeInformation()
        {
            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            maskedTextBoxHiredDate.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxEmail.Clear();
            comboBoxJobtitle.SelectedIndex = -1;
            textBoxEmployeeId.Focus();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.EmployeeId = Convert.ToInt32(textBoxEmployeeId.Text);
            emp.FirstName = textBoxFirstName.Text;
            emp.LastName = textBoxLastName.Text;
            emp.HireDate = Convert.ToDateTime(maskedTextBoxHiredDate.Text);
            emp.PhoneNumber = maskedTextBoxPhoneNumber.Text;
            emp.Email = textBoxEmail.Text;
            emp.JobTitle = comboBoxJobtitle.Text;
            DialogResult ans = MessageBox.Show("Are you sure you want to update this employee?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                emp.UpdateEmployee(emp);
                MessageBox.Show("Employee record has been updated successfully", "Confirmation");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            DialogResult ans = MessageBox.Show("Do you really want to delete this employee?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                emp.DeleteEmployee(Convert.ToInt32(textBoxEmployeeId.Text));
                MessageBox.Show("Employee record has been deleted successfully","Confirmation");
            }
        }

        private void buttonListAll_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            List<Employee> listE = emp.GetListEmployees();
            emp.DisplayList(listViewEmployee, listE);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int searchIndex = comboBoxSearchOption.SelectedIndex;
            
            switch (searchIndex)
            {
                case 0:
                    if (!Validator.IsValidedId(textBoxSearchInfo.Text.ToString()))
                    {
                        MessageBox.Show("Employee number must be a 4-digit number.", "Invalid Employee Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Text = "";
                        textBoxSearchInfo.Focus();
                        return;
                    }
                    int searchId = Convert.ToInt32(textBoxSearchInfo.Text);
                    Employee emp = new Employee();
                    emp = emp.searchEmployee(searchId);
                    if (emp != null)
                    {
                        textBoxEmployeeId.Text = emp.EmployeeId.ToString();
                        textBoxFirstName.Text = emp.FirstName;
                        textBoxLastName.Text = emp.LastName;
                        maskedTextBoxHiredDate.Text = emp.HireDate.ToString();
                        maskedTextBoxPhoneNumber.Text = emp.PhoneNumber;
                        textBoxEmail.Text = emp.Email;
                        comboBoxJobtitle.Text = emp.JobTitle;
                    }
                    else
                    {
                        MessageBox.Show("Employee ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }

                    buttonDelete.Enabled = true;
                    buttonUpdate.Enabled = true;
                    textBoxSearchInfo.Clear();
                    break;

                case 1:
                    Employee emp1 = new Employee();                    
                    List<Employee> listEmp = emp1.SearchEmployee(textBoxSearchInfo.Text.Trim());                    
                    if (listEmp.Count != 0)
                    {
                        foreach (Employee empTemp in listEmp)
                        {
                            listViewEmployee.Items.Clear();
                            ListViewItem item = new ListViewItem(empTemp.EmployeeId.ToString());                            
                            item.SubItems.Add(empTemp.FirstName);
                            item.SubItems.Add(empTemp.LastName);
                            item.SubItems.Add(empTemp.HireDate.ToString());
                            item.SubItems.Add(empTemp.PhoneNumber);
                            item.SubItems.Add(empTemp.Email);
                            item.SubItems.Add(empTemp.JobTitle);
                            listViewEmployee.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee First Name not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }
                    buttonDelete.Enabled = true;
                    buttonUpdate.Enabled = true;
                    textBoxSearchInfo.Clear();
                    break;

                case 2:
                    Employee emp2 = new Employee();
                    List<Employee> listEmp2 = emp2.SearchEmployee(textBoxSearchInfo.Text.Trim());                    
                    if (listEmp2.Count != 0)
                    {
                        foreach (Employee empTemp2 in listEmp2)
                        {
                            listViewEmployee.Items.Clear();
                            ListViewItem item = new ListViewItem(empTemp2.EmployeeId.ToString());
                            item.SubItems.Add(empTemp2.FirstName);
                            item.SubItems.Add(empTemp2.LastName);
                            item.SubItems.Add(empTemp2.HireDate.ToString());
                            item.SubItems.Add(empTemp2.PhoneNumber);
                            item.SubItems.Add(empTemp2.Email);
                            item.SubItems.Add(empTemp2.JobTitle);
                            listViewEmployee.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Last Name not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }
                    buttonDelete.Enabled = true;
                    buttonUpdate.Enabled = true;
                    textBoxSearchInfo.Clear();
                    break;

                default:
                    break;
            }
        }

        private void comboBoxSearchOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxSearchOption.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    textBoxSearchInfo.Text = "";
                    labelInfo.Text = "Please enter Employee ID.";
                    break;

                case 1:
                    textBoxSearchInfo.Text = "";
                    labelInfo.Text = "Please enter First Name.";
                    break;

                case 2:
                    textBoxSearchInfo.Text = "";
                    labelInfo.Text = "Please enter Last Name";
                    break;
                default:
                    break;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /*User section*/

        private void buttonSaveUser_Click(object sender, EventArgs e)
        {
            User usr = new User();

            if (usr.UserDuplicated(Convert.ToInt32(textBoxUserId.Text)))
            {
                MessageBox.Show("User account information already saved.", "Duplicated User Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmployeeId.Text = "";
                textBoxEmployeeId.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Please enter a password for saving.", "Invaild Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Text = "";
                textBoxPassword.Focus();
                return;
            }

            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Invaild Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxStatus.SelectedIndex = -1;
                return;
            }

            
            usr.UserId = Convert.ToInt32(textBoxUserId.Text);
            usr.Password = textBoxPassword.Text;
            usr.Status = comboBoxStatus.Text;
            usr.SaveUser(usr);
            textBoxPassword.Clear();
            comboBoxStatus.SelectedIndex = -1;
        }

        private void buttonExitUser_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonListAllUser_Click(object sender, EventArgs e)
        {
            User usr = new User();
            List<User> listU = usr.GetUserList();
            usr.DisplayUserList(listViewUser, listU);
        }

        private void FormEmployeeUser_Load(object sender, EventArgs e)
        {
            comboBoxSearchOption.SelectedIndex = 0;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            buttonUpdateUser.Enabled = false;
            buttonDeleteUser.Enabled = false;
            buttonSearch.Enabled = false;
            buttonSearchUser.Enabled = false;
            buttonSaveUser.Enabled = false;
        }

        private void buttonSearchUser_Click(object sender, EventArgs e)
        {
            /*Search ID*/
            if (!Validator.IsValidedId(textBoxSearchUserID.Text.ToString()))
            {
                MessageBox.Show("Employee number must be a 4-digit number.", "Invalid Employee Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Text = "";
                textBoxEmployeeId.Focus();
                return;
            }
            int usrId = Convert.ToInt32(textBoxSearchUserID.Text);
            User usr = new User();
            usr = usr.searchUserId(usrId);
            if (usr != null)
            {
                textBoxUserId.Text = usr.UserId.ToString();
                textBoxPassword.Text = usr.Password;
                //comboBoxStatus.SelectedIndex = UserDA.Comboboxcheck(Convert.ToInt32(usr.UserId));
                comboBoxStatus.Text = usr.Status;
                buttonUpdateUser.Enabled = true;
                buttonDeleteUser.Enabled = true;
                textBoxSearchUserID.Clear();
            }
            else
            {
                MessageBox.Show("Employee ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSearchInfo.Clear();
                textBoxSearchInfo.Focus();
            }

        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUserId.Text))
            {
                MessageBox.Show("Please enter or search a User ID.", "Invaild User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUserId.Text = "";
                textBoxUserId.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Please enter a password for saving.", "Invaild Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Text = "";
                textBoxPassword.Focus();
                return;
            }

            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Invaild Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxStatus.SelectedIndex = -1;
                return;
            }

            User usr = new User();
            usr.UserId = Convert.ToInt32(textBoxUserId.Text);
            usr.Password = textBoxPassword.Text;
            usr.Status = comboBoxStatus.Text;
            DialogResult ans = MessageBox.Show("Are you sure you want to update this user?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                usr.UpdateUser(usr);
                MessageBox.Show("User record has been updated successfully", "Confirmation");
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {            
            User usr = new User();
            DialogResult ans = MessageBox.Show("Do you really want to delete this user?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                usr.DeleteUserRecord(Convert.ToInt32(textBoxUserId.Text));
                MessageBox.Show("User account has been deteled successfully");
            }
        }

        private void buttonClearTextBox_Click(object sender, EventArgs e)
        {
            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            maskedTextBoxHiredDate.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxEmail.Clear();
            comboBoxJobtitle.SelectedIndex = -1;
            textBoxEmployeeId.Focus();

            buttonDelete.Enabled = false;
            buttonUpdate.Enabled = false;
        }

        private void buttonClearUserText_Click(object sender, EventArgs e)
        {
            textBoxUserId.Clear();
            textBoxPassword.Clear();
            comboBoxStatus.SelectedIndex = -1;

            buttonUpdateUser.Enabled = false;
            buttonDeleteUser.Enabled = false;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                buttonUpdateUser.Enabled = false;
                buttonDeleteUser.Enabled = false;
            }
            else
            {
                buttonUpdateUser.Enabled = true;
                buttonDeleteUser.Enabled = true;
            }
        }

        private void textBoxSearchInfo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearchInfo.Text == "")
            {
                buttonSearch.Enabled = false;
            }
            else
            {
                buttonSearch.Enabled = true;
            }
        }

        private void textBoxSearchUserID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearchUserID.Text == "")
            {
                buttonSearchUser.Enabled = false;
            }
            else
            {
                buttonSearchUser.Enabled = true;
            }
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to Log Out?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmEmpUser = new FormLogin();
                frmEmpUser.ShowDialog();               
            }         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to Log Out?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmEmpUser = new FormLogin();
                frmEmpUser.ShowDialog();
            }
        }

        private void textBoxUserId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserId.Text == "")
            {
                buttonSaveUser.Enabled = false;
            }
            else
            {
                buttonSaveUser.Enabled = true;
            }
        }
    }
}
