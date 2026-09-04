using UnityEngine;
using System.Collections.Generic;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab; // 生成するクリスタルのプレハブ
    [SerializeField] private Transform[] spawnPoints; // クリスタルを生成する位置の配列
    [SerializeField] private int SpawnCount = 6; // 生成するクリスタルの数


    void Start()
    {
        SpawnCrystals();
    }

    public void SpawnCrystals()
    {
        if (crystalPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.Log("クリスタルのッ設定足りてないよ");
            return;
        }

        List<Transform> pointsList = new List<Transform>(spawnPoints);
        for (int i = 0; i < pointsList.Count; i++)
        {
            Transform temp = pointsList[i];
            int randomIndex = Random.Range(i, pointsList.Count);
            pointsList[i] = pointsList[randomIndex];
            pointsList[randomIndex] = temp;
        }

        int actualSpawnCount = Mathf.Min(SpawnCount, pointsList.Count);
        for (int i = 0; i < actualSpawnCount; i++)
        {
            Instantiate(crystalPrefab, pointsList[i].position, pointsList[i].rotation);
        }

    }
}
