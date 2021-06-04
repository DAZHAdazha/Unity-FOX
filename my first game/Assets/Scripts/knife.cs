using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    // Start is called before the first frame update
    new private Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;
    
    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float faceDirection,float y)
    {
        rigidbody.velocity = new Vector2(speed * faceDirection,rigidbody.velocity.y + y);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.tag=="Grid" || other.tag=="Enemies"){
        if(other.tag=="Enemies"){
            // Destroy(gameObject);

            rigidbody.velocity = new Vector2(0,0);
            animator.Play("explode");
            soundManager.instance.explodeAudioPlay();
            // ObjectPool.Instance.PushObject(gameObject);//移动到动画结束执行
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Area"){
            // Destroy(gameObject);
            //!!!对象池对象不能多次删除！
            if(gameObject.activeSelf){
                ObjectPool.Instance.PushObject(gameObject);
            }
           
        }
    }

    public void death(){
        animator.StopPlayback();
        ObjectPool.Instance.PushObject(gameObject);
    }
}
