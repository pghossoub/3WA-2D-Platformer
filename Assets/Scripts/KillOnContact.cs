using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    public IntVariable pv;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pv.value = 0;
            Destroy(collision.gameObject.GetComponentInParent<Player>().gameObject);
        }
    }
}
