using UnityEngine;

public class CameraMechanics : MonoBehaviour
{
    [Range (4f, 8f)]
    public float Sensitivity;
    private float Rotation_X;
    private float Rotation_Y;
    private bool Scoping;
    private Transform PlayerCamera;
    private Transform Holder;
    private Transform GunPoint;
    private BaseMechanics Base;

    void Start()
    {
        Base = transform.parent.GetComponent<BaseMechanics>();
        GunPoint = gameObject.transform.parent.GetChild(0);
        Holder = transform.GetChild(0);
        PlayerCamera = Holder.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2)) Rotation_X += Input.GetAxis("Mouse X") * Sensitivity;
        else Rotation_X = 0;
        Rotation_Y += Input.GetAxis("Mouse Y") * Sensitivity;
        Rotation_Y = Mathf.Clamp(Rotation_Y, -45, 45);

        //Player scopes
        Scoping = Input.GetKey(KeyCode.Mouse1);
        if (Input.GetKeyDown(KeyCode.Mouse1)) PlayerCamera.GetComponent<Camera>().fieldOfView = 17;
        if (Input.GetKeyUp(KeyCode.Mouse1)) PlayerCamera.GetComponent<Camera>().fieldOfView = 60;

        //Lookingat
        Ray Pointer = new Ray(Holder.position, Holder.forward);
        Vector3 LookingAt = Physics.Raycast(Pointer, out RaycastHit Target, 100, ~8) ? Target.point : GunPoint.position + new Vector3(0, 0, 0);

        Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out Target, 100, ~8);
        Debug.Log(LookingAt + ":" + Target.point + " = " + (LookingAt - Target.point));

        //Debug
        Debug.DrawRay(Pointer.origin, Pointer.direction * 100, Color.red);
        Pointer = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(Pointer.origin, Pointer.direction * 100, Color.red);

        //Pivot rotation
        transform.localRotation = Quaternion.Euler(-Rotation_Y, Rotation_X, 0);

        //Camera rotation and position
        if (!Scoping){
            //When Not Scoping
            PlayerCamera.localPosition = Vector3.zero;
            PlayerCamera.localEulerAngles = Vector3.zero;
        } else {
            //When Scoping
            PlayerCamera.position = GunPoint.position;
            PlayerCamera.LookAt(LookingAt);
        }
    }

    void FixedUpdate()
    {
        //Wall Detection
        if (Physics.Raycast(transform.position, Holder.position - transform.position, out RaycastHit hit, 5.21f, ~8)) 
            Holder.position = Vector3.Lerp(transform.position, hit.point, .9f);
        else Holder.localPosition = new Vector3(.7f, 1.59f, -4.97f);
    }
}