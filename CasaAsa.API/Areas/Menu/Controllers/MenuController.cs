using AutoMapper;
using CasaAsa.API.Areas.Menu.Data;
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
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryViewModel model)
        {
            var menuCategory = _mapper.Map<MenuCategories>(model);

            await _menuComponent.CreateNewMenuCategoryAsync(menuCategory);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuCategory([FromBody] MenuCategoryViewModel model)
        {
            var menuCategory = _mapper.Map<MenuCategories>(model);

            await _menuComponent.UpdateMenuCategoryAsync(menuCategory);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveMenuCategory(int categoryId)
        {
            await _menuComponent.DeleteMenuCategoryAsync(categoryId);

            return Ok();
        }
    }
}
