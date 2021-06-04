using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public float interval;
    protected float timer;
    public GameObject knifePrefab;
    // Start is called before the first frame update


    // Update is called once per frame
    protected virtual void Update()
    {
        Shoot();
    }

    protected virtual void Shoot()
    {
        // direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        // transform.right = direction;

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        if (Input.GetKeyDown("j"))
        {
            if (timer == 0)
            {
                timer = interval;
                Fire();
                soundManager.instance.shotAudioPlay();
            }
        }
    }
    protected virtual void Fire()
    {
        float faceDirection = transform.parent.parent.gameObject.transform.localScale.x;
        // GameObject knife = Instantiate(knifePrefab, transform.position,Quaternion.identity);

        GameObject knife = ObjectPool.Instance.GetObject((knifePrefab));
        knife.transform.position = transform.position;
        knife.transform.rotation = Quaternion.identity;

        knife.transform.Rotate(0,0,faceDirection);
        knife.GetComponent<knife>().SetSpeed(faceDirection,0f);
    }
}
