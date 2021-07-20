using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Advert : ControllerBase
    {
        private IAdverStorageService _advertStorageService;

        public Advert(IAdverStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException)
            {

                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(201, new CreateAdvertResponse() { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {

                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {

                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }
    }
}
