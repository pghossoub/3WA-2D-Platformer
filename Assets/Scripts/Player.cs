using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private Transform _tr;

    public float m_speed;
    public float m_maxVelocityX;


    void Start()
    {
        _tr = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);

        _rb.velocity = new Vector2(direction.x * m_speed * Time.deltaTime, _rb.velocity.y);


        if (Input.GetAxis("Horizontal") > 0)
        {

           _tr.localScale = Vector3.one;
           //_anim.SetBool(Animator.StringToHash("IsWalking"), true);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {

            _tr.localScale = new Vector3(-1, 1, 1);
            //_anim.SetBool(Animator.StringToHash("IsWalking"), true);
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            //_anim.SetBool(Animator.StringToHash("IsWalking"), false);
            //_rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        /*
        if(_rb.velocity.x > m_maxVelocityX)
        {
            _rb.velocity = new Vector2(m_maxVelocityX, _rb.velocity.y);
        }

        if (_rb.velocity.x < - m_maxVelocityX)
        {
            _rb.velocity = new Vector2(- m_maxVelocityX, _rb.velocity.y);
        }
        */
        _anim.SetFloat(Animator.StringToHash("VelocityX"), Mathf.Abs(_rb.velocity.x) / m_maxVelocityX);




    }

}
