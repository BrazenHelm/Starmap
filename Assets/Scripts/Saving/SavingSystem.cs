using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingSystem : MonoBehaviour {

    public void Save(string saveFile) {
        var state = LoadFile(saveFile);
        CaptureGameState(state);
        SaveFile(saveFile, state);
    }

    public void Load(string saveFile) {
        RestoreGameState(LoadFile(saveFile));
    }

    private string GetPathFromSaveFile(string saveFile) {
        return Path.Combine(Application.persistentDataPath, saveFile);
    }

    private void SaveFile(string saveFile, object gameState) {
        string path = GetPathFromSaveFile(saveFile);
        using (FileStream stream = File.Open(path, FileMode.Create)) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, gameState);
        }
    }

    private Dictionary<string, object> LoadFile(string saveFile) {
        string path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path)) {
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(path, FileMode.Open)) {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void CaptureGameState(Dictionary<string, object> state) {
        foreach(SaveableEntity entity in FindObjectsOfType<SaveableEntity>()) {
            state[entity.GetUniqueIdentifier()] = entity.CaptureEntityState();
        }
    }

    private void RestoreGameState(Dictionary<string, object> state) {
        foreach(SaveableEntity entity in FindObjectsOfType<SaveableEntity>()) {
            var entityID = entity.GetUniqueIdentifier();
            if (state.ContainsKey(entityID))
                entity.RestoreEntityState((Dictionary<string, object>)state[entityID]);
        }
    }

}
