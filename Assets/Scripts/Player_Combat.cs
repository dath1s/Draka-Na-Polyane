using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;
    public int damage = 1;
    public float knockbackForce = 10;
    public float knockbackTime = .15f;
    public float stunDuration = .3f;

    public Animator anim;

    public float cooldown = 1;
    private float timer;


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack() 
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = cooldown;
        }

    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
                enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunDuration);
            }

    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
