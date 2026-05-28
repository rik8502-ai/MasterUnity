using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    //現在のHP
    private int hp;
    //最大HP
    [SerializeField] private int maxHp = 5;

    //無敵時間
    [SerializeField] private float invincibleTime = 0.5f;

    //点滅時間
    [SerializeField] private float blinkingTime = 0.1f;

    //ダメージ受けた時のテキスト表示と、頭の上に表示させるために取得したコライダー
    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] private HitEffect hitEffectSpawner;
    [SerializeField] private Transform effectPoint;

    //無敵中にtrue
    private bool isInvincible = false;

    //private SpriteRenderer sr;
    //子のスプライトレンダラーを入れる箱
    [SerializeField] private SpriteRenderer[] renderers;
    //HPバー
    [SerializeField] private Image hpBar;

    private void Start()
    {
        hp = maxHp;
        UpdateUI();
    }

    public bool TakeDamage(int damageValue)
    {
        
        //戻り値を追加して、ダメージが入ったかどうかを返すようにする（playerAttackHitbox）
        if(hp <= 0 || isInvincible) return false; //HPが0以下 or 無敵中だったらこのメソッドを中断する
        hp -= damageValue; //ダメージ分HPを減らす
        UpdateUI();
        hitEffectSpawner.SpawnHitEffect(effectPoint); //ヒットエフェクト

        //無敵と点滅の処理
        StartCoroutine(BecomeInvincible());

        // 敵の位置にプレハブを生成（Instantiate）する
        // 戻り値として、生成したDamageText型のオブジェクトを「textObj」という変数で受け取る
        Vector3 spawnDamagetxtPos = new Vector3(transform.position.x, enemyCollider.bounds.max.y, transform.position.z);
        DamageText textObj = Instantiate(damageTextPrefab, spawnDamagetxtPos, Quaternion.identity);
        // 生成したオブジェクト（textObj）の「Setup」メソッドを呼び出し、現在の攻撃力を渡す！
        textObj.Setup(damageValue);

        if (hp <= 0) //HPが0以下になったら
        {
            hp = 0; //HPが0以下にならないようにする
            
            //ステージクリアかどうかの処理
            if(SceneClearManager.instance != null)
            {
                SceneClearManager.instance.StageClear();
            }

            UpdateUI();
            Destroy(gameObject); 
            
        }
        return true;
    }

    private IEnumerator BecomeInvincible()
    {
        //無敵中であるためtrueにする
        isInvincible = true;

        //タイマー用意
        float timer = 0;

        //もしタイマーが指定時間を過ぎていなければ無敵継続（ループ）
        while (timer < invincibleTime)
        {
            //スプライトの表示・非表示を切り替える
            //sr.enabled = !sr.enabled;
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.enabled = !renderer.enabled;
                }
            //タイマーに点滅時間を足す
            timer += blinkingTime;

            //点滅時間待機
            yield return new WaitForSeconds(blinkingTime);
        }

        //スプライトを表示する
        //sr.enabled = true;
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
        //無敵が終わったためfalseにする
        isInvincible = false;
    }
    
    private void UpdateUI()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = (float)hp / maxHp;
        }
    }

}
