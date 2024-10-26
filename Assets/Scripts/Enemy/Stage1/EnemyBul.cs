using UnityEngine;

public class EnemyBul : MonoBehaviour
{
    private float dmg = 0.5f;
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