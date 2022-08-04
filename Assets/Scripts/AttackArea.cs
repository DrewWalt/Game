using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 10;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //animator.SetTrigger("Attack");
        Health health = collider.GetComponent<Health>();
        health.Damage(damage);
    }
}
