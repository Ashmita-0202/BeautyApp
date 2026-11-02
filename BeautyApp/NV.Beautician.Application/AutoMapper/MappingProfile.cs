using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Beautician
            CreateMap<GetAllBeauticianByQueryResponse, Beauticians>().ReverseMap();
            CreateMap<PagedResult<Beauticians>, PagedResult<GetAllBeauticianByQueryResponse>>().ReverseMap();
            CreateMap<AddBeauticianResponse, Beauticians>().ReverseMap();
            CreateMap<AddBeauticianCommand, Beauticians>().ReverseMap();
            CreateMap<GetBeauticianByIdQueryResponse, Beauticians>().ReverseMap();
            CreateMap<UpdateBeauticianCommand, Beauticians>().ReverseMap();

            #endregion
        }

    }
}

