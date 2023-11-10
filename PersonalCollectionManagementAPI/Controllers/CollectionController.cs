using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.DTOs.CollectionDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly ITopicService _topicService;

        public CollectionController(ICollectionService collectionService, ITopicService topicService)
        {
            _collectionService = collectionService;
            _topicService = topicService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CollectionForCreationDto model)
        {
            try
            {
                await _collectionService.CreateCollectionAsync(model);
                return Ok("Collection created.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("largest")]
        public async Task<IActionResult> GetFiveLargestAsync()
        {
            try
            {
                var largestCollections = await _collectionService.GetFivaLargestAsync();

                return Ok(largestCollections);
            }
            catch (NotSucceededException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var collections = await _collectionService.GetAllAsync();

                return Ok(collections);
            }
            catch (NotSucceededException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("alltopics")]
        //[Autorize]
        public async Task<IActionResult> GetAllTopicsAsync()
        {
            try
            {
                var topics = await _topicService.GetAllAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("userscollections/{userId}")]
        //[Autorize]
        public async Task<IActionResult> GetAllUsersCollectionsAsync(string userId)
        {
            try
            {
                var topics = await _collectionService.GetAllUsersCollectionsAsync(userId);
                return Ok(topics);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }


        [HttpDelete]
        [Route("delete/{id}")]
        //[Autorize]
        public async Task<IActionResult> DeleteCollectionAsync(int id, string userId)
        {
            try
            {
                await _collectionService.DeleteCollectionAsync(id, userId);
                return Ok("Collection deleted.");
            }
            catch (NotSucceededException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("collectionbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var collection = await _collectionService.GetCollectionByIdAsync(id);

                return Ok(collection);
            }
            catch (NotSucceededException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("topoicbyid/{id}")]
        public async Task<IActionResult> GetTopicByIdAsync(int id)
        {
            try
            {
                var topic = await _topicService.GetByTopicIdAsync(id);

                return Ok(topic);
            }
            catch (NotSucceededException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
