using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class Enemy3 : EnemyClass
{
    [SerializeField]
    private GameObject bul;
    public bool leftFirst = true;

    private float delayTime = 1.0f;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 10;
        AttackDamage = 0.5f;
        Size = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();
        if (GameObject.FindGameObjectWithTag("Player") != null) // 플레이어가 살아있는지 확인
        {
            nextRoutines.Enqueue(NewActionRoutine(FanFanShot()));
            nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(delayTime)));
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }
    private IEnumerator FanFanShot() {
        Vector3 shootPos = transform.position;
        shootPos.y -= 0.3f;
        Vector3 playerPos = GetPlayerPos();
        
        for(int i=0; i<3; i++) {
            for (int j=0; j<8; j++) {
                GameObject cur = Instantiate(bul, shootPos, Quaternion.identity);
                Vector3 xVector = new Vector3(j - 4.0f + (float)i / 2, 0, 0) / 0.6f * (leftFirst ? 1 : -1);

                cur.GetComponent<Rigidbody2D>().linearVelocity
                    = (playerPos - shootPos + xVector).normalized * 4.0f;

                Vector2 direction = (playerPos - shootPos + xVector).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float imageRotationOffset = 90f;
                cur.transform.rotation = Quaternion.Euler(0, 0, angle + imageRotationOffset);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }
        
        yield return null;
    }
}