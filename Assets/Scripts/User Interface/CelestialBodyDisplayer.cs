using UnityEngine;

public class CelestialBodyDisplayer : MonoBehaviour {

    [SerializeField] CelestialBodyUI[] uIPrefabs = null;

    CelestialBody[] bodies = null;
    public void SetCelestialBodies(CelestialBody[] bodies) { this.bodies = bodies; }

    const int NUMBER_OF_FILTERS = 5;
    readonly int[] NUMBER_OF_SETTINGS = new int[]{ 2, 2, 2, 2, 3 };
    int[] filters = new int[NUMBER_OF_FILTERS];

    private void Start() {
        ResetFilters();
    }

    public void ResetFilters() {
        for (int i = 0; i < NUMBER_OF_FILTERS; ++i)
            filters[i] = 0;
        foreach (FilterButton button in FindObjectsOfType<FilterButton>())
            button.ResetButton();
        ShowFiltered();
    }

    public void SetFilter(int index, int value) {
        filters[index] = value;
        ShowFiltered();
    }

    public void HideAll() {
        foreach (CelestialBodyUI ui in FindObjectsOfType<CelestialBodyUI>()) {
            Destroy(ui.gameObject);
        }
    }

    public void ShowFiltered() {
        HideAll();
        if (bodies == null) return;
        foreach (CelestialBody body in bodies)
            if (FiltersAllow(body)) ShowBody(body);        
    }

    private void ShowBody(CelestialBody body) {
        CelestialBodyUI ui = Instantiate(uIPrefabs[ToInt(body.Type)], transform);
        ui.SetCelestialBody(body);
    }

    private bool FiltersAllow(CelestialBody body) {
        if (filters[ToInt(body.Type)] == 1)   return false;
        if (filters[4] == 1 && !body.IsInhabited)   return false;
        if (filters[4] == 2 && body.IsInhabited)    return false;
        return true;
    }

    public int ToInt(CelestialBody.ObjectType type) {
        switch (type) {
            case CelestialBody.ObjectType.Star:     return 0;
            case CelestialBody.ObjectType.Planet:   return 1;
            case CelestialBody.ObjectType.Moon:     return 2;
            case CelestialBody.ObjectType.Manmade:  return 3;
            default: throw new System.Exception("Unknown celestial body type");
        }
    }
}
