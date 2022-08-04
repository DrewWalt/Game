using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    public static Action OnPlayerDeath;
    public static Action OnEnemyDeath;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            if (health + Mathf.Abs(amount) > MAX_HEALTH)
            {
                this.health = MAX_HEALTH;
            }
            else
            {
                this.health += Mathf.Abs(amount);
            }
        }

        this.health -= amount;
        StartCoroutine(VisualIndicator(Color.red)); 
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        if(this.CompareTag("Player"))
        {
            Time.timeScale = 0;
            OnPlayerDeath?.Invoke();
        }
        else
        {
            OnEnemyDeath?.Invoke();
        }
    }
}
