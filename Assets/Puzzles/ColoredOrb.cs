using System.Collections;
using UnityEngine;

public class ColoredOrb : MonoBehaviour
{
   [SerializeField] private int _OrbNumber;
    private bool isFlashing;

    private SpriteRenderer spriteRenderer;
    public int OrbNumber => _OrbNumber;
    private ColoredOrbsPuzzle coloredOrbsPuzzle;

    private void Start()
    {
        coloredOrbsPuzzle = GetComponentInParent<ColoredOrbsPuzzle>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out _))
        {
            coloredOrbsPuzzle.CheckCollectedOrb(_OrbNumber);
        }
    }

    public IEnumerator FlashSpriteRoutine()
    {
        if (spriteRenderer == null || !spriteRenderer.enabled) yield break;
        isFlashing = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red; ;
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}
