using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && gameManager.enemyCount == 0) {
            gameManager.NextStage();
        }
    }
}
