using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyClass
{
    [SerializeField]
    private GameObject bul;

    [SerializeField]
    private GameObject missle;

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

            nextRoutines.Enqueue(NewActionRoutine(FireMissle()));

            if (rand<0.5f) {
                nextRoutines.Enqueue(NewActionRoutine(FanShot()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
            }
            else {
                nextRoutines.Enqueue(NewActionRoutine(FanFanShot()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }
    private IEnumerator FanShot(){
        Vector3 shootPos = transform.position;
        shootPos.y -= 1f;
        Vector3 playerPos = GetPlayerPos();
        
        for(int i=0; i<3; i++) {
            for (int j=0; j<7; j++) {
                GameObject cur = Instantiate(bul, shootPos, Quaternion.identity);
                cur.GetComponent<Rigidbody2D>().linearVelocity
                    = (playerPos + new Vector3(j-3, 0, 0) / 0.6f - shootPos).normalized * 4.0f;
            }
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    private IEnumerator FireMissle()
    {
        Vector3 shootPos = transform.position;
        shootPos.y -= 1f;

        GameObject bullet = Instantiate(missle, shootPos, transform.rotation);

        yield return null;
    }

    private IEnumerator FanFanShot() {
        Vector3 shootPos = transform.position;
        shootPos.y -= 1f;
        Vector3 playerPos = GetPlayerPos();
        
        for(int i=0; i<3; i++) {
            float rand = Random.value;

            for (int j=0; j<2; j++) {
                if (j == 1) rand = rand > 0.5f ? 0.0f : 1.0f;

                for (int k=0; k<8; k++) {
                    GameObject cur = Instantiate(bul, shootPos, Quaternion.identity);
                    Vector3 xVector = new Vector3(k - 4.0f + (float)i / 2, 0, 0) / 0.6f * (rand > 0.5f ? 1 : -1);

                    cur.GetComponent<Rigidbody2D>().linearVelocity
                        = (playerPos - shootPos + xVector).normalized * 4.0f;

                    Vector2 direction = (playerPos - shootPos + xVector).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    float imageRotationOffset = 90f;
                    cur.transform.rotation = Quaternion.Euler(0, 0, angle + imageRotationOffset);

                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
        
        yield return null;
    }
}