using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

[ApiController]
[ApiVersion("2.0")]
[Route("api/helloworld")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class HelloWorldV2Controller : ControllerBase
{
    [HttpGet]
    public string Get() => "Hello world! Version 2";
}