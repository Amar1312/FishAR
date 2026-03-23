Shader "Custom/FishPondWater_TopOnly_Fixed"
{
    Properties
    {
        _WaterColor("Water Color", Color) = (0,0.5,0.7,0.7)
        _WaveSpeed("Wave Speed", Float) = 1
        _WaveHeight("Wave Height", Float) = 0.05
        _WaveFrequency("Wave Frequency", Float) = 2
        _Transparency("Transparency", Range(0,1)) = 0.7
        _TopHeight("Top Surface Height", Float) = 0.5
        _SurfaceFade("Surface Fade Range", Float) = 0.1
    }

        SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
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
            };

            float4 _WaterColor;
            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;
            float _Transparency;
            float _TopHeight;
            float _SurfaceFade;

            v2f vert(appdata v)
            {
                v2f o;

                float3 pos = v.vertex.xyz; // LOCAL SPACE

                // mask only top surface
                float heightMask = smoothstep(
                    _TopHeight - _SurfaceFade,
                    _TopHeight,
                    pos.y
                );

                // wave using LOCAL coordinates
                float wave = sin(pos.x * _WaveFrequency + _Time.y * _WaveSpeed) *
                             cos(pos.z * _WaveFrequency + _Time.y * _WaveSpeed);

                pos.y += wave * _WaveHeight * heightMask;

                o.vertex = UnityObjectToClipPos(float4(pos,1));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(_WaterColor.rgb,_Transparency);
            }

            ENDCG
        }
    }
}