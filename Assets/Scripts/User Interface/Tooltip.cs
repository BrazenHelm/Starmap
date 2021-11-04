using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    static Tooltip instance;
    Text tooltipText;
    RectTransform textRectTransform;
    RectTransform backgroundRectTransform;

    private void Awake() {
        instance = this;
        tooltipText = transform.Find("Text").GetComponent<Text>();
        textRectTransform = tooltipText.GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        Hide();
    }

    private void Update() {
        transform.position = Input.mousePosition;
    }

    private void Show(string text) {
        gameObject.SetActive(true);

        tooltipText.text = text;
        float textPadding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPadding * 2, tooltipText.fontSize + textPadding * 2);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip(string text) {
        instance.Show(text);
    }

    public static void HideTooltip() {
        instance.Hide();
    }

}
