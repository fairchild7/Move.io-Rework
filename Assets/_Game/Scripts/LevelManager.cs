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

    void Start()
    {
        //OnLoadLevel(0);
        currentLevel = levels[0]; //temporary
        OnInit();
    }

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (currentEnemies.Count < currentLevel.GetMaxCurrentEnemy() && totalEnemy < currentLevel.GetMaxEnemy())
            {
                SpawnEnemy();
            }
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                currentEnemies[i].currentState.OnExecute(currentEnemies[i]);
            }
        }
    }

    public void OnInit()
    {
        totalEnemy = 1;
        player.OnInit();    
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    private void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-45f, 45f), 0f, Random.Range(-45f, 45f));

        Enemy enemy = (Enemy)SimplePool.Spawn(PoolType.Character, pos, Quaternion.identity);
        enemy.OnInit();
        enemy.SetId(totalEnemy);
        totalEnemy++;
        currentEnemies.Add(enemy);
    }
}
