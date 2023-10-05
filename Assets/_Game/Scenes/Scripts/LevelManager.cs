using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Transform parent;

    public Player player;
    public Level currentLevel;
    public List<Enemy> currentEnemies = new List<Enemy>();
    public int totalEnemy;
    public int currentAlive;

    public bool winCondition => (currentAlive == 1 && !player.IsDead());

    void Start()
    {
        OnLoadLevel(DataManager.Ins.GetCurrentLevel());
    }

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i].currentState != null && !currentEnemies[i].IsDead())
                {
                    currentEnemies[i].currentState.OnExecute(currentEnemies[i]);
                }
                Indicator.Ins.CheckNavigation(currentEnemies[i]);
            }
        }
        else
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i].currentState != null)
                {
                    currentEnemies[i].StopMoving();
                    currentEnemies[i].PlayerUI.gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnInit()
    {
        totalEnemy = 0;
        player.OnInit();
        currentAlive = currentLevel.GetMaxEnemy();
        UpdateAliveText();
        FirstSpawn();
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level], parent);
        currentLevel.gameObject.SetActive(true);
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.GetNavMeshData());
    }

    public void OnWin()
    {
        player.WeaponPos.gameObject.SetActive(true);
        player.PlayerUI.gameObject.SetActive(false);
        player.ChangeAnim(Constants.ANIM_DANCE_WIN);
        GameManager.ChangeState(GameState.Finish);
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIWin>();
        UIManager.Ins.GetUI<UIWin>().UpdateCongratText(DataManager.Ins.GetCurrentLevel() + 1);
        if (DataManager.Ins.GetCurrentLevel() < levels.Length - 1)
        {
            DataManager.Ins.SaveCurrentLevel(DataManager.Ins.GetCurrentLevel() + 1);
        }
        UIManager.Ins.GetUI<UIWin>().UpdatePointText(player.GetLevel());
        UIManager.Ins.GetUI<UIWin>().UpdateCoinText(CoinRewards(player.GetLevel()));
        AddCoin(CoinRewards(player.GetLevel()));
    }

    public void OnLose()
    {
        GameManager.ChangeState(GameState.Revive);
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UILose>();
        UIManager.Ins.GetUI<UILose>().UpdateTextRanking(currentAlive + 1);
        UIManager.Ins.GetUI<UILose>().UpdateTextKilled(player.killedBy);
        AddCoin(CoinRewards(player.GetLevel()));
    }

    public void OnReset()
    {
        Indicator.Ins.ClearIndicator();
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            currentEnemies[i].OnDespawn();
        }
        currentEnemies.Clear();
        SimplePool.CollectAll();
        player.ChangeAnim(Constants.ANIM_IDLE);
        player.PlayerUI.gameObject.SetActive(false);
        player.WeaponPos.gameObject.SetActive(true);
    }

    public void Revive()
    {
        currentAlive++;
        UpdateAliveText();
        player.OnInit();
        Vector3 pos = RandomNavmeshLocation(100f);
        player.tf.position = pos;
    }

    public void UpdateAliveText()
    {
        UIManager.Ins.GetUI<UIGamePlay>().UpdateAliveText(currentAlive);
    }

    public void FirstSpawn()
    {
        for (int i = 0; i < currentLevel.GetMaxCurrentEnemy(); i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (currentEnemies.Count < currentLevel.GetMaxCurrentEnemy() && totalEnemy < currentLevel.GetMaxEnemy() - 1)
        {
            totalEnemy++;
            //Vector3 pos = new Vector3(Random.Range(-45f, 45f), 0f, Random.Range(-45f, 45f));
            Vector3 pos = RandomNavmeshLocation(100f);

            Enemy enemy = (Enemy)SimplePool.Spawn(PoolType.Character, pos, Quaternion.identity);
            enemy.OnInit();
            currentEnemies.Add(enemy);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position + Vector3.up;

        }
        return finalPosition;
    }

    public int CoinRewards(int point)
    {
        return 10 * point;
    }

    public void AddCoin(int coin)
    {
        int currentCoin = DataManager.Ins.GetCurrentCoin();
        int updatedCoin = currentCoin + coin;
        DataManager.Ins.SaveCurrentCoin(updatedCoin);
    }
}
