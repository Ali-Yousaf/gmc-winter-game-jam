using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float throwForce = 8f;
    [SerializeField] private float spawnHeight = 0.5f;

    [SerializeField] private Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowTheBall();
        }
    }

    public void ThrowTheBall()
    {
        Vector3 spawnPosition = player.position + Vector3.up * spawnHeight;
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(0.5f, 1f)
            ).normalized;

            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

            // Spin in the direction of travel
            float spinSpeed = 720f; 

            if (direction.x > 0)
                rb.angularVelocity = -spinSpeed; 
            
            else
                rb.angularVelocity = spinSpeed; 
        }
    }
}