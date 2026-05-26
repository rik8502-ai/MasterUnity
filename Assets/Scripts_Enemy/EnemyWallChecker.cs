using UnityEngine;

public class EnemyWallChecker : MonoBehaviour
{
    //敵キャラ
    [SerializeField] private Transform enemy;
    //[SerializeField] private Enemy enemyScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemyがnullだったらこのメソッドを中断する
        if (enemy == null) return;

        //壁を検知したら
        if (collision.CompareTag("Ground"))
        {
            EnemyMoveState enemyMoveStateScript = enemy.GetComponent<EnemyMoveState>();

            if (enemyMoveStateScript != null)
            {
                enemyMoveStateScript.FlipDirection();
            }
        }
    }
}
