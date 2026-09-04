using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class GameManarger : MonoBehaviour
{
    public static GameManarger instance;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeLimit = 60f; // タイマーの制限時間（秒）

    private int count = 0;
    private bool isGameActive = true; // ゲーム中かどうかのフラグ

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        UpdatecountText();
        UpdateTimerText();
        CountData.finalScore = 0; // ゲーム開始時にスコアをリセット

        CrystalSpawner crystalSpawner = FindFirstObjectByType<CrystalSpawner>();
        // 🛠️ 修正1：変数の名前を crystalSpawner に統一
        if (crystalSpawner != null) { crystalSpawner.SpawnCrystals(); }
    }

    void Update()
    {
        if (isGameActive)
        {
            timeLimit -= Time.deltaTime; // 毎フレーム経過時間を引く

            if (timeLimit <= 0f)
            {
                timeLimit = 0f;
                GameOver(false); // 🛠️ 修正2：時間切れは「false（ゲームオーバー）」で終了
            }
            UpdateTimerText();
        }
    }

    public void AddScore(int amount)
    {
        if (!isGameActive) return;

        // 🛠️ 修正3：クラス名ではなく、内部の変数 count に加算する
        count += amount;
        CountData.finalScore = count;
        UpdatecountText();

        if (count >= 6)
        {
            GameOver(true); // 🛠️ 修正4：6個集まったら「true（クリア）」で終了
        }
    }

    // 🛠️ 修正5：関数名がバラバラだったのを「UpdatecountText」に統一
    void UpdatecountText()
    {
        if (countText != null)
        {
            countText.text = "Crystal: " + count.ToString() + " / 6";
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLimit).ToString();
        }
    }

    // 🛠️ 修正6：引数 (bool isClear) を追加して結果をリザルトへ送る
    void GameOver(bool isClear)
    {
        isGameActive = false;
        CountData.isClear = isClear; // クリアしたかどうかの結果を保存

        Debug.Log(isClear ? "ゲームクリア！" : "ゲームオーバー！");

        if (FadeInEffect.instance != null)
        {
            FadeInEffect.instance.FadeToScene("Result"); 
        }
        else
        {
            SceneManager.LoadScene("Result");
        }
    }
}

// 🛠️ 修正7：スコアとクリアフラグを保存するクラス（ここに書いておけばエラーになりません）
public static class CountData
{
    public static int finalScore;
    public static bool isClear;
}