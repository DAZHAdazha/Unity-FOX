using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //使用protected 使得只有子类能够使用
    protected Animator animator;
    protected AudioSource deathAudio;

    //Virtual使 start()可以被override
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void death(){
        // 死亡时不会二次碰撞
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void jumpOn(){
        deathAudio.Play();
        animator.SetTrigger("death");
    }
}
