using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSetup : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject targetObject;

    [Header("Level settings")]
    [SerializeField]
    private int enemyCount;
    private bool hasWin;
    private bool hasLose;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private int currentEnemyCount;

    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private PlayerDeathEventSO playerDeathEvent;






    //Setup enemy spawns



    void Awake()
    {
        currentEnemyCount = enemyCount;
    }

    void Start()
    {
        hasWin = false;

        // Get cube bounds
        Renderer rend = targetObject.GetComponent<Renderer>();

        Bounds bounds = rend.bounds;


        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 point = Vector3.zero;
            point = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y, Random.Range(bounds.min.z, bounds.max.z));
            gameObject.transform.position = point;
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform);
            enemy.transform.SetParent(null, true);
            enemy.SetActive(true);
            enemy.GetComponent<Actor>().enabled = true;
        }

    }

    public void UpdateEnemyCount()
    {
        currentEnemyCount -= 1;
        Debug.Log(currentEnemyCount);
    }

    void FixedUpdate()
    {
        if (currentEnemyCount == 0)
        {
            if (!hasWin)
            {
                hasWin = true;
                Debug.Log("win");
                player.SetActive(false);
                winScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                // unlock levels

                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    UnlockLevels.UnlockMedium();
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    UnlockLevels.UnlockHard();
                }
            }

        }

        if (playerMotor.currentHealth <= 0)
        {
            if (!hasLose)
            {

                playerDeathEvent.Raise();

            }
        }
    }
}
