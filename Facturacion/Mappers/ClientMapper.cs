using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Mappers
{
  public class ClientMapper : Profile
  {
    public ClientMapper()
    {
      CreateMap<Client, ClientDto>();

      CreateMap<CreateClientDto, Client>();
    }
  }
}
