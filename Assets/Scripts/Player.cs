using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed;
    public float m_maxVelocityX;
    public float m_jumpIntensity;
    public LayerMask m_layerGround;

    public enum State
    {
        STANDING,
        JUMPING,
        LANDING,
        STRIKING
    }

    private State _state = State.STANDING;
    private Animator _anim;
    private Rigidbody2D _rb;
    private Transform _tr;

    private float m_distanceTocheckGround = 2.0f;
    private int nbDoubleJumps = 1;
    private int nbCurrentDoubleJumps = 0;

    //private bool _onAir;


    void Start()
    {
        _tr = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {

        switch (_state)
        {
            case (State.STANDING):
                MoveHorizontal();
                AnimWalk();
                PrepareJump();
                PrepareFalling();
                PrepareStrike();

                break;

            case (State.STRIKING):
                MoveHorizontal();
                AnimWalk();
                PrepareJump();
                PrepareFalling();
                break;


            case (State.JUMPING):
                MoveHorizontal();
                PrepareToLand();
                PrepareDoubleJump();
                break;

            case (State.LANDING):
                nbCurrentDoubleJumps = 0;
                break;
        }
        //Debug.Log(_state);
    }

    private void PrepareStrike()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            _state = State.STRIKING;
            StartCoroutine(Strike());
        }
    }

    private IEnumerator Strike()
    {
        _anim.SetTrigger(Animator.StringToHash("Strike"));
        yield return new WaitForSeconds(0.5f);
        _state = State.STANDING;
    }

    private void PrepareDoubleJump()
    {
        if (Input.GetButtonDown("Jump") && nbCurrentDoubleJumps < nbDoubleJumps)
        {
            StartCoroutine(Jump());
            nbCurrentDoubleJumps++;
        }
    }

    private void PrepareFalling()
    {
        if (_rb.velocity.y < 0)
        {
            _state = State.JUMPING;
            _anim.SetTrigger(Animator.StringToHash("Fall"));
        }
    }

    private void PrepareToLand()
    {
        if (CheckIfOnGround() && _rb.velocity.y < 0)
        {
            _state = State.LANDING;
            _anim.SetTrigger(Animator.StringToHash("Landing"));
            StartCoroutine(Land());
        }
    }

    private IEnumerator Land()
    {
        yield return new WaitForSeconds(0.1f);
        _state = State.STANDING;
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

    private void PrepareJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger(Animator.StringToHash("Jump"));
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        _state = State.JUMPING;
        yield return new WaitForSeconds(0.2f);
        _rb.velocity = new Vector2(_rb.velocity.x, Time.deltaTime * m_jumpIntensity);
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(
                        _tr.position,
                        Vector2.down,
                        m_distanceTocheckGround,
                        m_layerGround
                        );

        return(hit.collider);
    }
}
