using UnityEngine;

[CreateAssetMenu(fileName = "Star_System", menuName = "Starmap Objects/Star System")]
public class StarSystemInfo : ScriptableObject {

    [SerializeField] private string _name = null;
    [SerializeField] private float timeToHyperspace = 0f;
    [SerializeField] private string[] coordinates = new string[3] { "0.0", "00h00m00s", "00º00'00\""};
    [SerializeField] private CelestialBody[] celestialBodies = null;

    public string Name { get { return _name; } }
    public float TimeToHyperspace { get { return timeToHyperspace; } }
    public string[] Coordinates { get { return coordinates; } }
    public CelestialBody[] CelestialBodies { get { return celestialBodies; } }

}
