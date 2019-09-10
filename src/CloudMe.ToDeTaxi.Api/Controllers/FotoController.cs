using System;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Foto;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

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
        [ProducesResponseType(typeof(IEnumerable<FotoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _FotoService.GetAllSummariesAsync(), _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Foto.
        /// <param name="id">Foto's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FotoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _FotoService.GetSummaryAsync(id), _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Corrida.
        /// </summary>
        /// <param name="fotoSummary">Corrida's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FotoSummary fotoSummary)
        {
            try
            {
                return await base.ResponseAsync((await this._FotoService.CreateAsync(fotoSummary)).Id.ToString(), _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Foto.
        /// </summary>
        /// <param arquivo="IFormFile">Imagem da foto</param>
        [HttpPost("upload"), DisableRequestSizeLimit]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile arquivo)
        {
            try
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

                        return await base.ResponseAsync((await this._FotoService.CreateAsync(fotoSummary)).Id.ToString(), _FotoService);
                    }
                }
                return await base.ResponseAsync(string.Empty, _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
            
        }

        /// <summary>
        /// Modifies an existing Foto.
        /// </summary>
        /// <param name="FotoSummary">Modified Foto list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FotoSummary FotoSummary)
        {
            try
            {
                return await base.ResponseAsync((await this._FotoService.UpdateAsync(FotoSummary)).Id.ToString(), _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Foto.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._FotoService.DeleteAsync(id), _FotoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}