using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
최근 수정 일 : 2020.10.19
최근 수정 인 : (Hotbar)
최근 수정 작업 : 투명부분 버튼 인식 X

설명 : 투명부분 버튼 인식 X
Click.cs와 같은 위치에 넣어 주면 작동됩니다.

사용하기 전에 Sprite Advanced의 Read/Write Enable이 On 상태여야지만 작동이 됩니다.
buttons[i].GetComponent<Image>().alphaHitTestMinimumThreshold : 투명도에 따른 버튼 이미지 인식
*/

public class ButtonTransparent : MonoBehaviour
{
    private Button button;

    [Header("Alpha Hit Value")]
    public float value = 0.5f;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.GetComponent<Image>().alphaHitTestMinimumThreshold = value;
    }
}
