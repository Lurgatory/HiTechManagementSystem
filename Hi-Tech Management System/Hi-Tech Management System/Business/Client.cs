using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.DataAccess;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.Business
{
    public class Client : Order
    {
        private int clientId;
        private string clientName;
        private string clientAddress;
        private string clientCity;
        private string clientPost;
        private string clientPhoneNumber;
        private string faxNumber;
        private string creditLimit;
        private int bankAccount;

        public int ClientId { get => clientId; set => clientId = value; }
        public string ClientName { get => clientName; set => clientName = value; }        
        public string ClientCity { get => clientCity; set => clientCity = value; }
        public string ClientPost { get => clientPost; set => clientPost = value; }
        public string FaxNumber { get => faxNumber; set => faxNumber = value; }
        public string CreditLimit { get => creditLimit; set => creditLimit = value; }
        public string ClientAddress { get => clientAddress; set => clientAddress = value; }
        public string ClientPhoneNumber { get => clientPhoneNumber; set => clientPhoneNumber = value; }
        public int BankAccount { get => bankAccount; set => bankAccount = value; }
        

        public void SaveClient(Client clt)
        {
            ClientDA.Save(clt);
        }

        public List<Client> GetListClient()
        {
            return ClientDA.GetAllClientRecord();
        }

        public void DisplayClientList(ListView listviewC, List<Client> listC)
        {
            listviewC.Items.Clear();
            foreach (Client cltItem in listC)
            {
                ListViewItem item = new ListViewItem(cltItem.ClientId.ToString());
                item.SubItems.Add(cltItem.ClientName);
                item.SubItems.Add(cltItem.ClientAddress);
                item.SubItems.Add(cltItem.ClientCity);
                item.SubItems.Add(cltItem.ClientPost);
                item.SubItems.Add(cltItem.ClientPhoneNumber);
                item.SubItems.Add(cltItem.FaxNumber);
                item.SubItems.Add(cltItem.BankAccount.ToString());
                item.SubItems.Add(cltItem.CreditLimit);              
                listviewC.Items.Add(item);
            }
        }

        public void UpdateClient(Client clt)
        {
            ClientDA.UpdateRecord(clt);
        }

        public Client searchClient(int cltId)
        {
            return ClientDA.Search(cltId);
        }

        public List<Client> searchClientName(string input)
        {
            return ClientDA.Searchname(input);
        }

        public void DeleteClient (int id)
        {
            ClientDA.DeleteRecord(id);
        }

        public bool IsDuplicated(int cltid)
        {
            return ClientDA.IsDuplicated(cltid);
        }
    }
}
