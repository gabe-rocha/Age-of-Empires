using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _previousMousePosition;
    private void Update()
    {
        //MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            GameObject tower = GetTowerTouched(Input.mousePosition);
            GameObject button = GetButtonTouched(Input.mousePosition);

            //if (tower != null)
            //{
            //    EventManager.TriggerEventWithGOParam(GameData.EventTypes.TowerTouched, tower);
            //}
            //else if(tower == null && button == null)
            //{
            //    //touched the board
            //    EventManager.TriggerEvent(GameData.EventTypes.BoardTouched);
            //}
        }

        else if (Input.GetMouseButton(0) && Input.mousePosition != _previousMousePosition) //Dragging
        {
            //EventManager.TriggerEventWithVec3Param(GameData.EventTypes.DraggingAnywhereOnScreen, Input.mousePosition);

            //GameObject tower = GetTowerTouched(Input.mousePosition);
            //GameObject button = GetButtonTouched(Input.mousePosition);

            //if (tower != null)
            //{
            //    EventManager.TriggerEventWithGOParam(GameData.EventTypes.TowerTouchedWhileDragging, tower);
            //}
            //else if (tower == null && button == null)
            //{
            //    EventManager.TriggerEvent(GameData.EventTypes.DraggingOverTheBoard);
            //}
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //GameObject tower = GetTowerTouched(Input.mousePosition);
            //GameObject button = GetButtonTouched(Input.mousePosition);

            //if (tower != null)
            //{
            //    EventManager.TriggerEventWithGOParam(GameData.EventTypes.TouchReleasedOnATower, tower);
            //}
            //else if (button != null)
            //{
            //    if (button.CompareTag("Button Upgrade"))
            //        EventManager.TriggerEventWithGOParam(GameData.EventTypes.UpgradeButtonTouched, button);
            //}
            //else if (tower == null && button == null)
            //{
            //    EventManager.TriggerEvent(GameData.EventTypes.BoardTouched);
            //}
        }
        _previousMousePosition = Input.mousePosition;

    }

    private GameObject GetTowerTouched(Vector3 pos)
    {
        //Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        //RaycastHit2D hitInfo = Physics2D.Raycast(worldPoint, Vector2.zero);
        //if (hitInfo.collider != null)
        //{
        //    var towerGO = hitInfo.collider.gameObject;

        //    towerGO.TryGetComponent(out Tower _tower);
        //    if (_tower == null)
        //    {
        //        //we might've touched the "Touch Collider GameObject, child of the Tower, so let's check the parent"
        //        towerGO.transform.parent.gameObject.TryGetComponent(out Tower _colliderParent);
        //        if (_colliderParent == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            if (_colliderParent.CompareTag("Player") || _colliderParent.CompareTag("Enemy") || _colliderParent.CompareTag("Alien"))
        //            {
        //                //Debug.Log($"Tower Touch Collider Obj touched");
        //                return towerGO.transform.parent.gameObject;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_tower.CompareTag("Player") || _tower.CompareTag("Enemy") || _tower.CompareTag("Alien"))
        //        {
        //            //Debug.Log($"Tower Touched");

        //            return towerGO;
        //        }
        //    }
        //}
           
        return null;
    }

    private GameObject GetButtonTouched(Vector3 pos)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        RaycastHit2D hitInfo = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hitInfo.collider != null)
        {
            GameObject buttonGO = hitInfo.collider.gameObject;
            if (buttonGO.CompareTag("Button Upgrade"))// || buttonGO.CompareTag("TransformAttack") || buttonGO.CompareTag("Alien"))
            {
                //Debug.Log($"Button Touched: {buttonGO.name}");
                return buttonGO;
            }
        }
        return null;
    }
}
