using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject towerSprite;

    [Header("Tower Properties")]

        [SerializeField] private float _ATK;
        [SerializeField] private float _Range;
        [SerializeField] private GameObject rangeArea;
        [SerializeField] private float _Cooldown;

    public float ATK;
    public float Range;
    public float Cooldown;

    public struct TowerData
    {
        public int Type;
        public int Level;
    }

    [HideInInspector] public TowerData info;
    private float shootTimeCounter = 0.0f;
    private bool foundEnemy = false;

    [HideInInspector] public GameObject compTile;
    private LinkedList<GameObject> enemyList;


    private void Start()
    {
        ATK = _ATK;
        Range = _Range;
        Cooldown = _Cooldown;
        enemyList = new LinkedList<GameObject>();
        rangeArea.transform.localScale = new Vector2(rangeArea.transform.localScale.x * _Range * 2, 
                                                    rangeArea.transform.localScale.y * _Range * 2);
        rangeArea.SetActive(false);

        CheckBuff();
    }

    private void Update()
    {
        if (GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            // If tower was clicked -> Show RangeArea that can shoot
            if(GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE &&
                GameManage.clickPos == transform.position)
            {
                rangeArea.SetActive(true);
            }
            else
            {
                rangeArea.SetActive(false);
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

    // Shoot bullet to Enemy
    private void Shoot()
    {
        Vector3 dir = transform.position - enemyList.First.Value.gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        GameObject bulletGo = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletScript bullet = bulletGo.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetDamage(ATK);
            bullet.Seek(enemyList.First.Value);
        }
    }

    private void CheckEnemy()
    {
        CheckEnemyDis();
        // No Enemies / Enemies in range are killed then !! Check OverlapCircle !!
        if(enemyList.Count == 0)
        {
            Collider2D[] enemyRef = Physics2D.OverlapCircleAll(transform.position, _Range);
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
        else if (Vector2.Distance(enemyList.First.Value.transform.position, transform.position) > _Range)
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
        ATK = _ATK;
        Range = _Range;
        Cooldown = _Cooldown;
    }
}