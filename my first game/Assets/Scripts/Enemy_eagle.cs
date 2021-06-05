using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{

    private Rigidbody2D rb;
    // private Collider2D coll;
    public float speed;
    public float topY, bottomY;
    public Transform Top,Bottom;
    private bool isUp = true;
    protected override void Start()
    {
        base.Start();//调用父级的start
        rb = GetComponent<Rigidbody2D>();
        // coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        topY = Top.position.y;
        bottomY = Bottom.position.y;
        Destroy(Top.gameObject);
        Destroy(Bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move(){
        if(isUp){
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > topY){
                isUp = false;
            }
        } else{
            rb.velocity = new Vector2(rb.velocity.x,-speed);
            if(transform.position.y < bottomY){
                isUp = true;
            }
        }
    }
}
