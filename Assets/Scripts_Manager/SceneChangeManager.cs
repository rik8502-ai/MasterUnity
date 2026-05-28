using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private string titleSceneName;
    [SerializeField] private string stageSceneName;

    private void Awake()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // ゲームが一時停止している状態でこのスクリプトが呼ばれたら、ゲームを再開する
        }
    }

    //ステージに遷移する
    public void OnClickGoStage()
    {
        SceneManager.LoadScene(stageSceneName);
    }

    //タイトル画面に遷移する
    public void OnClickGoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
