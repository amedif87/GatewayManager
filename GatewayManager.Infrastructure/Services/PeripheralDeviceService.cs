using AutoMapper;
using GatewayManager.Domain.IServices;
using GatewayManager.Domain.IRepositories;
using GatewayManager.Domain.Pagination;
using GatewayManager.Domain.Pagination.Filters;
using GatewayManager.Domain.Entities;
using GatewayManager.Domain.DTOs;
using GatewayManager.Infrastructure.Persistance.Shared;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Linq;

namespace GatewayManager.Infrastructure.Services
{
    public class PeripheralDeviceService : IPeripheralDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IPeripheralDeviceRepository _repository;
        private readonly IGatewayRepository _repositoryGateway;
        public PeripheralDeviceService(IPeripheralDeviceRepository repository, IGatewayRepository repositoryGateway, IMapper mapper)
        {
            _repository = repository;
            _repositoryGateway = repositoryGateway;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PeripheralDeviceDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
                return _mapper.Map<List<PeripheralDeviceDTO>>(result.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PeripheralDeviceDTO> GetById(long id)
        {
            try
            {
                var result = _mapper.Map<PeripheralDeviceDTO>(await _repository.Find(id));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PeripheralDeviceDTO?> Create(PeripheralDeviceDTO item)
        {
            try
            {
                var gateway = await _repositoryGateway.Find(item.GatewayId);
                if (gateway.PeripheralDevices.Count >= 10)
                {
                    throw new Exception("No more that 10 peripheral devices are allowed");
                }
                
                var countUID = await _repository.Count(x => x.UID == item.UID);                
                if (countUID > 0)
                {
                    throw new Exception("UID already exist");
                }

                var result = await _repository.Create(_mapper.Map<PeripheralDevice>(item));
                return _mapper.Map<PeripheralDeviceDTO?>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(PeripheralDeviceDTO item)
        {
            try
            {
                var countSerNumb = await _repository.Count(x => x.UID == item.UID && x.Id != item.Id);              
                if (countSerNumb > 0)
                {
                    throw new Exception("UID already exist");
                }

                var existingDevice = await _repository.Find(item.Id);
                if (existingDevice == null)
                {
                    throw new Exception("Gateway not found");
                }
                if (!Enum.TryParse(item.Status, true, out DeviceStatus parsedStatus))
                {
                    throw new Exception("Invalid device status");
                }
                existingDevice.Vendor = item.Vendor;
                existingDevice.UID = item.UID;
                existingDevice.Status = parsedStatus;
                existingDevice.DateUpdated = DateTime.Now;
                await _repository.Update(existingDevice);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaginationDTO<PeripheralDeviceDTO>> GetPage(PeripheralDeviceFilter conditions)
        {
            try
            {
                var where = ConditionBuilder.True<PeripheralDevice>();

                if (conditions.FilterByUID != null)
                {
                    where = where.And(s => s.UID.Equals(conditions.FilterByUID));
                }

                if (!string.IsNullOrEmpty(conditions.FilterByVendor))
                {
                    where = where.And(s => s.Vendor.Contains(conditions.FilterByVendor));
                }

                /*if (conditions.FilterByStatus.HasValue)
                {
                    where = where.And(s => s.Status == conditions.FilterByStatus);
                }*/

                Expression<Func<PeripheralDevice, string>> order;
                SortOrder sort;
                switch (conditions.SortBy)
                {
                    case "uid":
                        order = item => item.UID.ToString();
                        break;
                    case "vendor":
                        order = item => item.Vendor;
                        break;
                    /*case "iscompleted":
                        order = item => item.IPv4Address;
                        break;*/
                    default:
                        order = item => item.UID.ToString();
                        break;
                }

                switch (conditions.OrderBy)
                {
                    case "ASC":
                        sort = SortOrder.Ascending;
                        break;
                    default:
                        sort = SortOrder.Descending;
                        break;
                }

                var result = await _repository.GetPage(conditions, where, order, sort);

                return new PaginationDTO<PeripheralDeviceDTO>
                {
                    Items = _mapper.Map<IEnumerable<PeripheralDeviceDTO>>(result.Item1),
                    TotalCount = result.Item2
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
