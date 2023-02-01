using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public void Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }
        public void InsertList(IList<TEntity> objs)
        {
            foreach(var obj in objs)
            {
                Insert(obj);
            }
        }

        public void Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var freightTables = _mySqlContext.Set<TEntity>().ToList();
            foreach(var freightTable in freightTables)
            {
                _mySqlContext.Set<TEntity>().Remove(freightTable);
            }
        }

        public IList<TEntity> Select() =>
            _mySqlContext.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _mySqlContext.Set<TEntity>().Find(id);

    }
}