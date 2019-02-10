using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForwardedHeadersMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetIpController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<string> GetIp()
        {
            Console.WriteLine("Getting IP");
            string ipFromContext = HttpContext.Connection.RemoteIpAddress.ToString();
            Console.WriteLine($"Ip from context is: {ipFromContext}");
            string ipFromHeader = "";
            string[] ips = HttpContext.Request.Headers.GetCommaSeparatedValues("X-Forwarded-For");
            Console.WriteLine($"Number of ips from forwarded is: {ips.Length}");
            IPAddress userIP;
            if (ips.Length > 0)
            {
                Console.WriteLine($"First ip from forwarded is: {ips[0]}");
                // the ip can contain port which we should remove
                string[] ipAndPort = ips[0].Split(':');
                //take only the ip which is the first argument in the array
                userIP = IPAddress.Parse(ipAndPort[0]);
                ipFromHeader = userIP.ToString();
            }
             
            return $"ip from context: {ipFromContext} | ip from header: {ipFromHeader}";
        }        
    }
}
