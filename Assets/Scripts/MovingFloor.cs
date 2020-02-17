using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float m_speed;
    public float m_behaviorTime;
    public float m_waitTime;
    public Vector2 m_direction;


    //private Rigidbody2D _rb;
    private Vector2 _direction;

    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();

        StartCoroutine(MovePattern());
    }

    private void Update()
    {
        //_rb.velocity = _direction * m_speed * Time.deltaTime;

        //If KINEMATIC, DONT USE velocity, or the child will not follow. Use transform to move. 
        transform.Translate(_direction * m_speed * Time.deltaTime);

    }

    IEnumerator MovePattern()
    {
        while (gameObject)
        {
            _direction = Vector2.zero;
            yield return new WaitForSeconds(m_waitTime);
            _direction = m_direction;
            yield return new WaitForSeconds(m_behaviorTime);
            _direction = Vector2.zero;
            yield return new WaitForSeconds(m_waitTime);
            _direction = -m_direction;
            yield return new WaitForSeconds(m_behaviorTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,
            (Vector2)transform.position + m_direction * m_speed * m_behaviorTime);
    }
}
