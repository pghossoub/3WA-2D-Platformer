using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldNugget : MonoBehaviour
{
    public IntVariable score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float destroyTime = 0.5f;

            score.value += 10;

            DOTween.Init();
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale
                (new Vector3(0, 0, 0), destroyTime).SetEase((Ease.InOutQuad)));

            Destroy(gameObject, destroyTime);
        }
    }

    private void Start()
    {
        DOTween.Init();
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate
            (new Vector3(0, 0, 30f), 1.5f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo));
    }
}
