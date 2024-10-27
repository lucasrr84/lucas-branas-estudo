using invoice.src.domain.service;
using Microsoft.AspNetCore.Mvc;

namespace invoice.src.infra.controller;

[ApiController]
[Route("[controller]")]
public class QueueController : ControllerBase
{
    private readonly MessageConsumer _messageConsumer;

    public QueueController(MessageConsumer messageConsumer)
    {
        _messageConsumer = messageConsumer;
    }

    [HttpGet]
    public IActionResult getMessages()
    {
        var messages = _messageConsumer.getMessages();
        return Ok(messages.ToArray());
    }
}