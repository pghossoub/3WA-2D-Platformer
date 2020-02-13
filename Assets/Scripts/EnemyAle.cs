using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAle : MonoBehaviour
{
    public float m_speed;
    public float m_distance;
    private AudioSource _audioSource;
    private Vector2 _targetPosition;
    private Vector2 _originPosition;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _originPosition = (Vector2)transform.position;
        _targetPosition = (Vector2)transform.position + Vector2.right * m_distance;

        StartCoroutine(MovePattern());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            _audioSource.Play();
            Destroy(gameObject, 0.1f);
        }
    }

    IEnumerator MovePattern()
    {
        while (gameObject)
        {
            StartCoroutine(Move(_targetPosition));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(Move(_originPosition));
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Move(Vector2 targetPosition)
    {
        float step = m_speed * Time.deltaTime;
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //targetPosition = (Vector2)transform.position - targetPosition;
        //StartCoroutine(Move(targetPosition));
    }

}
