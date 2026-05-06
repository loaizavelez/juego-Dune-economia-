using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Casillas : MonoBehaviour
{
    public int x;
    public int y;
    public ControllerManager.Controller controller = ControllerManager.Controller.None;

    private SpriteRenderer spriteRenderer;
    private GameStats stats;

    public string TerritoryType;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = FindObjectOfType<GameStats>();

        // ✅ color inicial según tipo de terreno
        Color terrenoColor = Color.gray;
        switch (TerritoryType)
        {
            case "Desert":
                terrenoColor = new Color(1f, 0.9f, 0.6f); // arena
                break;
            case "Oasis":
                terrenoColor = Color.green; // vegetación
                break;
            case "Plain":
                terrenoColor = Color.yellow; // pradera
                break;
            default:
                terrenoColor = Color.gray; // fallback
                break;
        }

        spriteRenderer.color = terrenoColor;
    }



    void OnMouseDown()
    {

        ControllerManager.Controller jugadorActual = stats.currentTurn;

        // Validar que queden movimientos
        if (stats.movesRemaining <= 0)
        {
            Debug.Log("No quedan movimientos en este turno.");
            return;
        }

        // Caso 0: casilla enemiga → abrir popup de batalla
        if (controller != ControllerManager.Controller.None && controller != jugadorActual)
        {
            FindObjectOfType<BattleSystem>().ShowBattlePopup(this);
            return; // ✅ salimos para no ejecutar la captura normal
        }

        // Caso 1: casilla vacía y vecina
        if (controller == ControllerManager.Controller.None && EsVecinaDeJugador(jugadorActual))
        {
            if (TryCapture(jugadorActual))
            {
                stats.ConsumeMove(); // ✅ consumir movimiento
            }
        }
        // Caso 2: primera casilla del jugador (no necesita vecinos)
        else if (controller == ControllerManager.Controller.None && !HayCasillasDelJugador(jugadorActual))
        {
            if (TryCapture(jugadorActual))
            {
                stats.ConsumeMove(); // ✅ consumir movimiento
            }
        }
        else
        {
            Debug.Log($"Jugador {jugadorActual} intentó capturar ({x},{y}) pero no es vecina de ninguna casilla controlada.");
        }
    }
    private bool TryCapture(ControllerManager.Controller jugador)
    {
        controller = jugador;
        ActualizarColor();

        GameStats.PlayerStats statsJugador =
            jugador == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;

        switch (TerritoryType)
        {
            case "Oasis":
                statsJugador.Water += 10;
                statsJugador.Stability += 10;
                statsJugador.Inhabitants += 5;
                break;

            case "Desert":
                statsJugador.Water -= 5;
                statsJugador.Stability -= 5;
                statsJugador.Spice += 2;
                statsJugador.Inhabitants -= 6;
                break;

            case "Plain":
                statsJugador.Water -= 5;
                statsJugador.Stability -= 5;
                statsJugador.Spice += 1;
                statsJugador.Inhabitants -= 7;
                break;
        }

        Debug.Log($"Casilla ({x},{y}) capturada por {jugador} ({TerritoryType})");
        return true;
    }

    private bool EsVecinaDeJugador(ControllerManager.Controller jugador)
    {
        GridManager grid = FindObjectOfType<GridManager>();

        // ✅ lista de direcciones como vectores
        Vector2Int[] direcciones = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

        foreach (var dir in direcciones)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (nx >= 0 && nx < grid.width && ny >= 0 && ny < grid.height)
            {
                Casillas vecina = grid.GetCasilla(nx, ny);
                if (vecina.controller == jugador)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool HayCasillasDelJugador(ControllerManager.Controller jugador)
    {
        GridManager grid = FindObjectOfType<GridManager>();

        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                if (grid.GetCasilla(i, j).controller == jugador)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ActualizarColor()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // color base del terreno
        Color terrenoColor = Color.gray;
        switch (TerritoryType)
        {
            case "Desert": terrenoColor = new Color(1f, 0.9f, 0.6f); break;
            case "Oasis": terrenoColor = Color.green; break;
            case "Plain": terrenoColor = Color.yellow; break;
        }

        // color del jugador
        Color jugadorColor = Color.white;
        switch (controller)
        {
            case ControllerManager.Controller.Player1: jugadorColor = Color.blue; break;
            case ControllerManager.Controller.Player2: jugadorColor = Color.red; break;
            case ControllerManager.Controller.None: jugadorColor = Color.white; break;
        }

        // mezcla terreno + jugador
        Color finalColor = terrenoColor;
        if (controller != ControllerManager.Controller.None)
        {
            finalColor = Color.Lerp(terrenoColor, jugadorColor, 0.5f);
        }

        spriteRenderer.color = finalColor;
    }
}
