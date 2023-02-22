using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;

    public Transform[] sprites; // ���� ��������Ʈ ��ġ

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2; // ���� ī�޶��� orthographicSize���̸� (��������Ʈ�� 5��) �� viewHeight�� 10��

    }
    void Update()
    {
        //Vector3 curPos = transform.position;  
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime; //���ͺ��� nextPos y������(-1 * speed) 1�ʿ� 1�� ��ŭ�̵� 

        transform.position += nextPos; //curPos �����ϰ� �׳� +=�� ���� �̽�ũ��Ʈ�� ž���� ������Ʈ�� �������� �̵���Ŵ

        if (sprites[endIndex].position.y < viewHeight*(-1))  //���� ��������Ʈ endIndex��° -10�̻� �������� ����
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition; //���������ִ� �༮�� �̹����� ���� ��ġ�� ���ͺ��� backSpritePos �� ����
            Vector3 frontSpritePos = sprites[endIndex].localPosition; // ���X
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight; //����ؿ��ִ� �̹��� �༮�� ��ġ�� ���������ִ� �̹����� ��ġ�� 10(viewHeight)�������� �ٽ����ġ

            Debug.Log("���� ����� �ε�����ȣ" + endIndex); //üũ

            //�ε��� ���ġ �κ�  �����ϱ� ������� ����§��

            startIndex = endIndex; //���ִ� ���� endindex�� �����ڵ忡�� ���ġ�Ͽ� �������� �ö󰬱⶧���� ���������ִ� �ε��� ���� �־������ 012 ���� 0�� ���ΰ��� 120�̵Ǵ°�ó��
            endIndex +=1;  //�ε��������� 

            if (endIndex == sprites.Length) // ���� �ε����� (sprites.Length)�� ũ�� 3 �̵Ǹ� �ε��� 3�� ���⿡ 0���� �ٲ���
            {
                endIndex = 0;
            }

            //������ ���ǿ��� �˷��ذ� �����ϱ� �����;;
            /* 
            int startIndexSave = startIndex;  
            startIndex = endIndex; //
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            
            */

        }
    }
}
