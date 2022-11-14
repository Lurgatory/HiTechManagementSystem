using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.DataAccess;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.Business
{
     public class Order
    {
        private int orderID;
        private string clientInfo;
        private string productInfo;
        private string contactMethod;
        private string contactNumber;
        private DateTime requireDate;
        private DateTime shipDate;
        private int orderQuantity;
        private string status;

        public int OrderID { get => orderID; set => orderID = value; }
        public string ClientInfo { get => clientInfo; set => clientInfo = value; }
        public string ProductInfo { get => productInfo; set => productInfo = value; }
        public string ContactMethod { get => contactMethod; set => contactMethod = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public DateTime RequireDate { get => requireDate; set => requireDate = value; }
        public int OrderQuantity { get => orderQuantity; set => orderQuantity = value; }
        public DateTime ShipDate { get => shipDate; set => shipDate = value; }
        public string Status { get => status; set => status = value; }

        public void SaveOrder(Order odr)
        {
            OrderDA.Save(odr);
        }

        public List<Order> GetAllRecords()
        {
            return OrderDA.GetAllRecords();
        }

        public void DisplayList(ListView listviewO, List<Order> listO)
        {
            listviewO.Items.Clear();
            foreach (Order odrItem in listO)
            {
                ListViewItem item = new ListViewItem(odrItem.ClientInfo);
                item.SubItems.Add(odrItem.ProductInfo);
                item.SubItems.Add(odrItem.ContactMethod);
                item.SubItems.Add(odrItem.ContactNumber);
                item.SubItems.Add(odrItem.OrderID.ToString());
                item.SubItems.Add(odrItem.OrderQuantity.ToString());
                item.SubItems.Add(odrItem.RequireDate.ToShortDateString());
                item.SubItems.Add(odrItem.ShipDate.ToShortDateString());
                item.SubItems.Add(odrItem.Status);
                listviewO.Items.Add(item);
            }
        }

        public Order SearchOrderID(int odrId)
        {
            return OrderDA.Search(odrId);
        }

        public List<Order> SearchOrderOption(string input)
        {
            return OrderDA.SearchOption(input);
        }

        public void UpdateOrderRecord(Order odr)
        {
            OrderDA.UpdateRecord(odr);
        }

        public void CancelOrderRecord(Order odr)
        {
            OrderDA.CancelRecord(odr);
        }

        public bool IsOrderDuplicated(int odrid)
        {
            return OrderDA.IsDuplicated(odrid);
        }
    }
}
