using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;


namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);
        void InsertList(IList<TEntity> objs);
        void Update(TEntity obj);

        void Delete(int id);
        
        void  DeleteAll();

        IList<TEntity> Select();

        TEntity Select(int id);

    }
    public interface IFreightPriceRepository<TEntity> where TEntity : FreightPrice
    {
        void SelectFreightsPrices(TEntity obj);


    }
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> AddList<TValidator>(IList<TEntity> obj) where TValidator : AbstractValidator<IList<TEntity>>;
        void Delete(int id);
        void CleanFreightPriceTable();
        IList<TEntity> Get();


    }

    public interface IFreightPriceService<TEntity> where TEntity : FreightPrice
    {
        FreightPrice GetFreightPrice(Archive obj);


    }   
    
    
    public interface IFreightService<TEntity> where TEntity : Freight
    {
        IList<Freight> SaveFreights(DataTable obj);

    }
}
