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
    public class FreightPriceService<TEntity> : IFreightPriceService<TEntity> where TEntity : FreightPrice
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
 

        public FreightPriceService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public FreightPrice GetFreightPrice(Archive obj)
        {
            if (obj.Origin == "CDD São Paulo")
            {
                try
                {
                    var entity = _baseRepository.Select().Where(x => x.Destination == obj.Destination && x.VechicleType == obj.VechicleType).First();
                    return entity;
                }
                catch (Exception e)
                {
                    return default(FreightPrice);
                }
            }
            else
            {

                try
                {
                    var entity = _baseRepository.Select().Where(x => x.Client == "CDD Ribeirão Preto" && x.VechicleType == obj.VechicleType).First();
                    return entity;
                }
                catch (Exception e)
                {
                    return default(FreightPrice);
                }
            }
        }

    }

}
