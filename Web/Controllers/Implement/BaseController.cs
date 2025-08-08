using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implement
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase, IBaseController<T> where T : class
    {
        protected readonly IBaseBusiness<T> _baseBusiness;

        protected BaseController(IBaseBusiness<T> baseBusiness)
        {
            _baseBusiness = baseBusiness;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var result = await _baseBusiness.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("all-including-inactive")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public virtual async Task<ActionResult<IEnumerable<T>>> GetAllIncludingInactiveAsync()
{
    try
    {
        var result = await _baseBusiness.GetAllIncludingInactiveAsync();
        return Ok(result);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            new { message = "Error interno del servidor", details = ex.Message });
    }
}


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<T>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID debe ser mayor que cero" });

                var result = await _baseBusiness.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new { message = $"No se encontró el registro con ID {id}" });

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<T>> CreateAsync([FromBody] T entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "La entidad no puede ser nula" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _baseBusiness.CreateAsync(entity);

                var entityId = GetEntityId(result);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                return Created($"/api/{controllerName}/{entityId}", result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<T>> UpdateAsync([FromBody] T entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "La entidad no puede ser nula" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _baseBusiness.UpdateAsync(entity);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID debe ser mayor que cero" });

                var result = await _baseBusiness.DeleteAsync(id);
                return Ok(new { success = result, message = "Registro eliminado exitosamente" });
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

        [HttpPatch("delete-logical/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<bool>> DeleteLogicalAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID debe ser mayor que cero" });

                var result = await _baseBusiness.DeleteLogicalAsync(id);
                return Ok(new { success = result, message = "Eliminación lógica realizada exitosamente" });
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

        protected abstract object GetEntityId(T entity);
    }
}