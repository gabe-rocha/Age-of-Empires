using UnityEngine;

public class EventManager : MonoBehaviour
{
    //EVENTS GOES HERE
    public delegate void OnGatherablesAmountChangedHandler();
    public event OnGatherablesAmountChangedHandler OnGatherablesAmountChanged;

    
    //VARIABLES
    public static EventManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void GatherablesAmountChanged() {
        OnGatherablesAmountChanged();
    }
}
