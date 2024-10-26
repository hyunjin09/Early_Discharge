using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public void StartStage(int stageNum)
    {
        SceneManager.LoadScene("Stage"+stageNum.ToString());
    }
}
