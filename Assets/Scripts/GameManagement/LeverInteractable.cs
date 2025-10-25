using UnityEngine;

public class LeverInteractable : MonoBehaviour
{
    public FireAlarmScript fireAlarmScript;
    public float interactRange = 2f;
    public string interactKey = "e";
    public bool oneUse = true;

    private bool playerNearby = false;
    private GameObject player;
    private Collider leverCollider;
    private ItemHolder playerItemHolder;

    void Start()
    {
        leverCollider = GetComponent<Collider>();
        if (leverCollider == null)
            Debug.LogWarning("LeverInteractable requires a Collider.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject;
            playerItemHolder = other.GetComponent<ItemHolder>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player = null;
            playerItemHolder = null;
        }
    }

    void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Disable item holder temporarily to prevent E key conflicts
            if (playerItemHolder != null)
                playerItemHolder.enabled = false;

            UseLever();

            // Re-enable after a short delay
            if (playerItemHolder != null)
                Invoke("ReEnableItemHolder", 0.1f);
        }
    }

    void ReEnableItemHolder()
    {
        if (playerItemHolder != null)
            playerItemHolder.enabled = true;
    }

    void UseLever()
    {
        if (fireAlarmScript == null)
        {
            Debug.LogWarning("LeverInteractable: fireAlarmScript not set.");
            return;
        }

        fireAlarmScript.DeactivateAlarm();

        if (oneUse)
        {
            // disable so it can't be used again
            leverCollider.enabled = false;
        }
    }
}