using UnityEngine;

public class SceneClearManager : MonoBehaviour
{
    private int enemyCount;
    public static SceneClearManager instance; // どこからでもアクセスできる「instance（連絡先）」という箱を作っておく

    [Header("ステージクリアのUIとかを入れる箱")]
    [SerializeField] private GameObject stageClearUI;
    [SerializeField] private GameObject MainUI;

    private void Awake()
    {        
        if (instance == null)
        {
            instance = this; // もし「instance」が空っぽだったら、このオブジェクトを「instance」にする
        }
    }
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void StageClear()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            if (stageClearUI != null)
            {
                stageClearUI.SetActive(true);
                MainUI.SetActive(false);

                Time.timeScale = 0f; // ゲームを一時停止する
            }
        }
    }
}
