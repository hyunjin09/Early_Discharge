using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyClass
{
    [SerializeField]
    private GameObject bul;

    private float delayTime = 2f;


    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 20;
        Size = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();
        if (GameObject.FindGameObjectWithTag("Player") != null) // 플레이어가 살아있는지 확인
        {
            float rand = Random.value;
            if(true){
                nextRoutines.Enqueue(NewActionRoutine(Fire()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
            }
            else if(rand < 0.66f){

            }
            else{
                nextRoutines.Enqueue(NewActionRoutine(Fire()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
            }
            

        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }
    private IEnumerator Fire()
    {
        for(int i=0; i<5; i++){
            Vector3 playerPos = GetPlayerPos();
            Vector3 curPos = GetObjectPos();
            Vector3 toPlayer = playerPos - curPos;

            Vector3 shootPos = transform.position;
            shootPos.y -= 1f;

            GameObject bullet = Instantiate(bul, shootPos, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.linearVelocity = toPlayer.normalized * 5f;

            Destroy(bullet, 10f);
            yield return new WaitForSeconds(1f);
        }

    }

    // private IEnumerator FanShot(){

    // }
}