using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D body;
    public AudioClip hitClip;
    // Start is called before the first frame update
    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 moveDirection,float moveFore)
    {
        body.AddForce(moveDirection* moveFore);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if(ec!=null)
        {
            Debug.Log("碰到敌人了");
            ec.Fixed();
        }
        AudioManager.instance.AudioPlay(hitClip);
        Destroy(this.gameObject);
    }
}
