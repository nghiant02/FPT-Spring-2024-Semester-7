using Repository.Models;
using System;

namespace Repository
{
    public class UnitOfWork : IDisposable
    {
        private Eyeglasses2024DBContext context = new Eyeglasses2024DBContext();
        private GenericRepository<Eyeglass> eyeglassesRepository;
        private GenericRepository<LensType> lensTypeRepository;
        private GenericRepository<StoreAccount> storeAccountRepository;

        public GenericRepository<Eyeglass> EyeglassRepository
        {
            get
            {

                if (this.eyeglassesRepository == null)
                {
                    this.eyeglassesRepository = new GenericRepository<Eyeglass>(context);
                }
                return eyeglassesRepository;
            }
        }

        public GenericRepository<LensType> LensTypeRepository
        {
            get
            {

                if (this.lensTypeRepository == null)
                {
                    this.lensTypeRepository = new GenericRepository<LensType>(context);
                }
                return lensTypeRepository;
            }
        }

        public GenericRepository<StoreAccount> StoreAccountRepository
        {
            get
            {

                if (this.storeAccountRepository == null)
                {
                    this.storeAccountRepository = new GenericRepository<StoreAccount>(context);
                }
                return storeAccountRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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