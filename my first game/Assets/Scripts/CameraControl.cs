using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        //注意是小写的transform是当前物体 即 maincamera的transform
        transform.position = new Vector3(player.position.x, 0, -10);
    }
}
