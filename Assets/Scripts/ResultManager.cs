using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [Header("結果を表示するテキスト（例：GAME CLEAR! / GAME OVER）")]
    [SerializeField] private TextMeshProUGUI statusText;


    void Start()
    {

        // 🌟ここが最重要！クリアフラグ（isClear）を見て文字を完全に切り替えます
        if (statusText != null)
        {
            if (CountData.isClear)
            {
                statusText.text = "GAME CLEAR!";
                statusText.color = Color.yellow; // クリア時は黄色
            }
            else
            {
                statusText.text = "GAME OVER";
                statusText.color = Color.red; // タイムオーバー（ゲームオーバー）時は赤色
            }
        }
    }
}
