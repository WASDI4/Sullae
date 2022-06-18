using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스크립트를 에디트 모드에서도 실행시킨다
//게임플레이를 누르지 않아도 발동됨.
[ExecuteInEditMode]

public class ImageEffectCameraScript : MonoBehaviour
{
    public Material mat;
    public Camera cam;

    private void Start() {
        //이 스크립트가 붙어있는 오브젝트의 카메라를 cam에 저장
        cam = GetComponent<Camera>();
        //그 카메라가 보고 있는 화면의 뎁스노말 텍스쳐를 저장하도록 만듬.
        //뎁스 텍스쳐 말고 노말텍스쳐(카메라를 기준으로한 노말)을
        //이용해서 외곽선을 만듬.
        cam.depthTextureMode = DepthTextureMode.DepthNormals;
    }



    //모든 화면이 렌더링 되었을 때 화면 이미지에 mat을 적용
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, mat);
    }
}
