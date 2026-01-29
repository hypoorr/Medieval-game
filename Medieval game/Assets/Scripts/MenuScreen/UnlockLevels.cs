using UnityEngine;
using UnityEngine.UI;
public class UnlockLevels : MonoBehaviour
{
    public static bool unlockedMedium;
    public static bool unlockedHard;
    public static bool unlockedBoss;

    public Image mediumLockImage;
    public Image hardLockImage;
    public Image bossLockImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //unlockedBoss = true;
    }

    public static void UnlockMedium()
    {
        unlockedMedium = true;
    }

    public static void UnlockHard()
    {
        unlockedHard = true;
    }
    public static void UnlockBoss()
    {
        unlockedBoss = true;
    }


    void FixedUpdate()
    {
        if (unlockedMedium)
        {
            mediumLockImage.GetComponent<Image>().enabled = false;
        }
        if (unlockedHard)
        {
            hardLockImage.GetComponent<Image>().enabled = false;
        }
        if (unlockedBoss)
        {
            bossLockImage.GetComponent<Image>().enabled = false;
        }
    }
}
