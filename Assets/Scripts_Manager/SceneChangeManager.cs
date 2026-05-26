using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private string titleSceneName;
    [SerializeField] private string stageSceneName;

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
