using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs.ClienteDto;
using System;
using System.Threading.Tasks;

namespace Business.Implement
{
    public class ClienteBusiness : BaseBusiness<ClienteDto>, IClienteBusiness
    {
        private readonly IClienteData _clienteData;

        public ClienteBusiness(IClienteData clienteData) : base(clienteData)
        {
            _clienteData = clienteData;
        }

        public async Task<bool> UpdateClienteAsync(UpdateClienteDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de actualización no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                await ValidateUpdateClienteAsync(entity);

                return await _clienteData.UpdateClienteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar cliente: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteLogicalClienteAsync(DeleteLogicalClienteDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de eliminación lógica no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                return await _clienteData.DeleteLogicalClienteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica del cliente: {ex.Message}", ex);
            }
        }

        public async Task<ClienteDto> GetByNumeroClienteAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    throw new ArgumentException("El número de cliente no puede estar vacío", nameof(numeroCliente));

                return await _clienteData.GetByNumeroClienteAsync(numeroCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cliente por número: {ex.Message}", ex);
            }
        }

        public async Task<ClienteDto> GetByNumeroDocumentoAsync(string numeroDocumento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroDocumento))
                    throw new ArgumentException("El número de documento no puede estar vacío", nameof(numeroDocumento));

                return await _clienteData.GetByNumeroDocumentoAsync(numeroDocumento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cliente por documento: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExistsNumeroClienteAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    return false;

                return await _clienteData.ExistsNumeroClienteAsync(numeroCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de número de cliente: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroDocumento))
                    return false;

                return await _clienteData.ExistsNumeroDocumentoAsync(numeroDocumento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de número de documento: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return await _clienteData.ExistsEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de email: {ex.Message}", ex);
            }
        }

        public async Task<ClienteDto> ValidarClienteAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    throw new ArgumentException("El número de cliente es requerido", nameof(numeroCliente));

                var cliente = await _clienteData.GetByNumeroClienteAsync(numeroCliente);

                if (cliente == null)
                    throw new InvalidOperationException($"No se encontró el cliente con número {numeroCliente}");

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar cliente: {ex.Message}", ex);
            }
        }

        protected override async Task ValidateEntityAsync(ClienteDto entity)
        {
            if (string.IsNullOrWhiteSpace(entity.NumeroCliente))
                throw new ArgumentException("El número de cliente es requerido");

            if (string.IsNullOrWhiteSpace(entity.NumeroDocumento))
                throw new ArgumentException("El número de documento es requerido");

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
                throw new ArgumentException("El nombre completo del cliente es requerido");

            if (string.IsNullOrWhiteSpace(entity.Email))
                throw new ArgumentException("El email del cliente es requerido");

            if (!IsValidEmail(entity.Email))
                throw new ArgumentException("El formato del email no es válido");

            // Validar unicidad
            if (await _clienteData.ExistsNumeroClienteAsync(entity.NumeroCliente))
                throw new InvalidOperationException($"Ya existe un cliente con el número {entity.NumeroCliente}");

            if (await _clienteData.ExistsNumeroDocumentoAsync(entity.NumeroDocumento))
                throw new InvalidOperationException($"Ya existe un cliente con el documento {entity.NumeroDocumento}");

            if (await _clienteData.ExistsEmailAsync(entity.Email))
                throw new InvalidOperationException($"Ya existe un cliente con el email {entity.Email}");

            await Task.CompletedTask;
        }

        private async Task ValidateUpdateClienteAsync(UpdateClienteDto entity)
        {
            if (!string.IsNullOrEmpty(entity.Email) && !IsValidEmail(entity.Email))
                throw new ArgumentException("El formato del email no es válido");

            await Task.CompletedTask;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}