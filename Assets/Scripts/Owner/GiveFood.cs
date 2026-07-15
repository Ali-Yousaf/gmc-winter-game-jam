using UnityEngine;

public class GiveFood : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private Transform cat;
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float maxDistance = 1.5f;
    [SerializeField] private float spawnHeight = 0.5f;

    public void GiveFoodToCat()
    {
        float side = Random.value < 0.5f ? -1f : 1f;
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPosition = cat.position + new Vector3(side * distance, spawnHeight, 0f);

        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }
}