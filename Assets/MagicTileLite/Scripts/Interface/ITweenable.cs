using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

namespace Drland.MagicTileLite
{
    public interface ITweenable
    {
        Tween PunchScale(Transform target, float targetScale, float duration);

        Tween ChangeImageAlpha(Image target, float targetAlpha, float duration);
        Tween ChangeImageColor(Image target, Color targetColor, float duration);

    }
}