using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public PlayerMotor player;
    public Image slideImage;
    float r;
    float g;
    float b;
    float dividedHealth;

    void Start()
    {
        healthBar.value = 1;
    }

    void FixedUpdate()
    {
        dividedHealth = player.currentHealth / 100f;
        healthBar.value = dividedHealth;

        UpdateTextColour();
    }

    void UpdateTextColour()
    {
        r = 255f - (2.55f * player.currentHealth); // as health gets lower, increase RED value
        g = 2.55f * player.currentHealth; // as health gets lower, decrease GREEN value

        r = Mathf.Clamp(r, 0, 255) / 255f;
        g = Mathf.Clamp(g, 0, 255) / 255f;
        b = 0;

        Color newColor = new Color(r, g, b);
        if (slideImage)
        {
            slideImage.color = newColor;
        }
        else
        {
            
        }
    }    
}
