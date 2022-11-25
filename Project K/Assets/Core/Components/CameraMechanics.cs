using UnityEngine;

public class CameraMechanics : MonoBehaviour
{
    [Range (4f, 8f)]
    public float Sensitivity;
    private float Rotation_X;
    private float Rotation_Y;
    private Transform PlayerCamera;

    void Start()
    {
        PlayerCamera = transform.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2)) Rotation_X += Input.GetAxis("Mouse X") * Sensitivity;
        else Rotation_X = 0;
        Rotation_Y += Input.GetAxis("Mouse Y") * Sensitivity;
        Rotation_Y = Mathf.Clamp(Rotation_Y, -45, 45);
    }

    void FixedUpdate()
    {
        //walls must be layer 8
        transform.localRotation = Quaternion.Euler(-Rotation_Y, Rotation_X, 0);
        if (Physics.Raycast(transform.position, PlayerCamera.position - transform.position, out RaycastHit hit, 5.21f, ~8)) 
            PlayerCamera.position = Vector3.Lerp(transform.position, hit.point, .9f);
        else PlayerCamera.localPosition = new Vector3(.7f, 1.59f, -4.97f);
    }
}