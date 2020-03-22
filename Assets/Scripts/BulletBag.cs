using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public ParticleSystem collectEffect;
    public int bulletCount=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if(pc!=null)
        {
            if(pc.curBulletCount<pc.maxBulletCount)
            {
                Instantiate(collectEffect,transform.position,Quaternion.identity);
                pc.ChangeBulletCount(bulletCount);
                Destroy(this.gameObject);
            }
           
        }
    }
}
