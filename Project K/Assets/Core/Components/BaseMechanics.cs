using UnityEngine;
using Core;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody)), SelectionBase()]
public class BaseMechanics : MonoBehaviour
{
    public PlayerDB Player;
    //references
    public Animator Animator;
    public Rigidbody Body;

    void Start()
    {
        Animator = transform.GetComponent<Animator>();
        Body = transform.GetComponent<Rigidbody>();
    }

    public void Shoot(RaycastHit hit)
    {
        //shooting
        Vector3 GunPoint = gameObject.transform.GetChild(0).position;
        if (Physics.Raycast(GunPoint, hit.point - GunPoint, out hit, 100, ~8))
            AttackMechanics.Attack(Player, hit.transform.GetComponent<BaseMechanics>().Player);
    }
}
