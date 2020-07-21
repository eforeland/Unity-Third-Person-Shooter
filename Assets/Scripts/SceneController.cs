using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject bossPrefab;
    private GameObject boss;
    private GameObject enemy;
    private List<GameObject> enemies;
    private const int DEFAULT_DIFFICULTY = 5;
    private int enemyCount = DEFAULT_DIFFICULTY;
    private bool allJewelsCollected = false;
    private int numJewelsCollected = 0;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.JEWEL_COLLECTED, OnJewelCollected);
        Messenger<GameObject>.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.RemoveListener(GameEvent.JEWEL_COLLECTED, OnJewelCollected);
        Messenger<GameObject>.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    }

    // Start is called before the first frame update
    void Start()
    {
        //set difficulty from memory at start
        enemyCount = PlayerPrefs.GetInt("difficulty", 1);

        Debug.Log("difficulty initialized to: " + enemyCount);

        // instantiate enemies as a list with 
        //starting size of enemyCount (current Difficulty).
        enemies = new List<GameObject>();
        
        for (int i = 0; i < enemyCount; i += 1)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(20, 0, 1);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count < enemyCount && !allJewelsCollected)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(15, 0, 0);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemies.Add(enemy);
            Debug.Log("Size of list: " + enemies.Count);
            Debug.Log("Size of enemyCount in SceneController: " + enemyCount);
        }

        //if (true)
        //{
        //    boss = Instantiate(bossPrefab) as GameObject;
        //    boss.transform.position = new Vector3(-60, 2, 5);
        //}

    }

    //update enemy count based on difficulty slider.
    public void OnDifficultyChanged(int difficulty)
    {
        enemyCount = difficulty;
       
        Debug.Log("New enemy count: " + enemyCount);
    } 

    private void OnEnemyDead(GameObject e)
    {
        enemies.Remove(e);
        Messenger.Broadcast(GameEvent.POINT);
    }

    private void OnJewelCollected()
    {
        numJewelsCollected += 1;
        if(numJewelsCollected == 3)
        {
            allJewelsCollected = true;
        }
    }

}