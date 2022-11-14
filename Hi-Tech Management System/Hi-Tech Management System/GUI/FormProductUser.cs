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
using Hi_Tech_Management_System.DataAccess;
using Hi_Tech_Management_System.Validation;

namespace Hi_Tech_Management_System.GUI
{
    public partial class FormProductUser : Form
    {
        public FormProductUser()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Products pdt = new Products();
            if (!Validator.IsValidedId(textBoxAuthorId.Text.ToString()))
            {
                MessageBox.Show("Author ID must be inserted by a 4-digit number.", "Invalid Employee Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAuthorId.Text = "";
                textBoxAuthorId.Focus();
                return;
            }

            if (!Validator.IsValidedName(textBoxFirstName.Text))
            {
                MessageBox.Show("Employee First Name must be letters.", "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            if (!Validator.IsValidedName(textBoxLastName.Text))
            {
                MessageBox.Show("Employee Last Name must be letters.", "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxProductID.Text))
            {
                MessageBox.Show("Must insert a Product ID", "Invalid Product ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxProductID.Clear();
                textBoxProductID.Focus();
                return;
            }

            if (pdt.IsPdtDuplicated(textBoxProductID.Text))
            {
                MessageBox.Show("Product ID already inserted.", "Duplicated Product ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxProductID.Text = "";
                textBoxProductID.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(textBoxTitle.Text))
            //{
            //    MessageBox.Show("Must insert a Product Title", "Invalid Product Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxTitle.Clear();
            //    textBoxTitle.Focus();
            //    return;
            //}

            //if (!Validator.IsValidedNumber(textBoxUnitPrice.Text))
            //{
            //    MessageBox.Show("Unit Price must be numbers", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxUnitPrice.Clear();
            //    textBoxUnitPrice.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(textBoxPublisher.Text))
            //{
            //    MessageBox.Show("Must insert a Publisher", "Invalid Publisher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxUnitPrice.Clear();
            //    textBoxUnitPrice.Focus();
            //    return;
            //}

            //if (!Validator.IsValidedNumber(textBoxYearPublished.Text))
            //{
            //    MessageBox.Show("Published Year must be numbers", "Invalid Published Year", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxYearPublished.Clear();
            //    textBoxYearPublished.Focus();
            //    return;
            //}

            //if (!Validator.IsValidedNumber(textBoxQOH.Text))
            //{
            //    MessageBox.Show("Quantity must be numbers", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxQOH.Clear();
            //    textBoxQOH.Focus();
            //    return;
            //}

            pdt.Category = comboBoxCategory.Text;
            pdt.AuthorId = Convert.ToInt32(textBoxAuthorId.Text);
            pdt.AuthorFirstName = textBoxFirstName.Text;
            pdt.AuthorLastName = textBoxLastName.Text;
            pdt.ProductId = textBoxProductID.Text;
            pdt.ProductTitle = textBoxTitle.Text;
            pdt.UnitPrice = Convert.ToSingle(textBoxUnitPrice.Text);
            pdt.Publisher = textBoxPublisher.Text;
            pdt.YearPublished = Convert.ToInt32(textBoxYearPublished.Text);
            pdt.Qoh = Convert.ToInt32(textBoxQOH.Text);
            pdt.SaveProduct(pdt);
            ClearAllProductInfomation();
        }

        private void ClearAllProductInfomation()
        {
            comboBoxCategory.SelectedIndex = -1;
            textBoxAuthorId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxProductID.Clear();
            textBoxTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxPublisher.Clear();
            textBoxYearPublished.Clear();
            textBoxQOH.Clear();
        }

        private void buttonListAll_Click(object sender, EventArgs e)
        {
            Products pdt = new Products();
            List<Products> listP = pdt.GetListProducts();
            pdt.DisplayProducts(listViewProduct, listP);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAllProductInfomation();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int searchIndex = comboBoxSearchOption.SelectedIndex;
            switch (searchIndex)
            {
                case 0:
                    Products pdt0 = new Products();
                    List<Products> listP0 = pdt0.searchProducts(comboBoxFirstOption.Text.Trim());
                    if (listP0.Count != 0)
                    {
                        pdt0.DisplayProducts(listViewProduct, listP0);
                    }
                    else
                    {
                        MessageBox.Show("Categroy is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
 
                    textBoxSearchInfo.Clear();
                    break;
                case 1:
                    Products pdt1 = new Products();
                    List<Products> listP1 = pdt1.searchProducts(textBoxSearchInfo.Text.Trim());
                    if (listP1.Count != 0)
                    {
                        pdt1.DisplayProducts(listViewProduct, listP1);
                    }
                    else
                    {
                        MessageBox.Show("Author ID not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
                    textBoxSearchInfo.Clear();
                    break;
                case 2:
                    Products pdt2 = new Products();
                    List<Products> listP2 = pdt2.searchProducts(textBoxSearchInfo.Text.Trim());
                    if (listP2.Count != 0)
                    {
                        pdt2.DisplayProducts(listViewProduct, listP2);
                    }
                    else
                    {
                        MessageBox.Show("Author First Name is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
                    textBoxSearchInfo.Clear();
                    break;
                case 3:
                    Products pdt3 = new Products();
                    List<Products> listP3 = pdt3.searchProducts(textBoxSearchInfo.Text.Trim());
                    if (listP3.Count != 0)
                    {
                        pdt3.DisplayProducts(listViewProduct, listP3);
                    }
                    else
                    {
                        MessageBox.Show("Author Last Name is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
                    textBoxSearchInfo.Clear();
                    break;
                case 4:
                    string searchProductID = textBoxSearchInfo.Text;
                    Products pdt = new Products();
                    pdt = pdt.searchProductID(searchProductID);
                    if (pdt != null)
                    {
                        comboBoxCategory.Text = pdt.Category;
                        textBoxAuthorId.Text = pdt.AuthorId.ToString();
                        textBoxFirstName.Text = pdt.AuthorFirstName;
                        textBoxLastName.Text = pdt.AuthorLastName;
                        textBoxProductID.Text = pdt.ProductId;
                        textBoxTitle.Text = pdt.ProductTitle;
                        textBoxUnitPrice.Text = pdt.UnitPrice.ToString();
                        textBoxPublisher.Text = pdt.Publisher;
                        textBoxYearPublished.Text = pdt.YearPublished.ToString();
                        textBoxQOH.Text = pdt.Qoh.ToString();
                        buttonenable();
                        buttonDelete.Enabled = true;
                        buttonUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Product ISBN/SDN is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }                   
                    break;
                case 5:
                    Products pdt5 = new Products();
                    List<Products> listP5 = pdt5.searchProducts(textBoxSearchInfo.Text.Trim());
                    if (listP5.Count != 0)
                    {
                        pdt5.DisplayProducts(listViewProduct, listP5);
                    }
                    else
                    {
                        MessageBox.Show("Product title is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
                    textBoxSearchInfo.Clear();
                    break;
                case 6:
                    Products pdt6 = new Products();
                    List<Products> listP6 = pdt6.searchProducts(textBoxSearchInfo.Text.Trim());
                    if (listP6.Count != 0)
                    {
                        pdt6.DisplayProducts(listViewProduct, listP6);
                    }
                    else
                    {
                        MessageBox.Show("Publisher is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearchInfo.Clear();
                    }
                    textBoxSearchInfo.Clear();
                    break;
                default:
                    break;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Products pdt = new Products();
            pdt.Category = comboBoxCategory.Text;
            pdt.AuthorId = Convert.ToInt32(textBoxAuthorId.Text);
            pdt.AuthorFirstName = textBoxFirstName.Text;
            pdt.AuthorLastName = textBoxLastName.Text;
            pdt.ProductId = textBoxProductID.Text;
            pdt.ProductTitle = textBoxTitle.Text;
            pdt.UnitPrice = Convert.ToSingle(textBoxUnitPrice.Text);
            pdt.Publisher = textBoxPublisher.Text;
            pdt.YearPublished = Convert.ToInt32(textBoxYearPublished.Text);
            pdt.Qoh = Convert.ToInt32(textBoxQOH.Text);
            DialogResult ans = MessageBox.Show("Are you sure you want to update the information of this product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                pdt.UpdateProducts(pdt);
                MessageBox.Show("Product information has been updated successfully", "Confirmation");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Products pdt = new Products();
            DialogResult ans = MessageBox.Show("Are you sure you want to delete the information of this product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                pdt.DeletRecord(textBoxProductID.Text);
                MessageBox.Show("Product information has been deleted successfully", "Confirmation");
            }
        }

        private void FormProductUser_Load(object sender, EventArgs e)
        {
            comboBoxStatus.SelectedIndex = 0;
            buttonUpdateUser.Enabled = false;
            comboBoxSearchOption.SelectedIndex = 4;
            textBoxUserId.ReadOnly = true;           
            buttonSave.Enabled = false;
            comboBoxFirstOption.Visible = false;
            labelOption.Visible = false;
            buttondisable();
 
        }

        private void buttondisable()
        {
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            textBoxAuthorId.Enabled = false;
            textBoxFirstName.Enabled = false;
            textBoxLastName.Enabled = false;
            textBoxProductID.Enabled = false;
            textBoxTitle.Enabled = false;
            textBoxUnitPrice.Enabled = false;
            textBoxPublisher.Enabled = false;
            textBoxYearPublished.Enabled = false;
            textBoxQOH.Enabled = false;
        }

        private void buttonenable()
        {
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
            textBoxAuthorId.Enabled = true;
            textBoxFirstName.Enabled = true;
            textBoxLastName.Enabled = true;
            textBoxProductID.Enabled = true;
            textBoxTitle.Enabled = true;
            textBoxUnitPrice.Enabled = true;
            textBoxPublisher.Enabled = true;
            textBoxYearPublished.Enabled = true;
            textBoxQOH.Enabled = true;
        }

        private void textboxdisable()
        {
            textBoxAuthorId.Enabled = false;
            textBoxFirstName.Enabled = false;
            textBoxLastName.Enabled = false;
            textBoxProductID.Enabled = false;
            textBoxTitle.Enabled = false;
            textBoxUnitPrice.Enabled = false;
            textBoxPublisher.Enabled = false;
            textBoxYearPublished.Enabled = false;
            textBoxQOH.Enabled = false;
        }

        private void textboxenable()
        {
            textBoxAuthorId.Enabled = true;
            textBoxFirstName.Enabled = true;
            textBoxLastName.Enabled = true;
            textBoxProductID.Enabled = true;
            textBoxTitle.Enabled = true;
            textBoxUnitPrice.Enabled = true;
            textBoxPublisher.Enabled = true;
            textBoxYearPublished.Enabled = true;
            textBoxQOH.Enabled = true;
        }

        private void comboBoxSearchOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxSearchOption.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    labelOption.Visible = true;
                    comboBoxFirstOption.Visible = true;
                    textBoxSearchInfo.Visible = false;
                    labelInfo.Text = "Please choose a category!";
                    break;
                case 1:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter an Author ID!";
                    break;
                case 2:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter an Author's First Name!";
                    break;
                case 3:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter an Author's Last Name!";
                    break;
                case 4:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter a ISBN or SDN!";
                    break;
                case 5:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter a product title!";
                    break;
                case 6:
                    labelOption.Visible = false;
                    comboBoxFirstOption.Visible = false;
                    textBoxSearchInfo.Visible = true;
                    labelInfo.Text = "Please enter a Publisher!";
                    break;
                default:
                    break;
            }
        }

        //User section

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

        private void tabControlProductsUser_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int comboxIndex = comboBoxCategory.SelectedIndex;
            if (comboxIndex == -1)
            {
                textboxdisable();
            }
            else if(comboxIndex == 0)
            {
                textboxenable();
                labelISBNSDN.Text = "ISBN";
            }
            else if (comboxIndex == 1)
            {
                textboxenable();
                labelISBNSDN.Text = "SDN";
            }
        }

        private void textBoxQOH_TextChanged(object sender, EventArgs e)
        {
            if (textBoxQOH.Text == "")
            {
                buttonSave.Enabled = false;
            }
            else
            {
                buttonSave.Enabled = true;
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
    }
}
