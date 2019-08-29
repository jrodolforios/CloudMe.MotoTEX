using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FavoritoController : BaseController
    {
        IFavoritoService _favoritoService;

        public FavoritoController(IFavoritoService favoritoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _favoritoService = favoritoService;
        }

        /// <summary>
        /// Gets all favoritos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FavoritoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _favoritoService.GetAllSummariesAsync(), _favoritoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Favorito.
        /// <param name="id">Favorito's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FavoritoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _favoritoService.GetSummaryAsync(id), _favoritoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Favorito.
        /// </summary>
        /// <param name="favoritoSummary">Favorito's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FavoritoSummary favoritoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._favoritoService.CreateAsync(favoritoSummary) != null, _favoritoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Favorito.
        /// </summary>
        /// <param name="favoritoSummary">Modified Favorito list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FavoritoSummary favoritoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._favoritoService.UpdateAsync(favoritoSummary) != null, _favoritoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Favorito.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._favoritoService.DeleteAsync(id), _favoritoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}