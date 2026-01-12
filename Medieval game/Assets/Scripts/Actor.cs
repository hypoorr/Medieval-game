using UnityEngine;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        
        if(currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        //Death function
        // temporary, destroy enemy
        Destroy(gameObject);
    }
}
