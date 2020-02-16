using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collision.gameObject.CompareTag("Player")){
            if (pv.value < 3)
                pv.value++;
            audioSource.Play();
            //StartCoroutine(PlaySound());
            Destroy(gameObject, 0.4f);
            //Destroy(gameObject);
        }
    }
    /*
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(1.0f);
        audioSource.Play();
    }
    */
}
