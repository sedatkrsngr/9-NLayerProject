using NLayerProject.Core.NewModels;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
    public class SampleSqlRepository : GenericRepository<SampleSql>, ISampleSqlRepository
    {
        public SampleSqlRepository(NLayerContext context) : base(context)
        {
        }
    }
}
