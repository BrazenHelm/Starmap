using UnityEngine;

public class Star : MonoBehaviour {

    [SerializeField] float absoluteMagnitude = 1f;
    [SerializeField] float colorIndex = 0f;

    [SerializeField] Gradient colorGradient = new Gradient();
    
    const float SCALE_FACTOR = 0.2f;
    const float MINIMUM_COLOR_INDEX = -0.33f;

    private void Start() {
        RandomiseRotation();
        SetSize();
        SetColor();
    }

    private void RandomiseRotation() {
        transform.rotation = Random.rotation;
    }

    private void SetSize() {
        float size = SCALE_FACTOR * (1f + 3f * Mathf.Exp(-0.1f * absoluteMagnitude));
        transform.localScale = new Vector3(size, size, size);
    }
    
    private void SetColor() {

        if (colorIndex < MINIMUM_COLOR_INDEX) {
            colorIndex = MINIMUM_COLOR_INDEX;
            Debug.LogWarning("Color index for " + gameObject.name + " below minimum permitted value.");
        }

        float positionOnScale = 1f / (colorIndex + (1 - MINIMUM_COLOR_INDEX));
        Color starColor = colorGradient.Evaluate(positionOnScale);

        GetComponent<Renderer>().material.color = starColor;

        ParticleSystem.MainModule glowMain = transform.Find("Glow").GetComponent<ParticleSystem>().main;
        glowMain.startColor = starColor;

        ParticleSystem.MainModule flaresMain = transform.Find("Flares").GetComponent<ParticleSystem>().main;
        flaresMain.startColor = starColor;

        ParticleSystem.MainModule bigflaresMain = transform.Find("Flares_big").GetComponent<ParticleSystem>().main;
        bigflaresMain.startColor = starColor;
    }
    
}
