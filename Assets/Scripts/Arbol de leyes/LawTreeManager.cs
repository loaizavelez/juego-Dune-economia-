using UnityEngine;

public class LawTreeManager : MonoBehaviour
{
    [SerializeField] private LawNode[] rootLaws;


    public void  Start()
    {
        foreach (var law in rootLaws)
        {
            law.lawButton.interactable = true;
        }
    }
    public void ApplyLaw(LawNode law)
    {
        if (law.isApplied)
        {
            Debug.Log($"La ley {law.lawName} ya fue aplicada.");
            return;
        }

        law.isApplied = true;
        law.lawButton.interactable = false;
        Debug.Log($"Ley aplicada: {law.lawName}");

       
        foreach (var unlocked in law.unlockedLaws)
        {
            unlocked.lawButton.interactable = true;
            Debug.Log($"Nueva ley disponible: {unlocked.lawName}");
        }
    }

}
