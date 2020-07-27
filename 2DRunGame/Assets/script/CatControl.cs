using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl : MonoBehaviour
{
    #region 資料欄位
    [Tooltip("移動速度"), Range(0, 100)]
    public float movingSpeed = 1f;
    [Tooltip("跳躍高度"), Range(0, 1000)]
    public int jumpHeight = 300;
    [Tooltip("血量"), Range(0, 1000)]
    public float hp = 500;    
    [Tooltip("中心點位移")]
    public Vector3 offset = new Vector3(0,-1.2f,0);


    [Header("音效")]
    public AudioClip hurtAudio;
    public AudioClip slideAudio;
    public AudioClip jumpAudio;
    public AudioClip getAudio;

    bool isTouchingGround = false;

    int moneyGet;
    Animator _animator;
    Rigidbody2D _rigidbody;
    CapsuleCollider2D _collider;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        
    }
    
    void Update()
    {
        Slide();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Slide()
    {
        if (isTouchingGround && Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetBool("isSliding", true);
        }
        else
        {
            _animator.SetBool("isSliding", false);
        }
    }

    void Jump()
    {
        if (isTouchingGround && Input.GetKeyDown(KeyCode.Space))
        {
            isTouchingGround = false;
            _rigidbody.AddForce(transform.up * jumpHeight);
        }
    }

    void Move()
    {
        _animator.SetBool("isMoving", true);
        _animator.SetBool("isJumping", !isTouchingGround);
        transform.Translate(transform.right * movingSpeed / 10);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "地板")
        {
            isTouchingGround = true;
        }
    }
}
