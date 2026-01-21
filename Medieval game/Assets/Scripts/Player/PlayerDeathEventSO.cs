using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerDeathEventSO", menuName = "ScriptableObjects/Player")]
public class PlayerDeathEventSO : ScriptableObject
{
    [SerializeField] UnityEvent playerDeathEvent;

    void Raise()
    {
        
    }

}
