using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*public class AIController : MonoBehaviour
{
    public GridManager grid;
    public float interval = 3f; // cada 3 segundos
    private float timer = 0f;

    

    private void Start()
    {
        // Esperar un frame para que el GridManager termine de generar las casillas
        StartCoroutine(InitAI());
    }

    private IEnumerator InitAI()
    {
        yield return null; // espera un frame

        Casillas start = grid.GetCasilla(0, 0);
        if (start != null)
        {
            start.SetController(Controller.AI);
            Debug.Log($"IA inicia en la casilla ({start.x}, {start.y})");
        }
        else
        {
            Debug.LogError("⚠️ No se encontró la casilla inicial (0,0). Revisa que el GridManager haya generado el tablero.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            ExpandTerritory();
        }
    }

    void ExpandTerritory()
    {
        List<Casillas> aiCasillas = new List<Casillas>();

        // Buscar todas las casillas controladas por la IA
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Casillas c = grid.GetCasilla(x, y);
                if (c != null && c.controller == Controller.AI)
                {
                    aiCasillas.Add(c);
                }
            }
        }

        if (aiCasillas.Count == 0) return;

        // Elegir una casilla de la IA al azar
        Casillas baseCasilla = aiCasillas[Random.Range(0, aiCasillas.Count)];

        // Intentar expandirse a una casilla vecina
        List<Vector2Int> vecinos = new List<Vector2Int>()
        {
            new Vector2Int(baseCasilla.x+1, baseCasilla.y),
            new Vector2Int(baseCasilla.x-1, baseCasilla.y),
            new Vector2Int(baseCasilla.x, baseCasilla.y+1),
            new Vector2Int(baseCasilla.x, baseCasilla.y-1)
        };

        foreach (var v in vecinos)
        {
            if (v.x >= 0 && v.x < grid.width && v.y >= 0 && v.y < grid.height)
            {
                Casillas vecino = grid.GetCasilla(v.x, v.y);
                if (vecino != null && vecino.controller == Controller.None)
                {
                    // 🔹 Usar TryCapture en lugar de SetController
                    GameStats stats = grid.stats; // referencia al objeto de stats
                    bool captured = vecino.TryCapture(Controller.AI, grid, stats);

                    if (captured)
                    {
                       // Debug.Log($"IA capturó la casilla ({vecino.x}, {vecino.y})");
                    }
                    else
                    {
                        //Debug.Log($"IA intentó capturar ({vecino.x}, {vecino.y}) pero no tenía agua suficiente");
                    }

                    break;
                }
            }
        }

    }
}
*/