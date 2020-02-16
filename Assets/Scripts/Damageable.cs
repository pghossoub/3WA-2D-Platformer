using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Sprite m_dmgSprite;
    public GameObject brokenColumn;
    public float m_damageRate = 0.7f;

    private int _hp = 2;
    private SpriteRenderer _spriteRenderer;
    //private Collider2D _collider;
    private float _nextDamage;
    private AudioSource audiosource;
    

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Axe") && Time.time > _nextDamage)
        {
            _nextDamage = Time.time + m_damageRate;
            _hp--;
            audiosource.Play();

            if (_hp == 1)
            {
                _spriteRenderer.sprite = m_dmgSprite;
            }
            else if (_hp == 0)
            {
                //_spriteRenderer.sprite = m_dmgSprites[1];
                //_collider.enabled = false;
                brokenColumn.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
