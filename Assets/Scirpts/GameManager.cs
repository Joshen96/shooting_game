using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //생성 위치 포지션와 생성할 적 오브젝트
    public Transform[] spawnPoints;
    public GameObject[] EnemyPrefabs;

   

    // 적 스폰딜레이 위함 계속생성방지
    public float curEnemySpawnDelay;
    public float nextEnemySpawnDelay;
    
    public GameObject player;

    public bool playerdie = false;

    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverset;

    bool isReplay = false;

    // Start is called before the first frame update
    public static GameManager gamemanager = null;
    private void Awake()
    {
        if (gamemanager == null)
        {
            gamemanager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReplay)
           curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay > nextEnemySpawnDelay) //스폰시간되면 수행
        {
            SpawnEnemy();


            nextEnemySpawnDelay = Random.Range(1f, 2.0f); //계속 랜덤으로 딜레이수정해줌
            curEnemySpawnDelay = 0f;
                        
        }

        Player playLogic = player.GetComponent<Player>();
        scoreText.text=string.Format("{0:n0}",playLogic.score); // 000,000으로 fotmat사용
    }  
    void SpawnEnemy()
    {
        int ranType = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 7);
        GameObject goEnemy = Instantiate(EnemyPrefabs[ranType], spawnPoints[ranPoint].position, Quaternion.identity);
        Enemy enemyLogic = goEnemy.GetComponent<Enemy>();
        enemyLogic.playerObj = player;
        enemyLogic.Move(ranPoint);
    }

    

    public void GameOver()
    {
        gameOverset.SetActive(true); ;//게임오버 UI 활성화
    }
    public void RespawnPlayer() //재소환
    {
        
        Invoke("AilvePlayer", 1.0f);
        
    }
    
    void AilvePlayer()  //부활하면서 플레이어 죽은시간
    {
        player.transform.position = Vector3.down * 4.2f; //게임오브젝트위치 -y 4.2위치에 플레이어 소환
        player.SetActive(true);
        Player playrLogic = player.GetComponent<Player>();
        //playrLogic.Respawning = true;
        playrLogic.power = 1;
        playrLogic.isHit = false;
        
        player.GetComponent<PolygonCollider2D>().enabled = false;
        Invoke("DamageEnd", 2f); //무적시간 2초 후 플레이어의 랜더러와 콜라이더 부착해줌
    }
    void DamageEnd()
    {
        playerdie = false;
        Player playrLogic = player.GetComponent<Player>();
        playrLogic.Respawning = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PolygonCollider2D>().enabled = true;
    }
    public void Playerdie_delebullet()// 이때 애너미 총알 없앰
    {
        playerdie = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach(GameObject temp in temps)
        {
            Destroy(temp);
        }
        
    }
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < lifeImage.Length; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }

    }
    public void Replay()
    {
        /*
        player.gameObject.SetActive(true);
        isReplay = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            Destroy(temp);
        }
        */
        isReplay = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            Destroy(temp);
        }
        RespawnPlayer();

        Player playLogic = player.GetComponent<Player>();
        playLogic.life = 3;
        UpdateLifeIcon(playLogic.life);
        playLogic.score = 0;
        gameOverset.SetActive(false);
        Invoke(nameof(DelayEnemySpawn), 1f);

    }
    
    void DelayEnemySpawn()
    {
        isReplay = false;
    }
}
