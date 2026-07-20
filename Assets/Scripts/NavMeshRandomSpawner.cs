using UnityEngine;
using UnityEngine.AI; // Bắt buộc phải có để dùng NavMesh

public class NavMeshRandomSpawner : MonoBehaviour
{
    [Header("Spawn Configuration")]
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private int spawnCount = 10;

    [Header("Area Setup")]
    [SerializeField] private BoxCollider spawnZone;

    [SerializeField] private float maxNavMeshSearchDistance = 5f;
    [SerializeField] private Transform[] points;

    void Start()
    {
        if (spawnZone == null)
        {
            spawnZone = GetComponent<BoxCollider>();
        }

        foreach(var npc in prefabsToSpawn)
        {
            RandomPatrol patrol = npc.GetComponent<RandomPatrol>();
            patrol.waypoints = points;
        }

        SpawnObjectsOnNavMesh();
    }

    private void SpawnObjectsOnNavMesh()
    {
        if (prefabsToSpawn == null || prefabsToSpawn.Length == 0 || spawnZone == null)
        {
            return;
        }

        int successfullySpawned = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPositionInBox = GetRandomPositionInBounds(spawnZone.bounds);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPositionInBox, out hit, maxNavMeshSearchDistance, NavMesh.AllAreas))
            {
                Vector3 validNavMeshPosition = hit.position;

                int randomIndex = Random.Range(0, prefabsToSpawn.Length);
                Instantiate(prefabsToSpawn[randomIndex], validNavMeshPosition, Quaternion.identity);

                successfullySpawned++;
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy NavMesh hợp lệ quanh điểm: {randomPositionInBox}.");
            }
        }

        Debug.Log($"spawn thành công {successfullySpawned}/{spawnCount} objects trên NavMesh!");
    }

    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}