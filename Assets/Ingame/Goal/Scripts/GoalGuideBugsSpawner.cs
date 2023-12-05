using UnityEngine;

public class GoalGuideBugsSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform targetPos;
    public GameObject goalGuideBugsPrefab;
    public float spawnInterval = 5.0f; // 秒単位での生成間隔

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnGoalGuideBug();
            timer = 0f;
        }
    }

    void SpawnGoalGuideBug()
    {
        GameObject bug = Instantiate(goalGuideBugsPrefab, spawnPoint.position, Quaternion.identity);
        GoalGuideBugController controller = bug.GetComponent<GoalGuideBugController>();
        if (controller != null)
        {
            controller.SetTarget(targetPos);
        }
    }
}