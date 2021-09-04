using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
   

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }



        [HttpGet("sub/{firstNumber1}/{secondNumber2}")]
        public IActionResult  Subtraction(string firstNumber1, string secondNumber2)
        {
            if (IsNumeric(firstNumber1) && IsNumeric(secondNumber2))
            {
                var sum = ConcertToDecimal(firstNumber1) - ConcertToDecimal(secondNumber2);
                return Ok(sum.ToString());
            }

            return BadRequest("Imput Invalído!");
        }



        [HttpGet ("mul/{firstNumber}/{secondNumber}")]
        public IActionResult Mul(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConcertToDecimal(firstNumber) * ConcertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Imput Invalído!");
        }



        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConcertToDecimal(firstNumber) + ConcertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Imput Invalído!");
        }
        private decimal ConcertToDecimal(string Number)
        {
            decimal decimalValue;
            if(decimal.TryParse(Number, out decimalValue))
            {
                return decimalValue;
            }
            return  0;
        }


        private bool IsNumeric(string Number)
        {
            double number;
            return double.TryParse(Number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
          
        }
    }
}
