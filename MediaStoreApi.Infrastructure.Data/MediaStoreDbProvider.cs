using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Interfaces;
using MediaStoreApi.Domain.Core;
using System.Data.Entity;
using System.IO;

namespace MediaStoreApi.Infrastructure.Data
{
    public class MediaStoreDbProvider : IMediaInfoProvider,IDisposable
    {
        private DbContext _dbContext;
        public MediaStoreDbProvider() : this(new MediaStoreAppContext())
        {

        }
        public MediaStoreDbProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public FileModel Delete(FileModel model)
        {
            var item = _dbContext.Set<FileModel>().Find(model.Id);
            if (item == null)
                throw new FileNotFoundException("in the database there is no information on the desired file");
            _dbContext.Set<FileModel>().Remove(item);
            return item;
        }

       
        public FileModel Get(FileModel model)
        {
            var item = _dbContext.Set<FileModel>().Find(model.Id);
            if (item == null)
                throw new FileNotFoundException("in the database there is no information on the desired file");
            return item;
        }

        public IEnumerable<FileModel> List()
        {
            return _dbContext.Set<FileModel>();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public FileModel Set(FileModel model)
        {
            model.Id = Guid.NewGuid();
            model.DateOfCreate = DateTime.Now;
            var file = _dbContext.Set<FileModel>().Add(model);
            return file;
        }

        public void Update(FileModel model)
        {
            _dbContext.Entry<FileModel>(model).State = EntityState.Modified;
        }


        //Disposing context
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
