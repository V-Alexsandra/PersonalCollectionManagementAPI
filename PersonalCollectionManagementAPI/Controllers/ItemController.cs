using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.DTOs.ItemDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize]
        public async Task<IActionResult> CreateItemAsync([FromBody] ItemForCreationDto model)
        {
            try
            {
                await _itemService.CreateItemAsync(model);
                return Ok("Item created.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var collections = await _itemService.GetAllAsync();

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
        [Route("collectionitems/{id}")]
        //[Autorize]
        public async Task<IActionResult> GetAllCollectionItemsAsync(int id)
        {
            try
            {
                var topics = await _itemService.GetAllCollectionItemsAsync(id);
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
        public async Task<IActionResult> DeleteCollectionAsync(int id)
        {
            try
            {
                await _itemService.DeleteItemnAsync(id);
                return Ok("Item deleted.");
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
        [Route("itembyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var collection = await _itemService.GetItemByIdAsync(id);

                return Ok(collection);
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("alltags")]
        //[Autorize]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                var topics = await _itemService.GetAllTagsAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Route("update")]
        //[Authorize]
        public async Task<IActionResult> UpdateItemAsync([FromBody] ItemForUpdateDto model)
        {
            try
            {
                await _itemService.UpdateItemAsync(model);
                return Ok("Item updated.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateTags")]
        //[Authorize]
        public async Task<IActionResult> UpdateTagsAsync([FromBody] TagForUpdateDto model)
        {
            try
            {
                await _itemService.UpdateTagsEntities(model);
                return Ok("Tag updated.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getlastaddeditems")]
        //[Authorize]
        public async Task<IActionResult> GetLastAddedItems()
        {
            try
            {
                var items = await _itemService.GetLastAddedItemsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}