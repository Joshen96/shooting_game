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

    public GameObject boomEfft;

    // 적 스폰딜레이 위함 계속생성방지
    public float curEnemySpawnDelay;
    public float nextEnemySpawnDelay;

    public int kill_Enemy = 0;

   
    public int boss = 10;
    
    public GameObject player;

    public bool playerdie = false;

    public Text scoreText;
    public Text BossText;
    public Text OTotalScoreText;
    public Text CTotalScoreText;

    public Image[] lifeImage;
    public Image[] BoomImage;

    public GameObject gameOverset;
    public GameObject gameClearset;

    bool bosslife = false;
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
        if (boss < 0)
        {
            boss = 0;
        }
        Boss_start();

        if (!isReplay)
           curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay > nextEnemySpawnDelay) //스폰시간되면 수행
        {
            SpawnEnemy();


            nextEnemySpawnDelay = Random.Range(1f, 2.0f); //계속 랜덤으로 딜레이수정해줌
            curEnemySpawnDelay = 0f;
                        
        }

        Player playLogic = player.GetComponent<Player>();
        scoreText.text=string.Format("{0:n0}",playLogic.score); // 000,000으로 fotmat사용
        OTotalScoreText.text = string.Format("{0:n0}", playLogic.score);
        CTotalScoreText.text = string.Format("{0:n0}", playLogic.score);
        BossText.text = string.Format("{0}",boss);
    }

    public void Bossdie_Gameclear()
    {
        delebullet();

        gameClearset.SetActive(true); ;//게임오버 UI 활성화

    }
   
        

    void Boss_start()
    {
        if (boss > 0)
            return;

        isReplay = true;
        Debug.Log("보스등장");
        EnemyallDie();

        if (bosslife == false)
        {
            bosslife = true;
            Invoke(nameof(SpawnBoss), 1.0f);
        }
    
    }
    void SpawnBoss()
    {
        
        GameObject goBoss = Instantiate(EnemyPrefabs[3], spawnPoints[7].position, Quaternion.identity);
        Enemy BossLogic = goBoss.GetComponent<Enemy>();
        BossLogic.playerObj = player;
      



       

    }
    
    void Scale()
    {

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
    public void Playerdie_delebullet()// 플레이어죽으면 애너미 총알 없앰
    {
        playerdie = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach(GameObject temp in temps)
        {
            Destroy(temp);
        }
        
    }

     void delebullet()// 총알 없앰
    {
        
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject temp in temps)
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
    public void UpdateBoomIcon(int Boom)
    {
        for (int index = 0; index < BoomImage.Length; index++)
        {
            BoomImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < Boom; index++)
        {
            BoomImage[index].color = new Color(1, 1, 1, 1);
        }

    }
    public void EnemyallDie()
    {
        
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            if (temp.GetComponent<Enemy>().enemyName == "Boss")
            {
                return;
            }
            Destroy(temp);
           
        }

    }
    public void ItemallDie(string tag)
    {

        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject temp in temps)
        {

            Destroy(temp);
        }

    }


    public void Enemyallkill()
    {

        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            temp.GetComponent<Enemy>().OnHit(15);
            // Destroy(temp);
        }

    }
    public void Booming()
    {
        boomEfft.gameObject.SetActive(true);
        Invoke(nameof(Boomed), 0.5f);

    }
    public void Boomed()
    {
        boomEfft.gameObject.SetActive(false);

    }
    void bossdestory()
    {
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject temp in temps)
        {
            
            Destroy(temp);

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

        Time.timeScale = 1f;
        isReplay = true;
        bossdestory();
        EnemyallDie();
        delebullet();
        ItemallDie("Coin");
        ItemallDie("Powerup");
        ItemallDie("Boom");

        RespawnPlayer();

        bosslife = false;
        Player playLogic = player.GetComponent<Player>();
        playLogic.life = 3;
        playLogic.BoomCount = 0;
        playLogic.score = 0;
        boss = 10;
        UpdateLifeIcon(playLogic.life);
        UpdateBoomIcon(playLogic.BoomCount);

       
        gameOverset.SetActive(false);
        gameClearset.SetActive(false);
        Invoke(nameof(DelayEnemySpawn), 2f);

    }
    
    void DelayEnemySpawn()
    {
        isReplay = false;
    }
}
