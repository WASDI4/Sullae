Shader "Image_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        //화면 전체에 적용되는 쉐이더. 앞뒤가 없고 z값이 기록되면 안됨.
        //z값이 항상 그려져야함.
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _ToneTex;
            //float4 (텍스쳐이름)_TexelSize 로 변수를 선언하면 
            //별다른 대입을 하지 않아도 변수 안에서 
            //x : 1/텍스처너비 y : 1/텍스처 높이
            //z : 텍스처너비  w : 텍스처 높이 가 들어가게 됨.
            float4 _ToneTex_TexelSize;
            //인스펙터에서 조절할 수 있도록 변수 선언
            float4 _ToneLimit;
            float _GrayScalePow;

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                //(r+g+b)/3을 하면 색상의 명도 값이 나옴. 
                //그것을 rgb에 모두 적용하면 그레이스케일이 적용된 화면이 나옴.
                float grayScale = (col.r+col.g+col.b)/3;
                //그레이스케일에 pow를 줘서 레벨을 조절할 수 있게 함.
                grayScale = pow(grayScale, _GrayScalePow);
                
                //_ScreenParams은 유니티 내부의 변수. xy에 화면 너비,높이가 들어감.
                //이렇게 하면 타일링시킨 결과가 화면에 나오게 됨.
                float4 tone = tex2D(_ToneTex, 
                (i.uv*_ScreenParams.xy*_ToneTex_TexelSize.xy));

                float4 final;
                final.rgb = tone.rgb;
                //그레이스케일이 _ToneLimit.x보다 밝으면 흰색을 넣는다:
                final.rgb = (grayScale > _ToneLimit.x)? 1:
                //아니라면 Y보다 밝은지 확인해서 밝으면 tone.r을 넣는다:
                    (grayScale > _ToneLimit.y)? tone.r :
                //아니라면 z보다 밝은지 확인해서 tone.g를 넣는다:
                    (grayScale > _ToneLimit.z)? tone.g :
                //아니라면 w보다 밝은지 확인해서 tone.b를 넣는다:
                    (grayScale > _ToneLimit.w)? tone.b :
                //그것보다 어두우면 그냥 검정을 넣는다.
                    0;
                

                final.a = 1;
                // just invert the colors
                //_MainTex를 불러와서 색 반전을 함.
                //col.rgb = 1 - col.rgb;
                return final;
            }
            ENDCG
        }
    }
}
