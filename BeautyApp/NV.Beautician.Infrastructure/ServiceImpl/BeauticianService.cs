using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Beautician.Infrastructure.ServiceImpl
{
    public class BeauticianService:IBeauticianService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BeauticianService(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<Result<AddBeauticianResponse>> Add(AddBeauticianCommand addBeauticianCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    string newId = Guid.NewGuid().ToString();
                    var beauticiansData = new Beauticians
                    (
                            newId,
                            addBeauticianCommand.experienceYear,
                            addBeauticianCommand.skills,
                            addBeauticianCommand.userId
                    );

                    await _unitOfWork.BaseRepository<Beauticians>().AddAsync(beauticiansData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddBeauticianResponse>(beauticiansData);
                    return Result<AddBeauticianResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Beauticians ", ex);

                }
            }

        }

        public async Task<Result<PagedResult<GetAllBeauticianByQueryResponse>>> GetAllBeautician(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var beautician = await _unitOfWork.BaseRepository<Beauticians>().GetAllAsyncWithPagination();
                var allBeauticianResponse = _mapper.Map<PagedResult<GetAllBeauticianByQueryResponse>>(beautician);
                return Result<PagedResult<GetAllBeauticianByQueryResponse>>.Success(allBeauticianResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all Beautician ", ex);
            }
        }

        public async Task<Result<GetBeauticianByIdQueryResponse>> GetBeauticiansById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
               
                var beauticians = await _unitOfWork.BaseRepository<Beauticians>().GetByGuIdAsync(id);
                var beauticiansResponse = _mapper.Map<GetBeauticianByIdQueryResponse>(beauticians);      
                return Result<GetBeauticianByIdQueryResponse>.Success(beauticiansResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching beauticians by using Id", ex);
            }

        }

        public async Task<Result<UpdateBeauticianResponse>> Update(string id, UpdateBeauticianCommand updateBeauticianCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateBeauticianResponse>.Failure("NotFound", "Please provide valid beautician Id");
                    }

                    var beauticianToBeUpdated = await _unitOfWork.BaseRepository<Beauticians>().GetByGuIdAsync(id);
                    if (beauticianToBeUpdated is null)
                    {
                        return Result<UpdateBeauticianResponse>.Failure("NotFound", "Beauticians are not Found");
                    }
                 
                    _mapper.Map(updateBeauticianCommand, beauticianToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateBeauticianResponse
                        (

                          id,
                          beauticianToBeUpdated.ExperienceYear,
                          beauticianToBeUpdated.Skills,
                          beauticianToBeUpdated.UserId
                        );

                    return Result<UpdateBeauticianResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Beauticians", ex);
                }

            }

        }
    }
}


