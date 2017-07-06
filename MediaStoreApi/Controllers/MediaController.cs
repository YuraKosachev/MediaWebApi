using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using MediaStoreApi.Extensions;
using MediaStoreApi.Services.Interfaces;
using MediaStoreApi.Domain.Core;
using System.IO;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Exceptions;


namespace MediaStoreApi.Controllers
{
    public class MediaController : ApiController
    {
        private IMediaService Service { get; set; }
        public MediaController(IMediaService service)
        {
            Service = service;
        }
       
        // GET api/media
        public IHttpActionResult Get()
        {

            try
            {
                return Ok(Service.List().Select(item=>new { id = item.Id }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET api/media/4dbe20c8-2c60-4056-adbf-46be192c4968
        public IHttpActionResult Get(Guid id)
        {

            try
            {
                var model = new FileModel { Id = id };
                return new MediaFileContentResult(Service.Get(model));
            }
            catch (FileNotFoundException ex)
            {
                return new NotFoundMediaFile(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/media
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            try
            {

                var provider = await Request.Content.ReadAsMultipartAsync();
                var fileNames = new List<object>();
                foreach (var file in provider.Contents)
                {

                    fileNames.Add(new { id = Service.Set(await file.GetModelAsync()).Id });
                }

                return Ok(fileNames);
            }
            catch (InvalidMediaFileTypeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT api/media/4dbe20c8-2c60-4056-adbf-46be192c4968
        public IHttpActionResult Put(Guid id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            try
            {
                //Provider.Update(id, content);
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return new NotFoundMediaFile(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE api/media/4dbe20c8-2c60-4056-adbf-46be192c4968

        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                Service.Delete(new FileModel { Id = id });
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return new NotFoundMediaFile(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/media/miniature/{id}")]
        //GET api/media/miniature?id=4dbe20c8-2c60-4056-adbf-46be192c4968
        public IHttpActionResult Miniature(Guid id)
        {
            try
            {
                var model = new FileModel { Id = id, IsMiniature = true };
                return new MediaFileContentResult(Service.Get(model));
            }
            catch (InvalidMediaFileTypeException ex)
            {
                return new BadRequestData(ex);
            }
            catch (FileNotFoundException ex)
            {
                return new NotFoundMediaFile(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/media/{h:int}/{w:int}/{id}")]
        //GET api/media/200/200/4dbe20c8-2c60-4056-adbf-46be192c4968
        public IHttpActionResult Resizable(Guid id, int h, int w)
        {
            try
            {
                var model = new FileModel { Id = id };
                return new MediaFileContentResult(Service.Get(model, h, w));
            }
            catch (InvalidMediaFileTypeException ex)
            {
                return new BadRequestData(ex);
            }
            catch (FileNotFoundException ex)
            {
                return new NotFoundMediaFile(ex);
            }
            catch (BadRequestValues ex)
            {
                return new BadRequestData(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
