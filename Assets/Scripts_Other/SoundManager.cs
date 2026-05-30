using UnityEngine;
// AudioMixer使用するために必要
using UnityEngine.Audio;
// スライダーを使用するために必要
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    public AudioClip se1;
    public AudioClip se2;
    public AudioClip se3;

    public AudioClip se4;

    [SerializeField] private AudioSource seAudioSource;

        private void Awake()
    {
        // シングルトンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもこのオブジェクトを破棄しない
        }
        else
        {
            Destroy(gameObject); // すでにインスタンスが存在する場合はこのオブジェクトを破棄する
        }

    }

    private void Start()
    {
        if (bgmSlider != null)
        {
            // BGMのボリュームをbgmSliderで調整できるようにする
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        }

        if (seSlider != null)
        {
            // SEのボリュームをseSliderで調整できるようにする
            seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
        }

    }

    // スライダーの値をdB（デシベル）に変換して、AudioMixerの「BGM」パラメータに反映させる
    private void OnBGMVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        // "BGM"はAudioMixerで定義したパラメータ名をと一致している必要がある
        audioMixer.SetFloat("BGM", decibel);
    }

    // スライダーの値をdB（デシベル）に変換して、AudioMixerの「SE」パラメータに反映させる
    private void OnSEVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        // "SE"はAudioMixerで定義したパラメータ名をと一致している必要がある
        audioMixer.SetFloat("SE", decibel);
    }

    public void PlaySE1()
    {
        seAudioSource.PlayOneShot(se1);
    }

    public void PlaySE2()
    {
        seAudioSource.PlayOneShot(se2);
    }

    public void PlaySE3()
    {
        seAudioSource.PlayOneShot(se3);
    }

    public void PlaySE4()
    {
        seAudioSource.PlayOneShot(se4);
    }
}