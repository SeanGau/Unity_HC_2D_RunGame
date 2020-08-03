using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatControl : MonoBehaviour
{
    #region 資料欄位
    [Tooltip("移動速度"), Range(0, 100)]
    public float movingSpeed = 1f;
    [Tooltip("移動速度"), Range(0, 100)]
    public float deadHeight = -10f;
    [Tooltip("跳躍高度"), Range(0, 1000)]
    public int jumpHeight = 300;
    [Tooltip("滑行時間(ms)"), Range(0, 3000)]
    public float slideLength = 1000;
    [Tooltip("血量"), Range(0, 1000)]
    public float hp = 500;
    [Tooltip("血量衰減"), Range(0, 1)]
    public float hpDecreasing = 0.25f;
    [Tooltip("障礙傷害"), Range(0, 100)]
    public float hurt = 20;
    [Tooltip("大障礙傷害"), Range(0, 100)]
    public float hurtBig = 50;

    [Tooltip("中心點位移")]
    public Vector3 offset = new Vector3(0,-1.2f,0);
    [Tooltip("介面物件群")]
    public Transform uiObject;


    [Header("音效")]
    public AudioSource aud;
    public AudioClip hurtAudio;
    public AudioClip slideAudio;
    public AudioClip jumpAudio;
    public AudioClip getAudio;

    bool isTouchingGround = false;
    bool isStop = true;
    int coinGet;
    float hp_max;
    Image blood_osd;
    Animator _animator;
    Rigidbody2D _rigidbody;
    CapsuleCollider2D _collider;
    Vector2 default_collider_offset;
    Vector2 default_collider_size;
    #endregion

    void Start()
    {
        Cursor.visible = false;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();

        default_collider_offset = _collider.offset;
        default_collider_size = _collider.size;
        hp_max = hp;
        blood_osd = uiObject.GetChild(2).gameObject.GetComponent<Image>();
        StartCoroutine("Timer_ready");
    }
    
    void Update()
    {
        if (!isStop)
        {
            Dead();
            Jump();
            Slide();
        }
    }

    void FixedUpdate()
    {
        if (!isStop)
        {
            Move();
        }
    }

    IEnumerator Timer_ready()
    {
        Text ready_counter = uiObject.GetChild(4).gameObject.GetComponent<Text>();
        print("3");
        ready_counter.text = "3";
        yield return new WaitForSeconds(1);
        print("2");
        ready_counter.text = "2";
        yield return new WaitForSeconds(1);
        print("1");
        ready_counter.text = "1";
        yield return new WaitForSeconds(1);
        ready_counter.text = "GO!";
        yield return new WaitForSeconds(1);
        Destroy(ready_counter);
        isStop = false;
    }

    IEnumerator Timer_slider()
    {
        yield return new WaitForSeconds(slideLength/1000);
        _animator.SetBool("isSliding", false);
    }

    void Slide()
    {
        if (isTouchingGround && Input.GetKeyDown(KeyCode.LeftControl))
        {
            aud.PlayOneShot(slideAudio);
            _animator.SetBool("isSliding", true);
            StartCoroutine("Timer_slider");
            _collider.offset = new Vector2(-0.1f, -1.4f);
            _collider.size = new Vector2(1.8f, 1.8f);
        }
        else if(_animator.GetBool("isSliding") == false || !isTouchingGround)
        {
            _animator.SetBool("isSliding", false);
            _collider.offset = default_collider_offset;
            _collider.size = default_collider_size;
        }
    }

    void Jump()
    {
        if (isTouchingGround && Input.GetKeyDown(KeyCode.Space))
        {
            aud.PlayOneShot(jumpAudio);
            isTouchingGround = false;
            _rigidbody.AddForce(transform.up * jumpHeight);
        }
    }

    void Move()
    {
        hp -= hpDecreasing;
        blood_osd.fillAmount = hp / hp_max;
        _animator.SetBool("isMoving", true);
        _animator.SetBool("isJumping", !isTouchingGround);
        transform.Translate(transform.right * movingSpeed / 10);
    }

    void Dead()
    {
        if (transform.position.y < deadHeight || hp <= 0)
        {
            Endgame(true);
        }
    }

    void Endgame(bool isDead)
    {
        isStop = true;
        Cursor.visible = true;
        _rigidbody.simulated = false;
        Transform end_scene = uiObject.GetChild(3);
        end_scene.GetChild(2).GetComponent<Text>().text = "獲得金幣：" + coinGet;
        if (isDead)
        {
            _animator.SetTrigger("trigDead");
            end_scene.GetChild(0).GetComponent<Text>().text = "恭喜你死了！";
        }
        else
        {
            end_scene.GetChild(0).GetComponent<Text>().text = "恭喜過關！";
        }
       end_scene.gameObject.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "地板")
        {
            isTouchingGround = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "地板")
        {
            isTouchingGround = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {        
        switch(collision.gameObject.tag)
        {
            case "地板":
                isTouchingGround = true;
                break;

            case "金幣":
                aud.PlayOneShot(getAudio);
                coinGet++;
                Destroy(collision.gameObject);
                uiObject.GetChild(0).gameObject.GetComponent<Text>().text = "金幣數量：" + coinGet;
                break;

            case "障礙":
                aud.PlayOneShot(hurtAudio);
                _animator.SetTrigger("trigHurt");
                hp -= hurt;
                Destroy(collision.gameObject);
                break;

            case "大障礙":
                aud.PlayOneShot(hurtAudio);
                _animator.SetTrigger("trigHurt");
                hp -= hurtBig;
                Destroy(collision.gameObject);
                break;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "傳送門")
        {
            Endgame(false);
        }
    }
}
