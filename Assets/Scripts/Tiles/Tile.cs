using UnityEngine;

public enum Controller
{
    None,
    Player1,
    Player2
}

public class Tile : MonoBehaviour
{
    public Controller controller;

    public  int cost;
    public Color color;
}
