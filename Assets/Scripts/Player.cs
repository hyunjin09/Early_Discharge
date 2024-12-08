using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    private float speed = 5;
    public bool isMovingAuto = false;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    private float maxShotDelay = 0.2f;
    public float curShotDelay;

    public GameObject bulletObjA;

    private float MaxHealth = 5f;
    private float Health;
    private bool isInvincible = false;

    void Start(){
        Health = MaxHealth;
        maxShotDelay = 0.2f;

    }
    void Update()
    {
        if (!isMovingAuto) {
            Move();
            Fire();
            Reload();
        }
    }

    void Move(){
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Fire(){
        // if(!Input.GetButton("Fire1"))
        //     return;
        if(!Input.GetKey(KeyCode.I))
            return;
        
        if(curShotDelay < maxShotDelay)
            return;

        Vector3 shootPos = transform.position;
        shootPos.y += 0.3f;

        GameObject bullet = Instantiate(bulletObjA, shootPos, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    void Reload(){
        curShotDelay += Time.deltaTime;
    }

    public void GetDamaged(float dmg){
        if(!isInvincible){
            maxShotDelay += 0.1f;
            Health -= dmg;
            StartCoroutine(DamageRoutine());
            if(Health <= 0){
                Destroy(gameObject);
            }
            Debug.Log(Health);

            gameManager.UpdateHealthImage(Health);
        }

    }
    private IEnumerator DamageRoutine()
    {
        isInvincible = true;
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
        isInvincible = false;

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Border"){
            switch(collision.gameObject.name){
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Border"){
            switch(collision.gameObject.name){
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
