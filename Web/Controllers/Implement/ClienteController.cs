using Business.Interfaces;
using Entity.DTOs.ClienteDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implement
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Clientes")]
    public class ClienteController : BaseController<ClienteDto>, IClienteController
    {
        private readonly IClienteBusiness _clienteBusiness;

        public ClienteController(IClienteBusiness clienteBusiness) : base(clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }

        protected override object GetEntityId(ClienteDto entity)
        {
            return entity.Id;
        }

        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateClienteAsync([FromBody] UpdateClienteDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "Los datos del cliente no pueden ser nulos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _clienteBusiness.UpdateClienteAsync(entity);
                return Ok(new { success = result, message = "Cliente actualizado exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("numero/{numeroCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDto>> GetByNumeroClienteAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    return BadRequest(new { message = "El número de cliente no puede estar vacío" });

                var result = await _clienteBusiness.GetByNumeroClienteAsync(numeroCliente);

                if (result == null)
                    return NotFound(new { message = $"No se encontró el cliente con número {numeroCliente}" });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("documento/{numeroDocumento}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDto>> GetByNumeroDocumentoAsync(string numeroDocumento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroDocumento))
                    return BadRequest(new { message = "El número de documento no puede estar vacío" });

                var result = await _clienteBusiness.GetByNumeroDocumentoAsync(numeroDocumento);

                if (result == null)
                    return NotFound(new { message = $"No se encontró el cliente con documento {numeroDocumento}" });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        

        [HttpGet("validar/{numeroCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDto>> ValidarClienteAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    return BadRequest(new { message = "El número de cliente no puede estar vacío" });

                var result = await _clienteBusiness.ValidarClienteAsync(numeroCliente);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("exists/numero/{numeroCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsNumeroClienteAsync(string numeroCliente)
        {
            try
            {
                var result = await _clienteBusiness.ExistsNumeroClienteAsync(numeroCliente);
                return Ok(new { exists = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("exists/documento/{numeroDocumento}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsNumeroDocumentoAsync(string numeroDocumento)
        {
            try
            {
                var result = await _clienteBusiness.ExistsNumeroDocumentoAsync(numeroDocumento);
                return Ok(new { exists = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("exists/email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsEmailAsync(string email)
        {
            try
            {
                var result = await _clienteBusiness.ExistsEmailAsync(email);
                return Ok(new { exists = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}