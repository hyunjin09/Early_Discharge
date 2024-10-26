using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg = 1f;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "BorderBullet"){
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy"){
            collision.gameObject.GetComponent<EnemyClass>().GetDamaged(dmg);
            Destroy(gameObject);
        }


    }
}
