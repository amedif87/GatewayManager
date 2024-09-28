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
using System.Net;

namespace GatewayManager.Infrastructure.Services
{
    public class GatewayService: IGatewayService
    {
        private readonly IMapper _mapper;
        private readonly IGatewayRepository _repository;
        public GatewayService(IGatewayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GatewayDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
                return _mapper.Map<List<GatewayDTO>>(result.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GatewayDTO> GetById(long id)
        {
            try
            {
                var result = _mapper.Map<GatewayDTO>(await _repository.Find(id));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GatewayDTO?> Create(GatewayDTO item)
        {
            try
            {
                var validGateway = await ValidateGateway(item, "Create");
                if (validGateway != "OK")
                {
                    throw new Exception(validGateway);
                }
                var result = await _repository.Create(_mapper.Map<Gateway>(item));
                return _mapper.Map<GatewayDTO?>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(GatewayDTO item)
        {
            try
            {
                var validGateway = await ValidateGateway(item, "Update");
                if (validGateway != "OK")
                {
                    throw new Exception(validGateway);
                }
                var existingGateway = await _repository.Find(item.Id);
                if (existingGateway == null)
                {
                    throw new Exception("Gateway not found");
                }
                existingGateway.SerialNumber = item.SerialNumber;
                existingGateway.Name = item.Name;
                existingGateway.IPv4Address = item.IPv4Address;
                existingGateway.DateUpdated = DateTime.Now;
                await _repository.Update(existingGateway);
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

        public async Task<PaginationDTO<GatewayDTO>> GetPage(GatewayFilter conditions)
        {
            try
            {
                var where = ConditionBuilder.True<Gateway>();

                if (!string.IsNullOrEmpty(conditions.FilterBySerialNumber))
                {
                    where = where.And(s => s.SerialNumber.Contains(conditions.FilterBySerialNumber));
                }

                if (!string.IsNullOrEmpty(conditions.FilterByName))
                {
                    where = where.And(s => s.Name.Contains(conditions.FilterByName));
                }

                if (!string.IsNullOrEmpty(conditions.FilterByIPv4Address))
                {
                    where = where.And(s => s.IPv4Address.Contains(conditions.FilterByIPv4Address));
                }

                Expression<Func<Gateway, string>> order;
                SortOrder sort;
                switch (conditions.SortBy)
                {
                    case "serial_number":
                        order = item => item.SerialNumber;
                        break;
                    case "name":
                        order = item => item.Name;
                        break;
                    case "ipv4_address":
                        order = item => item.IPv4Address;
                        break;
                    default:
                        order = item => item.SerialNumber;
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

                return new PaginationDTO<GatewayDTO>
                {
                    Items = _mapper.Map<IEnumerable<GatewayDTO>>(result.Item1),
                    TotalCount = result.Item2
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ValidateGateway(GatewayDTO item, string op)
        {
            if (IPAddress.TryParse(item.IPv4Address, out var ip) && ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return("Invalid IPV4 format");
            }

            var countSerNumb = 0;
            if (op == "Create")
            {
                countSerNumb = await _repository.Count(x => x.SerialNumber == item.SerialNumber);
            }
            else
            {
                countSerNumb = await _repository.Count(x => x.SerialNumber == item.SerialNumber && x.Id != item.Id);
            }
            if (countSerNumb > 0)
            {
                return("Serial number already exist");
            }
            return "OK";
        }
    }
}
