using Ak.Template.DataClasses;
using Ak.Template.Logic.Contract;
using Ak.Template.Logic.Contract.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Ak.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MitarbeiterController : ControllerBase
    {
        private readonly ILogger<MitarbeiterController> _logger;
        private readonly IMitarbeiterManager _mitarbeiterManager;

        public MitarbeiterController(ILogger<MitarbeiterController> logger, IMitarbeiterManager mitarbeiterManager)
        {
            _logger = logger;
            _mitarbeiterManager = mitarbeiterManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _mitarbeiterManager.GetAll();

            if (!result.Any())
            {
                return NotFound("Keine Einträge gefunden.");
            }

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _mitarbeiterManager.GetById(id);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning(e, $"Hier ein sinnvolle Fehlerbeschreibung einfügen...");
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Mitarbeiter mitarbeiter)
        {
            var result = _mitarbeiterManager.Create(mitarbeiter);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] Mitarbeiter modifiedMitarbeiter)
        {
            try
            {
                var result = _mitarbeiterManager.Modify(id, modifiedMitarbeiter);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning(e, $"Hier ein sinnvolle Fehlerbeschreibung einfügen...");
                return NotFound();
            }            
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _mitarbeiterManager.Remove(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning(e, $"Hier ein sinnvolle Fehlerbeschreibung einfügen...");
                return NotFound();
            }
        }

    }
}
