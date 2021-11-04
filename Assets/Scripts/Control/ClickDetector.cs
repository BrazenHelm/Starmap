using UnityEngine;

public abstract class ClickDetector : MonoBehaviour {

    const float DOUBLE_CLICK_DELAY = 0.4f;  // in seconds

    bool clicked = false;
    float lastClickTime = 0f;

    private void OnMouseDown() {
        if (!clicked) {
            OnSingleClick();
            clicked = true;
            lastClickTime = Time.time;
        }
        else if (Time.time - lastClickTime < DOUBLE_CLICK_DELAY) {
            OnDoubleClick();            
            clicked = false;
        }
        else {
            OnSingleClick();
            lastClickTime = Time.time;
        }
    }

    protected abstract void OnSingleClick();
    protected abstract void OnDoubleClick();

}
