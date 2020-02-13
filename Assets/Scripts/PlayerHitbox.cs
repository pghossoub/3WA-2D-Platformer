using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private Player _player;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.HitPlayer(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _player.HitPlayer(collision);
    }
}
