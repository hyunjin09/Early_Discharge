using UnityEngine;

public class Boss2Missle : MonoBehaviour
{
    private float dmg = 1f;
    private float speed = 4f;
    private GameObject player;
    private bool condition = true;

    Vector3 dir;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){
        Vector3 dir = (player.transform.position - this.transform.position);

        float vx = dir.x;
        float vy = -1f;

        if(condition){
            if(gameObject.GetComponent<Rigidbody2D>().position.y > player.GetComponent<Rigidbody2D>().position.y){
                this.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy).normalized * speed;
            }
            else{
                condition = false;
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "BorderBullet"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player>().GetDamaged(dmg);
            Destroy(gameObject);
        }


    }
}
