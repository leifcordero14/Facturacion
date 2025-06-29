using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;

namespace Facturacion.Services
{
  public class SellerService(IRepository<Seller> repository, IMapper mapper) : IService<SellerDto, CreateSellerDto, UpdateSellerDto>
  {
    private readonly IRepository<Seller> _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<SellerDto>> GetAll()
    {
      var sellers = await _repository.GetAll();
      var sellerDtos = _mapper.Map<List<SellerDto>>(sellers);
      return sellerDtos;
    }
    public async Task<SellerDto?> GetById(int id)
    {
      var seller = await _repository.GetById(id);
      if (seller == null) return null;
      var sellerDto = _mapper.Map<SellerDto>(seller);
      return sellerDto;
    }
    public async Task<SellerDto> Create(CreateSellerDto createSellerDto)
    {
      var seller = _mapper.Map<Seller>(createSellerDto);
      await _repository.Create(seller);
      await _repository.Save();
      var sellerDto = _mapper.Map<SellerDto>(seller);
      return sellerDto;
    }
    public async Task Update(int id, UpdateSellerDto updateSellerDto)
    {
      var seller = await _repository.GetById(id);
      if (seller == null) throw new Exception("No se encontró el vendedor");

      seller.FirstName = updateSellerDto.FirstName;
      seller.LastName = updateSellerDto.LastName;
      seller.CommissionPercentage = updateSellerDto.CommissionPercentage;
      seller.IsActive = updateSellerDto.IsActive;

      _repository.Update(seller);
      await _repository.Save();
      return;
    }
    public async Task Delete(int id)
    {
      var seller = await _repository.GetById(id);
      if (seller == null) throw new Exception("No se encontró el vendedor");
      _repository.Delete(seller);
      await _repository.Save();
      return;
    }
  }
}
