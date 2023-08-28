using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;

    public Player player;

    public Level currentLevel;

    public List<Enemy> currentEnemies = new List<Enemy>();
    public int totalEnemy;
    public int currentAlive;

    void Start()
    {
        //OnLoadLevel(0);
        currentLevel = levels[0]; //temporary
    }

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (currentEnemies.Count < currentLevel.GetMaxCurrentEnemy() && totalEnemy < currentLevel.GetMaxEnemy() - 1)
            {
                SpawnEnemy();
            }
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i].currentState != null)
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
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {
        GameManager.ChangeState(GameState.Revive);
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UILose>();
        UIManager.Ins.GetUI<UILose>().UpdateTextRanking(currentAlive);
        UIManager.Ins.GetUI<UILose>().UpdateTextKilled(player.killedBy);
    }

    public void UpdateAliveText()
    {
        UIManager.Ins.GetUI<UIGamePlay>().UpdateAliveText(currentAlive);
    }

    private void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-45f, 45f), 0f, Random.Range(-45f, 45f));

        Enemy enemy = (Enemy)SimplePool.Spawn(PoolType.Character, pos, Quaternion.identity);
        enemy.OnInit();
        totalEnemy++;
        currentEnemies.Add(enemy);
    }
}
