using UnityEngine;

namespace PatternGame
{
    public class MonoFactoryBase
    {
        protected TPrefab InstantiatePrefab<TPrefab>(TPrefab prefab, Transform root)
            where TPrefab : MonoBehaviour
            => Object.Instantiate(prefab, root);

        protected void Destroy<TInstance>(TInstance instance)
            where TInstance : MonoBehaviour
            => Object.Destroy(instance.gameObject);
    }
}