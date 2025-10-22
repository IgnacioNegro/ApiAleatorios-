using Ejercicio_Api_2210.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio_Api_2210.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AleatoriosController : ControllerBase
    {
        [HttpGet("numero")]
        public IActionResult GetNumero()
        {
            int numero = Random.Shared.Next(1, 1000000);
            return Ok(numero);
        }

        //Utilice patron singleton y un servicio
        //para obtener una palrba random del servicio español utilizando handspell, sino la otra solucion era utilizar un archivo de bloc de notas pero las palabras serian limitadas.

        [HttpGet("palabra")]        
        public IActionResult GetPalabra([FromServices] IWordProvider words)
          => Ok(words.GetRandomWord());

        [HttpGet("color")]
        public IActionResult GetColor()
        {
            int rgb = Random.Shared.Next(0, 0x1000000);
            string hex = $"#{rgb:X6}"; //formato hexadecimall
            return Ok(hex);

        } 
    }
}