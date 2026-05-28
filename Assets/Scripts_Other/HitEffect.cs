using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("ここにパーティクルのプレハブを入れます")]
    [SerializeField] private GameObject hitEffectPrefab;

    [Header("エフェクトの大きさの倍率（1が元のサイズ）")]
    [SerializeField] private float effectScale = 0.2f;


    // エフェクトを発生させるメソッド
    public void SpawnHitEffect(Transform effectPoint)
    {
        if (hitEffectPrefab == null) return;

        // 発生させる位置（座標）を決定する
        Vector3 spawnPosition;

        if (effectPoint != null)
        {
            // AttackPointが設定されていれば、そのオブジェクトの位置にする
            spawnPosition = effectPoint.position;
        }
        else
        {
            spawnPosition = transform.position;
            Debug.LogWarning("attackPoint が設定されていないため、自身の位置にエフェクトを生成しました。");
        }

        // 1. 指定した位置にプレハブを生成する
        GameObject effectInstance = Instantiate(hitEffectPrefab, spawnPosition, Quaternion.identity);
        
        // 2. 生成したエフェクトの大きさを変更する
        effectInstance.transform.localScale = new Vector3(effectScale, effectScale, effectScale);

        Destroy(effectInstance, 1f); // 1秒後にエフェクトを消す（エフェクトの長さに合わせて調整してください）
    }
}