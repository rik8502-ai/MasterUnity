using UnityEngine;

public class EnemyCliffChecker : MonoBehaviour
{
    //敵キャラ
    [SerializeField] private Transform enemy;


    private void OnTriggerExit2D(Collider2D collision)
    {
        //enemyがnullだったらこのメソッドを中断する
        if (enemy == null) return;

        //壁を検知したら
        if (collision.CompareTag("Ground"))
        {
            //敵キャラの向きを変える
            EnemyMoveState enemyMoveStateScript = enemy.GetComponent<EnemyMoveState>(); 
            
            if (enemyMoveStateScript != null)
            {
                enemyMoveStateScript.FlipDirection();
            }
        }
    }
}
