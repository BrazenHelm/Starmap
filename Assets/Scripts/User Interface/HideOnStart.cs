using UnityEngine;

public class HideOnStart : MonoBehaviour {

    private void Start() {
        FindObjectOfType<TabHider>().Hide(gameObject);
    }

}
