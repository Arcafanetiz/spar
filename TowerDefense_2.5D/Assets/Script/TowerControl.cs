using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject towerSprite;

    [Header("Tower Properties")]

        [SerializeField] private float ATK;
        [SerializeField] private float range;
        [SerializeField] private GameObject rangeArea;
        [SerializeField] private float Cooldown;

    private struct TowerData
    {
        public int Type;
        public int Level;
    }

    private TowerData info;
    private float shootTimeCounter = 0.0f;
    private bool foundEnemy = false;

    private GameObject compTile;
    private LinkedList<GameObject> enemyList;

    private void Start()
    {
        enemyList = new LinkedList<GameObject>();
        rangeArea.transform.localScale = new Vector2(rangeArea.transform.localScale.x * range * 2, 
                                                    rangeArea.transform.localScale.y * range * 2);
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
            Collider2D[] enemyRef = Physics2D.OverlapCircleAll(transform.position, range);
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
        else if (Vector2.Distance(enemyList.First.Value.transform.position, transform.position) > range)
        {
            enemyList.RemoveFirst();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Check Buff for speacial abilities Tiles
    private void CheckBuff()
    {
        if(compTile.GetComponent<TileProperties>().GetTileType() == TileProperties.TileType.ATK)
        {
            ATK *= 1.5f;
        }
    }

    // Other Function used to SET/GET value
    public void SetCompTile(GameObject _compTile)
    {
        compTile = _compTile;
    }

    public void SetTowerType(int _Type, int _Level)
    {
        info.Type = _Type;
        info.Level = _Level;
    }

    public int GetTowerType()
    {
        return info.Type;
    }

    public int GetTowerLevel()
    {
        return info.Level;
    }

    public float GetATK()
    {
        return ATK;
    }

    public float GetSpeed()
    {
        return 1/Cooldown;
    }

    public float GetRange()
    {
        return range;
    }
}