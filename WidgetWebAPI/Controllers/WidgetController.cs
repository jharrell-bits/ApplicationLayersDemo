using Microsoft.AspNetCore.Mvc;
using WidgetWebAPI.BusinessLogic;
using WidgetWebAPI.Models;

namespace WidgetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetController : ControllerBase
    {
        private readonly ILogger<WidgetController> _logger;
        private readonly IWidgetBusinessLogic _widgetBusinessLogic;

        public WidgetController(ILogger<WidgetController> logger, IWidgetBusinessLogic widgetBusinessLogic)
        {
            _logger = logger;
            _widgetBusinessLogic = widgetBusinessLogic;
        }

        [HttpGet()]
        [ProducesResponseType<IEnumerable<Widget>>(StatusCodes.Status200OK)]
        public IEnumerable<Widget> GetWidgets()
        {
            return _widgetBusinessLogic.GetWidgets();
        }

        [HttpGet("{id}")]
        [ProducesResponseType<Widget>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWidget(int id)
        {
            var result = await _widgetBusinessLogic.GetWidget(id); 

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Widget>> UpsertWidget(Widget widget)
        {
            var widgetToUpdate = await _widgetBusinessLogic.GetWidget(widget.Id);
            
            await _widgetBusinessLogic.UpsertWidget(widget);

            if (widgetToUpdate != null)
            {
                var widgetUpdated = await _widgetBusinessLogic.GetWidget(widget.Id);

                if (widgetUpdated != null)
                {
                    return CreatedAtAction(nameof(GetWidget), new { id = widgetUpdated.Id }, widgetUpdated);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWidget(int id)
        {
            var widgetToDelete = await _widgetBusinessLogic.GetWidget(id);

            if (widgetToDelete != null)
            {
                await _widgetBusinessLogic.DeleteWidget(id);

                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("{id}/Movement")]
        [ProducesResponseType<Widget>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Widget>> MoveWidget(int id, WidgetMovement widgetMovement)
        {
            Widget? result = null;

            if (widgetMovement.MovementType == WidgetMovement.TypeOfMove.Up)
            {
                result = await _widgetBusinessLogic.MoveWidgetUp(id);
            }
            else if (widgetMovement.MovementType == WidgetMovement.TypeOfMove.Down)
            {
                result = await _widgetBusinessLogic.MoveWidgetDown(id);
            }

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
