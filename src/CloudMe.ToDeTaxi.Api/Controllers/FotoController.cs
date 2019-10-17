using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Foto;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FotoController : BaseController
    {
        IFotoService _FotoService;

        public FotoController(IFotoService FotoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FotoService = FotoService;
        }

        /// <summary>
        /// Gets all Fotos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FotoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FotoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FotoService.GetAllSummariesAsync(), _FotoService);
        }

        /// <summary>
        /// Gets a Foto.
        /// <param name="id">Foto's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FotoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FotoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FotoService.GetSummaryAsync(id), _FotoService);
        }

        /// <summary>
        /// Creates a new Foto.
        /// </summary>
        /// <param name="FotoSummary">Foto's summary</param>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FotoSummary FotoSummary)
        {
            var entity = await this._FotoService.CreateAsync(FotoSummary);
            if (_FotoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FotoService);
            }
            return await base.ResponseAsync(entity.Id, _FotoService);
        }

        /// <summary>
        /// Modifies an existing Foto.
        /// </summary>
        /// <param name="FotoSummary">Modified Foto list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FotoSummary FotoSummary)
        {
            return await base.ResponseAsync(await this._FotoService.UpdateAsync(FotoSummary) != null, _FotoService);
        }

        /// <summary>
        /// Removes an existing Foto.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FotoService.DeleteAsync(id), _FotoService);
        }


        /// <summary>
        /// Creates a new Foto.
        /// </summary>
        /// <param arquivo="IFormFile">Imagem da foto</param>
        [HttpPost("upload"), DisableRequestSizeLimit]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Upload(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await arquivo.CopyToAsync(stream);
                    var fotoSummary = new FotoSummary
                    {
                        NomeArquivo = arquivo.FileName,
                        Dados = stream.GetBuffer()
                    };

                    var entity = await this._FotoService.CreateAsync(fotoSummary);
                    if (_FotoService.IsInvalid())
                    {
                        return await base.ErrorResponseAsync<Guid>(_FotoService);
                    }
                    return await base.ResponseAsync(entity.Id, _FotoService);
                }
            }
            return await base.ResponseAsync(Guid.Empty, _FotoService);            
        }
    }
}

