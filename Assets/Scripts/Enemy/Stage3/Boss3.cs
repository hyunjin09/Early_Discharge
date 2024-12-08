using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyClass
{
    [SerializeField]
    private GameObject bul;

    private float delayTime = 1.5f;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 40;
        Size = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();
        if (GameObject.FindGameObjectWithTag("Player") != null) // 플레이어가 살아있는지 확인
        {
            float rand = Random.value;
            if(rand<0.5f){
                nextRoutines.Enqueue(NewActionRoutine(FanShot()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
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

    private IEnumerator FanShot(){
        Vector3 shootPos = transform.position;
        shootPos.y -= 1f;
        Vector3 playerPos = GetPlayerPos();
        
        for(int i=0; i<3; i++){
            for (int j = 0; j < 5; j++)
            {

                GameObject cur = Instantiate(bul, shootPos, Quaternion.identity);
                cur.GetComponent<Rigidbody2D>().linearVelocity
                    = (playerPos + new Vector3(j - 2, 0, 0) / 0.5f - shootPos).normalized * 3f;
            }
            yield return new WaitForSeconds(1f);
        }

        yield return null;

    }
}