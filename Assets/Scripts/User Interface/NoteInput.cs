using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    const float EDIT_HEIGHT = 120f;

    [SerializeField] GameObject mainTab = null;
    [SerializeField] GameObject editTab = null;
    [SerializeField] GameObject optionsOverlay = null;
    [SerializeField] InputField inputField = null;
    [SerializeField] Text mainText = null;
    public string text {
        get {
            return mainText.text;
        }
        set {
            mainText.text = value;
            inputField.text = value;
        }
    }

    NoteTaker noteTaker;
    RectTransform rect;

    private void Awake() {
        noteTaker = FindObjectOfType<NoteTaker>();
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData data) {
        if (!editTab.activeInHierarchy)
            optionsOverlay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data) {
        optionsOverlay.SetActive(false);
    }

    public void StartEditing() {
        editTab.SetActive(true);
        CameraController.allowsKeyboardInput = false;
        inputField.text = mainText.text;
        mainTab.SetActive(false);
        optionsOverlay.SetActive(false);
        SetHeightToEdit();
    }

    public void DoneEditing() {
        mainTab.SetActive(true);
        mainText.text = inputField.text;
        editTab.SetActive(false);
        CameraController.allowsKeyboardInput = true;
        ScaleHeightWithText();

        int i = gameObject.transform.GetSiblingIndex();
        noteTaker.UpdateNoteText(i, text);

        if (string.IsNullOrEmpty(text)) Delete();
    }

    public void NotEditing() {
        mainTab.SetActive(true);
        editTab.SetActive(false);
        CameraController.allowsKeyboardInput = true;
        optionsOverlay.SetActive(false);
        ScaleHeightWithText();
    }

    public void MoveUp() {
        int i = gameObject.transform.GetSiblingIndex();
        if (i == 0) return;

        gameObject.transform.SetSiblingIndex(i - 1);
        noteTaker.SwapNotes(i, i - 1);
    }

    public void MoveDown() {
        int i = gameObject.transform.GetSiblingIndex();
        int n = gameObject.transform.parent.childCount;
        if (i == n - 1) return;

        gameObject.transform.SetSiblingIndex(i + 1);
        noteTaker.SwapNotes(i, i + 1);
    }

    public void Delete() {
        int i = gameObject.transform.GetSiblingIndex();
        noteTaker.DeleteNote(i);
        Destroy(gameObject);
    }

    private void SetHeightToEdit() {
        rect.sizeDelta = new Vector2(rect.rect.width, EDIT_HEIGHT);
        noteTaker.ForceUpdateLayoutGroup();
    }

    private void ScaleHeightWithText() {
        Vector2 newSize = new Vector2(rect.rect.width, mainText.preferredHeight);
        rect.sizeDelta = newSize;
        optionsOverlay.GetComponent<RectTransform>().sizeDelta = newSize;
        noteTaker.ForceUpdateLayoutGroup();
    }

}