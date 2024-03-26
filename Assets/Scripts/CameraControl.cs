using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform centerPoint;

    [Header("Look settings")]
    [SerializeField] private float sensitivityX = 0.5F;
    [SerializeField] private float sensitivityY = 0.5F;

    [Header("Scrolling behaviour")]
    [SerializeField] private float scrollSensitivity = 15F;
    [SerializeField] private float centerPointMinDistance = 45F;
    [SerializeField] private float centerPointMaxDistance = 400F;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Receive mouse input
        Vector2 mouseMovementInput = GameInput.Instance.GetMouseMovement();
        Vector2 mouseScrollInput = GameInput.Instance.GetMouseScroll();

        // Move camera horizontally
        camera.transform.Rotate(Vector3.left, mouseMovementInput.y * sensitivityX);

        // Move camera vertically around the center point
        camera.transform.RotateAround(centerPoint.position, Vector3.up, mouseMovementInput.x * sensitivityY);

        // Move camera away/towards center point based on scroll input
        Vector3 cameraToPoint = centerPoint.position - camera.transform.position;
        camera.transform.position = centerPoint.position - cameraToPoint.normalized
            * Mathf.Clamp(cameraToPoint.magnitude - (mouseScrollInput.y * scrollSensitivity * Time.deltaTime),
            centerPointMinDistance, centerPointMaxDistance);
    }
}

