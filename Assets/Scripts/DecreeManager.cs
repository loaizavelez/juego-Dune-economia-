using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

/*
public class DecreeManager : MonoBehaviour
{
    [SerializeField] private List<DecreeData> availableDecrees;
    [SerializeField] private GameStats stats;

    public void ApplyDecree(DecreeData decree)
    {

        if (decree.IsApplied)
        {
            Debug.Log($"El decreto {decree.DecreeName} ya fue aplicado y no puede repetirse.");
            return;
        }
        // Aquí aplicas los efectos del decreto
        Debug.Log($"Aplicando decreto: {decree.DecreeName}");
     

        stats.AddSpice(decree.ProductionChange);
        stats.ChangeCommunitySupport(decree.CommunitySupportChange);
        stats.ChangeStability(+10);

        decree.IsApplied = true;

    }

    public void ApplyBanespecia()
    {
        ApplyDecree(availableDecrees[0]); // primera posición en la lista 
    }

    public void ApplyWaterRationing()
    {
        ApplyDecree(availableDecrees[1]); // segunda posición en la lista
    }
     public void ApplyMilitaryDraft()
    {
        ApplyDecree(availableDecrees[2]); // tercera posición en la lista
    }

}
*/