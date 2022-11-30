using UnityEngine;
using Core;
[RequireComponent(typeof(BaseMechanics))]
public class PlayerMechanics : MonoBehaviour
{
    //variable basket
    [SerializeField] [Range(10, 40)]
    public int Speed;
    [SerializeField] [Range(5, 30)]
    private int Sensitivity;
    private float Rotation_X;
    private float Rotation_Y;
    private float Movement_Forward;
    private float Movement_Right;
    private BaseMechanics Base;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Base = transform.GetComponent<BaseMechanics>();
        Base.Player = new PlayerDB("Player", gameObject);
    }

    void Update()
    {
        //player movement
        Movement_Forward = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
        Movement_Right = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        Rotation_X = Input.GetAxis("Mouse X") * Sensitivity;


        //Player shoots
        if (Input.GetKeyDown(KeyCode.Mouse0)) Base.Shoot();
    }
    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Mouse2)) Base.Body.rotation *= Quaternion.Euler(0, Rotation_X, 0);
        Base.Body.MovePosition(transform.TransformPoint(new Vector3(Movement_Right, 0, Movement_Forward)));
    }
}