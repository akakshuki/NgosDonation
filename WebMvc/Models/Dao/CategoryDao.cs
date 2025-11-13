    using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    /// <summary>
    /// Data Access Object for Category.
    /// </summary>
    public class CategoryDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CategoryDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A list of categories.</returns>
        public List<CategoryDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Category>, List<CategoryDTO>>(_unitOfWork.CategoryRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category.</returns>
        public CategoryDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Category, CategoryDTO>(_unitOfWork.CategoryRepository.GetById(id));
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">The category to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
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

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public bool Delete(int id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Checks if a category with the given name already exists.
        /// </summary>
        /// <param name="categoryCateName">The name of the category to check.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public bool CheckHaveExist(string categoryCateName)
        {
            return _unitOfWork.CategoryRepository.Get()
                       .FirstOrDefault(x => x.CateName.Equals(categoryCateName)) != null;
        }

        /// <summary>
        /// Edits an existing category.
        /// </summary>
        /// <param name="category">The category to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
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