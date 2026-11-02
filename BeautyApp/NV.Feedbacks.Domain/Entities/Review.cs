using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NV.Buautician.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace NV.Feedbacks.Domain.Entities
{
    public class Review : Entity
    {
        public Review(
            string id,
            string userId,
            string beauticianId,
            int rating,
            string comments
            ): base(id)
        {
            UserId = userId;
            BeauticianId = beauticianId;
            Rating = rating;
            Comments = comments;

            
        }

        public string UserId { get; set; }  
        public virtual ApplicationUser User { get; set; }

        public string BeauticianId { get; set; }

        public virtual Beauticians Beauticians { get; set; }

        public int Rating {get; set; }
        public string Comments { get; set; }
    }
}
