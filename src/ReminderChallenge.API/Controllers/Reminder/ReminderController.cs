using Microsoft.AspNetCore.Mvc;
using ReminderChallenge.API.RequestModels.Reminder;
using ReminderChallenge.Service.Services;

namespace ReminderChallenge.API.Controllers.Reminder;

[Route("[controller]")]
[ApiController]
public class ReminderController : ControllerBase
{
    private readonly IReminderService _reminderService;


    public ReminderController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] ReminderRequest request, CancellationToken cancellationToken)
    {
        var resposne = await _reminderService.CreateReminder(
            request.TypeExpiration,
            request.ExpirationDate,
            request.Description,
            request.CondominiumId,
            cancellationToken);

        return Ok(resposne);
    }

    [HttpPut("{reminderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(
        Guid reminderId,
        [FromBody] ReminderRequest request, CancellationToken cancellationToken)
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


    [HttpGet("{reminderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetById(Guid reminderId, CancellationToken cancellationToken)
    {
        var response = await _reminderService.GetReminderById(reminderId, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _reminderService.GetAllReminders(cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{reminderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(Guid reminderId, CancellationToken cancellationToken)
    {
        await _reminderService.DeleteReminder(reminderId, cancellationToken);

        return NoContent();
    }
}
