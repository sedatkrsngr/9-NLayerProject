using NLayerProject.Core.NewModels;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Service.Services
{
    public class SampleSqlService : GenericService<SampleSql>, ISampleSqlService
    {
        public SampleSqlService(IUnitOfWork unitOfWork, IGenericRepository<SampleSql> repository) : base(unitOfWork, repository)
        {
        }
    }
}
