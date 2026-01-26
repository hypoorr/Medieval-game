using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private PlayerDeathEventSO playerDeathEvent;
    private bool hasLose;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject player;


    void Start()
    {
        hasLose = false;
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        playerDeathEvent.playerDeathEvent.AddListener(Death);
    }

    private void OnDisable()
    {
        playerDeathEvent.playerDeathEvent.RemoveListener(Death);
    }

    private void Death()
    {
        hasLose = true;
        player.SetActive(false);
        loseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
