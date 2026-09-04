using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;

    private void Update()
    {
        //クリスタルをその場で回転させる
        transform.Rotate(new Vector3(0,-30,0) * Time.deltaTime, Space.World);
    }


    void OnTriggerEnter(Collider other)//プレイヤーがぶつかったら自動で呼ばれる関数
    {
        if (other.CompareTag("Player"))//ぶつかった物がplayerタグを持っていたら
        {
            if(crystalPrefab != null)//エフェクトが設定されていればその場に生成する
            {
                Instantiate(crystalPrefab, transform.position, Quaternion.identity);
            }

            GameManarger.instance.AddScore(1);//クリスタルをとったら、1ポイント獲得

            Destroy(gameObject);//クリスタルを消滅
        }
    }

}
