using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour
{

    protected Coroutine CurrentRoutine { get; private set; }
    private Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }
    public float AttackDamage { get; protected set; }
    public float Size { get; protected set; }

    private float tick = 0f;
    public float StartDelay = 1f;
    private GameObject player;
    public GameObject gameManager;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null){
            if(player.GetComponent<Player>().isMovingAuto == false){
                if (CurrentRoutine == null && tick > StartDelay)
                {
                    NextRoutine();
                }
                tick += Time.deltaTime;
            }
        }


    }
    public virtual void GetDamaged(float damage)
    {

        Health -= damage;
        Debug.Log(Health);
        StartCoroutine(DamageRoutine());
        if (Health <= 0)
        {
            gameManager = GameObject.Find("GameManager");
            gameManager.GetComponent<GameManager>().enemyCount--;
            // GameManager.Instance.EnemyCount--;
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageRoutine()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        for (float t = 0; t < 0.25f; t += Time.deltaTime)
        {
            foreach (var renderer in renderers)
            {
                if (renderer)
                {
                    renderer.color = Color.Lerp(Color.red, Color.white, t * 4);
                }
            }
            yield return null;
        }

    }
    protected Vector3 GetObjectPos()    // 현재 오브젝트 벡터3 반환
    {
        return gameObject.transform.position;
    }
    protected Vector3 GetPlayerPos()    // 플레이어 벡터3 반환; 플레이어가 살아있는지 먼저 확인해야함
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }


    protected float distToPlayer()    // 오브젝트와 플레이어 거리 반환; 플레이어가 살아있는지 먼저 확인해야함
    {
        return (GetObjectPos() - GetPlayerPos()).magnitude;
    }

    private void NextRoutine()
    {
        if (nextRoutines.Count <= 0)
        {
            nextRoutines = DecideNextRoutine();
        }
        StartCoroutineUnit(nextRoutines.Dequeue());
    }
    protected abstract Queue<IEnumerator> DecideNextRoutine();
    private void StartCoroutineUnit(IEnumerator coroutine)
    {
        if (CurrentRoutine != null)
        {
            StopCoroutine(CurrentRoutine);
        }
        CurrentRoutine = StartCoroutine(coroutine);
    }
    protected IEnumerator NewActionRoutine(IEnumerator action)
    {
        yield return action;
        CurrentRoutine = null;
    }
    protected IEnumerator WaitRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }


}