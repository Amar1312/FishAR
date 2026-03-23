Shader "Custom/FishPondWater"
{
    Properties
    {
        _WaterColor("Water Color", Color) = (0, 0.5, 0.7, 0.7)
        _WaveSpeed("Wave Speed", Float) = 1
        _WaveHeight("Wave Height", Float) = 0.05
        _WaveFrequency("Wave Frequency", Float) = 2
        _Transparency("Transparency", Range(0,1)) = 0.7
    }

        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _WaterColor;
            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;
            float _Transparency;

            v2f vert(appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Wave movement
                float wave = sin(worldPos.x * _WaveFrequency + _Time.y * _WaveSpeed) *
                             cos(worldPos.z * _WaveFrequency + _Time.y * _WaveSpeed);

                worldPos.y += wave * _WaveHeight;

                o.vertex = UnityObjectToClipPos(float4(worldPos,1));
                o.worldPos = worldPos;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(_WaterColor.rgb, _Transparency);
            }

            ENDCG
        }
    }
}