using UnityEngine;

namespace UniStats.Disabler
{
    public class Unity2021OrOlderDisabler : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_2022_1_OR_NEWER
            gameObject.SetActive(false);
#endif
        }
    }
}