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
using Hi_Tech_Management_System.Validation;

namespace Hi_Tech_Management_System.GUI
{
    public partial class FormOrderUser : Form
    {
        public FormOrderUser()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FormOrderUser_Load(object sender, EventArgs e)
        {
            buttonUpdateUser.Enabled = false;
            comboBoxFirstCondition.SelectedIndex = 3;
            textBoxUserId.ReadOnly = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            comboBoxFirstOption.Visible = false;
            labelOption.Visible = false;
            comboBoxStatus.SelectedIndex = 0;
            Client aClient = new Client();
            List<Client> listC = aClient.GetListClient();
            comboBoxClient.Items.Clear();
            foreach (Client cltItem in listC)
            {
                comboBoxClient.Items.Add(cltItem.ClientName);
            }

            Products aProduct = new Products();
            List<Products> listP = aProduct.GetListProducts();
            comboBoxProduct.Items.Clear();
            foreach (Products pdtItem in listP)
            {
                comboBoxProduct.Items.Add(pdtItem.ProductTitle);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Order odr = new Order();
            int selectClientIndex = comboBoxClient.SelectedIndex;
            int selectProductIndex = comboBoxProduct.SelectedIndex;
            int selectMethodIndex = comboBoxMethod.SelectedIndex;
            if (selectClientIndex == -1)
            {
                MessageBox.Show("Must choose a Client.", "Invalid Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxClient.Focus();
                return;
            }

            if (selectProductIndex == -1)
            {
                MessageBox.Show("Must choose a Product.", "Invalid Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxProduct.Focus();
                return;
            }

            if (selectMethodIndex == -1)
            {
                MessageBox.Show("Must choose a Method for contact.", "Invalid Method", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxMethod.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Must insert a number or email address with the method.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNumber.Clear();
                textBoxNumber.Focus();
                return;
            }

            if (!Validator.IsValidedNumber(textBoxOrderID.Text))
            {
                MessageBox.Show("Order Id must be numbers.", "Invalid Order ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxOrderID.Clear();
                textBoxOrderID.Focus();
                return;
            }

            if (odr.IsOrderDuplicated(Convert.ToInt32(textBoxOrderID.Text)))
            {
                MessageBox.Show("Order ID already inserted.", "Duplicated Order ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOrderID.Text = "";
                textBoxOrderID.Focus();
                return;
            }

            if (!Validator.IsValidedNumber(textBoxOrderQuantity.Text))
            {
                MessageBox.Show("Order quantity must be numbers.", "Invalid Order quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxOrderQuantity.Clear();
                textBoxOrderQuantity.Focus();
                return;
            }

            if (!Validator.IsValideDate(maskedTextBoxRequriedDate.Text))
            {
                MessageBox.Show("Requried Date must be inserted.", "Invalid Required Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxRequriedDate.Clear();
                maskedTextBoxRequriedDate.Focus();
                return;
            }
            
            odr.ClientInfo = comboBoxClient.Text;
            odr.ProductInfo = comboBoxProduct.Text;
            odr.ContactMethod = comboBoxMethod.Text;
            odr.ContactNumber = textBoxNumber.Text;
            odr.OrderID = Convert.ToInt32(textBoxOrderID.Text);
            odr.OrderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);
            odr.RequireDate = Convert.ToDateTime(maskedTextBoxRequriedDate.Text);
            odr.ShipDate = odr.RequireDate.AddDays(-1);
            odr.Status = "Processed";
            odr.SaveOrder(odr);
            ClearAllOrderInforamtion();
        }

        private void ClearAllOrderInforamtion()
        {
            comboBoxClient.SelectedIndex = -1;
            comboBoxProduct.SelectedIndex = -1;
            comboBoxMethod.SelectedIndex = -1;
            textBoxNumber.Clear();
            textBoxOrderID.Clear();
            textBoxOrderQuantity.Clear();
            maskedTextBoxRequriedDate.Clear();
        }

        private void buttonListAll_Click(object sender, EventArgs e)
        {
            Order odr = new Order();
            List<Order> listO = odr.GetAllRecords();
            odr.DisplayList(listViewOrder, listO);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int searchIndex = comboBoxFirstCondition.SelectedIndex;

            switch (searchIndex)
            {
                case 0:
                    Order odr0 = new Order();
                    List<Order> listO = odr0.SearchOrderOption(comboBoxFirstOption.Text.Trim());
                    if (listO.Count != 0)
                    {
                        odr0.DisplayList(listViewOrder, listO);
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Client order not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBoxFirstOption.SelectedIndex = -1;
                    }
                    break;
                case 1:
                    Order odr1 = new Order();
                    List<Order> listO1 = odr1.SearchOrderOption(comboBoxFirstOption.Text.Trim());
                    if (listO1.Count != 0)
                    {
                        odr1.DisplayList(listViewOrder, listO1);
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Product order not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBoxFirstOption.SelectedIndex = -1;
                    }
                    break;
                case 2:
                    Order odr2 = new Order();
                    List<Order> listO2 = odr2.SearchOrderOption(comboBoxFirstOption.Text.Trim());
                    if (listO2.Count != 0)
                    {
                        odr2.DisplayList(listViewOrder, listO2);
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Particular Method not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBoxFirstOption.SelectedIndex = -1;
                    }
                    break;
                case 3:
                    if (!Validator.IsValidedNumber(textBoxSearchInfo.Text))
                    {
                        MessageBox.Show("Order Id must be inserted.", "Invalid Order ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                        return;
                    }
                    int searchId = Convert.ToInt32(textBoxSearchInfo.Text);
                    Order odr = new Order();
                    odr = odr.SearchOrderID(searchId);
                    if (odr != null)
                    {
                        comboBoxClient.Text = odr.ClientInfo;
                        comboBoxProduct.Text = odr.ProductInfo;
                        comboBoxMethod.Text = odr.ContactMethod;
                        textBoxNumber.Text = odr.ContactNumber;
                        textBoxOrderID.Text = odr.OrderID.ToString();
                        textBoxOrderQuantity.Text = odr.OrderQuantity.ToString();
                        maskedTextBoxRequriedDate.Text = odr.RequireDate.ToString();
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Product ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                        textBoxSearchInfo.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBoxFirstCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFirstCondition.SelectedIndex == 0)
            {
                labelInfo.Text = "Please select a Client!";
                comboBoxFirstOption.SelectedIndex = -1;
                textBoxSearchInfo.Text = "";
                comboBoxFirstOption.Visible = true;
                labelOption.Visible = true;
                comboBoxFirstOption.Enabled = true;
                textBoxSearchInfo.Visible = false;
                Client aClient = new Client();
                List<Client> listC = aClient.GetListClient();
                comboBoxFirstOption.Items.Clear();
                foreach (Client cltItem in listC)
                {
                    comboBoxFirstOption.Items.Add(cltItem.ClientName);
                }
                //comboBoxSecondCondition.Items.RemoveAt(0);
            }
            else if (comboBoxFirstCondition.SelectedIndex == 1)
            {
                labelInfo.Text = "Please select a Product!";
                comboBoxFirstOption.SelectedIndex = -1;
                textBoxSearchInfo.Text = "";
                comboBoxFirstOption.Visible = true;
                labelOption.Visible = true;
                comboBoxFirstOption.Enabled = true;
                textBoxSearchInfo.Visible = false;
                Products aProduct = new Products();
                List<Products> listP = aProduct.GetListProducts();
                comboBoxFirstOption.Items.Clear();
                foreach (Products pdtItem in listP)
                {
                    comboBoxFirstOption.Items.Add(pdtItem.ProductTitle);
                }
                //comboBoxSecondCondition.Items.RemoveAt(1);
            }
            else if (comboBoxFirstCondition.SelectedIndex == 2)
            {
                labelInfo.Text = "Please select a Method!";
                comboBoxFirstOption.SelectedIndex = -1;
                textBoxSearchInfo.Text = "";
                comboBoxFirstOption.Visible = true;
                labelOption.Visible = true;
                comboBoxFirstOption.Enabled = true;
                textBoxSearchInfo.Visible = false;
                comboBoxFirstOption.Items.Clear();
                comboBoxFirstOption.Items.Add("Phone");
                comboBoxFirstOption.Items.Add("Fax");
                comboBoxFirstOption.Items.Add("Email");
                //comboBoxSecondCondition.Items.RemoveAt(2);
            }
            else if (comboBoxFirstCondition.SelectedIndex == 3)
            {
                labelInfo.Text = "Please enter a Order ID!";
                comboBoxFirstOption.SelectedIndex = -1;
                textBoxSearchInfo.Text = "";
                comboBoxFirstOption.Visible = false;
                labelOption.Visible = false;
                comboBoxFirstOption.Enabled = false;
                textBoxSearchInfo.Visible = true;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Order odr = new Order();
            odr.ClientInfo = comboBoxClient.Text;
            odr.ProductInfo = comboBoxProduct.Text;
            odr.ContactMethod = comboBoxMethod.Text;
            odr.ContactNumber = textBoxNumber.Text;
            odr.OrderID = Convert.ToInt32(textBoxOrderID.Text);
            odr.OrderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);
            odr.RequireDate = Convert.ToDateTime(maskedTextBoxRequriedDate.Text);
            odr.ShipDate = odr.RequireDate.AddDays(-1);
            odr.Status = "Processed";
            DialogResult ans = MessageBox.Show("Are you sure you want to update this order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                odr.UpdateOrderRecord(odr);
                MessageBox.Show("Order record has been updated successfully", "Confirmation");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Order odr = new Order();
            odr.ClientInfo = comboBoxClient.Text;
            odr.ProductInfo = comboBoxProduct.Text;
            odr.ContactMethod = comboBoxMethod.Text;
            odr.ContactNumber = textBoxNumber.Text;
            odr.OrderID = Convert.ToInt32(textBoxOrderID.Text);
            odr.OrderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);
            odr.RequireDate = Convert.ToDateTime(maskedTextBoxRequriedDate.Text);
            odr.ShipDate = odr.RequireDate.AddDays(-1);
            odr.Status = "Cancelled";
            DialogResult ans = MessageBox.Show("Do you really want to cancel this order?", "Confirm",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                odr.CancelOrderRecord(odr);
                MessageBox.Show("Order record has been cancelled successfully", "Confirmation");
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAllOrderInforamtion();
        }

        // User section

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

        private void buttonExitUser_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUserId.Text = FormLogin.passingUserId;
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

    }
}
