using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] AnimationCurve transitionCurve = null;

    const float HORIZONTAL_ROTATION_SPEED = 60f;
    const float VERTICAL_ROTATION_SPEED = 60f;
    const float ZOOM_SPEED = 50f;
    const float MIN_CAMERA_DISTANCE = 2f;
    const float MAX_VERTICAL_ANGLE = 60f;
    const float TRANSITION_TIME = 0.6f;

    Vector3 pointOfFocus = Vector3.zero;

    bool cameraLocked = false;
    public static bool allowsScrollInput { get; set; } = true;
    public static bool allowsKeyboardInput { get; set; } = true;
    float timeElapsed = 0f;
    Vector3 transitionStartPosition;
    Vector3 transitionPath;

    private void Update() {
        if (cameraLocked) {
            DoSmoothTransition();
        }
        else {
            bool mouseHeld = Input.GetMouseButton(1);
            DoHorizontalRotation(mouseHeld);
            DoVerticalRotation(mouseHeld);
            DoZooming();
        }
    }

    public void ChangePOF(Vector3 newPointOfFocus) {
        cameraLocked = true;
        transitionStartPosition = transform.position;
        transitionPath = newPointOfFocus - pointOfFocus;
        pointOfFocus = newPointOfFocus;
    }

    private void DoSmoothTransition() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= TRANSITION_TIME) {
            transform.position = transitionStartPosition + transitionPath;
            cameraLocked = false;
            timeElapsed = 0f;
        }
        else {
            transform.position = transitionStartPosition + transitionPath * transitionCurve.Evaluate(timeElapsed / TRANSITION_TIME);
        }
    }

    private void DoHorizontalRotation(bool mouseHeld) {
        if (!mouseHeld && !allowsKeyboardInput) return;

        float hInput = (mouseHeld) ? Input.GetAxis("Mouse X") : -1f * Input.GetAxis("Horizontal");
        float hAngleToRotate = HORIZONTAL_ROTATION_SPEED * hInput * Time.deltaTime;
        transform.RotateAround(pointOfFocus, Vector3.up, hAngleToRotate);
    }

    private void DoVerticalRotation(bool mouseHeld) {
        if (!mouseHeld && !allowsKeyboardInput) return;

        float vInput = (mouseHeld) ? -1f * Input.GetAxis("Mouse Y") : Input.GetAxis("Vertical");
        float vAngleToRotate = VERTICAL_ROTATION_SPEED * vInput * Time.deltaTime;
        RotateVerticalBy(vAngleToRotate);

        float currentVAngle = transform.eulerAngles.x;
        if (currentVAngle > 180f && currentVAngle < (360f - MAX_VERTICAL_ANGLE))
            RotateVerticalBy(- MAX_VERTICAL_ANGLE - currentVAngle);
        if (currentVAngle < 180f && currentVAngle > MAX_VERTICAL_ANGLE)
            RotateVerticalBy(MAX_VERTICAL_ANGLE - currentVAngle);
    }

    private void RotateVerticalBy(float angle) {
        transform.RotateAround(pointOfFocus, transform.right, angle);
    }

    private void DoZooming() {
        if (!allowsScrollInput) return;

        float zInput = Input.GetAxis("Mouse ScrollWheel");
        float distanceToZoom = ZOOM_SPEED * zInput * Time.deltaTime;
        transform.Translate(Vector3.forward * distanceToZoom);

        float distanceFromPOF = Vector3.Distance(transform.position, pointOfFocus);
        if (distanceFromPOF < MIN_CAMERA_DISTANCE) {
            transform.Translate(Vector3.forward * (distanceFromPOF - MIN_CAMERA_DISTANCE));
        }
    }

}
