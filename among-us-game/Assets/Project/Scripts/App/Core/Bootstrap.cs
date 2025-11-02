using System;
using System.Threading;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class Bootstrap : IStartable, IDisposable
    {
        private readonly BootstrapRootController _bootstrapController;

        private CancellationTokenSource _cancellationTokenSource = new();

        [Inject]
        public Bootstrap(BootstrapRootController bootstrapController)
        {
            _bootstrapController = bootstrapController;
        }

        void IStartable.Start()
        {
            try
            {
                _bootstrapController.LaunchTree(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Can't start bootstrap controller {exception}");
            }
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = null;
        }
    }
}