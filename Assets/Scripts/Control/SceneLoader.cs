using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadMap() {
        SceneManager.LoadScene("MainMap");
    }

    public void Pause() {
        Time.timeScale = 0f;
    }

    public void Unpause() {
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }

}
