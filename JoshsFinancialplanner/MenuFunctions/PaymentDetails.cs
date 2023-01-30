using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsFinancialplanner.MenuFunctions
{
    public class PaymentDetails : IEnumerable
    {
        public string Month { get; set; }
        public string PaymentName { get; set; }
        public string DueDate { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }

    }

    public interface IEnumerable
    {
    }

    /*
    public PaymentDetails ConvertPaymentEntriesToClass(List<PaymentDetails> paymentEntries)
    {
        
    }
    */
}
