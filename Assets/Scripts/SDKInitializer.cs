using Agava.YandexGames;
using System.Collections;
using UnityEngine;

namespace JustTanks.GameLogic
{
    public class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();
#endif
            yield break;
        }
    }
}
