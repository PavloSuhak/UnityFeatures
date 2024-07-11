using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class VectorExtensions
    {
        public static bool IsEqual(this Vector2 left, Vector2 right, float sensitivity = 0.1f)
        {
            return (left - right).magnitude <= sensitivity;
        }
        
        public static bool IsEqual(this Vector3 left, Vector3 right, float sensitivity = 0.1f)
        {
            return (left - right).magnitude <= sensitivity;
        }
    }
}