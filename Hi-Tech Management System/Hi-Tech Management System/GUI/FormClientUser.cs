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
using System.IO;
using System.Xml;
using Hi_Tech_Management_System.Validation;

namespace Hi_Tech_Management_System.GUI
{
    public partial class FormClientUser : Form
    {
        static string filePath = Application.StartupPath + @"\Client.dat";
        public FormClientUser()
        {
            InitializeComponent();
        } 

        private void ClearAllClientInformation()
        {
            textBoxClientId.Clear();
            textBoxClientName.Clear();
            textBoxAddress.Clear();
            textBoxCity.Clear();
            maskedTextBoxClientPhone.Clear();
            maskedTextBoxClientFaxNumber.Clear();
            maskedTextBoxClientPost.Clear();
            textBoxBankAccount.Clear();
            textBoxCreditLimit.Clear();
        }

        private void FormClientUser_Load(object sender, EventArgs e)
        {
            comboBoxSearchOption.SelectedIndex = 0;
            textBoxUserId.Text = FormLogin.passingUserId;
            textBoxUserId.ReadOnly = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            buttonSearch.Enabled = false;
            labelOption.Visible = false;
            comboBoxFirstOption.Visible = false;
            textBoxSearchInfo.Visible = true;
            comboBoxStatus.SelectedIndex = 0;
            buttonUpdateUser.Enabled = false;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int searchIndex = comboBoxSearchOption.SelectedIndex;

            switch (searchIndex)
            {
                case 0:                    
                    if (!Validator.IsValidedId(textBoxSearchInfo.Text))
                    {
                        MessageBox.Show("Please insert a 4-digit number to search.", "Invalid Client Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Text = "";
                        textBoxSearchInfo.Focus();
                        return;
                    }
                    int searchId = Convert.ToInt32(textBoxSearchInfo.Text);
                    Client clt = new Client();
                    clt = clt.searchClient(searchId);
                    if (clt != null)
                    {
                        textBoxClientId.Text = clt.ClientId.ToString();
                        textBoxClientName.Text = clt.ClientName;
                        textBoxAddress.Text = clt.ClientAddress;
                        textBoxCity.Text = clt.ClientCity;
                        maskedTextBoxClientPost.Text = clt.ClientPost;
                        maskedTextBoxClientPhone.Text = clt.ClientPhoneNumber;
                        maskedTextBoxClientFaxNumber.Text = clt.FaxNumber;
                        textBoxBankAccount.Text = clt.BankAccount.ToString();
                        textBoxCreditLimit.Text = clt.CreditLimit;
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Client ID not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }
                    break;
                case 1:
                    Client clt1 = new Client();
                    List<Client> listC = clt1.searchClientName(comboBoxFirstOption.Text.Trim());
                    if (listC.Count != 0)
                    {
                        foreach (Client cltTemp in listC)
                        {
                            listViewClient.Items.Clear();
                            ListViewItem item = new ListViewItem(cltTemp.ClientId.ToString());
                            item.SubItems.Add(cltTemp.ClientName);
                            item.SubItems.Add(cltTemp.ClientAddress);
                            item.SubItems.Add(cltTemp.ClientCity);
                            item.SubItems.Add(cltTemp.ClientPost);
                            item.SubItems.Add(cltTemp.ClientPhoneNumber);
                            item.SubItems.Add(cltTemp.FaxNumber);
                            item.SubItems.Add(cltTemp.BankAccount.ToString());
                            item.SubItems.Add(cltTemp.CreditLimit);
                            listViewClient.Items.Add(item);
                        }
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Client Name not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBoxSearchOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = comboBoxSearchOption.SelectedIndex;
            switch (selectIndex)
            {
                case 0:
                    comboBoxFirstOption.SelectedIndex = -1;
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    comboBoxFirstOption.Items.Clear();
                    textBoxSearchInfo.Text = "";
                    labelInfo.Text = "Please enter Client ID";
                    break;
                case 1:
                    textBoxSearchInfo.Text = "";
                    labelOption.Visible = true;
                    comboBoxFirstOption.Visible = true;
                    textBoxSearchInfo.Visible = false;
                    comboBoxFirstOption.Items.Clear();
                    Client aClient = new Client();
                    List<Client> listC = aClient.GetListClient();
                    comboBoxFirstOption.Items.Clear();
                    foreach (Client cltItem in listC)
                    {
                        comboBoxFirstOption.Items.Add(cltItem.ClientName);
                    }
                    labelInfo.Text = "Please enter Client Name";
                    break;
                default:
                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Client clt = new Client();
            if (!Validator.IsValidedId(textBoxClientId.Text))
            {
                MessageBox.Show("Client number must be a 4-digit number.", "Invalid Client Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxClientId.Text = "";
                textBoxClientId.Focus();
                return;
            }

            if (clt.IsDuplicated(Convert.ToInt32(textBoxClientId.Text)))
            {
                MessageBox.Show("Client number has already inserted.", "Invalid Client Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxClientId.Text = "";
                textBoxClientId.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxClientName.Text))
            {
                MessageBox.Show("Must insert a Client Name", "Invalid Client Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxClientName.Text = "";
                textBoxClientName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxAddress.Text))
            {
                MessageBox.Show("Must insert an address", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAddress.Text = "";
                textBoxAddress.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxCity.Text))
            {
                MessageBox.Show("Must insert a City", "Invalid City", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCity.Text = "";
                textBoxCity.Focus();
                return;
            }

            if (!Validator.IsValidPostalCode(maskedTextBoxClientPost.Text))
            {
                MessageBox.Show("Must insert a postial code", "Invalid Post Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxClientPost.Text = "";
                maskedTextBoxClientPost.Focus();
                return;
            }

            if (!Validator.IsValidPhoneNumber(maskedTextBoxClientPhone.Text))
            {
                MessageBox.Show("Must insert a Client Phone Number", "Invalid Client Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxClientPhone.Text = "";
                maskedTextBoxClientPhone.Focus();
                return;
            }

            if (!Validator.IsValidPhoneNumber(maskedTextBoxClientFaxNumber.Text))
            {
                MessageBox.Show("Must insert a Client Fax Number", "Invalid Client Fax Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxClientFaxNumber.Text = "";
                maskedTextBoxClientFaxNumber.Focus();
                return;
            }

            if (!Validator.IsValidedNumber(textBoxBankAccount.Text))
            {
                MessageBox.Show("Bank Account must be numbers", "Invalid Bank Account Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBankAccount.Text = "";
                textBoxBankAccount.Focus();
                return;
            }

            if (!Validator.IsValidedNumber(textBoxCreditLimit.Text))
            {
                MessageBox.Show("Credit Limit must be numbers", "Invalid Credit Limit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCreditLimit.Text = "";
                textBoxCreditLimit.Focus();
                return;
            }
            clt.ClientId = Convert.ToInt32(textBoxClientId.Text);
            clt.ClientName = textBoxClientName.Text;
            clt.ClientAddress = textBoxAddress.Text;
            clt.ClientCity = textBoxCity.Text;
            clt.ClientPhoneNumber = maskedTextBoxClientPhone.Text;
            clt.FaxNumber = maskedTextBoxClientFaxNumber.Text;
            clt.ClientPost = maskedTextBoxClientPost.Text.ToUpper();
            clt.BankAccount = Convert.ToInt32(textBoxBankAccount.Text);
            clt.CreditLimit = textBoxCreditLimit.Text;
            clt.SaveClient(clt);
            ClearAllClientInformation();
        }

        private void buttonListAll_Click(object sender, EventArgs e)
        {
            Client clt = new Client();
            List<Client> listC = clt.GetListClient();
            clt.DisplayClientList(listViewClient, listC);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Client clt = new Client();
            clt.ClientId = Convert.ToInt32(textBoxClientId.Text);
            clt.ClientName = textBoxClientName.Text;
            clt.ClientAddress = textBoxAddress.Text;
            clt.ClientCity = textBoxCity.Text;
            clt.ClientPost = maskedTextBoxClientPost.Text.ToUpper();
            clt.ClientPhoneNumber = maskedTextBoxClientPhone.Text;
            clt.FaxNumber = maskedTextBoxClientFaxNumber.Text;
            clt.BankAccount = Convert.ToInt32(textBoxBankAccount.Text);
            clt.CreditLimit = textBoxCreditLimit.Text;
            DialogResult ans = MessageBox.Show("Are you sure you want to update this Client?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ans == DialogResult.Yes)
            {
                clt.UpdateClient(clt);
                MessageBox.Show("Client record has been updated successfully", "Confirmation");
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Client clt = new Client();
            DialogResult ans = MessageBox.Show("Do you really want to delete this client?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                clt.DeleteClient(Convert.ToInt32(textBoxClientId.Text));
                MessageBox.Show("Employee record has been deleted successfully!");
            }
        }

        // User section

        private void buttonExitUser_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void tabControlClientUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUserId.Text = FormLogin.passingUserId;
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Must enter a password to update.", "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                return;
            }
            User usr = new User();
            usr.UserId = Convert.ToInt32(textBoxUserId.Text);
            usr.Password = textBoxPassword.Text;
            usr.Status = comboBoxStatus.Text;
            DialogResult ans = MessageBox.Show("Are you sure you want to update your acount info??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ans == DialogResult.Yes)
            {
                usr.UpdateUser(usr);
                MessageBox.Show("Account info has been updated successfully", "Confirmation");
            }
        }

        private void buttonWriteToXML_Click(object sender, EventArgs e)
        {
            Client clt = new Client();
            List<Client> listC = clt.GetListClient();

            string dir = @"";
            string xmlPath = dir + "ClientsXML.xml";

            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.IndentChars = ("    ");


            XmlWriter xmlWriter = XmlWriter.Create(xmlPath, xmlWriterSetting);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Clents");

            foreach (Client cltItem in listC)
            {
                xmlWriter.WriteStartElement("Client");
                xmlWriter.WriteAttributeString("ClientID", cltItem.ClientId.ToString());
                xmlWriter.WriteElementString("ClientName", cltItem.ClientName);
                xmlWriter.WriteElementString("Address", cltItem.ClientAddress);
                xmlWriter.WriteElementString("City", cltItem.ClientCity);
                xmlWriter.WriteElementString("PostalCode", cltItem.ClientPost);
                xmlWriter.WriteElementString("PhoneNumber", cltItem.ClientPhoneNumber);
                xmlWriter.WriteElementString("FaxNumber", cltItem.FaxNumber);
                xmlWriter.WriteElementString("BankAccount", cltItem.BankAccount.ToString());
                xmlWriter.WriteElementString("CreditLimit", cltItem.CreditLimit);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.Close();

            MessageBox.Show("The xml file has been created successfully.", "File Created");
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAllClientInformation();
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
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

        private void comboBoxFirstOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFirstOption.SelectedIndex == -1)
            {
                buttonSearch.Enabled = false;
            }
            else
            {
                buttonSearch.Enabled = true;
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

        private void buttonUserLogout_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to Log Out?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmEmpUser = new FormLogin();
                frmEmpUser.ShowDialog();
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                buttonUpdateUser.Enabled = false;
            }
            else
            {
                buttonUpdateUser.Enabled = true;
            }
        }
    }
}
