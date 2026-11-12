using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColoredOrb : MonoBehaviour
{
   [SerializeField] private int _OrbNumber;
    private bool isFlashing;

    [SerializeField] private Light2D spotLight;
    private SpriteRenderer spriteRenderer;
    public int OrbNumber => _OrbNumber;
    private ColoredOrbsPuzzle coloredOrbsPuzzle;

    private void Start()
    {
        coloredOrbsPuzzle = GetComponentInParent<ColoredOrbsPuzzle>();
        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    public void CheckOrb()
    {
        if (coloredOrbsPuzzle.CheckCollectedOrb(_OrbNumber)) spotLight.enabled = true;
    }

    public IEnumerator FlashSpriteRoutine()
    {
        if (spriteRenderer == null || !spriteRenderer.enabled) yield break;
        isFlashing = true;
        spotLight.enabled = true;
        yield return new WaitForSeconds(0.1f);

        spotLight.enabled = false;
        isFlashing = false;
    }
}
