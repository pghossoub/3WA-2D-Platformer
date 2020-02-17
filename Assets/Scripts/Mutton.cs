using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Mutton : MonoBehaviour
{
    public IntVariable pv;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pv.value < 3)
                pv.value++;
            audioSource.Play();


            float destroyTime = 0.5f;
            DOTween.Init();
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale
                (new Vector3(0, 0, 0), destroyTime).SetEase((Ease.InOutQuad)));

            Destroy(gameObject, destroyTime);
        }

    }
}
