using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> spawnableObjects; // 生成可能なオブジェクトのリスト
    public Transform spawnLocation; // 生成位置
    public float spawnInterval = 5.0f; // 生成間隔（秒）
    public Transform parentObject; // 生成後の親オブジェクト

    private float timer = 0.0f;

    private void Start()
    {
        // 初回の生成を待たずにすぐに生成するために、タイマーを生成間隔より大きな値に設定します
        timer = spawnInterval + 1.0f;
    }

    private void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // タイマーが生成間隔を超えたらオブジェクトを生成
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0.0f; // タイマーをリセット
        }
    }

    private void SpawnObject()
    {
        if (spawnableObjects.Count == 0)
        {
            Debug.LogWarning("SpawnableObjectsリストにオブジェクトがありません。");
            return;
        }

        // リストからランダムにオブジェクトを選び、指定された位置に生成
        int randomIndex = Random.Range(0, spawnableObjects.Count);
        GameObject objectToSpawn = spawnableObjects[randomIndex];
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, Quaternion.identity);

        // 生成したオブジェクトの親を設定
        if (parentObject != null)
        {
            spawnedObject.transform.parent = parentObject;
        }
    }
}