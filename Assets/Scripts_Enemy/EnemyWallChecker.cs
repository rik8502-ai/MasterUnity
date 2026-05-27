using UnityEngine;

public class EnemyWallChecker : MonoBehaviour
{
    //敵キャラ
    [SerializeField] private Transform enemy;
    [SerializeField] private EnemyMoveState enemyMoveStateScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemyがnullだったらこのメソッドを中断する
        if (enemy == null) return;

        //壁を検知したら
        if (collision.CompareTag("Ground"))
        {
            if (enemyMoveStateScript != null)
            {
                enemyMoveStateScript.FlipDirection();
            }
        }
    }
}
