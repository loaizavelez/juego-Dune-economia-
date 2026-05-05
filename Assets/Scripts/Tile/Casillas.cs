using UnityEngine;
using System.Collections.Generic;



public enum TerritoryType
{
    Desert,
    Plains,
    Oasis,
    Spice
}

public class Casillas : MonoBehaviour
{
    public int x;
    public int y;

    public TerritoryType territoryType;

    public Controller controller;

    // 🔹 Captura con clic directo en la casilla
    void OnMouseDown()
    {
        GridManager grid = FindFirstObjectByType<GridManager>();
        GameStats stats = grid.stats;

        // Permitir captura inicial si está vacía y es la primera jugada
        if (controller == Controller.None && EsVecinaDeJugador(grid))
        {
            TryCapture(Controller.Player1, grid, stats);
        }
        else
        {
            Debug.Log($"Jugador intentó capturar ({x},{y}) pero no es vecina de ninguna casilla controlada.");
        }
    }

    public void SetController(Controller newController)
    {
        controller = newController;

        var renderer = GetComponent<SpriteRenderer>();

        // 🔹 Color base según tipo de terreno
        Color terrainColor = Color.gray;
        switch (territoryType)
        {
            case TerritoryType.Desert:
                terrainColor = new Color(1f, 0.9f, 0.6f); // arena
                break;
            case TerritoryType.Oasis:
                terrainColor = Color.green; // vegetación
                break;
            case TerritoryType.Plains:
                terrainColor = Color.yellow; // pradera
                break;
            case TerritoryType.Spice:
                terrainColor = Color.purple;
                break;
            default:
                terrainColor = Color.gray; // fallback
                break;
        }

        // 🔹 Color del controlador
        Color controllerColor = Color.white;
        switch (controller)
        {
            case Controller.Player1:
                controllerColor = Color.blue;
                break;
            case Controller.Player2:
                controllerColor = Color.red;
                break;
            case Controller.None:
                controllerColor = Color.white;
                break;
        }

        // 🔹 Mezclar terreno + controlador
        Color finalColor = terrainColor;
        if (controller != Controller.None)
        {
            finalColor = Color.Lerp(terrainColor, controllerColor, 0.5f);
        }

        renderer.color = finalColor;

        Debug.Log($"Casilla ({x}, {y}) tipo {territoryType} ahora pertenece a {controller} y cambió a color {finalColor}");
    }


    public bool TryCapture(Controller newController, GridManager grid, GameStats stats)
    {
        int cost = 1;

        if (newController == Controller.Player1)
        {
            if (stats.playerWater >= cost)
            {
                stats.playerWater -= cost;
                SetController(newController);
                Debug.Log($"Jugador capturó ({x},{y}) pagando {cost} agua. Agua restante: {stats.playerWater}");
                return true;
            }
            else
            {
                Debug.Log($"Jugador NO tiene agua suficiente. Costo: {cost}, Agua disponible: {stats.playerWater}");
                return false;
            }
        }
        else if (newController == Controller.Player2)
        {
            if (stats.aiWater >= cost)
            {
                stats.aiWater -= cost;
                SetController(newController);
                Debug.Log($"IA capturó ({x},{y}) pagando {cost} agua. Agua restante: {stats.aiWater}");
                return true;
            }
            else
            {
                Debug.Log($"IA NO tiene agua suficiente. Costo: {cost}, Agua disponible: {stats.aiWater}");
                return false;
            }
        }

        return false;
    }

    // 🔹 Verifica si esta casilla es vecina de alguna casilla del jugador
    bool EsVecinaDeJugador(GridManager grid)
    {
        List<Vector2Int> vecinos = new List<Vector2Int>()
        {
            new Vector2Int(x+1, y),
            new Vector2Int(x-1, y),
            new Vector2Int(x, y+1),
            new Vector2Int(x, y-1)
        };

        foreach (var v in vecinos)
        {
            if (v.x >= 0 && v.x < grid.width && v.y >= 0 && v.y < grid.height)
            {
                Casillas vecino = grid.GetCasilla(v.x, v.y);
                if (vecino != null && vecino.controller == Controller.Player1)
                {
                    return true;
                }
            }
        }

        return false;
    }
}