using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public delegate void OnGatherablesAmountChangedHandler();

public class GatherablesManager : MonoBehaviour
{
    public int foodAmount, woodAmount, stoneAmount, goldAmount;

    public event OnGatherablesAmountChangedHandler OnGatherablesAmountChanged;

    private void Awake()
    {
        GameManager.Instance.gatherablesManager = this;
    }

    public void DeliverGatherable(GatherableSO gatherable)
    {
        switch (gatherable.type)
        {
            case ResourceType.Food:
                foodAmount += gatherable.amountPerGather;
                break;
            case ResourceType.Wood:
                woodAmount += gatherable.amountPerGather;
                break;
            case ResourceType.Stone:
                stoneAmount += gatherable.amountPerGather;
                break;
            case ResourceType.Gold:
                goldAmount += gatherable.amountPerGather;
                break;
        }

        OnGatherablesAmountChanged();
    }
}
