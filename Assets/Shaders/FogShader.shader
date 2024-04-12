Shader "Unlit/FogShader"
{
    Properties
    {
        _MainTex ("In Texture", 2D) = "white" {}
        _Far ("Far", float) = 0
        _Near ("Near", float) = 0
        _Density ("Density", float) = 0
        _FogColour ("Fog Colour", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _MainTex_ST;

            float _Far;
            float _Near;
            float _Density;
            float4 _FogColour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float depth = 1/((1 - _Far) / _Near * tex2D(_CameraDepthTexture, i.uv).x + (_Far / _Near));
                float fogFactor = pow(2, -pow(_Density * depth, 2));
                return (1 - fogFactor) * col + fogFactor * _FogColour;
            }
            ENDCG
        }
    }
}
