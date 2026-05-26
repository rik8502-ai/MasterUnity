using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    [Header("ここにパーティクルのプレハブを入れます")]
    [SerializeField] private GameObject hitEffectPrefab;

    [Header("エフェクトを発生させたい場所（AttackObjectなど）")]
    [SerializeField] private Transform attackPoint; // ←ここを追加

    [Header("エフェクトの大きさの倍率（1が元のサイズ）")]
    [SerializeField] private float effectScale = 0.2f;

    private void Update()
    {
        // テスト用：Hキーを押したらエフェクトを発生させる
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnHitEffect();
        }
    }

    // エフェクトを発生させるメソッド
    public void SpawnHitEffect()
    {
        if (hitEffectPrefab == null) return;

        // 発生させる位置（座標）を決定する
        Vector3 spawnPosition;

        if (attackPoint != null)
        {
            // AttackPointが設定されていれば、そのオブジェクトの位置にする
            spawnPosition = attackPoint.position;
        }
        else
        {
            // 設定されていなければ、念のためスライム自身の位置にする（エラー防止）
            spawnPosition = transform.position;
            Debug.LogWarning("attackPoint が設定されていないため、自身の位置にエフェクトを生成しました。");
        }

        // 1. 指定した位置にプレハブを生成する
        GameObject effectInstance = Instantiate(hitEffectPrefab, spawnPosition, Quaternion.identity);
        
        // 2. 生成したエフェクトの大きさを変更する
        effectInstance.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
    }
}