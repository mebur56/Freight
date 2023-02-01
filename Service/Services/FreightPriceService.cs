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
        private readonly IBaseService<TEntity> _baseService;

        public FreightPriceService(IBaseRepository<TEntity> baseRepository, IBaseService<TEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        public FreightPrice GetFreightPrice(Archive obj)
        {
            if (obj.Destination != null && obj.Destination != "")
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
                var entity = _baseRepository.Select().Where(x => x.Client == "CDD Ribeirão Preto" && x.VechicleType == obj.VechicleType).First();
                return entity;
            }
        }

    }

}
