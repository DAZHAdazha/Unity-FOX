using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    private Rigidbody2D rb;
    // private Animator animator;
    public Transform leftpoint, rightpoint;
    public LayerMask Ground;
    private float leftx,rightx;
    private Collider2D coll;

    public float speed,jumpForce;

    private bool faceleft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();//调用父级的start
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //将左右两个点的子类分离
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // move();//以及在event中添加过了
        SwitchAnim();
    }

    void move(){
        if(faceleft){
            if(coll.IsTouchingLayers(Ground)){
                animator.SetBool("jumping",true);
                rb.velocity = new Vector2(-speed,jumpForce);
            }
            
            if(transform.position.x < leftx){
                transform.localScale = new Vector3(-1,1,1);
                faceleft = false; 
            }
        } else{
            if(coll.IsTouchingLayers(Ground)){
                animator.SetBool("jumping",true);
                rb.velocity = new Vector2(speed,jumpForce);
            }
            if(transform.position.x > rightx){
                transform.localScale = new Vector3(1,1,1);
                faceleft = true; 
            }
        }
    }

    void SwitchAnim(){
        if(animator.GetBool("jumping")){
            if(rb.velocity.y < 0.1){
                animator.SetBool("jumping",false);
                animator.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(Ground) && animator.GetBool("falling")){
            animator.SetBool("falling",false);
        }
    }

    

}
