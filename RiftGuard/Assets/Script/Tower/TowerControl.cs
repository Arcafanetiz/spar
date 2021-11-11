using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject towerSprite;

    [Header("Tower Properties")]

        public float Base_ATK;
        public float Base_Range;
        public float Base_Cooldown;

    [HideInInspector] public float ATK;
    [HideInInspector] public float Range;
    [HideInInspector] public float Cooldown;

    public struct TowerData
    {
        public int Type;
        public int Level;
    }

    [HideInInspector] public TowerData info;
    private float shootTimeCounter = 0.0f;
    private bool foundEnemy = false;
    [HideInInspector] public bool piercing = false;

    [HideInInspector] public GameObject compTile;
    private LinkedList<GameObject> enemyList;

    private float ScaleX;
    private float ScaleY;

    private void Awake()
    {
        ATK = Base_ATK;
        Range = Base_Range;
        Cooldown = Base_Cooldown;
        GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        GetComponent<LineRenderer>().startColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        GetComponent<LineRenderer>().endColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        enemyList = new LinkedList<GameObject>();
    }

    private void Start()
    {
        CheckBuff();
    }

    private void Update()
    {
        if (GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.TUTORIAL_PAUSE)
        {
            // If tower was clicked -> Show RangeArea(LineRenderer) that can shoot
            if(GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE &&
                GameManage.clickPos == transform.position)
            {
                GetComponent<LineRenderer>().enabled = true;
                DrawCircle(Range, 0.05f);
            }
            else
            {
                GetComponent<LineRenderer>().enabled = false;
            }
            CheckEnemy();
            CoolDown();
        }
    }

    // Cool down for Shooting bullet
    private void CoolDown()
    {
        if (shootTimeCounter >= Cooldown)
        {
            if (foundEnemy)
            {
                Shoot();
                shootTimeCounter = 0;
            }
        }
        else
        {
            shootTimeCounter += Time.deltaTime;
        }
    }

    // Shoot Single bullet to Enemy
    private void Shoot()
    {
        // Go to Direction of Enemy
        CheckEnemy();
        if (enemyList.First.Value.gameObject != null)
        {
            Vector3 dir = towerSprite.transform.position - enemyList.First.Value.gameObject.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            towerSprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {
            return;
        }


        GameObject bulletGo = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletScript bullet = bulletGo.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetDamage(ATK);
            bullet.piercing = piercing;
            bullet.Seek(enemyList.First.Value);
        }
    }

    private void CheckEnemy()
    {
        CheckEnemyDis();
        // No Enemies / Enemies in range are killed then !! Check OverlapCircle !!
        if(enemyList.Count == 0)
        {
            Collider2D[] enemyRef = Physics2D.OverlapCircleAll(transform.position, Range);
            foreach (var enemy in enemyRef)
            {
                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    enemyList.AddLast(enemy.gameObject);
                }
            }
        }
        // If there are enemy in list then foundEnemy = true -> prepare to shoot
        foundEnemy = (enemyList.Count > 0) ? true : false;
    }


    // Check Enemy 
    private void CheckEnemyDis()
    {
        if (enemyList.Count == 0)
        {
            return;
        }
        // If Enemy die / Enemy has gone to base then Remove from list
        else if (enemyList.First.Value == null)
        {
            enemyList.RemoveFirst();
        }
        // If Enemy is out of range then Remove from list
        else if (Vector2.Distance(enemyList.First.Value.transform.position, transform.position) > Range)
        {
            enemyList.RemoveFirst();
        }
    }

    // Check Buff for speacial abilities Tiles
    private void CheckBuff()
    {
        if(compTile.GetComponent<TileProperties>().GetTileType() == TileProperties.TileType.ATK)
        {
            ATK *= 1.5f;
        }
    }

    // SET Tower Type
    public void SetTowerType(int _Type, int _Level)
    {
        info.Type = _Type;
        info.Level = _Level;
    }

    public void SetDefault()
    {
        ATK = Base_ATK;
        Range = Base_Range;
        Cooldown = Base_Cooldown;
    }

    // Draw Circle (Range that can shoot)
    public void DrawCircle(float radius, float lineWidth)
    {
        var segment = 360;
        var line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segment + 1;

        var pointCount = segment + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360.0f / segment);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, -1.0f);
        }
        line.SetPositions(points);
    }
}