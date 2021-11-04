using UnityEngine;

[CreateAssetMenu(fileName = "Celestial_Body", menuName = "Starmap Objects/Celestial Body")]
public class CelestialBody : ScriptableObject {

    public enum ObjectType { Star, Planet, Moon, Manmade }

    [SerializeField] ObjectType objectType = ObjectType.Planet;
    public ObjectType Type { get { return objectType; } }

    [Header("Universal")]
    [SerializeField] Sprite sprite = null;
    [SerializeField] string classification = null;
    [SerializeField] string size = null;
    [Header("Stars")]
    [SerializeField] string solarMasses = null;
    [SerializeField] string age = null;
    [Header("Other")]
    [SerializeField] string temperature = null;
    [SerializeField] string gravity = null;
    [SerializeField] string population = null;
    
    public Sprite Sprite { get { return sprite; } }
    public string Classification { get { return classification; } }
    public string Size { get { return size; } }
    public string SolarMasses { get { return solarMasses; } }
    public string Age { get { return age; } }
    public string Temperature { get { return temperature; } }
    public string Gravity { get { return gravity; } }
    public string Population { get { return population; } }
    public bool IsInhabited { get { return (population != "Uninhabited") && objectType != ObjectType.Star; } }

}
