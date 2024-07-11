using DG.Tweening;

namespace Game.Scripts.Extensions
{
    public static class TweenerExtensions
    {
        public static void KillIfValid(this Tween tween)
        {
            if (tween != null && tween.IsActive())
            {
                tween.Kill();
            }
        }
    }
}