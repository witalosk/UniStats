using UnityEngine;

namespace UniStats
{
    public class DefaultFpsProvider : MonoBehaviour, IFpsProvider
    {
        public float Fps => 1f / Time.unscaledDeltaTime;
    }
}