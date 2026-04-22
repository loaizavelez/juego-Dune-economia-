using UnityEngine;

public enum Controller
{
    None,
    Player,
    AI
}

public class Casillas : MonoBehaviour
{
    public int x;
    public int y;
    public string TerritoryType;

    public Controller controller = Controller.None;

    void Start()
    {
        // Inicializa el color según el estado del Inspector
        SetController(controller);
    }

    public void SetController(Controller newController)
    {
        controller = newController;

        var renderer = GetComponent<SpriteRenderer>();
        Color newColor = Color.white;

        switch (controller)
        {
            case Controller.Player:
                newColor = Color.blue;
                break;
            case Controller.AI:
                newColor = Color.red;
                break;
            default:
                newColor = Color.white;
                break;
        }

        renderer.color = newColor;

        // 🔹 Debug para saber qué casilla cambió de color
        Debug.Log($"Casilla ({x}, {y}) ahora pertenece a {controller} y cambió a color {newColor}");
    }

    void OnMouseDown()
    {
        SetController(Controller.Player);
    }
}
