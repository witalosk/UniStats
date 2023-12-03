using UnityEngine;

namespace UniStats.Disabler
{
    public class ReleaseBuildDisabler : MonoBehaviour
    {
        private void Awake()
        {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }
    }
}