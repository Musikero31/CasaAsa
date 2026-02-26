using AutoMapper;
using CasaAsa.API.Areas.Menu.Data;
using CasaAsa.API.Areas.Menu.Models;
using CasaAsa.Business.Component.Menu;
using CasaAsa.Core.BusinessModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuComponent _menuComponent;
        private readonly IMapper _mapper;

        public MenuController(IMenuComponent menuComponent, IMapper mapper)
        {
            _menuComponent = menuComponent;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetMenuCategories()
        {
            var result = await _menuComponent.GetMenuCategoriesAsync();

            return Ok(result.Select(_mapper.Map<MenuCategoryViewModel>).ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryRequest model)
        {
            var menuCategory = _mapper.Map<MenuCategories>(model);

            await _menuComponent.CreateNewMenuCategoryAsync(menuCategory);

            return Ok("Menu category created");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuCategory([FromBody] MenuCategoryRequest model)
        {
            var menuCategory = _mapper.Map<MenuCategories>(model);

            await _menuComponent.UpdateMenuCategoryAsync(menuCategory);

            return Ok("Menu category updated.");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveMenuCategory(int categoryId)
        {
            await _menuComponent.DeleteMenuCategoryAsync(categoryId);

            return Ok("Menu category removed.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetMenuListByCategory(int categoryId)
        {
            var result = await _menuComponent.GetMenuListByCategoryAsync(categoryId);

            return Ok(result.Select(_mapper.Map<MenuViewModel>));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewMenu([FromBody] MenuViewModel model)
        {
            var menu = _mapper.Map<Core.BusinessModels.Menu>(model);

            await _menuComponent.CreateMenuDetailAsync(menu);

            return Ok("New menu created");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuViewModel model)
        {
            var menu = _mapper.Map<Core.BusinessModels.Menu>(model);

            await _menuComponent.UpdateMenuDetailAsync(menu);

            return Ok("Menu has been updated.");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveMenu(int menuId)
        {
            await _menuComponent.RemoveMenuDetailAsync(menuId);

            return Ok("Menu has been deleted");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetMenuAvailabilityStatus(int menuId, bool isAvailable)
        {
            await _menuComponent.UpdateMenuAvailabilityStatusAsync(menuId, isAvailable);

            return Ok("Menu availability status has been updated.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetMenuDetails(int menuId)
        {
            var result = await _menuComponent.GetMenuDetailAsync(menuId);

            return Ok(_mapper.Map<MenuViewModel>(result));
        }
    }
}
