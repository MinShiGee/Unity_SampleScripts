using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    [SerializeField] private float sensitivity = 1.5f;
    [Header("Lerp Speed")]
    [SerializeField] [Range(0, 1f)] private float lerpT = 0.5f;
    [Header("Camera Object")]
    [SerializeField] private GameObject camera;
    [Header("Clamp Setting")]
    [SerializeField] [Range(0, 90)] private float cameraRotationLimit = 85f;

    [Header("For Checking")]
    [SerializeField] private float currentCameraRotationX = 0f;

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {

        float yRot = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * sensitivity;
        transform.rotation = transform.rotation * Quaternion.Euler(rotation);

        float xRot = Input.GetAxis("Mouse Y");
        float cameraRotationX = xRot * sensitivity;
        Quaternion nrot = camera.transform.rotation;

        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        Vector3 toRotation = new Vector3(currentCameraRotationX, 0f, 0f);
        camera.transform.rotation = Quaternion.Lerp(nrot, transform.rotation * Quaternion.Euler(toRotation), 0.5f);

    }

}
