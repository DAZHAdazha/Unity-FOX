using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot3 : shot
{
    public int bulletNum = 3;
    public float bulletAngle = 15;
    // Start is called before the first frame update
    protected override void Fire()
    {

        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            float faceDirection = transform.parent.parent.gameObject.transform.localScale.x;

            // GameObject knife = Instantiate(knifePrefab, transform.position,Quaternion.identity);
            
            GameObject knife = ObjectPool.Instance.GetObject((knifePrefab));
            knife.transform.position = transform.position;
            knife.transform.rotation = Quaternion.identity;

            knife.transform.Rotate(0,0,faceDirection);
            if (bulletNum % 2 == 1)
            {
                knife.GetComponent<knife>().SetSpeed(faceDirection,bulletAngle * (i - median));
                knife.transform.Rotate(0,0,bulletAngle * (i - median)* faceDirection);
            }
            else
            {
                knife.GetComponent<knife>().SetSpeed(faceDirection,bulletAngle * (i - median) + bulletAngle / 2);
                knife.transform.Rotate(0,0,(bulletAngle * (i - median) + bulletAngle / 2)* faceDirection);
            }
        }
        
    }

}
