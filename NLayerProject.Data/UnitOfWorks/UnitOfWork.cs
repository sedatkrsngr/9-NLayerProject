using NLayerProject.Core.Repositories;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly NLayerContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        private PersonRepository _personRepository;
        private SampleSqlRepository _sampleSqlRepository;
      

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IPersonRepository Persons => _personRepository = _personRepository ?? new PersonRepository(_context);

        public ISampleSqlRepository SampleSqls => _sampleSqlRepository = _sampleSqlRepository ?? new SampleSqlRepository(_context);

        public UnitOfWork(NLayerContext nLayerContext)
        {
            _context = nLayerContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
