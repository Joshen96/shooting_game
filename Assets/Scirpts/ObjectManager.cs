using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;

    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;



    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;

    GameObject[] targetpool; //리턴할 오브젝트 배열

    private void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[10];


        itemCoin = new GameObject[10];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];


        Generate();


    }

    void Generate()
    {
        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index]=Instantiate(enemyLPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            enemyS[index].SetActive(false);
        }
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            itemBoom[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab); // 오브젝트 매니저로 받은 프리펩 미리생성한 배열애 할당하는작업
            bulletEnemyB[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string Type)
    {
        
        switch (Type)
        {
            case "EnemyL":
                
                break;
            case "EnemyM":
                break;
            case "EnemyS":
                break;
            case "itemCoin":
                break;
            case "itemPower":
                break;
            case "itemBoom":
                break;



        }

        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            if (!targetpool[index].activeSelf)// 비활성화인 애너미 찾기 총알 아이템
            {
                targetpool[index].SetActive(true); // 활성화
                return targetpool[index]; //  오브젝트배열 내에서  비활성화 된 오브젝트 찾아서 활성화 시키고 리턴
            }
        }
        return null; //예외처리
    }
}
