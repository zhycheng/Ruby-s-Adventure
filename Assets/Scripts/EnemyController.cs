using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    public float speed = 3;
    private Rigidbody2D body;
    public bool isVertical;
    public Vector2 moveDirection;
    public float changeDirectionTime = 2f;
    public float changeTimer;
    public ParticleSystem brokenEffect;
    public AudioClip fixedClip;
    private bool isFixed;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeTimer = changeDirectionTime;
        anim = this.GetComponent<Animator>();
        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFixed)
        {
            return;
        }
        changeTimer -= Time.deltaTime;
        if(changeTimer<0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }
        Vector2 position = body.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        body.MovePosition(position);
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if(pc!=null)
        {
            pc.ChangeHealth(-1);
        }
    }

    public void Fixed()
    {
        anim.SetTrigger("fix");
        body.simulated=false;
        isFixed = true;
        AudioManager.instance.AudioPlay(fixedClip);
        if(brokenEffect.isPlaying==true)
        {
            brokenEffect.Stop();
        }
    
    }
}
