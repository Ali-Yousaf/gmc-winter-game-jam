using UnityEngine;

public class CatPickup : MonoBehaviour
{
    [SerializeField] private Transform cat;
    [SerializeField] private float throwForce = 8f;

    private GameObject nearbyItem;
    private GameObject heldItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                PickUp();
            }

            else
            {
                ThrowItem();
            }
        }
    }

    void PickUp()
    {
        if (nearbyItem == null)
            return;

        heldItem = nearbyItem;
        nearbyItem = null;

        heldItem.SetActive(false);

        Debug.Log("Picked up!");
    }

    void ThrowItem()
    {
        heldItem.SetActive(true);

        heldItem.transform.position = cat.position + Vector3.up * 0.5f;

        Rigidbody2D rb = heldItem.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;

            Vector2 direction = new Vector2(
                Random.Range(-0.7f, 0.7f),
                Random.Range(0.2f, 0.5f)
            ).normalized;

            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }

        heldItem = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.gameObject;
            Debug.Log("Press E to Pick");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearbyItem)
        {
            nearbyItem = null;
        }
    }
}