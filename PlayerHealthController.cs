using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int MaxHealht = 100;

    private int CurrentHealth;

    private bool isDead = false;

    void Start()
    {
        CurrentHealth = MaxHealht;
    }

    public void TakeDamage(int damageAmount)
    {

        if (isDead) return;

        CurrentHealth -= damageAmount;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            isDead = true;
            HandleDeath();

        }



    }

    private void HandleDeath()
    {
        GameManager.GMinstance.DiePlayer();
        print("Player Öldü");
    }

    public void Respawn()
    {
        CurrentHealth = MaxHealht;
        isDead = false;

        transform.position = GameManager.GMinstance.GetLastChecpointPosition();
    }
}
