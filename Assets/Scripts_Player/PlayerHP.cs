using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    //現在のHP
    private int hp;

    //最大HP
    [SerializeField] private int maxHp = 5;

    //無敵時間
    [SerializeField] private float invincibleTime = 0.5f;

    //点滅時間
    [SerializeField] private float blinkingTime = 0.1f;

    //無敵中にtrue
    public bool isInvincible = false;

    private SpriteRenderer[] renderers;

    //HPのテキスト
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private Image hpBar;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject mainUI;

    private void Start()
    {
        hp = maxHp;

        //sr = GetComponent<SpriteRenderer>();
        renderers = GetComponentsInChildren<SpriteRenderer>();

        UpdateUI();
    }

    public void TakeDamage(int damageValue)
    {
        if (isInvincible || hp <= 0) return;

        hp -= damageValue;

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
