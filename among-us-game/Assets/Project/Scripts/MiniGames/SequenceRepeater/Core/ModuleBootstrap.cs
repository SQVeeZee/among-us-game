using System;
using System.Threading;
using VContainer;
using VContainer.Unity;

namespace MiniGames.SequenceRepeater
{
    public class ModuleBootstrap : IStartable, IDisposable
    {
        private readonly ModuleRootController _moduleController;

        private CancellationTokenSource _cancellationTokenSource = new();

        [Inject]
        public ModuleBootstrap(ModuleRootController bootstrapController)
        {
            _moduleController = bootstrapController;
        }

        void IStartable.Start()
        {
            try
            {
                _moduleController.LaunchTree(_cancellationTokenSource.Token);
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