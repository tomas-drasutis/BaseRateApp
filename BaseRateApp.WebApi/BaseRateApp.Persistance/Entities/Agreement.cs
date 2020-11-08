using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Persistance.Entities
{
    public class Agreement : BaseEntity
    {
        public decimal Amount { get; set; }
        public decimal Margin { get; set; }

        public string BaseRateCode { get; set; } //Should be enum
        public int AgreementDuration { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
