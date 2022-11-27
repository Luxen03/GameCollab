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
    }

    void FixedUpdate()
    {
        //walls must be layer 8
        if (Physics.Raycast(transform.position, Holder.position - transform.position, out RaycastHit hit, 5.21f, ~8)) 
            Holder.position = Vector3.Lerp(transform.position, hit.point, .9f);
        else
            Holder.localPosition = new Vector3(.7f, 1.59f, -4.97f);
        Quaternion Rotation = Quaternion.Euler(-Rotation_Y, Rotation_X, 0);
        //if scoping
        if (!Scoping){
            //normal
            transform.localRotation = Rotation;
            PlayerCamera.localPosition = Vector3.zero;
            PlayerCamera.localEulerAngles = new Vector3(4.27f, 0, 0);
        } else {
            //look at where its supposed to look at
            Vector3 LookingAt = !Physics.Raycast(Holder.position, Rotation * transform.forward, out hit, 100, ~8) ? hit.point : Holder.TransformPoint(new Vector3(0, 0, 100));
            Debug.Log(LookingAt);
            PlayerCamera.position = GunPoint.position;
            PlayerCamera.LookAt(LookingAt);
        }
    }
}