using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAle : MonoBehaviour
{
    public float m_speed;
    //public float m_distance;
    public float m_behaviorTime;
    public float m_waitTime;

    private AudioSource _audioSource;
    //private Vector2 _targetPosition;
    //private Vector2 _originPosition;
    private Rigidbody2D _rb;
    private Vector2 _direction = Vector2.right;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        //_originPosition = (Vector2)transform.position;
        //_targetPosition = (Vector2)transform.position + Vector2.right * m_distance;

        StartCoroutine(MovePattern());

    }

    private void Update()
    {
        _rb.velocity = _direction * m_speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            _audioSource.Play();
            Destroy(gameObject, 0.1f);
        }
    }

    /*IEnumerator MovePattern()
    {
        while (gameObject)
        {
            //PROBLEM: coroutine must be done before WaitForSeconds, else several move at the same time
            //StartCoroutine(Move(_targetPosition));
            yield return new WaitForSeconds(1.0f);
            //StartCoroutine(Move(_originPosition));
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Move(Vector2 targetPosition)
    {
        float step = m_speed * Time.deltaTime;
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return new WaitForSeconds(0);
        }

        //targetPosition = (Vector2)transform.position - targetPosition;
        //StartCoroutine(Move(targetPosition));
    }*/

    IEnumerator MovePattern()
    {
        while (gameObject)
        {
            _direction = Vector2.zero;
            yield return new WaitForSeconds(m_waitTime);
            _direction = Vector2.right;
            yield return new WaitForSeconds(m_behaviorTime);
            _direction = Vector2.zero;
            yield return new WaitForSeconds(m_waitTime);
            _direction = Vector2.left;
            yield return new WaitForSeconds(m_behaviorTime);
        }
    }

}
