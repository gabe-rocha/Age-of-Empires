using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRTSUnit : MonoBehaviour
{
    protected enum UnitType {
        Gatherer,
        Soldier,
    }

    protected UnitType type;
    [SerializeField] protected GameObject mouseOverReticle, selectedReticle;
    private bool isSelected;


    private void Awake() {
        SetSelected(false);
    }

    public virtual void SetSelected(bool select) {
        isSelected = select;
        selectedReticle.SetActive(select);
        mouseOverReticle.SetActive(false);
    }

    protected virtual void OnMouseEnter() {
        if(!isSelected) {
            mouseOverReticle.SetActive(true);
        }
    }

    protected virtual void OnMouseExit() {
        if (!isSelected) {
            mouseOverReticle.SetActive(false);
        }
    }

}
