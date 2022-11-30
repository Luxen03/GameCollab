using UnityEngine;
using Core;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody)), SelectionBase()]
public class BaseMechanics : MonoBehaviour
{
    public PlayerDB Player;
    //references
    public Animator Animator;
    public Rigidbody Body;
    public Transform GunPoint;

    void Start()
    {
        Animator = transform.GetComponent<Animator>();
        Body = transform.GetComponent<Rigidbody>();
        GunPoint = gameObject.transform.GetChild(0);
    }
    public void Shoot()
    {
        //shooting
        if (Physics.Raycast(GunPoint.position, Vector3.zero - GunPoint.position, out RaycastHit hit, 100, 64))
            AttackMechanics.Attack(Player, hit.transform.GetComponent<BaseMechanics>().Player);
    }
}
