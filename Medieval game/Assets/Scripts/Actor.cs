using UnityEngine;
using TMPro;
public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public GameObject prefab;

    public Transform parentTransform = null;
    TextMeshProUGUI healthText;
    public Vector3 spawnPosition = new Vector3(0, 4f, 0);
    public Vector3 spawnRotation = new Vector3(0, 180f, 0);
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        healthText.text = currentHealth.ToString() + "hp";
        
        if(currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        //Death function
        // temporary, destroy enemy
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        //healthText.text = currentHealth.ToString() + "hp";
    }
    void Start()
    {
        try
        {

            
            Quaternion rotation = Quaternion.Euler(spawnRotation);
            Debug.Log(rotation);

            GameObject newObject;
            if (parentTransform != null)
            {
                newObject = Instantiate(prefab, spawnPosition, rotation);
                newObject.transform.SetParent(parentTransform, false);
                newObject.transform.Rotate(0, 180, 0);

                Transform healthTextTransform = newObject.transform.Find("HealthText");
                healthText = healthTextTransform.GetComponent<TextMeshProUGUI>();

                if (healthText)
                {
                    Debug.Log("Health text found");
                }
                
            }
            else
            {
                newObject = Instantiate(prefab, spawnPosition, rotation);
            }


            newObject.name = prefab.name;

            Debug.Log("healthbar created");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error creating prefab: " + ex.Message);
        }
    }
}

