using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public int Health = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = Health;
        Debug.Log(gameObject.name + " health initialized to " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log(gameObject.name + " took " + amount + " damage, current health: " + currentHealth);
    }

    void Die()
    {
        // Disable the tank or play explosion
        Debug.Log(gameObject.name + " is destroyed!");
        gameObject.SetActive(false);
        Destroy(gameObject, 1f); // destroy after 1 second
    }
}
