using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    public class MonoFactoryBase
    {
        protected TPrefab InstantiatePrefab<TPrefab>(TPrefab prefab, Transform root)
            where TPrefab : MonoBehaviour
            => Object.Instantiate(prefab, root);
    }
}