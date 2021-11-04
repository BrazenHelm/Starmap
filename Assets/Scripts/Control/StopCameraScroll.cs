using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopCameraScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData data) {
        CameraController.allowsScrollInput = false;
    }

    public void OnPointerExit(PointerEventData data) {
        CameraController.allowsScrollInput = true;
    }

}
