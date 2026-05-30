using UnityEngine;

public class Fisreball : MonoBehaviour
{
    [Header("弾の飛ぶスピード")]
    [SerializeField] private float speed = 10f;
    
    [Header("攻撃力")]
    [SerializeField] private int damage = 1;
    
    [Header("何秒後に自動で消滅するか（画面外まで飛んだ時用）")]
    [SerializeField] private float lifeTime = 3f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 生成された瞬間、自分自身の「右方向(X軸)」に向かってスピードを設定する
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }

        // 撃ちっぱなしでゴミが溜まらないよう、数秒後に自動で消滅させる
        Destroy(gameObject, lifeTime);
    }

    // 何かにぶつかった瞬間の処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵に当たった時
        if (collision.CompareTag("Enemy"))
        {
            // 敵のHPスクリプトを取得してダメージを与える
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(damage,2);
                SoundManager.instance.PlaySE2();
            }

            // 当たったらファイアーボール自身を消滅させる
            Destroy(gameObject);
        }
        // もし「壁（Groundなど）」に当たっても消えるようにしたい場合は、ここに条件を追加します
    }
}