using DG.Tweening;
using UnityEngine;

public class ChunkView : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(1.25f, 1).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360f, 0), 4, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}