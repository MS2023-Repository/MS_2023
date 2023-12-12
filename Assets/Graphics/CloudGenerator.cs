using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject cloudPrefab; // 雲のプレハブ
    public Transform[] spawnPoints; // 雲を生成する位置のTransform配列
    public float cloudSpeed = 2f; // 雲のX軸方向の速度

    private float generationInterval = 5f; // 雲を生成する間隔
    private int numberOfCloudsToGenerate = 3; // 一度に生成する雲の数
    private float cloudLifetime = 20f; // 雲の寿命

    private void Start()
    {
        StartCoroutine(GenerateClouds());
    }

    private IEnumerator GenerateClouds()
    {
        while (true)
        {
            // ランダムに5つのTransformを選択
            List<Transform> selectedSpawnPoints = new List<Transform>();
            int numberOfSpawnPoints = spawnPoints.Length;
            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, numberOfSpawnPoints);
                selectedSpawnPoints.Add(spawnPoints[randomIndex]);
            }

            // 選択されたTransformから雲を生成
            foreach (Transform spawnPoint in selectedSpawnPoints)
            {
                GameObject newCloud = Instantiate(cloudPrefab, spawnPoint.position, Quaternion.identity);

                // 雲をX軸方向に移動
                Rigidbody cloudRigidbody = newCloud.GetComponent<Rigidbody>();
                if (cloudRigidbody != null)
                {
                    cloudRigidbody.velocity = new Vector3(cloudSpeed, 0f, 0f);
                }

                Destroy(newCloud, cloudLifetime);
            }

            yield return new WaitForSeconds(generationInterval);
        }
    }
}