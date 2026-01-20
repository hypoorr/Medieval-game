using UnityEngine;
public class LevelSetup : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject targetObject;

    [Header("Level settings")]
    [SerializeField]
    private int enemyCount;




    //Setup enemy spawns


    

    void Start()
    {

        // Get cube bounds
        Renderer rend = targetObject.GetComponent<Renderer>();

        Bounds bounds = rend.bounds;

        for(int i = 0; i < enemyCount; i++)
        {
            Vector3 point = Vector3.zero;
            point = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y, Random.Range(bounds.min.z, bounds.max.z));
            gameObject.transform.position = point;
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform);
            enemy.transform.SetParent(null, true);
            enemy.GetComponent<Actor>().enabled = true;
        }

        
    }
}
