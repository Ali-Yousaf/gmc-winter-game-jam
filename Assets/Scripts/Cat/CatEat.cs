using TMPro;
using UnityEngine;

public class CatEat : MonoBehaviour
{
    [SerializeField] private Transform cat;
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
                Eat();
            }
        }
    }

    void Eat()
    {
        if (nearbyItem == null)
            return;

        heldItem = nearbyItem;
        nearbyItem = null;

        heldItem.SetActive(false);
        Destroy(heldItem);

        Debug.Log("Eating...");

        CatStats.Instance.IncreaseHunger();
        interactionText.text = "";

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            nearbyItem = other.gameObject;
            Debug.Log("Press E to EAT");
            interactionText.text = "Press E to Eat";
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
