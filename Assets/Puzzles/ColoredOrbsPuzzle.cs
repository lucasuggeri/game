using System.Collections;
using UnityEngine;

public class ColoredOrbsPuzzle : MonoBehaviour
{
    [SerializeField] private int[] order;

    [SerializeField] private ColoredOrb[] orbs;
    private int currentOrderIndex = 0;
    private bool isShowingOrder; 
    [SerializeField] private float delayBetweenFlashes = 0.3f;
    public bool CheckCollectedOrb(int number)
    {
        if(number == order[currentOrderIndex])
        {
            currentOrderIndex++;
            Debug.Log("acertou");
            if (currentOrderIndex >=  order.Length)
            {
                CheckForClear();

                return true;
            }

            return true;
        }

        else if (number != order[currentOrderIndex])
        {
            Debug.Log("Errou");
            return false;
           
        }

        return false;
    }

    private void CheckForClear()
    {
        GameManager.instance.MarkPuzzleAsCompleted(0);
    }


    public void ShowOrder()
    {
        if (isShowingOrder) return;
        Debug.Log("Show order");
        StartCoroutine(ShowOrderRoutine());
    }

    private IEnumerator ShowOrderRoutine()
    {
        isShowingOrder = true;
        currentOrderIndex = 0;

        // percorre a ORDEM correta e procura a orb com aquele número
        for (int i = 0; i < order.Length; i++)
        {
            ColoredOrb orb = FindOrb(order[i]);
            if (orb != null)
            {
                yield return orb.FlashSpriteRoutine(); // espera a piscada terminar
                yield return new WaitForSeconds(delayBetweenFlashes);
            }
        }

        isShowingOrder = false;
    }

    private ColoredOrb FindOrb(int number)
    {
        foreach (ColoredOrb orb in orbs)
        {
            if (orb.OrbNumber == number)
                return orb;
        }
        return null;
    }
}
