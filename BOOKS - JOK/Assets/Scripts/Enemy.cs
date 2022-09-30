using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startHealth = 3;
    private int health;
    public float speed;
    private float dazedTime;
    public float startDazedTime;

    public LayerMask whatIsGround;
    public LayerMask isPlayer;
    

    Rigidbody2D rb;
    Transform trans;
    float width;

    private void Start()
    {
        trans = transform;
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        width = mySprite.bounds.extents.x;

        health = startHealth;
    }

    private void FixedUpdate()
    {
        //Check ground below
        Vector2 raycastPos = trans.position - trans.right * width;
        Debug.DrawLine(raycastPos, raycastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(raycastPos, raycastPos + Vector2.down, whatIsGround);
        Debug.DrawLine(raycastPos, raycastPos - trans.right.toVector2() * 0.05f);
        bool isBlocked = Physics2D.Linecast(raycastPos, raycastPos - trans.right.toVector2() * 0.05f, whatIsGround);
        
        //If no ground or is blocked, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currentRot = trans.eulerAngles;
            currentRot.y += 180;
            trans.eulerAngles = currentRot;
        }

        //Always move forward
        Vector2 myVel = rb.velocity;
        myVel.x = -trans.right.x * speed;
        rb.velocity = myVel;
    }

    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        //play a hurt sound
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage TAKEN");
        Debug.Log(health);
    }
}
