using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;

namespace Facturacion.Services
{
  public class ClientService(IRepository<Client> repository, IMapper mapper) : IService<ClientDto, CreateClientDto, UpdateClientDto>
  {
    private readonly IRepository<Client> _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<ClientDto>> GetAll()
    {
      var clients = await _repository.GetAll();
      var clientDtos = _mapper.Map<List<ClientDto>>(clients);
      return clientDtos;
    }
    public async Task<ClientDto?> GetById(int id)
    {
      var client = await _repository.GetById(id);
      if (client == null) return null;
      var clientDto = _mapper.Map<ClientDto>(client);
      return clientDto;
    }
    public async Task<ClientDto> Create(CreateClientDto createClientDto)
    {
      var client = _mapper.Map<Client>(createClientDto);
      await _repository.Create(client);
      await _repository.Save();
      var clientDto = _mapper.Map<ClientDto>(client);
      return clientDto;
    }
    public async Task Update(int id, UpdateClientDto updateClientDto)
    {
      var client = await _repository.GetById(id);
      if (client == null) throw new Exception("No se encontró el cliente");

      client.BussinesName = updateClientDto.BussinesName;
      client.IdentificationNumber = updateClientDto.IdentificationNumber;
      client.LedgerAccount = updateClientDto.LedgerAccount;
      client.IsActive = updateClientDto.IsActive;

      _repository.Update(client);
      await _repository.Save();
      return;
    }
    public async Task Delete(int id)
    {
      var client = await _repository.GetById(id);
      if (client == null) throw new Exception("No se encontró el cliente");
      _repository.Delete(client);
      await _repository.Save();
      return;
    }
  }
}
