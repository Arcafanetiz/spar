using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    // Used as reference
    private GameObject mapGenerator;

    // Sprite & Color in Tile
    private SpriteRenderer currentRenderer;
    private Color currentColor;

    private Vector2 nowPos;
    
    public enum TileType
    {
        Grass,
        ATK
    }

    [SerializeField] private TileType tileType;

    private void Start()
    {
        nowPos = GetComponent<Transform>().position;
        currentRenderer = GetComponent<SpriteRenderer>();
        currentColor = currentRenderer.color;
    }

    // Mouse Event will work when Object has Collision
    private void OnMouseOver()
    {
        // Change Color when Mouse Hover
        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            currentRenderer.color = new Color(currentColor.r + 0.1f, currentColor.g + 0.1f, currentColor.b + 0.1f);
        }
    }

    private void OnMouseExit()
    {
        currentRenderer.color = currentColor;
    }

    private void OnMouseDown()
    {
        // Mouse Click -> Show createTowerUI if there is no tower on that grid
        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            // Track the position that have been clicked
            Vector3 nowPos = GetComponent<Transform>().position;
            GameManage.clickPos = nowPos;

            if(nowPos.x < 0 || nowPos.y > 0)
            {
                return;
            }

            // Check for Existance of Tower
            if (mapGenerator.GetComponent<MapGenerator>().CheckMap((int)nowPos.x, (int)-nowPos.y))
            {
                GameManage.currentGameStatus = GameManage.GameStatus.UPGRADE;
            }
            else
            {
                GameManage.currentGameStatus = GameManage.GameStatus.CREATE;
            }
        }
    }

    // Other Function used to SET/GET value
    public TileType GetTileType()
    {
        return tileType;
    }

    public void SetMapGenerator(GameObject _mapGenerator)
    {
        mapGenerator = _mapGenerator;
    }
}
