using UnityEngine;
using UnityEngine.Events;

public class TriggerInteraction : MonoBehaviour
{
    [SerializeField] public UnityEvent OnInteract;
    private bool playerInRange;
    private PlayerInputReader inputReader;
    public UnityEvent OnplayerInRangeUpdated;
    public UnityEvent OnplayerEnterRange;
    public UnityEvent OnplayerLeaveRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInputReader reader))
        {
            inputReader = reader;
            SetPlayerInRange(true);
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInputReader reader) && reader == inputReader)
        {
            inputReader = null;
            SetPlayerInRange(false);
        }
            
    }

    private void Update()
    {
        if (inputReader != null && inputReader.InteractPressed)
        {
            Debug.Log("Interact trigger");
            OnInteract?.Invoke();
        }
    }

    private void OnDisable()
    {
        inputReader = null;
    }

    
    public void SetPlayerInRange(bool value)
    {
        playerInRange = value;

        if(!playerInRange)
        {
            OnplayerLeaveRange?.Invoke();
        }
        else if (playerInRange)
        {
            OnplayerEnterRange?.Invoke();
        }

            OnplayerInRangeUpdated?.Invoke();
    }
}