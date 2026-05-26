using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    //攻撃力
    [SerializeField] private int attackPower = 1;
    [SerializeField] private PlayerHitEffect hitEffectSpawner;

    //接触した瞬間を検知する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵に当たったとき
        if (collision.CompareTag("Enemy"))
        {
            //EnemyHPスクリプトを取得する
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();
            
            //EnemyHPスクリプトnullじゃなかったら
            if (enemyHP != null)
            {
                //TakeDamageでダメージがあった時のみ以下の処理を行う
                if (enemyHP.TakeDamage(attackPower))
                {
                    hitEffectSpawner.SpawnHitEffect();
                }
            }

        }
    }
}
