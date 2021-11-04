using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavingWrapper : MonoBehaviour {

    [SerializeField] SaveFile saveFilePrefab = null;
    [SerializeField] Transform saveFileDisplay = null;
    [SerializeField] GameObject enterNewSaveName = null;
    SavingSystem savingSystem;

    const int SAVE_SLOTS = 6;

    public static SaveFile selectedSaveLocation = null;
    SaveFile lastSaveLocation = null;
    bool nameSet;

    private void Awake() {
        savingSystem = FindObjectOfType<SavingSystem>();
    }

    private void Start() {
        DisplaySaveFiles();
    }

    public void SaveSelectedLocation() {
        StartCoroutine(SaveAfterGettingName());
    }

    private IEnumerator SaveAfterGettingName() {
        if (selectedSaveLocation == null) yield break;
        if (string.IsNullOrEmpty(selectedSaveLocation.fileName)) {
            enterNewSaveName.SetActive(true);
            nameSet = false;
            while (!nameSet) yield return null;
        }
        savingSystem.Save(selectedSaveLocation.fileName + ".sav");
        lastSaveLocation = selectedSaveLocation;
        DisplaySaveFiles();
    }

    public void SetName() {
        string newName = enterNewSaveName.transform.Find("InputField").GetComponent<InputField>().text;
        if (string.IsNullOrEmpty(newName)) return;
        selectedSaveLocation.fileName = newName;
        nameSet = true;
        enterNewSaveName.SetActive(false);
    }

    public void SaveLastLocation() {
        savingSystem.Save(lastSaveLocation.fileName + ".sav");
    }

    public void LoadSelectedLocation() {
        if (selectedSaveLocation == null) {
            Debug.LogWarning("No save file selected");
        }
        savingSystem.Load(selectedSaveLocation.fileName + ".sav");
        lastSaveLocation = selectedSaveLocation;
    }

    private FileInfo[] GetSaveFiles() {
        var saveFolderInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] saveFilesAndOutputLog = saveFolderInfo.GetFiles();
        FileInfo[] saveFiles = new FileInfo[saveFilesAndOutputLog.Length - 1];
        int indexUpTo = 0;
        foreach (FileInfo file in saveFilesAndOutputLog) {
            if (file.Name != "output_log.txt")
                saveFiles[indexUpTo++] = file;
        }
        return saveFiles;
    }

    public void DisplaySaveFiles() {
        foreach (Transform child in saveFileDisplay)
            Destroy(child.gameObject);

        FileInfo[] saveFiles = GetSaveFiles();
        int numberOfSaves = saveFiles.Length;

        for (int i = 0; i < SAVE_SLOTS; ++i) {
            SaveFile save = Instantiate(saveFilePrefab, saveFileDisplay);
            save.Deselect();
            if (i < numberOfSaves)
                save.DisplayInfo(Path.GetFileNameWithoutExtension(saveFiles[i].Name), i + 1, saveFiles[i].LastWriteTimeUtc);
            else
                save.DisplayNewSave();
        }

        selectedSaveLocation = null;
    }

}