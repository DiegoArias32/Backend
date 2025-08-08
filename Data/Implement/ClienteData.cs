using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.DTOs.ClienteDto;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public class ClienteData : BaseData<Cliente, ClienteDto>, IClienteData
    {
        private readonly ApplicationDbContext _dbContext;

        protected override DbSet<Cliente> DbSet => _dbContext.Clientes;

        public ClienteData(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _dbContext = context;
        }

        public async Task<bool> UpdateClienteAsync(UpdateClienteDto entity)
        {
            var existingEntity = await _dbContext.Clientes.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicalClienteAsync(DeleteLogicalClienteDto entity)
        {
            var existingEntity = await _dbContext.Clientes.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            existingEntity.IsActive = false;
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ClienteDto> GetByNumeroClienteAsync(string numeroCliente)
        {
            var entity = await _dbContext.Clientes
                .Where(c => c.NumeroCliente == numeroCliente && c.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<ClienteDto>(entity);
        }

        public async Task<ClienteDto> GetByNumeroDocumentoAsync(string numeroDocumento)
        {
            var entity = await _dbContext.Clientes
                .Where(c => c.NumeroDocumento == numeroDocumento && c.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<ClienteDto>(entity);
        }

        public async Task<bool> ExistsNumeroClienteAsync(string numeroCliente)
        {
            return await _dbContext.Clientes
                .AnyAsync(c => c.NumeroCliente == numeroCliente && c.IsActive);
        }

        public async Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento)
        {
            return await _dbContext.Clientes
                .AnyAsync(c => c.NumeroDocumento == numeroDocumento && c.IsActive);
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _dbContext.Clientes
                .AnyAsync(c => c.Email == email && c.IsActive);
        }
    }
}