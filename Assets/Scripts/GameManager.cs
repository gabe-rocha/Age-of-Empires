using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public delegate void OnGameStateChangedHandler();

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(GUIManager))]
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Initializing,
        Loading,
        MainMenu,
        Playing,
        GameOver,
        GameWin
    }

    public GameState gameState;


    //Game Specific Variables
    [HideInInspector] public GameObjectPooler playerUnitsPool, enemyUnitsPool, unitDestroyedParticlesPool, bulletsPool, laserRaysPool;

    //References to other Managers
    [HideInInspector] public InputManager inputManager;
    [HideInInspector] public SoundManager soundManager;
    [HideInInspector] public GatherablesManager gatherablesManager;
    [HideInInspector] public GUIManager GUIManager;

    //Private variables
    private GameObject _levelInstance;
    private GameObject _levelPrefab;

    public event OnGameStateChangedHandler OnGameStateChanged;
    public static GameManager Instance;

    private void OnEnable()
    {
        //EventManager.StartListening(GameData.EventTypes.TappedToPlay, OnTappedToPlay);
    }

    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        inputManager = GetComponent<InputManager>();
        soundManager = GetComponent<SoundManager>();

        GameData.LoadPlayerPrefs();

        SetGameState(GameState.Initializing);
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        //OnGameStateChanged();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => gatherablesManager && inputManager && soundManager && GUIManager);
        SetGameState(GameState.Playing);

        yield return new WaitForSeconds(2f);
        GatherableSO g = new GatherableSO
        {
            type = ResourceType.Wood,
            amountPerGather = 5
        };
        gatherablesManager.DeliverGatherable(g);
    }

    private void Update()
    {
        //I won't use a complex state machine for a simple game

        switch (gameState)
        {
            case GameState.Initializing:
                //Do whatever
                SetGameState(GameState.Loading);
                break;
            case GameState.Loading:
                //Show loading screen
                //_loadingScreen.setActive(true);

                //Load Game/Player data
                GameData.currentLevel = PlayerPrefs.GetInt("currentLevel") == 0 ? 1 : PlayerPrefs.GetInt("currentLevel"); //maybe 1st time playing
                    
                //Instantiate Level
                Destroy(_levelInstance); //destroy old level

                var levelName = $"Level{GameData.currentLevel}";
                //_levelPrefab = Resources.Load<GameObject>($"Levels/{levelName}");
                //if(_levelPrefab != null)
                //{
                //    _levelInstance = Instantiate(_levelPrefab);
                //}
                //else
                //{
                //    Debug.LogError($"Prefab {levelName} Not Found!");
                //}

                //_levelInstance.transform.Find("Floor").gameObject.SetActive(false);

                //EventManager.TriggerEvent(GameData.EventTypes.GameReady);
                SetGameState(GameState.MainMenu);
                break;
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                break;
            case GameState.GameWin:
                break;
            default:
                break;
        }
    }

    private void OnTappedToPlay()
    {
        if (gameState != GameState.MainMenu)
        {
            return;
        }
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        SetGameState(GameState.Playing);
        //EventManager.TriggerEvent(GameData.EventTypes.GameStarted);
    }

    public void ReloadScene()
    {
        //reset stuff from GameData
        GameData.ResetAllData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
