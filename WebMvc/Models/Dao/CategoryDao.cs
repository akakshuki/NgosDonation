    using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    public class CategoryDao
    {
        private IUnitOfWork _unitOfWork;

        public CategoryDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CategoryDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Category>, List<CategoryDTO>>(_unitOfWork.CategoryRepository.Get().ToList());
        }

        public CategoryDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Category, CategoryDTO>(_unitOfWork.CategoryRepository.GetById(id));
        }

        public bool Create(CategoryDTO category)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<CategoryDTO, Category>(category);
                _unitOfWork.CategoryRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(int id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool CheckHaveExist(string categoryCateName)
        {
            return _unitOfWork.CategoryRepository.Get()
                       .FirstOrDefault(x => x.CateName.Equals(categoryCateName)) != null;
        }

        public bool Edit(CategoryDTO category)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<Category>(category);
                _unitOfWork.CategoryRepository.Edit(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}