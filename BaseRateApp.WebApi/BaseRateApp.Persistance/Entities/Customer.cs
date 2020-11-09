using System.Collections.Generic;

namespace BaseRateApp.Persistance.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
