using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collection : MonoBehaviour
{
    public void death(){
        //调用playercontroller的函数，使樱桃数增加
        FindObjectOfType<playerController>().cherryCount();
        Invoke("callDestory",0.4f);
    }

    void callDestory(){
        Destroy(gameObject);
    }
}
