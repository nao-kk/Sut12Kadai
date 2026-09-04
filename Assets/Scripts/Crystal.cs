using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;

    // 【ここがポイント！】2回以上点数が入るのを防ぐためのフラグ（旗）
    private bool isCollected = false;

    private void Update()
    {
        //クリスタルをその場で回転させる
        transform.Rotate(new Vector3(0, -30, 0) * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)//プレイヤーがぶつかったら自動で呼ばれる関数
    {
        // 🌟 修正ポイント：ぶつかった物がPlayerで、かつ「まだ取られていない（!isCollected）」ときだけ実行
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // 🌟 入った瞬間に即座に「取得済み」にする！これで2回目をブロックします

            if (crystalPrefab != null)//エフェクトが設定されていればその場に生成する
            {
                Instantiate(crystalPrefab, transform.position, Quaternion.identity);
            }

            if (GameManarger.instance != null)
            {
                GameManarger.instance.AddScore(1);//クリスタルをとったら、1ポイント獲得
            }

            Destroy(gameObject);//クリスタルを消滅
        }
    }
}
