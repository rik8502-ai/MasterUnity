using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    //現在のHP
    public int hp ;

    //最大HP
    private int maxHp;

    //無敵時間
    [SerializeField] private float invincibleTime = 0.5f;

    //点滅時間
    [SerializeField] private float blinkingTime = 0.1f;

    //無敵中にtrue
    public bool isInvincible { get; private set; } = false;
    private SpriteRenderer[] renderers;

    //HPのテキスト
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private Image hpBar;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject mainUI;

    //ダメージ受けた時のテキスト表示と、頭の上に表示させるために取得したコライダー
    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] Collider2D playerCollider;

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();

        if (GameManager.instance != null)
        {
            hp = GameManager.instance.playerHp;
            maxHp = GameManager.instance.playerMaxHp;
        }
        UpdateUI();

    }

    public void TakeDamage(int damageValue)
    {
        //Debug.Log( isInvincible + ", HP: " + hp );
        if (isInvincible || hp <= 0) return;

        hp -= damageValue;
        SoundManager.instance.PlaySE4();

        // 敵の位置にプレハブを生成（Instantiate）する
        // 戻り値として、生成したDamageText型のオブジェクトを「textObj」という変数で受け取る
        Vector3 spawnDamagetxtPos = new Vector3(transform.position.x, playerCollider.bounds.max.y, transform.position.z);
        DamageText textObj = Instantiate(damageTextPrefab, spawnDamagetxtPos, Quaternion.identity);
        // 生成したオブジェクト（textObj）の「Setup」メソッドを呼び出し、現在の攻撃力を渡す！
        textObj.Setup(damageValue);
        UpdateUI();
        StartCoroutine(BecomeInvincible());

        if (hp <= 0)
        {
            hp = 0;

            UpdateUI();
            Destroy(gameObject);
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
                mainUI.SetActive(false);
            }
        }
        
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
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        //無敵が終わったためfalseにする
        isInvincible = false;
    }

    //HPのテキストを更新する
    private void UpdateUI()
    {
        if (hpText != null)
        {
            hpText.text = hp.ToString();
        }

        if (hpBar != null)
        {
            hpBar.fillAmount = (float)hp / maxHp;
        }
    }

}
