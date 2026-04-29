using System;
using UnityEditor.UI;
using UnityEngine;



public class Tile : MonoBehaviour
{
    TileSetManager TSM;
    public Controller controller = Controller.None;

    public  int cost;
    public int waterPayout;

    [SerializeField] Color baseColor;
    Color color;
    SpriteRenderer spriteRenderer;
    public void DrawTile()
    {
        switch (controller)
        {
            case Controller.Player1:
                color = Color.Lerp(baseColor,TSM.player1Color,0.5f);
            break;   
            case Controller.Player2:
                color = Color.Lerp(baseColor,TSM.player2Color,0.5f);
            break;
            case Controller.None:
                color = baseColor;
            break;
        }
        spriteRenderer.color = color;
    }

    void Start()
    {
        spriteRenderer  = gameObject.GetComponent<SpriteRenderer>();
        TSM = FindFirstObjectByType<TileSetManager>();
        DrawTile();
    }
}
