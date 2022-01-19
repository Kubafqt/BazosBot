using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace BazosBot
{
    class Images : IDisposable
    {

      #region dispose
      private readonly IntPtr unmanagedResource;
        private readonly SafeHandle managedResource;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isManualDisposing)
        {
            ReleaseUnmanagedResource(unmanagedResource);
            if (isManualDisposing)
            {
                ReleaseManagedResources(managedResource);
            }
            // Release
        }

        private void ReleaseManagedResources(SafeHandle safeHandle)
        {
            if (safeHandle != null)
            {
                safeHandle.Dispose();
            }
        }

        private void ReleaseUnmanagedResource(IntPtr intPtr)
        {
            Marshal.FreeHGlobal(intPtr);
        }
      #endregion




   }
}