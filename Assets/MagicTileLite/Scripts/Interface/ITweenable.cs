using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

namespace Drland.MagicTileLite
{
    public interface ITweenable
    {
        void PunchScale(Transform target, float targetScale, float duration);

        void ChangeImageAlpha(Image target, float targetAlpha, float duration);
        void ChangeImageColor(Image target, Color targetColor, float duration);

    }
}