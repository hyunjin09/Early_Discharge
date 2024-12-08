using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyClass
{
    [SerializeField]
    private GameObject missle;

    private float delayTime = 1.5f;


    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 8f;
        AttackDamage = 0.5f;
        Size = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();
        if (GameObject.FindGameObjectWithTag("Player") != null) // 플레이어가 살아있는지 확인
        {
            
            nextRoutines.Enqueue(NewActionRoutine(Fire()));
            nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }
    private IEnumerator Fire()
    {
        // GetComponent<Animator>().SetTrigger("Shoot");

        Vector3 shootPos = transform.position;
        shootPos.y -= 0.3f;

        GameObject bullet = Instantiate(missle, shootPos, transform.rotation);

        Destroy(bullet, 10f);
        yield return null;
    }
}