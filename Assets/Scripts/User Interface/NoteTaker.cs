using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteTaker : MonoBehaviour, ISaveable {

    [SerializeField] NoteInput notePrefab = null;
    [SerializeField] Scrollbar scrollbar = null;

    private Dictionary<string, List<string>> notesList = null;

    private void Awake() {
        notesList = new Dictionary<string, List<string>>();
        foreach (StarSystem system in FindObjectsOfType<StarSystem>()) {
            notesList[system.name] = new List<string>();
        }
    }

    public void AddNote() {
        NoteInput note = Instantiate(notePrefab, transform);
        notesList[CurrentSystemName()].Add("");
        note.StartEditing();
        scrollbar.value = 0f;
    }

    public void ForceUpdateLayoutGroup() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void DisplayNotesForCurrentSystem() {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        foreach (string noteText in notesList[CurrentSystemName()])
            DisplayNote(noteText);
    }

    private void DisplayNote(string text) {
        NoteInput note = Instantiate(notePrefab, transform);
        note.text = text;
        note.NotEditing();
    }

    public void UpdateNoteText(int index, string newText) {
        notesList[CurrentSystemName()][index] = newText;
    }

    public void SwapNotes(int i, int j) {
        string temp = notesList[CurrentSystemName()][i];
        notesList[CurrentSystemName()][i] = notesList[CurrentSystemName()][j];
        notesList[CurrentSystemName()][j] = temp;
    }

    public void DeleteNote(int index) {
        notesList[CurrentSystemName()].RemoveAt(index);
    }

    private string CurrentSystemName() {
        return StarSystem.currentSystem.name;
    }

    // ISaveable
    public object CaptureState() { return notesList; }
    public void RestoreState(object state) {
        notesList = (Dictionary<string, List<string>>)state;
        DisplayNotesForCurrentSystem();
    }

}
