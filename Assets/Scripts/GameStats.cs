using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
    public enum Faction
    {
        Fremen,
        Harkonnen
    }

    [System.Serializable]
    public class PlayerStats
    {
        [Header("Resources")]
        public float Spice;
        public int Inhabitants;
        public float Water;

        [Header("Stats")]
        public float AttackPower;
        public float DefensePower;
        public float CommunitySupport;
        public float Stability;

        [Header("Faction")]
        public Faction faction;
        public float waterConsumptionRate; // proporción de agua por habitante

        public PlayerStats(Faction chosenFaction, int initialWater)
        {
            faction = chosenFaction;
            Spice = 0;
            Inhabitants = 100;
            Water = initialWater;

            switch (faction)
            {
                case Faction.Fremen:
                    AttackPower = 8;   // menos ataque
                    DefensePower = 12; // más defensa
                    CommunitySupport = 20;
                    Stability = 60;
                    waterConsumptionRate = 0.3f; // menor consumo de agua por habitante
                    break;

                case Faction.Harkonnen:
                    AttackPower = 12;  // más ataque
                    DefensePower = 8;  // menos defensa
                    CommunitySupport = 10;
                    Stability = 40;
                    waterConsumptionRate = 0.5f; // consumo estándar de agua por habitante
                    break;
            }
        }

        // 🔹 Convertir habitantes en ataque
        public void ConvertInhabitantsToAttack(int amount)
        {
            if (Inhabitants >= amount)
            {
                Inhabitants -= amount;
                AttackPower += amount;
                Debug.Log($"[{faction}] Convertidos {amount} habitantes en AttackPower. Nuevo AttackPower: {AttackPower}, Inhabitants: {Inhabitants}");
            }
            else
            {
                Debug.Log($"[{faction}] No hay suficientes habitantes para convertir en AttackPower.");
            }
        }

        // 🔹 Convertir habitantes en defensa
        public void ConvertInhabitantsToDefense(int amount)
        {
            if (Inhabitants >= amount)
            {
                Inhabitants -= amount;
                DefensePower += amount;
                Debug.Log($"[{faction}] Convertidos {amount} habitantes en DefensePower. Nuevo DefensePower: {DefensePower}, Inhabitants: {Inhabitants}");
            }
            else
            {
                Debug.Log($"[{faction}] No hay suficientes habitantes para convertir en DefensePower.");
            }
        }
    }

    [Header("Player vs Player")]
    public PlayerStats player1;
    public PlayerStats player2;

    public ControllerManager.Controller currentTurn = ControllerManager.Controller.Player1;

    public int movesRemaining = 3;
    public int turnCounter = 1;

    private void Start()
    {
        // 🔹 Ejemplo: selección inicial de facción (puedes reemplazar con menú de selección)
        player1 = new PlayerStats(FactionSelectionUI.player1Faction, 100);
        player2 = new PlayerStats(FactionSelectionUI.player2Faction, 100);

        StartCoroutine(RecoleccionDeAgua());
    }

    IEnumerator RecoleccionDeAgua()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); // cada 30 segundos

            // 🔹 Recolección de agua
            player1.Water += 1;
            player2.Water += 1;

            // 🔹 Ajuste de población según agua
            UpdatePopulation(player1);
            UpdatePopulation(player2);

            Debug.Log($"Recolección: Jugador 1 = {player1.Water}, Jugador 2 = {player2.Water}");
        }
    }

    // 🔹 Balance de población según agua
    public void UpdatePopulation(PlayerStats player)
    {
        float requiredWater = player.Inhabitants * player.waterConsumptionRate;

        if (player.Water >= requiredWater)
        {
            player.Inhabitants += 5; // población crece
            Debug.Log($"[{player.faction}] Población aumentó. Habitantes: {player.Inhabitants}, Agua: {player.Water}");
        }
        else
        {
            player.Inhabitants = Mathf.Max(player.Inhabitants - 5, 0); // población decrece
            Debug.Log($"[{player.faction}] Población disminuyó. Habitantes: {player.Inhabitants}, Agua: {player.Water}");
        }
    }

    // 🔹 Consumir movimiento
    public void ConsumeMove()
    {
        movesRemaining--;
        Debug.Log($"Movimientos restantes: {movesRemaining}");

        if (movesRemaining <= 0)
        {
            ChangeTurn();
        }
    }

    // 🔹 Cambiar turno
    public void ChangeTurn()
    {
        currentTurn = (currentTurn == ControllerManager.Controller.Player1) ? ControllerManager.Controller.Player2 : ControllerManager.Controller.Player1;
        movesRemaining = 3;
        turnCounter++;

        Debug.Log($"➡️ Cambio de turno. Ahora juega: {currentTurn}. Turno global: {turnCounter}");
    }

    // 🔹 Métodos auxiliares para HUD
    public int GetMovesRemaining() => movesRemaining;
    public int GetTurnCounter() => turnCounter;

    // 🔹 Métodos para modificar stats de cada jugador
    public void AddSpice(PlayerStats player, int amount)
    {
        player.Spice += amount;
        Debug.Log($"Spice actualizado: {player.Spice}");
    }

    public void AddWater(PlayerStats player, int amount)
    {
        player.Water += amount;
        Debug.Log($"Agua actualizada: {player.Water}");
    }

    public void ChangeInhabitants(PlayerStats player, int amount)
    {
        player.Inhabitants = Mathf.Max(player.Inhabitants + amount, 0);
        Debug.Log($"Habitantes: {player.Inhabitants}");
    }

    public void ChangeCommunitySupport(PlayerStats player, float amount)
    {
        player.CommunitySupport = Mathf.Clamp(player.CommunitySupport + amount, 0f, 100f);
        Debug.Log($"Apoyo comunitario: {player.CommunitySupport}");
    }

    public void ChangeStability(PlayerStats player, float amount)
    {
        player.Stability = Mathf.Clamp(player.Stability + amount, 0f, 100f);
        Debug.Log($"Estabilidad: {player.Stability}");
    }
}
