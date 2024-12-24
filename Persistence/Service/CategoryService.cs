using Application.DTOs;
using Application.IService;
using Application.IUnitOfWork;
using Domain.Entities;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryMapper _categoryMapper;

        public CategoryService(IUnitOfWork unitOfWork, CategoryMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryMapper = _mapper;
        }
        public async Task<ICollection<CategoryDTO>> GetAllCategories()
        {
            var cate = await _unitOfWork.Categories.GetAllAsync();
            return _categoryMapper.ToListDTO(cate);
        }
    }
}
