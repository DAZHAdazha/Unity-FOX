using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    //把不用经常更换的对象换成private 但是不能拖拽了 需要在start里面初始化
    [SerializeField]private Rigidbody2D rb; //加上[SerializeField]序列化可以让private变量也可也在Unity 中显示
    // private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private Animator animator;
    public LayerMask ground;
    public Collider2D coll;
    public Collider2D disColl;
    public int cheery;
    public Text cheeryNum;
    public bool hurt; // 默认为false
    public Transform cellingCheck;
    public Transform groundCheck; // 只要一个就ok了 需要设置到角色的左下边！！！因为角色转向后这个点也会转向！
    public bool isGround, isJump;
    bool jumpPressed,crouchPressed;
    int jumpCount = 2;
    public GameObject[] weapons;
    private int weaponNum;
    public int health = 3;
    private int hitNum = 0;
    public GameObject healthSystem;

    // public Joystick joystick;//手机端
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem.GetComponent<HealthSystem>().setHealth(health);
        healthSystem.GetComponent<HealthSystem>().UpdateHealthBar();
    }

    void Update() {
        if(Input.GetButtonDown("Jump") && jumpCount > 0){
            jumpPressed = true;
        }
        // if(joystick.Vertical>0.5f && jumpCount > 0){//-1f~1f 手机端
        //     jumpPressed = true;
        // }
        if(Input.GetButtonDown("Crouch")){
            crouchPressed = true;
        } else if(Input.GetButtonUp("Crouch")){
            crouchPressed = false;
        }
        // if(joystick.Vertical<-0.5f){//手机端
        //     crouchPressed = true;
        // } else {
        //     crouchPressed = false;
        // }

        SwitchGun();
    }

    void FixedUpdate() {
        isGround = Physics2D.OverlapCircle(groundCheck.position,0.1f,ground);

        
        if(!hurt){
            groundMovement();
            Crouch();
            jump();
        }
        SwitchAnim();
    }

    void Crouch(){
        if(!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground)){
            if(crouchPressed){
                animator.SetBool("crouching", true);
                animator.SetFloat("running", 0f);
                disColl.enabled = false;
            } else{
                animator.SetBool("crouching",false);
                disColl.enabled = true;
            }
        }
    }

    void groundMovement(){
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        // float horizontalMove = joystick.Horizontal;//-1f~1f 手机端
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        if(horizontalMove != 0){
            transform.localScale = new Vector3(horizontalMove,1,1);
        }
        // if(horizontalMove > 0){//手机端
        //     transform.localScale = new Vector3(1,1,1);
        // } else{
        //     transform.localScale = new Vector3(-1,1,1);
        // }
    }

    void jump(){
        if(isGround){
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed){
            // jumpAudio.Play();
            //引用自己写的soundManager代码, 但是很麻烦 所以直接用静态类
            // soundManager soundManager = gameObject.GetComponent<soundManager>();
            soundManager.instance.jumpAudioPlay();
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        } else if(jumpPressed && jumpCount > 0 && isJump){
            soundManager.instance.jumpAudioPlay();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }



    void SwitchAnim(){
        if(!animator.GetBool("crouching")){
            animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        }
        
        if(isGround){
            animator.SetBool("falling",false);
        } else if(!isGround && rb.velocity.y>0){
            animator.SetBool("jumping",true);
        } else if(rb.velocity.y < 0 ){
            animator.SetBool("jumping",false);
            animator.SetBool("falling",true);
        }
        if(hurt){
            animator.SetBool("hurt",true);
            animator.SetFloat("running",0);
        }
    }

    void recoverHurt(){
        animator.SetBool("hurt",false);
        hurt = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Collection"){
            soundManager.instance.cherryAudioPlay();
            other.GetComponent<Animator>().Play("collected");
        }
        if(other.tag == "Deadline"){
            //关闭所有音源
            GetComponent<AudioSource>().enabled = false;
            // 2s 后执行restart函数
            Invoke("restart",2f);
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D other) {
        
            if(other.gameObject.tag == "Enemies"){

                // Enemy_frog frog = other.gameObject.GetComponent<Enemy_frog>(); 

                if(animator.GetBool("falling") && transform.position.y > other.transform.position.y){
                    // frog.death();

                    Enemy enemy = other.gameObject.GetComponent<Enemy>();
                    enemy.death();
                    
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpCount = 2;
                    animator.SetBool("jumping",true);
                } else if(transform.position.x < other.gameObject.transform.position.x){
                    hitNum++;
                    healthSystem.GetComponent<HealthSystem>().TakeDamage(1f);
                    if(hitNum >= health){
                        GetComponent<AudioSource>().enabled = false;
                        Invoke("restart",0.2f);
                    }
                    //在敌人左侧
                    rb.velocity = new Vector2(-3,rb.velocity.y);
                    soundManager.instance.hurtAudioPlay();
                    hurt = true;
                    Invoke("recoverHurt",1f);
                } else if(transform.position.x > other.gameObject.transform.position.x){
                    hitNum++;
                    healthSystem.GetComponent<HealthSystem>().TakeDamage(1f);
                    if(hitNum >= health){
                        GetComponent<AudioSource>().enabled = false;
                        Invoke("restart",0.2f);
                    }
                    //在敌人左侧
                    rb.velocity = new Vector2(3,rb.velocity.y);
                    soundManager.instance.hurtAudioPlay();
                    hurt = true;
                    Invoke("recoverHurt",1f);
                }
            }
    }

    void restart(){
        //重新激活当前场景
        ObjectPool.Instance = null;//重置对象池
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void cherryCount(){
        cheery += 1;
        cheeryNum.text = cheery.ToString();
    }

    void SwitchGun(){
            if (Input.GetKeyDown(KeyCode.Q))
            {
                weapons[weaponNum].SetActive(false);
                if (--weaponNum < 0)
                {
                    weaponNum = weapons.Length - 1;
                }
                weapons[weaponNum].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                weapons[weaponNum].SetActive(false);
                if (++weaponNum > weapons.Length - 1)
                {
                    weaponNum = 0;
                }
                weapons[weaponNum].SetActive(true);
            }
        }

}
