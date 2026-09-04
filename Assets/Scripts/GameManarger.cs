using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class GameManarger : MonoBehaviour
{
    public static GameManarger instance;

    // 🌟【修正】テキストの代わりに、クリスタル画像（6個分）を入れる配列を用意
    [SerializeField] private Image[] crystalImages;
    [SerializeField] private float timeLimit = 60f;

    [SerializeField] private TextMeshProUGUI timerText; // タイマーテキストはそのまま維持

    private int count = 0;
    private bool isGameActive = true;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        UpdatecountText(); // 初期状態（すべてグレー）にする
        UpdateTimerText();
        CountData.finalScore = 0;

        CrystalSpawner crystalSpawner = FindFirstObjectByType<CrystalSpawner>();
        if (crystalSpawner != null) { crystalSpawner.SpawnCrystals(); }
    }

    void Update()
    {
        if (isGameActive)
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0f)
            {
                timeLimit = 0f;
                GameOver(false);
            }
            UpdateTimerText();
        }
    }

    public void AddScore(int amount)
    {
        if (!isGameActive) return;

        count += amount;
        CountData.finalScore = count;
        UpdatecountText(); // 🌟ここで画像のカラーを更新

        if (count >= 6)
        {
            GameOver(true);
        }
    }

    // 🌟【大幅修正】スコアに応じて画像のグレー／カラーを切り替える
    void UpdatecountText()
    {
        if (crystalImages == null || crystalImages.Length == 0) return;

        for (int i = 0; i < crystalImages.Length; i++)
        {
            if (crystalImages[i] != null)
            {
                if (i < count)
                {
                    // 🌟獲得したクリスタル：元の鮮やかな色（カラー）にする
                    crystalImages[i].color = Color.white;
                }
                else
                {
                    // 🌟まだ取っていないクリスタル：半透明のグレーにする
                    crystalImages[i].color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
                }
            }
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLimit).ToString();
        }
    }

    void GameOver(bool isClear)
    {
        isGameActive = false;
        CountData.isClear = isClear;

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

public static class CountData
{
    public static int finalScore;
    public static bool isClear;
}