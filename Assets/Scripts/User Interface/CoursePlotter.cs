using UnityEngine;
using UnityEngine.UI;

public class CoursePlotter : MonoBehaviour {

    const float HYPERSPACE_SPEED = 0.12f;    // light-years per hour of spaceship

    [SerializeField] Text systemOfOriginText = null;
    [SerializeField] Text destinationSystemText = null;
    [SerializeField] Text originCoordinatesText = null;
    [SerializeField] Text destinationCoordinatesText = null;
    [SerializeField] Text distanceText = null;
    [SerializeField] Text travelTimeText = null;

    string currentSystemName;
    string currentSystemCoordinates;
    float timeToLeaveCurrentSystem;
    Vector3 startPosition;
    Vector3 endPosition;
    float distance;
    float travelTime;

    private void Start() {
        ResetText();
    }

    public void StartPlot() {
        StarSystem.SetIsPlottingCourse(true);
        ResetText();
        StarSystem startSystem = StarSystem.currentSystem;
        startPosition = startSystem.transform.position;
        travelTime = startSystem.systemInfo.TimeToHyperspace;
        systemOfOriginText.text = startSystem.systemInfo.Name;
        originCoordinatesText.text = ToString(startSystem.systemInfo.Coordinates);
    }

    public void FinishPlot(Vector3 position, StarSystemInfo systemInfo) {
        StarSystem.SetIsPlottingCourse(false);

        endPosition = position;
        destinationSystemText.text = systemInfo.Name;
        destinationCoordinatesText.text = ToString(systemInfo.Coordinates);
        travelTime += systemInfo.TimeToHyperspace;

        distance = Vector3.Distance(startPosition, endPosition);
        distanceText.text = (Mathf.Round(10f * distance) / 10f).ToString();
        travelTime += distance / HYPERSPACE_SPEED;
        travelTimeText.text = Mathf.Round(travelTime).ToString();
    }

    public void CancelPlot() {
        StarSystem.SetIsPlottingCourse(false);
        ResetText();
    }

    private void ResetText() {
        systemOfOriginText.text = "No system selected";
        destinationSystemText.text = "No system selected";
        originCoordinatesText.text = "";
        destinationCoordinatesText.text = "";
        distanceText.text = "-";
        travelTimeText.text = "-";
    }

    private string ToString(string[] coordinates) {
        string result   = coordinates[0] + " LFE\n"
                        + coordinates[1] + "\n"
                        + coordinates[2] + "\n";
        return result;
    }

}
