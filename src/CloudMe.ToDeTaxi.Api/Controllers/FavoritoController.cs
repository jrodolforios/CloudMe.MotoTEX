using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FavoritoController : BaseController
    {
        IFavoritoService _FavoritoService;

        public FavoritoController(IFavoritoService FavoritoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FavoritoService = FavoritoService;
        }

        /// <summary>
        /// Gets all Favoritos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FavoritoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FavoritoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FavoritoService.GetAllSummariesAsync(), _FavoritoService);
        }

        /// <summary>
        /// Gets a Favorito.
        /// <param name="id">Favorito's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FavoritoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FavoritoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FavoritoService.GetSummaryAsync(id), _FavoritoService);
        }

        /// <summary>
        /// Creates a new Favorito.
        /// </summary>
        /// <param name="FavoritoSummary">Favorito's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FavoritoSummary FavoritoSummary)
        {
            var entity = await this._FavoritoService.CreateAsync(FavoritoSummary);
            if (_FavoritoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FavoritoService);
            }
            return await base.ResponseAsync(entity.Id, _FavoritoService);
        }

        /// <summary>
        /// Modifies an existing Favorito.
        /// </summary>
        /// <param name="FavoritoSummary">Modified Favorito list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FavoritoSummary FavoritoSummary)
        {
            return await base.ResponseAsync(await this._FavoritoService.UpdateAsync(FavoritoSummary) != null, _FavoritoService);
        }

        /// <summary>
        /// Removes an existing Favorito.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FavoritoService.DeleteAsync(id, false), _FavoritoService);
        }
    }
}