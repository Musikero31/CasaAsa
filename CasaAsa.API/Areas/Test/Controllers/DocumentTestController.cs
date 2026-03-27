using AutoMapper;
using CasaAsa.API.Models;
using CasaAsa.Business.Component.Document;
using CasaAsa.Core.BusinessModels;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTestController : ControllerBase
    {
        private readonly IDocumentComponent _docComponent;
        private readonly IMapper _mapper;

        public DocumentTestController(IDocumentComponent docComponent, IMapper mapper)
        {
            _docComponent = docComponent;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromBody] DocumentUploadRequest document)
        {
            var doc = _mapper.Map<Documents>(document);
            
            await _docComponent.UploadDocumentAsync(doc);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveDocument(int docId)
        {
            var data = await _docComponent.RetrieveDocumentAsync(docId);

            var result = _mapper.Map<DocumentViewModel>(data);
            result.Base64DocumentFile = Convert.ToBase64String(data.DocumentFile);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveDocument(int docId)
        {
            await _docComponent.RemoveDocumentAsync(docId);

            return Ok("File removed.");
        }
    }
}
