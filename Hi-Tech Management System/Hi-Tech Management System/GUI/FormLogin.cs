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
using Hi_Tech_Management_System.GUI;
using Hi_Tech_Management_System.Validation;
using System.IO;

namespace Hi_Tech_Management_System.GUI
{
    public partial class FormLogin : Form
    {
        static string filePath = Application.StartupPath + @"\Employees.dat";
        public static string passingUserId;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            User aUser = new User();
            aUser.UserId = Convert.ToInt32(textBoxUserId.Text);
            aUser.Password = textBoxPassword.Text.Trim();
            passingUserId = textBoxUserId.Text;
            if (aUser.LogIn(aUser))
            {
                Employee aEmp = new Employee();
                StreamReader sr = new StreamReader(filePath, true);
                aEmp.EmployeeId = Convert.ToInt32(textBoxUserId.Text);                
                if (File.Exists(filePath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] fields = line.Split(',');
                        if (aEmp.EmployeeId == Convert.ToInt32(fields[0]))
                        {
                            aEmp.JobTitle = fields[6];
                        }
                        line = sr.ReadLine();
                    }
                }
                sr.Close();

                if (aEmp.JobTitle == "MIS Manager")
                {
                    this.Hide();
                    FormEmployeeUser frmEmpUser = new FormEmployeeUser();
                    frmEmpUser.ShowDialog();
                }
                else if (aEmp.JobTitle == "Sales Manager")
                {
                    this.Hide();
                    FormClientUser frmEmpUser = new FormClientUser();
                    frmEmpUser.ShowDialog();
                }
                else if (aEmp.JobTitle == "Inventory Controller")
                {
                    this.Hide();
                    FormProductUser frmEmpUser = new FormProductUser();
                    frmEmpUser.ShowDialog();
                }
                else if (aEmp.JobTitle == "Order Clerk")
                {
                    this.Hide();
                    FormOrderUser frmEmpUser = new FormOrderUser();
                    frmEmpUser.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Oops, you lose your MEMORY???? ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBoxUserId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserId.Text == "")
            {
                buttonLogin.Enabled = false;
            }
            else
            {
                buttonLogin.Enabled = true;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
        }

    }
}
