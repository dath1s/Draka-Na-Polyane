using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockBack;

    public Player_Combat player_Combat;

    private void Update()
    {
        if(Input.GetButtonDown("Slash"))
        {
            player_Combat.Attack();
        }
    }

    // FizedUpdate is called 50x frame
    void FixedUpdate() 
    {
        if (isKnockBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 || 
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    public void Knockback(Transform enemy, float force, float stunDuration)
    {
        isKnockBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCount(stunDuration));

    }

    IEnumerator KnockbackCount(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
    }
}
