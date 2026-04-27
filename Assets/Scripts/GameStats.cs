using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
    [Header("Resources")]
    public int Spice;
    public int Inhabitants;
    public int Water;

    [Header("Stats")]
    public float MilitaryPower;
    public float CommunitySupport;
    public float Stability;

    [Header("Player vs AI")]
    public int playerWater = 100; // Agua inicial del jugador
    public int aiWater = 100; // Agua inicial de la IAB


    private void Start()
    {
        StartCoroutine(RecoleccionDeAgua());
    }

    IEnumerator RecoleccionDeAgua()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // cada 10 segundos

            playerWater += 1;
            aiWater += 1;

            Debug.Log($"Recolección: Jugador = {playerWater}, IA = {aiWater}");
        }
    }
    public void AddSpice(int amount)
    {
        Spice += amount;
        Debug.Log($"Spice actualizado: {Spice}");
    }

    public void AddWater(int amount)
    {
        Water += amount;
        Debug.Log($"Agua actualizada: {Water}");
    }

    public void ChangeInhabitants(int amount)
    {
        Inhabitants = Mathf.Max(Inhabitants + amount, 0);
        Debug.Log($"Habitantes: {Inhabitants}");
    }

    public void ChangeMilitaryPower(float amount)
    {
        MilitaryPower = Mathf.Clamp(MilitaryPower + amount, 0f, 100f);
        Debug.Log($"Poder militar: {MilitaryPower}");
    }

    public void ChangeCommunitySupport(float amount)
    {
        CommunitySupport = Mathf.Clamp(CommunitySupport + amount, 0f, 100f);
        Debug.Log($"Apoyo comunitario: {CommunitySupport}");
    }

    public void ChangeStability(float amount)
    {
        Stability = Mathf.Clamp(Stability + amount, 0f, 100f);
        Debug.Log($"Estabilidad: {Stability}");
    }
}
