using UnityEngine;
using UnityEngine.Events;

public class TriggerInteraction : MonoBehaviour
{
    [SerializeField] public UnityEvent OnInteract;

    private PlayerInputReader _playerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInputReader reader))
            _playerInRange = reader;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInputReader reader) && reader == _playerInRange)
            _playerInRange = null;
    }

    private void Update()
    {
        if (_playerInRange != null && _playerInRange.InteractPressed)
        {
            Debug.Log("Interact trigger");
            OnInteract?.Invoke();
        }
    }

    private void OnDisable()
    {
        _playerInRange = null;
    }
}