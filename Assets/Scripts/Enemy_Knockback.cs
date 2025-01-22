using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;

    private Enemy_Movement enemy_Movement;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();

        enemy_Movement = GetComponent<Enemy_Movement>();

    }
    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunDuration)
    {
        enemy_Movement.ChangeState(EnemyState.Knockbacking);
        StartCoroutine(StunTimer(knockbackTime, stunDuration));

        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
    }

    IEnumerator StunTimer(float knockbackTime, float stunDuration)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
