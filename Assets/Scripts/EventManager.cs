using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public delegate void OnSomethingHandler(GameObject go);


public class EventManager : MonoBehaviour
{
    private Dictionary<GameData.EventTypes, UnityEvent> simpleEventDictionary;
    private Dictionary<GameData.EventTypes, UnityEvent<GameObject>> paramGOEventDictionary;
    private Dictionary<GameData.EventTypes, UnityEvent<Vector3>> paramVec3EventDictionary;

    public event OnSomethingHandler OnSomething;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {

        if (simpleEventDictionary == null)
        {
            simpleEventDictionary = new Dictionary<GameData.EventTypes, UnityEvent>();
        }
        if (paramGOEventDictionary == null)
        {
            paramGOEventDictionary = new Dictionary<GameData.EventTypes, UnityEvent<GameObject>>();
        }
        if (paramVec3EventDictionary == null)
        {
            paramVec3EventDictionary = new Dictionary<GameData.EventTypes, UnityEvent<Vector3>>();
        }
    }

    public void Test()
    {
        OnSomething(this.gameObject);
    }


    //========================
    public static void StartListening(GameData.EventTypes eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (Instance.simpleEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.simpleEventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListeningWithGOParam(GameData.EventTypes eventName, UnityAction<GameObject> listener)
    {
        UnityEvent<GameObject> thisParamEvent = null;
        if (Instance.paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.AddListener(listener);
        }
        else
        {
            thisParamEvent = new UnityEvent<GameObject>();
            thisParamEvent.AddListener(listener);
            Instance.paramGOEventDictionary.Add(eventName, thisParamEvent);
        }
    }

    public static void StartListeningWithVec3Param(GameData.EventTypes eventName, UnityAction<Vector3> listener)
    {
        UnityEvent<Vector3> thisParamEvent = null;
        if (Instance.paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.AddListener(listener);
        }
        else
        {
            thisParamEvent = new UnityEvent<Vector3>();
            thisParamEvent.AddListener(listener);
            Instance.paramVec3EventDictionary.Add(eventName, thisParamEvent);
        }
    }

    //========================
    public static void StopListening(GameData.EventTypes eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (Instance.simpleEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListeningWithGOParam(GameData.EventTypes eventName, UnityAction<GameObject> listener)
    {
        if (eventManager == null) return;
        UnityEvent<GameObject> thisParamEvent = null;
        if (Instance.paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.RemoveListener(listener);
        }
    }

    public static void StopListeningWithVec3Param(GameData.EventTypes eventName, UnityAction<Vector3> listener)
    {
        if (eventManager == null) return;
        UnityEvent<Vector3> thisParamEvent = null;
        if (Instance.paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.RemoveListener(listener);
        }
    }


    //========================
    public static void TriggerEvent(GameData.EventTypes eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.simpleEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEventWithGOParam(GameData.EventTypes eventName, GameObject go)
    {
        UnityEvent<GameObject> thisParamEvent = null;
        if (Instance.paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.Invoke(go);
        }
    }

    public static void TriggerEventWithVec3Param(GameData.EventTypes eventName, Vector3 vec3)
    {
        UnityEvent<Vector3> thisParamEvent = null;
        if (Instance.paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent))
        {
            thisParamEvent.Invoke(vec3);
        }
    }
}
