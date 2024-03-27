using DG.Tweening;
using UnityEngine;

public class Shaker
{
    Transform item;

    public Shaker(Transform item)
    {
        this.item = item;
    }

    public void DoShakeAction(float duration, float strength)
    {
        item.DOShakePosition(duration, strength).SetEase(Ease.InBounce);
        item.DOShakeRotation(duration, strength / 60);
        item.DOShakeScale(duration, strength / 60);
    }
}
