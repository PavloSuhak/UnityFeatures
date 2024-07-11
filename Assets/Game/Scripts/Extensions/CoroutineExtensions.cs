using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class CoroutineExtensions
    {
        public static void StopIfValid(this Coroutine coroutine, MonoBehaviour owner)
        {
            if (coroutine != null)
            {
                owner.StopCoroutine(coroutine);
            }
        }
    }
}