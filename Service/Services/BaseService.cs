using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }


        public IList<TEntity> AddList<TValidator>(IList<TEntity> obj) where TValidator : AbstractValidator<IList<TEntity>>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.InsertList(obj);
            return obj;
        }
        public void CleanFreightPriceTable() => _baseRepository.DeleteAll();

        public void Delete(int id) => _baseRepository.Delete(id);

        public IList<TEntity> Get() => _baseRepository.Select();

        


        private void Validate(IList<TEntity> obj, AbstractValidator<IList<TEntity>> validator)
        {
            if (obj == null)
                throw new Exception("Nenhum dado encontrado");

            validator.ValidateAndThrow(obj);
        }
    }
}
