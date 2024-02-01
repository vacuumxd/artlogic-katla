using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KatlaSport.Services.HiveManagement;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/sections")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class HiveSectionsController : ApiController
    {
        private readonly IHiveSectionService _hiveSectionService;

        public HiveSectionsController(IHiveSectionService hiveSectionService)
        {
            _hiveSectionService = hiveSectionService ?? throw new ArgumentNullException(nameof(hiveSectionService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of hive sections.", Type = typeof(HiveSectionListItem[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult GetHiveSections()
        {
            var hives = _hiveSectionService.GetHiveSectionsAsync();
            return Ok(hives);
        }

        [HttpGet]
        [Route("{hiveSectionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a hive section.", Type = typeof(HiveSection))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult GetHiveSection(int hiveSectionId)
        {
            var hive = _hiveSectionService.GetHiveSectionAsync(hiveSectionId);
            return Ok(hive);
        }

        [HttpPut]
        [Route("{hiveSectionId:int:min(1)}/status/{deletedStatus:bool}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Sets deleted status for an existed hive section.")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> SetStatus([FromUri] int hiveSectionId, [FromUri] bool deletedStatus)
        {
            await _hiveSectionService.SetStatusAsync(hiveSectionId, deletedStatus);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }



        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new hive section.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid data provided.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddHiveSection([FromBody] UpdateHiveSectionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _hiveSectionService.CreateHiveSectionAsync(request);
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{hiveSectionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes a hive section.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Hive section not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteHiveSection(int hiveSectionId)
        {
            try
            {
                await _hiveSectionService.DeleteHiveSectionAsync(hiveSectionId);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{hiveSectionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates a hive section.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid data provided.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Hive section not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateHiveSection(int hiveSectionId, [FromBody] UpdateHiveSectionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _hiveSectionService.UpdateHiveSectionAsync(hiveSectionId, request);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{hiveSectionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes a hive section.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Hive section not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteHiveSectionAsync(int hiveSectionId)
        {
            try
            {
                await _hiveSectionService.DeleteHiveSectionAsync(hiveSectionId);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }
    }
}
