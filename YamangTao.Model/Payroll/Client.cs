using System.Collections.Generic;
namespace YamangTao.Model.Payroll
{
    public class Client
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ClientType { get; set; }
        public string ContactName { get; set; }
        public string Location { get; set; }

        public int? ParentClientId { get; set; }
        public Client ParentClient { get; set; }
        // Child Units
        public virtual IEnumerable<Client> ClientChildren { get; set; }
        public virtual IEnumerable<ClientPosition> Positions { get; set; }
        
    }
}