using UnityEngine;
using Core;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody)), SelectionBase()]
public class BaseMechanics : MonoBehaviour
{
    public PlayerDB Player;
    //references
    public Animator Animator;
    public Rigidbody Body;
    public Vector3 LookingAt;
    public RaycastHit Target;
    public Transform GunPoint;

    void Start()
    {
        Animator = transform.GetComponent<Animator>();
        Body = transform.GetComponent<Rigidbody>();
        GunPoint = gameObject.transform.GetChild(0);
    }

    void Update()
    {
        LookingAt = Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(.5f, .5f, 0)), out Target, 100) ? Target.point : GunPoint.position + new Vector3(0, 0, 0);
    }

    public void Shoot()
    {
        //shooting
        if (Physics.Raycast(GunPoint.position, LookingAt - GunPoint.position, out RaycastHit hit, 100, 64))
            AttackMechanics.Attack(Player, hit.transform.GetComponent<BaseMechanics>().Player);
    }
}
