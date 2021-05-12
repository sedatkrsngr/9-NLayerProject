using NLayerProject.Core.Models;
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
    public class PersonService : GenericService<Person>, IPersonService
    {
        public PersonService(IUnitOfWork unitOfWork, IGenericRepository<Person> repository) : base(unitOfWork, repository)
        {
            
        }
    }
}
