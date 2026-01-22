using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerDeathEventSO", menuName = "ScriptableObjects/Player")]
public class PlayerDeathEventSO : ScriptableObject
{
    [SerializeField] public UnityEvent playerDeathEvent;

    public void Raise()
    {
        playerDeathEvent?.Invoke();
    }

}
