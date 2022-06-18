using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitedTimer : MonoBehaviour
{
    // 시간을 표시하는 timerText.
    public TMP_Text timerText;

    // 전체 제한 시간을 설정. 여기서는 1800초 = 30분.
    float setTime = 1800;

    // 분단위와 초단위를 담당할 변수.
    int min;
    float sec;

    void Update()
    {
        // 남은 시간을 감소시킴.
        setTime -= Time.deltaTime;
        
        // 전체 시간이 60초 보다 클 때
        if (setTime >= 60f)
        {
            // 60으로 나눠서 생기는 몫을 분단위로 변경
            min = (int)setTime / 60;
            // 60으로 나눠서 생기는 나머지를 초단위로 설정
            sec = setTime % 60;
            // UI를 표현
            timerText.text = "남은 시간 : " + min + "분" + (int)sec + "초";
        }
        
        // 전체시간이 60초 미만일 때
        if (setTime < 60f)
        {
            // 분 단위는 필요없어지므로 초단위만 남도록 설정
            timerText.text = "남은 시간 : " + (int)setTime + "초";
        }
        
        // 남은 시간이 0보다 작아질 때
        if (setTime <= 0)
        {
            // 텍스트를 0초로 고정시킴.
            timerText.text = "남은 시간 : 0초";
        }
    }
}
