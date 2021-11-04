using UnityEngine;
using System.Collections.Generic;

public class StarSystem : ClickDetector {

    [SerializeField] bool isDefaultSystem = false;
    [SerializeField] public StarSystemInfo systemInfo = null;

    public static StarSystem currentSystem;
    static bool isPlottingCourse = false;
    public static void SetIsPlottingCourse(bool value) {
        isPlottingCourse = value;
    }

    StarSystemUI starSystemUI;

    private void Awake() {
        starSystemUI = FindObjectOfType<StarSystemUI>();
    }

    private void Start() {
        if (isDefaultSystem) {
            SelectThisSystem();
            Camera.main.transform.LookAt(transform);
        }
    }

    protected override void OnSingleClick() {
        if (isPlottingCourse) {
            FindObjectOfType<CoursePlotter>().FinishPlot(transform.position, systemInfo);
        }
    }

    protected override void OnDoubleClick() {
        SelectThisSystem();
        Camera.main.GetComponent<CameraController>().ChangePOF(transform.position);
    }

    private void OnMouseEnter() { Tooltip.ShowTooltip(systemInfo.Name); }
    private void OnMouseExit() { Tooltip.HideTooltip(); }

    private void SelectThisSystem() {
        currentSystem = this;
        starSystemUI.UpdateUI(systemInfo, transform.position);
    }

}
