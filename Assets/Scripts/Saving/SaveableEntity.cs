using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveableEntity : MonoBehaviour {

    [SerializeField] string uniqueIdentifier = null;
    static Dictionary<string, SaveableEntity> idList = new Dictionary<string, SaveableEntity>();

    public string GetUniqueIdentifier() { return uniqueIdentifier; }

    public Dictionary<string, object> CaptureEntityState() {
        var state = new Dictionary<string, object>();
        foreach (ISaveable saveable in GetComponents<ISaveable>()) {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }
        return state;
    }

    public void RestoreEntityState(Dictionary<string, object> state) {
        foreach (ISaveable saveable in GetComponents<ISaveable>()) {
            var saveableID = saveable.GetType().ToString();
            if (state.ContainsKey(saveableID))
                saveable.RestoreState(state[saveableID]);
        }
    }

#if UNITY_EDITOR

    private void Update() {
        if (Application.IsPlaying(gameObject)) return;
        if (string.IsNullOrEmpty(gameObject.scene.path)) return;

        var serializedObject = new SerializedObject(this);
        SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

        if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue)) {
            property.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }

        idList[property.stringValue] = this;
    }

    private bool IsUnique(string candidate) {
        if (!idList.ContainsKey(candidate))
            return true;
        else {
            if (idList[candidate] == this)
                return true;
            else if (idList[candidate] == null) {
                idList.Remove(candidate);
                return true;
            }
            else return false;
        }
    }
#endif

}
