using UnityEngine;

public class GameManager : MonoBehaviour
{
    // どこからでもアクセスできる連絡先
    public static GameManager instance;

    [Header("プレイヤーのHPデータ（ここに記憶する）")]
    public int playerHp;
    public int playerMaxHp = 5;

    private void Awake()
    {
        // ★超重要：すでに同じ管理人がいたら、自分を破壊して重複を防ぐ
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // ★最強の呪文：このオブジェクトを「シーンが切り替わっても絶対に破壊しない」設定にする！
        DontDestroyOnLoad(gameObject);

        // ゲーム開始時にHPを満タンにする
        playerHp = playerMaxHp;
    }

    public void Reset()
    {
        // ゲーム開始時にHPを満タンにする
        playerHp = playerMaxHp;
    }
}