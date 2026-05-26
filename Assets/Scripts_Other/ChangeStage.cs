using UnityEngine;
using UnityEngine.SceneManagement; // ★シーン移動機能を使うために絶対に必要です！

public class ChangeStage : MonoBehaviour
{
    [Header("移動先のシーン（ステージ）の名前")]
    [SerializeField] private string nextSceneName = "Stage2";

    // プレイヤーが出口の前にいるかどうかを判定するフラグ
    private bool isPlayerAtDoor = false;

    private void Update()
    {
        // プレイヤーが出口の前にいて、かつ左クリック(0)されたら
        if (isPlayerAtDoor == true && Input.GetMouseButtonDown(0))
        {
            GoToNextStage();
        }
    }

    // コライダー（Is Trigger）の中に誰かが入ってきた時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 入ってきたのがプレイヤーだったらフラグをON
        if (collision.CompareTag("Player"))
        {
            isPlayerAtDoor = true;
        }
    }

    // コライダー（Is Trigger）の中から誰かが出た時
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 出ていったのがプレイヤーだったらフラグをOFF
        if (collision.CompareTag("Player"))
        {
            isPlayerAtDoor = false;
        }
    }

    // 次のステージへ移動する処理
    private void GoToNextStage()
    {
        // 指定した名前のシーンをロードする
        SceneManager.LoadScene(nextSceneName);
    }
}