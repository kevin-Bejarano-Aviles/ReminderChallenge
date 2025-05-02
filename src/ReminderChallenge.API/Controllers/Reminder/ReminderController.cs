using Microsoft.AspNetCore.Mvc;
using ReminderChallenge.API.RequestModels.Reminder;
using ReminderChallenge.Service.Dtos;
using ReminderChallenge.Service.Services.RemindersService;

namespace ReminderChallenge.API.Controllers.Reminder;

[Route("[controller]")]
[ApiController]
public class ReminderController : ControllerBase
{
    private readonly IReminderService _reminderService;
    private readonly ILogger<ReminderController> _logger;

    public ReminderController(IReminderService reminderService, ILogger<ReminderController> logger)
    {
        _reminderService = reminderService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] ReminderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var resposne = await _reminderService.CreateReminder(
            request.TypeExpiration,
            request.ExpirationDate,
            request.Description,
            request.CondominiumId,
            cancellationToken);

            return Ok(resposne);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{reminderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(
        Guid reminderId,
        [FromBody] ReminderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _reminderService.UpdateReminder(
            reminderId,
            request.TypeExpiration,
            request.ExpirationDate,
            request.Description,
            request.CondominiumId,
            cancellationToken
            );

            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
        
    }


    [HttpGet("{reminderId}")]
    [ProducesResponseType(typeof(ReminderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetById(Guid reminderId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _reminderService.GetReminderById(reminderId, cancellationToken);

            return Ok(response);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ReminderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _reminderService.GetAllReminders(cancellationToken);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
        
    }

    [HttpDelete("{reminderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(Guid reminderId, CancellationToken cancellationToken)
    {
        try
        {
            await _reminderService.DeleteReminder(reminderId, cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
        
    }
}
