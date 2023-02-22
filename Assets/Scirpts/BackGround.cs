using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;

    public Transform[] sprites; // 맵의 스프라이트 위치

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2; // 메인 카메라의 orthographicSize높이를 (이프로젝트는 5임) 즉 viewHeight는 10임

    }
    void Update()
    {
        //Vector3 curPos = transform.position;  
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime; //벡터변수 nextPos y축으로(-1 * speed) 1초에 1번 만큼이동 

        transform.position += nextPos; //curPos 사용안하고 그냥 +=로 넣음 이스크립트를 탑재한 오브젝트의 포지션을 이동시킴

        if (sprites[endIndex].position.y < viewHeight*(-1))  //만약 스프라이트 endIndex번째 -10이상 내려가면 동작
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition; //가장위에있는 녀석의 이미지의 로컬 위치를 벡터변수 backSpritePos 로 받음
            Vector3 frontSpritePos = sprites[endIndex].localPosition; // 사용X
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight; //가장밑에있는 이미지 녀석의 위치를 가장위에있는 이미지의 위치의 10(viewHeight)정도위로 다시재배치

            Debug.Log("현재 사라질 인덱스번호" + endIndex); //체크

            //인덱스 재배치 부분  이해하기 어려워서 내가짠것

            startIndex = endIndex; //해주는 이유 endindex가 위에코드에서 재배치하여 가장위로 올라갔기때문에 가장위에있는 인덱스 값을 넣어줘야함 012 에서 0이 위로가면 120이되는것처럼
            endIndex +=1;  //인덱스값증가 

            if (endIndex == sprites.Length) // 만약 인덱스가 (sprites.Length)의 크기 3 이되면 인덱스 3은 없기에 0으로 바꿔줌
            {
                endIndex = 0;
            }

            //동영상 강의에서 알려준것 이해하기 어려움;;
            /* 
            int startIndexSave = startIndex;  
            startIndex = endIndex; //
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            
            */

        }
    }
}
