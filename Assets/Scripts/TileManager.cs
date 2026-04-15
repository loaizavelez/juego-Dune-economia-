using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private Tile[] tiles;

    public void CaptureTiles()
    {
        int tilesToCapture = Mathf.FloorToInt(stats.CommunitySupport / 20f);

        for (int i = 0; i < tilesToCapture && i < tiles.Length; i++)
        {
            tiles[i].Capture();
        }
    }
}