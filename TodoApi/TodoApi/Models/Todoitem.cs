using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace TodoApi.Models
{
    [ApiController]
    [Route("[Todoitem]")]
    public class Todoitem : ControllerBase
    {
        private static readonly string[] mat = new string[]
        {
            "matsedeln"
        };

        
        

    }
}
