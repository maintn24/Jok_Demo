using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask isEnemies;
    //public Animator camAnim;
    //public Animator playerAnim;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;

    private void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //attack
            if (Input.GetKey(KeyCode.Space))
            {
                timeBtwAttack = startTimeBtwAttack;
                //camAnim.SetTrigger("shake");
                //playerAnim.SetTrigger("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,new Vector2(attackRangeX, attackRangeY), 0, isEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
