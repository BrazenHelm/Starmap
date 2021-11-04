using UnityEngine;
using UnityEngine.UI;

public class StarSystemUI : MonoBehaviour {

    [SerializeField] Text systemNameText = null;

    [SerializeField] CelestialBodyDisplayer displayer = null;
    [SerializeField] NoteTaker notes = null;

    public void UpdateUI(StarSystemInfo info, Vector3 position) {
        systemNameText.text = info.Name;
        UpdatePlanetsTab(info.CelestialBodies);
    }

    private void UpdatePlanetsTab(CelestialBody[] celestialBodies) {
        displayer.SetCelestialBodies(celestialBodies);
        displayer.ShowFiltered();
        if (notes.isActiveAndEnabled)
            notes.DisplayNotesForCurrentSystem();
    }

}
