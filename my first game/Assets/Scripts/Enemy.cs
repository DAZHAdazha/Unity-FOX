using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //使用protected 使得只有子类能够使用
    protected Animator animator;
    protected AudioSource deathAudio;
    // protected int health;
    public float health;
    protected int hitNum = 0;

    public GameObject healthSystem;

    //Virtual使 start()可以被override
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        healthSystem.GetComponent<HealthSystem>().setHealth(health);
        healthSystem.GetComponent<HealthSystem>().UpdateHealthBar();
    }

    public void destory(){
        Destroy(gameObject);
    }

    public void death(){
        deathAudio.Play();
        animator.SetTrigger("death");
        // 死亡时不会二次碰撞
        GetComponent<Collider2D>().enabled = false;
        //禁止物体移动 防止敌人掉下去
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Destroy(healthSystem);
    }

    public void addHitNum(int value){
        hitNum += value;
        healthSystem.GetComponent<HealthSystem>().TakeDamage(1f);
    }

    public int getHitNum(){
        return hitNum;
    }

    public float getHealth(){
        return health;
    }

}
