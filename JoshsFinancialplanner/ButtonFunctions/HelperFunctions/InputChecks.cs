using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsFinancialplanner.ButtonFunctions.HelperFunctions
{
    internal class InputChecks
    {
        // This function is here to prevent long strings being entered in to the due date
        // and Payment name boxes
        public bool isPaymentNameShort(string input)
        {
            if(input.Length > 30)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public bool isDueDateInputShort(string input)
        {
            if (input.Length > 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
