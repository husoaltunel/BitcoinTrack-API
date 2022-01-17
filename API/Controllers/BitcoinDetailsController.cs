using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinDetailsController : ControllerBase
    {
        private readonly IBitcoinDetailService _bitcoinDetailService;
        public BitcoinDetailsController(IBitcoinDetailService bitcoinDetailService)
        {
            _bitcoinDetailService = bitcoinDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bitcoinDetailService.GetAllAsync());
        }
    }
}
