using System;

namespace TeduShop.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }
            isDisposed = true;
        }

        ~Disposable()
        {
            Dispose(false);
        }

        protected virtual void DisposeCore()
        {
        }
    }
}