using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveFile : MonoBehaviour, IPointerDownHandler {

    [SerializeField] Text nameText = null;
    [SerializeField] Text numberText = null;
    [SerializeField] Text lastEditedText = null;
    [SerializeField] GameObject selectionIndicator = null;

    public string fileName { get; set; }

    public void DisplayInfo(string name, int n, System.DateTime lastEdited) {
        fileName = name;
        nameText.text = name;
        numberText.text = "Save " + n;
        lastEditedText.text = "Last edited: " + lastEdited;
    }

    public void DisplayNewSave() {
        fileName = "";
        nameText.text = "";
        numberText.text = "New Save";
        lastEditedText.text = "";
    }

    public void Select() {
        selectionIndicator.SetActive(true);
        SavingWrapper.selectedSaveLocation = this;
    }

    public void Deselect() {
        selectionIndicator.SetActive(false);
    }

    public void OnPointerDown(PointerEventData data) {
        foreach (SaveFile save in FindObjectsOfType<SaveFile>())
            save.Deselect();
        Select();
    }

}