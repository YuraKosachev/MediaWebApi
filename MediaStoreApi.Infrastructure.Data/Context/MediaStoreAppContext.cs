using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Core;
using System.Data.Entity;

namespace MediaStoreApi.Infrastructure.Data
{
    public class MediaStoreAppContext:DbContext
    {
        public MediaStoreAppContext() : base("MediaConnection")
        { }
        public DbSet<FileModel> Files { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FileModel>().ToTable("Media");

        }
    }
}
