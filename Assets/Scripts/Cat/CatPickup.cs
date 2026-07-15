using TMPro;
using UnityEngine;

public class CatPickup : MonoBehaviour
{
    [SerializeField] private Transform cat;
    [SerializeField] private float throwForce = 8f;
    [SerializeField] private TextMeshProUGUI interactionText;

    private GameObject nearbyItem;
    private GameObject heldItem;

    void Start()
    {
        interactionText.text = "";
    }

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
                
                CatStats.Instance.IncreaseMood();
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
        interactionText.text = "";
    }

    void ThrowItem()
    {
        if (heldItem == null)
            return;

        heldItem.SetActive(true);

        Vector2 facing = CatController.Instance.GetFacingDirection();

        heldItem.transform.position = (Vector2)cat.position + facing * 0.5f + Vector2.up * 0.25f;

        Rigidbody2D rb = heldItem.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            Vector2 direction = new Vector2(
                facing.x,
                Random.Range(0.2f, 0.5f)
            ).normalized;

            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

            float spinSpeed = 720f;
            rb.angularVelocity = direction.x > 0 ? -spinSpeed : spinSpeed;
        }

        heldItem = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.gameObject;
            interactionText.text = "Press E to Pick";
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