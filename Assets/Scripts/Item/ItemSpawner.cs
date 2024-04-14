//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Wave TestWave; //only for testing, use SpawnWave to spawn a wave instead
    [SerializeField] bool runOnStart = true;

    [SerializeField] Vector2 bounds;
    [SerializeField] Vector2Int granularity;

    Vector2 halfExtents => bounds * 0.5f;
    HashSet<(int, int)> spawnRecord = new();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < granularity.x; i++)
        {
            var posX = transform.position.x - halfExtents.x + bounds.x/granularity.x * i;
            Gizmos.DrawLine(new Vector3(posX, transform.position.y - halfExtents.y), new Vector3(posX, transform.position.y + halfExtents.y));
        }
        for (int j = 0; j < granularity.y; j++)
        {
            var posY = transform.position.y - halfExtents.y + bounds.y / granularity.y * j;
            Gizmos.DrawLine(new Vector3(transform.position.x - halfExtents.x, posY), new Vector3(transform.position.x + halfExtents.x, posY));
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(bounds.x, bounds.y));
    }

    private void Start()
    {
        if (runOnStart) RunTestWave();
    }

    private void ResetSpawnRecord() => spawnRecord.Clear();

    [ContextMenu("Clear Wave")]
    public void ClearWave()
    {
        ResetSpawnRecord();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    [ContextMenu("Test Wave")]
    public void RunTestWave()
    {
        ClearWave();
        SpawnWave(TestWave.items, TestWave.amounts);
    }

    public void SpawnWave(Item[] items, int[] amounts)
    {
        for (int i = 0; i < Mathf.Min(items.Length, amounts.Length); i++)
        {
            Spawn(items[i], amounts[i]);
        }
    }

    public void Spawn(Item prefab, int amount = 1)
    {
        int loopSafety = 100;
        while (amount > 0 && --loopSafety >= 0)
        {
            int xOff = Random.Range(0, granularity.x);
            int yOff = Random.Range(0, granularity.y);

            if (spawnRecord.Contains((xOff, yOff))) continue;
            spawnRecord.Add((xOff, yOff));

            Vector2 randomPos = (Vector2)transform.position - halfExtents + new Vector2(xOff, yOff) * bounds / granularity;
            Vector2 unitOffset = bounds / granularity * 0.5f;
            Instantiate(prefab, randomPos + unitOffset, Quaternion.identity, transform);
            --amount;
        }
        
        if (amount > 0) 
        {
            Debug.LogWarning("Spawn record cleared due to too many collisions. Consider spawning fewer items or increasing granularity.");
            ResetSpawnRecord();
            Spawn(prefab, amount);
        }
    }

}
