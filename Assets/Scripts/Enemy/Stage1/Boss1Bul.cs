using UnityEngine;

public class Boss1Bul : MonoBehaviour
{
    private float dmg = 1f;
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
