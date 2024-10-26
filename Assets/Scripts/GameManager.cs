using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Vector3 hiddenPoint = new Vector3(0, -5.5f, 0);
    private Vector3 startPoint = new Vector3(0, -4, 0);
    public Player player;

    public int enemyCount;

    private int subStageIdx = 0;
    public GameObject[] subStages;

    public GameObject ready, clear;

    public void NextStage() {
        if (subStageIdx < subStages.Length - 1){
            subStages[subStageIdx].SetActive(false);
            subStageIdx++;
            subStages[subStageIdx].SetActive(true);

            player.transform.position = hiddenPoint;
            player.isMovingAuto = true;
            
            ready.SetActive(true);
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
        else {
            clear.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Start(){
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update() {
        if (player.isMovingAuto) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, startPoint, Time.deltaTime);

            if (player.transform.position == startPoint) {
                player.isMovingAuto = false;
                ready.SetActive(false);
            }
        }
    }
}
