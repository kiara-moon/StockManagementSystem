using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model.Model
{
    public class SalesInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<SalesProductInfo> SalesProductInfos { get; set; }
    }
}
