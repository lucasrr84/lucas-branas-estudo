using Microsoft.AspNetCore.Mvc;
using src.application.usecase;
using src.infra.http;

namespace src.infra.controller;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IHttpServer _httpServer;
    private readonly ProcessPayment _processPayment;

    public PaymentController(IHttpServer httpServer, ProcessPayment processPayment)
    {

        _httpServer = httpServer;
        _processPayment = processPayment;
        
        _httpServer.register("post", "/process_payment", async (object input) => {
            // Aqui, o input é o corpo da requisicao
            await _processPayment.execute((InputProcessPayment)input);
        });
    }

    [HttpGet("execute")]
    public ActionResult<string> execute()
    {
        return "aqui!!!!2";
    }
}
