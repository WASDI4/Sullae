using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스크립트를 에디트 모드에서도 실행시킨다
//게임플레이를 누르지 않아도 발동됨.
[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class CustomOutline : MonoBehaviour
{   public Material mat;
    //어트리뷰트를 작성해서 컨트롤 쉽게 레인지 바를 맹글어줌
    [Range(0,1)] // 음영이 지는 부분을 컨트롤 할 수 있음
    public float ShadowThreshold;

  

    //모든 화면이 렌더링 되었을 때 화면 이미지에 mat을 적용
    void OnRenderImage(RenderTexture src, RenderTexture dest) 
    {
        mat.SetFloat("_ShadowThreshold", ShadowThreshold);
        Graphics.Blit(src, dest, mat);
    }
}
