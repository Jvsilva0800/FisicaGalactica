using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public event Action OnDead;
    public event Action OnHurt;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHurt?.Invoke();
        }

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void Heal(int heal)//Função responsável por curar o player
    {
        currentHealth += heal;
        healthBar.SetCurrentHealth(currentHealth);
    }

}
