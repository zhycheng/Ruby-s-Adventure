using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ParticleSystem collectEffect;
    public AudioClip collectClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc!=null)
        {
            Debug.Log("玩家碰到了草莓");
            if(pc.currentHealth<pc.maxHealth)
            {
                Instantiate(collectEffect, this.transform.position, Quaternion.identity);
                pc.ChangeHealth(1);
                Destroy(this.gameObject);
                AudioManager.instance.AudioPlay(collectClip);
            }

        }
    }
}
