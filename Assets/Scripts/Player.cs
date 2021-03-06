﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed;
    public float m_maxVelocityX;
    public float m_jumpIntensity;
    public LayerMask m_layerGround;
    public LayerMask m_layerMovingGround;
    public float m_attackRate;
    public float m_damageRate;
    public float m_forcePush;

    public IntVariable m_pv;
    //public int m_pv = 3;

    public enum State
    {
        STANDING,
        JUMPING,
        FALLING,
        HURT
    }

    private State _state = State.STANDING;
    private Animator _anim;
    private Rigidbody2D _rb;
    private Transform _tr;
    private float _distanceTocheckGround = 2.0f;
    private int _nbDoubleJumps = 1;
    private int _nbCurrentDoubleJumps = 0;
    private float _nextAttack;
    private Collider2D _axeCollider;
    private float _nextDamage;
    private bool isDead = false;

    void Start()
    {
        _tr = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _axeCollider = GameObject.FindGameObjectWithTag("Axe").GetComponent<Collider2D>();
        _axeCollider.enabled = false;

    }

    void Update()
    {

        switch (_state)
        {
            case (State.STANDING):
                PrepareToLand();
                MoveHorizontal();
                AnimWalk();
                PrepareFalling();
                PrepareJump();
                PrepareStrike();
                FollowMovingFloor();
                break;

            case (State.JUMPING):
                MoveHorizontal();
                PrepareFalling();
                PrepareDoubleJump();
                PrepareStrike();
                break;

            case (State.FALLING):
                _anim.SetTrigger(Animator.StringToHash("Fall"));
                MoveHorizontal();
                PrepareDoubleJump();
                PrepareToLand();
                PrepareStrike();
                break;

            case (State.HURT):
                break;
        }

        //Debug.Log(_state);
        //Debug.Log(_nbCurrentDoubleJumps);
    }

    public void HitPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Time.time > _nextDamage && !isDead)
        {
            _nextDamage = Time.time + m_damageRate;

            m_pv.value--;
            if (m_pv.value <= 0)
            {
                isDead = true;
                _anim.SetBool(Animator.StringToHash("Dead"), true);
            }
            _state = State.HURT;

            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        Vector2 pushDirection;
        _anim.SetTrigger(Animator.StringToHash("Hurt"));
        if (_rb.velocity.x < 0)
            pushDirection = Vector2.right;
        else
            pushDirection = Vector2.left;
        _rb.velocity = Vector2.zero;
        _rb.AddForce((pushDirection + Vector2.up) * m_forcePush);

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            //GameOver, in charge by GameManager;
            _rb.velocity = Vector2.zero;
        }
        else
            _state = State.STANDING;
    }

    private void PrepareStrike()
    {
        if (Input.GetButtonDown("Fire3") && Time.time > _nextAttack)
        {
            _nextAttack = Time.time + m_attackRate;
            StartCoroutine(Strike());
        }
    }

    private IEnumerator Strike()
    {

        _anim.SetTrigger(Animator.StringToHash("Strike"));
        yield return new WaitForSeconds(0.25f);
        _axeCollider.enabled = true;
        yield return new WaitForSeconds(0.25f);
        _axeCollider.enabled = false;
    }

    private void PrepareJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger(Animator.StringToHash("Jump"));
            Jump();
        }
    }

    private void Jump()
    {
        _state = State.JUMPING;
        _rb.velocity = new Vector2(_rb.velocity.x, Time.deltaTime * m_jumpIntensity);
        Debug.Log(_rb.velocity);
    }

    private void PrepareDoubleJump()
    {
        if (Input.GetButtonDown("Jump") && _nbCurrentDoubleJumps < _nbDoubleJumps)
        {
            Jump();
            _nbCurrentDoubleJumps++;
        }
    }

    private void PrepareFalling()
    {
        if (_rb.velocity.y < 0)
        {
            _state = State.FALLING;
            _anim.SetTrigger(Animator.StringToHash("Fall"));
        }
    }

    private void PrepareToLand()
    {
        if (CheckIfOnGround() && _rb.velocity.y <= 0)
        {
            _anim.SetTrigger(Animator.StringToHash("Landing"));
            _state = State.STANDING;
            _rb.velocity = Vector2.zero;
            _nbCurrentDoubleJumps = 0;
        }
    }

    private void AnimWalk()
    {
        _anim.SetFloat(Animator.StringToHash("VelocityX"), Mathf.Abs(_rb.velocity.x) / m_maxVelocityX);
    }

    private void MoveHorizontal()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        _rb.velocity = new Vector2(direction.x * m_speed * Time.deltaTime, _rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0)
        {
            _tr.localScale = Vector3.one;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            _tr.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(
                        _tr.position,
                        Vector2.down,
                        _distanceTocheckGround,
                        m_layerGround
                        );

        if (!hit.collider)
            hit = Physics2D.Raycast(
                        _tr.position,
                        Vector2.down,
                        _distanceTocheckGround,
                        m_layerMovingGround
                        );

        return (hit.collider);
    }

    private void FollowMovingFloor()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(
                        _tr.position,
                        Vector2.down,
                        _distanceTocheckGround,
                        m_layerMovingGround
                        );

        if (hit.collider)
        {
            _tr.SetParent(hit.transform, true);
        }
        else
        {
            _tr.SetParent(null);
        }
    }
}
