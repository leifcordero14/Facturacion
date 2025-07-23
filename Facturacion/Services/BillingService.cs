using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;

namespace Facturacion.Services
{
  public class BillingService(
    IGetPostRepository<Billing> repository, 
    IFilterRepository<Billing, BillingFilterDto> filterRepository, 
    IMapper mapper) : ICreateReadService<BillingDto, CreateBillingDto>, IFilterService<BillingDto, BillingFilterDto>
  {
    private readonly IGetPostRepository<Billing> _repository = repository;
    private readonly IFilterRepository<Billing, BillingFilterDto> _filterRepository = filterRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<BillingDto>> GetAll()
    {
      var billings = await _repository.GetAll();
      var billingDtos = _mapper.Map<List<BillingDto>>(billings);
      return billingDtos;
    }
    public async Task<BillingDto?> GetById(int id)
    {
      var billing = await _repository.GetById(id);
      if (billing == null) return null;
      var billingDto = _mapper.Map<BillingDto>(billing);
      return billingDto;
    }
    public async Task<IEnumerable<BillingDto>> GetByFilter(BillingFilterDto filter)
    {
      var billings = await _filterRepository.GetByFilter(filter);
      var billingDtos = _mapper.Map<List<BillingDto>>(billings);
      return billingDtos;
    }
    public async Task<BillingDto> Create(CreateBillingDto createBillingDto)
    {
      var billing = _mapper.Map<Billing>(createBillingDto);
      await _repository.Create(billing);
      await _repository.Save();
      var billingDto = _mapper.Map<BillingDto>(billing);
      return billingDto;
    }
  }
}
