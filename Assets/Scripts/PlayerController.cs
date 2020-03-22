using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rigidbody2;
    public int maxHealth = 5;
    public int currentHealth;
    private float invincibleTime = 2;
    private Animator anim;
    private float invincibleTimer;
    public AudioClip hitClip;
    public AudioClip launchClip;
    public int curBulletCount;
    public int maxBulletCount = 99;
    private bool isInvincible;
    public GameObject bulletPrefab; 
    private Vector2 lookDirection = new Vector2(1,0);
    // Start is called before the first frame update
    void Start()
    {
        curBulletCount = 2;
        invincibleTimer = 0;
        currentHealth = 2;
        rigidbody2 = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        UIManager.instance.UpdateHealthBar(currentHealth, this.maxHealth);
        UIManager.instance.UpdateBulletCount(curBulletCount,maxBulletCount);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.right * speed * Time.deltaTime);
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveVector = new Vector2(moveX,moveY);
        if(moveVector.x!=0||moveVector.y!=0)
        {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);
        if (moveX!=0|| moveY!=0)
        {
            Vector2 position = rigidbody2.position;
            position.x += Time.deltaTime * moveX * speed;
            position.y += Time.deltaTime * moveY * speed;
            rigidbody2.position = position;
        }
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer<0)
            {
                isInvincible = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.J)&&curBulletCount>0)
        {
            ChangeBulletCount(-1);
            anim.SetTrigger("Launch");
            AudioManager.instance.AudioPlay(launchClip);
            GameObject bullet = Instantiate(bulletPrefab, rigidbody2.position+Vector2.up*0.5f,Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if(bc!=null)
            {
                bc.Move(lookDirection,300);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2.position,lookDirection,2f,LayerMask.GetMask("NPC"));
            if(hit.collider!=null)
            {
                Debug.Log("NPC");
                NPCManager npc = hit.collider.GetComponent<NPCManager>();
                if(npc!=null)
                {
                    npc.ShowDialog();
                }

            }

        }
    }

    public void ChangeHealth(int amount)
    {
        if(amount<0)
        { 
            if(isInvincible==true)
            {
                return;
            }
            AudioManager.instance.AudioPlay(hitClip);
            anim.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
        
        Debug.Log("before "+currentHealth);
        currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
        Debug.Log("before " + currentHealth);
        UIManager.instance.UpdateHealthBar(currentHealth, this.maxHealth);
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount+amount,0,maxBulletCount);
        UIManager.instance.UpdateBulletCount(curBulletCount,maxBulletCount);
    }
}
