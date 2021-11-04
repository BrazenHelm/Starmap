using UnityEngine;

public class TabHider : MonoBehaviour {

    public void Show(GameObject tab) {
        tab.SetActive(true);
    }

    public void Hide(GameObject tab) {
        tab.SetActive(false);
        CameraController.allowsScrollInput = true;
        CameraController.allowsKeyboardInput = true;
    }

}
