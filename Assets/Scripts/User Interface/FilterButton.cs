using UnityEngine;
using UnityEngine.UI;

public class FilterButton : MonoBehaviour {

    [SerializeField] int filterIndex = 0;
    [SerializeField] int numberOfPositions = 2;
    [SerializeField] Sprite[] sprites = null;
    int currentPosition = 0;

    CelestialBodyDisplayer celestialBodyDisplayer = null;
    Image image = null;

    private void Awake() {
        celestialBodyDisplayer = FindObjectOfType<CelestialBodyDisplayer>();
        image = GetComponent<Image>();
    }

    private void Start() {
        if (numberOfPositions != sprites.Length)
            Debug.LogWarning(name + " expects same number of sprites as positions");
    }

    public void OnClicked() {
        ++currentPosition;
        if (currentPosition == numberOfPositions)
            currentPosition = 0;
        image.sprite = sprites[currentPosition];
        celestialBodyDisplayer.SetFilter(filterIndex, currentPosition);
    }

    public void ResetButton() {
        currentPosition = 0;
        image.sprite = sprites[0];
    }

}