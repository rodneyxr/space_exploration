using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float defaultSensitivityX = 10f;
    public float defaultSensitivityY = 10f;

    // minimum and maximum yaw (left/right)
    public float minimumX = -360f;
    public float maximumX = 360f;

    // minimum and maximum pitch (up/down)
    public float minimumY = -90f;
    public float maximumY = 90f;

    // affects how sensitive mouse input will be
    private float sensitivityX;
    private float sensitivityY;

    private float rotationY = 0f;

    // Aiming
    public Texture2D crosshairImage;
    public float zoomFOV = 30f;
    private Camera cam;
    private float normal;
    private bool aiming = false;

    void Start() {
        cam = GetComponentInChildren<Camera>();
        normal = cam.fieldOfView;
        sensitivityX = defaultSensitivityX;
        sensitivityY = defaultSensitivityY;

        // Make the rigid body not change rotation
        //if (GetComponent<Rigidbody>())
        //    GetComponent<Rigidbody>().freezeRotation = true;
        //Rigidbody modelRigidbody = GetComponentInChildren<Rigidbody>();
        //if (modelRigidbody)
        //    modelRigidbody.freezeRotation = true;
    }

    /// <summary>
    /// Draws a reticle in the center of the screen for precision aiming
    /// </summary>
    void OnGUI() {
        float scaleFactor = Mathf.Max(Screen.width, Screen.height) / 1080f;
        float shrinkFactor = cam.fieldOfView / normal;
        float imgWidth = (crosshairImage.width * scaleFactor) * shrinkFactor;
        float imgHeight = (crosshairImage.height * scaleFactor) * shrinkFactor;
        float xMin = (Screen.width / 2) - (imgWidth / 2);
        float yMin = (Screen.height / 2) - (imgHeight / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, imgWidth, imgHeight), crosshairImage);
    }

    void Update() {
        Aim(Input.GetButton("Fire2"));
        HandleMouse();
    }

    /// <summary>
    /// Enables or disables aiming. If aiming has not changed since the last call,
    /// only the field of view will be updated and SetAiming() will not be called.
    /// </summary>
    /// <param name="aiming">true to enable aiming; false to disable</param>
    public void Aim(bool aiming) {
        if (this.aiming != aiming) {
            SetAiming(aiming);
        }
        if (aiming) {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * 10f);
        } else {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normal, Time.deltaTime * 10f);
        }
    }

    /// <summary>
    /// Handles the mouse input and rotates the game object accordingly
    /// </summary>
    private void HandleMouse() {
        if (axes == RotationAxes.MouseXAndY) {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        } else if (axes == RotationAxes.MouseX) {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        } else {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    /// <summary>
    /// Enables or disables aim mode
    /// </summary>
    /// <param name="aiming">true to enable aim mode; false to disable</param>
    private void SetAiming(bool aiming) {
        this.aiming = aiming;
        if (aiming) {
            sensitivityX = defaultSensitivityX / 10f;
            sensitivityY = defaultSensitivityY / 10f;
        } else {
            sensitivityX = defaultSensitivityX;
            sensitivityY = defaultSensitivityY;
        }
    }
}
