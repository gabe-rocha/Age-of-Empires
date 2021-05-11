using UnityEngine;
using TMPro;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    public TextMeshProUGUI textFoodAmount, textWoodAmount, textStoneAmount, textGoldAmount;

    private GatherablesManager gatherablesManager;

    private void Awake()
    {
        GameManager.Instance.GUIManager = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance.gatherablesManager);

        gatherablesManager = GameManager.Instance.gatherablesManager;
        EventManager.WorldEvents.OnGatherablesAmountChanged += OnGatherablesAmountChanged;
    }

    private void OnGatherablesAmountChanged()
    {
        textFoodAmount.text = $"Food: {gatherablesManager.foodAmount}";
        textWoodAmount.text = $"Wood: {gatherablesManager.woodAmount}";
        textStoneAmount.text = $"Stone: {gatherablesManager.stoneAmount}";
        textGoldAmount.text = $"Gold: {gatherablesManager.goldAmount}";
    }
}