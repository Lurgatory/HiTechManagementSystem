using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.DataAccess;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.Business
{
    public class Products : Order
    {
        private string category;
        private int authorId;
        private string authorFirstName;
        private string authorLastName;
        private string productId;
        private string productTitle;
        private float unitPrice;
        private int yearPublished;
        private int qoh;
        private string publisher;

        public int AuthorId { get => authorId; set => authorId = value; }
        public string AuthorFirstName { get => authorFirstName; set => authorFirstName = value; }
        public string AuthorLastName { get => authorLastName; set => authorLastName = value; }
        public string ProductTitle { get => productTitle; set => productTitle = value; }
        public int YearPublished { get => yearPublished; set => yearPublished = value; }
        public int Qoh { get => qoh; set => qoh = value; }
        public string ProductId { get => productId; set => productId = value; }
        public string Category { get => category; set => category = value; }
        public float UnitPrice { get => unitPrice; set => unitPrice = value; }
        public string Publisher { get => publisher; set => publisher = value; }

        public void SaveProduct(Products pdt)
        {
            ProductsDA.Save(pdt);
        }

        public List<Products> GetListProducts()
        {
            return ProductsDA.GetAllRecords();
        }

        public void DisplayProducts(ListView listviewP, List<Products> listP)
        {
            listviewP.Items.Clear();
            foreach (Products pdtItem in listP)
            {
                ListViewItem item = new ListViewItem(pdtItem.Category);
                item.SubItems.Add(pdtItem.authorId.ToString());
                item.SubItems.Add(pdtItem.AuthorFirstName);
                item.SubItems.Add(pdtItem.AuthorLastName);
                item.SubItems.Add(pdtItem.ProductId);
                item.SubItems.Add(pdtItem.ProductTitle);
                item.SubItems.Add(pdtItem.UnitPrice.ToString());
                item.SubItems.Add(pdtItem.Publisher);
                item.SubItems.Add(pdtItem.YearPublished.ToString());
                item.SubItems.Add(pdtItem.qoh.ToString());
                listviewP.Items.Add(item);
            }
        }

        public Products searchProductID(string pdtId)
        {
            return ProductsDA.SearchProductId(pdtId);
        }

        public List<Products> searchProducts (string input)
        {
            ListView listvewP = new ListView();
            List<Products> listP = new List<Products>();
            foreach (Products pdtItem in listP)
            {
                ListViewItem item = new ListViewItem(pdtItem.Category);
                item.SubItems.Add(pdtItem.authorId.ToString());
                item.SubItems.Add(pdtItem.AuthorFirstName);
                item.SubItems.Add(pdtItem.AuthorLastName);
                item.SubItems.Add(pdtItem.ProductId);
                item.SubItems.Add(pdtItem.ProductTitle);
                item.SubItems.Add(pdtItem.UnitPrice.ToString());
                item.SubItems.Add(pdtItem.YearPublished.ToString());
                item.SubItems.Add(pdtItem.qoh.ToString());
                listvewP.Items.Add(item);
            }
            return ProductsDA.SearchProduct(input);
        }

        public void UpdateProducts(Products pdt)
        {
            ProductsDA.UpdateRecord(pdt);
        }

        public void DeletRecord(string pdtId)
        {
            ProductsDA.DeletRecord(pdtId);
        }

        public bool IsPdtDuplicated(string pdtid)
        {
            return ProductsDA.IsProductIDDuplicated(pdtid);
        }
    }
}
