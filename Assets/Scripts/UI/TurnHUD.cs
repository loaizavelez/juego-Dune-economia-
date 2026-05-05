using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnHUD : MonoBehaviour
{
    [Header("Referencias")]
    public GameStats stats;                // referencia al objeto GameStats
    public TextMeshProUGUI turnoText;      // texto para mostrar quién juega
    public TextMeshProUGUI contadorText;   // texto para mostrar número de turno
    public TextMeshProUGUI aguaPlayer1Text;
    public TextMeshProUGUI aguaPlayer2Text;
    public Button saltarTurnoButton;       // botón para saltar turno

    private int contadorTurnos = 1;        // contador de turnos


    void Start()
    {
        saltarTurnoButton.onClick.AddListener(SaltarTurno);
        ActualizarHUD();
    }

    // Update is called once per frame
    void Update()
    {
        aguaPlayer1Text.text = $"Agua P1: {stats.player1.Water}";
        aguaPlayer2Text.text = $"Agua P2: {stats.player2.Water}";
    }


    void ActualizarHUD()
    {
        turnoText.text = $"Turno actual: {stats.currentTurn}";
        contadorText.text = $"Turno #: {contadorTurnos}";
    }

    public void SaltarTurno()
    {
        stats.ChangeTurn();
        contadorTurnos++;
        ActualizarHUD();
        Debug.Log($"Turno saltado. Ahora juega: {stats.currentTurn}, Turno #: {contadorTurnos}");
    }
}
