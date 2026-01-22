using UnityEngine;

public class OtherClass : MonoBehaviour
{
    void Start()
    {
        KillManager.TotalEnemiesDefeated++;
        KillManager.PrintEnemyCount();
    }
}
