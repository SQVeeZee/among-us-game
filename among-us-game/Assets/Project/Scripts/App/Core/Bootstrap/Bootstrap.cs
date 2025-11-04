using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class Bootstrap : IAsyncStartable
    {
        private readonly BootstrapRootController _root;

        [Inject]
        public Bootstrap(BootstrapRootController root) => _root = root;

        public UniTask StartAsync(CancellationToken cancellationToken)
        {
            _root.LaunchTree(cancellationToken);
            return UniTask.CompletedTask;
        }
    }
}