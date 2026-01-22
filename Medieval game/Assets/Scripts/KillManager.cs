using UnityEngine;

public class KillManager : MonoBehaviour
{
    public static int TotalEnemiesDefeated;

    public static void PrintEnemyCount()
    {
        Debug.Log($"Enemies defeated: {TotalEnemiesDefeated}");
    }
}
