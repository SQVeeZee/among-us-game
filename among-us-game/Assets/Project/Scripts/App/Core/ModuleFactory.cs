using PatternGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class ModuleFactory
    {
        private readonly ModuleLifetimeScope _moduleLifetimeScope;
        private readonly Transform _parentScope;

        [Inject]
        private ModuleFactory(
            ModuleLifetimeScope moduleLifetimeScope,
            [Key("root")] Transform parentScope)
        {
            _moduleLifetimeScope = moduleLifetimeScope;
            _parentScope = parentScope;
        }

        public ModuleLifetimeScope Create() => LifetimeScope.Instantiate(_moduleLifetimeScope, _parentScope);
        public void Dispose(ModuleLifetimeScope moduleLifetimeScope) => Object.Destroy(moduleLifetimeScope.gameObject);
    }
}