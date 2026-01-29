using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        if (UnlockLevels.unlockedMedium)
        {
            SceneManager.LoadScene("Level2");
        }
        
    }
    public void Level3()
    {
        if (UnlockLevels.unlockedHard)
        {
            SceneManager.LoadScene("Level3");
        }
    }
    public void Level4()
    {
        if (UnlockLevels.unlockedBoss)
        {
            SceneManager.LoadScene("Level4");
        }
    }
}
