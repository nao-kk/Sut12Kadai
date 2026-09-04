using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public static FadeInEffect instance;

    [Header("エディタからセットするフェード用UI")]
    public Canvas fadeCanvas; // ステップ1で作ったFadeCanvas
    public Image fadeImage;   // ステップ1で作ったFadeImage

    public float fadeDuration = 1.0f; // フェードにかける時間（秒）

    void Awake()
    {
        // シーンが切り替わっても、このオブジェクト（と子要素のCanvas）を消さない設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 開始時は確実に透明にしておく
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    // 外部（GameManagerなど）から呼び出される関数
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeSequence(sceneName));
    }

    IEnumerator FadeSequence(string sceneName)
    {
        float elapsedTime = 0f;
        Color c = fadeImage.color;

        // 1. フェードアウト（徐々に黒くする）
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 1f;
        fadeImage.color = c;

        // 2. シーン切り替え
        SceneManager.LoadScene(sceneName);

        // シーンの読み込みが完全に完了するまで1フレーム待つ
        yield return null;

        // 3. フェードイン（徐々に透明にする）
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0f;
        fadeImage.color = c;
    }
}
