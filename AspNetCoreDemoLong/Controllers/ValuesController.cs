using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoLong.Controllers
{
    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }
    public class RGBColor
    {
        public RGBColor(int d, int dd, int ddd)
        {

        }
        public static RGBColor FromRainbow(Rainbow colorBand) =>
    colorBand switch
    {
        Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
        Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
        Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
        Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
        Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
        Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
        Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
        _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
    };

        public RGBColor F(Rainbow rainbow) => rainbow switch
        {
            Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(rainbow)),

        };

        static int WriteLinesToFile(IEnumerable<string> lines)
        {
            string[] s = new string[]{

            "The",      // 0                   ^9
    "quick",    
    "brown",    // 2                   ^7
    "fox",      // 3                   ^6
    "jumped",   // 4                   ^5
    "over",     // 5                   ^4
    "the",      // 6                   ^3
    "lazy",     // 7                   ^2
    "dog"    };
            var aa = s[^1];
            var aas = s[^1..^4];
            var aaaa = s[0..8];
            Range phrase = ^1..^4;
            var aasss = s[phrase];

            using var files = new System.IO.StreamWriter("");
            using var file = new System.IO.StreamWriter("WriteLines2.txt");
            int skippedLines = 0;
            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            // Notice how skippedLines is in scope here.
            return skippedLines;
            // file is disposed here
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
