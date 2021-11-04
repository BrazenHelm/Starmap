using UnityEngine;
using UnityEngine.UI;

public class CelestialBodyUI : MonoBehaviour {

    [SerializeField] CelestialBody celestialBody = null;
    [SerializeField] Image spriteImage = null;
    [SerializeField] Text nameText = null;
    [SerializeField] Text classificationText = null;
    [SerializeField] Text[] textFields = null;

    public void SetCelestialBody(CelestialBody celestialBody) {
        this.celestialBody = celestialBody;
        UpdateInfo();
    }

    private void UpdateInfo() {
        if (!celestialBody) throw new System.NullReferenceException();

        if (celestialBody.Sprite) spriteImage.sprite = celestialBody.Sprite;
        nameText.text = celestialBody.name;
        classificationText.text = celestialBody.Classification;

        switch (celestialBody.Type) {
            case CelestialBody.ObjectType.Star:
                textFields[0].text = celestialBody.SolarMasses;
                textFields[1].text = celestialBody.Size;
                textFields[2].text = celestialBody.Temperature;
                textFields[3].text = celestialBody.Age;
                break;

            case CelestialBody.ObjectType.Planet:
                textFields[0].text = celestialBody.Size;
                textFields[1].text = celestialBody.Gravity;
                textFields[2].text = celestialBody.Temperature;
                textFields[3].text = celestialBody.Population;
                break;

            case CelestialBody.ObjectType.Moon:
            case CelestialBody.ObjectType.Manmade:
                textFields[0].text = celestialBody.Size;
                textFields[1].text = celestialBody.Population;
                break;
        }
    }

}
