using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Domain.Primitive;

namespace NV.Buautician.Domain.Entities
{
    public class BeautyServices : Entity
    {
        public BeautyServices(
            string id,
            string name,
            decimal price,
            int durationInMinutes
            ): base(id)
        {
            Name = name;
            Price = price;
            DurationInMinutes = durationInMinutes;
            BeauticianServices = new List<BeauticianServices>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public virtual ICollection<BeauticianServices> BeauticianServices { get; set; }
    }
}
