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

    GameObject[] targetpool; //������ ������Ʈ �迭

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
            enemyL[index]=Instantiate(enemyLPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            enemyS[index].SetActive(false);
        }
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            itemBoom[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab); // ������Ʈ �Ŵ����� ���� ������ �̸������� �迭�� �Ҵ��ϴ��۾�
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
            if (!targetpool[index].activeSelf)// ��Ȱ��ȭ�� �ֳʹ� ã�� �Ѿ� ������
            {
                targetpool[index].SetActive(true); // Ȱ��ȭ
                return targetpool[index]; //  ������Ʈ�迭 ������  ��Ȱ��ȭ �� ������Ʈ ã�Ƽ� Ȱ��ȭ ��Ű�� ����
            }
        }
        return null; //����ó��
    }
}
