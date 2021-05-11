using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake() {
        
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public static class PlayerEvents {

        public delegate void OnPlayerPressedAttack01Handler();
        public static event OnPlayerPressedAttack01Handler OnPlayerPressedAttack01;
        public static void PlayerPressedAttack01() {
            OnPlayerPressedAttack01?.Invoke();
        }

        public delegate void OnPlayerPressedJumpHandler();
        public static event OnPlayerPressedJumpHandler OnPlayerPressedJump;

        public static void PlayerPressedJump() {
            OnPlayerPressedJump?.Invoke();
        }

        public delegate void OnPlayerStoppedMovingHandler();
        public static event OnPlayerStoppedMovingHandler OnPlayerStoppedMoving;

        public static void PlayerStoppedMoving() {
            OnPlayerStoppedMoving?.Invoke();
        }
    }

    public static class WorldEvents {

        public delegate void OnGatherablesAmountChangedHandler();
        public static event OnGatherablesAmountChangedHandler OnGatherablesAmountChanged;

        public static void GatherablesAmountChanged() {
            OnGatherablesAmountChanged?.Invoke();
        }
    }

    

    
}
