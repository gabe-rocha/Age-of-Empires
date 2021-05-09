using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class RTSController : MonoBehaviour{

    [SerializeField] private RectTransform selectionAreaImage;
    private Vector3 clickStartPositionWorld, clickEndPositionWorld;
    private Vector3 clickStartPositionScreen, clickEndPositionScreen;
    private List<BaseRTSUnit> selectedUnits = new List<BaseRTSUnit>();

    private void Start() {
        selectionAreaImage.gameObject.SetActive(false);
    }

    void Update(){

        if (Input.GetMouseButtonDown(0)) {
            clickStartPositionWorld = UtilsClass.GetMouseWorldPosition3D();
            clickStartPositionScreen = Input.mousePosition;
            selectionAreaImage.gameObject.SetActive(true);
        }

        if(Input.GetMouseButton(0)) {
            //Resize the Selection Area
            Vector3 currentMousePosition = Input.mousePosition; //we do not convert to world coordinates because hte Selection Area is a UI element
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(clickStartPositionScreen.x, currentMousePosition.x),
                Mathf.Min(clickStartPositionScreen.y, currentMousePosition.y));
            Vector3 upperRight = new Vector3(
                Mathf.Max(clickStartPositionScreen.x, currentMousePosition.x),
                Mathf.Max(clickStartPositionScreen.y, currentMousePosition.y));

            selectionAreaImage.position = lowerLeft;
            selectionAreaImage.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0)) {
            selectionAreaImage.gameObject.SetActive(false);
            foreach (BaseRTSUnit selectedUnit in selectedUnits) {
                selectedUnit.SetSelected(false);
            }
            selectedUnits.Clear();
            clickEndPositionWorld = UtilsClass.GetMouseWorldPosition3D();
            clickEndPositionScreen = Input.mousePosition;
            Vector3 middlePoint = Vector3.Lerp(clickStartPositionWorld, clickEndPositionWorld, 0.5f);

            //This is going to bring back all Colliders between clickStartPosition and clickEndPosition
            var colliders = Physics.OverlapSphere(middlePoint, Vector3.Distance(clickStartPositionWorld, clickEndPositionWorld) / 2);

            foreach (Collider collider in colliders) {
                //here we make sure we're selecting only units, not rocks, trees, etc...
                BaseRTSUnit unit = collider.gameObject.GetComponent<BaseRTSUnit>();
                if(unit != null) {
                    selectedUnits.Add(unit);
                    unit.SetSelected(true);
                }
            }
            Debug.Log($"{selectedUnits.Count}");
        }
    }
}
