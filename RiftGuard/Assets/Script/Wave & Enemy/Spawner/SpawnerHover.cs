using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHover : MonoBehaviour
{
    private SpriteRenderer currentRenderer;
    private Color currentColor;

    private void Start()
    {
        currentRenderer = GetComponent<SpriteRenderer>();
        currentColor = currentRenderer.color;
    }

    private void OnMouseOver()
    {
        // Change Color when Mouse Hover
        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            currentRenderer.color = new Color(currentColor.r - 0.1f, currentColor.g - 0.1f, currentColor.b - 0.1f);
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
            GameManage.currentGameStatus = GameManage.GameStatus.SPAWNER;
            this.GetComponent<SpawnerCardScript>().SetActivate();
        }
    }
}
