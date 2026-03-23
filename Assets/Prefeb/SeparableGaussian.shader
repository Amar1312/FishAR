Shader "UI/Blur/MaskedBackgroundBlur"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Size ("Blur Size", Range(0, 40)) = 1
        _Tint ("Tint", Color) = (1,1,1,0.5)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        // Grab background
        GrabPass { "_GrabTexture" }

        Pass
        {
            Name "UIBlur"

            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex    : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float2 uv        : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _GrabTexture;

            float4 _GrabTexture_TexelSize;
            float _Size;
            float4 _Tint;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeGrabScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Screen UV
                float2 uv = i.screenPos.xy / i.screenPos.w;

                // Pixel offset for blur
                float2 offset = _GrabTexture_TexelSize.xy * _Size;

                // 5-tap blur
                fixed4 col = tex2D(_GrabTexture, uv) * 0.2;

                col += tex2D(_GrabTexture, uv + float2(offset.x, 0)) * 0.2;
                col += tex2D(_GrabTexture, uv - float2(offset.x, 0)) * 0.2;
                col += tex2D(_GrabTexture, uv + float2(0, offset.y)) * 0.2;
                col += tex2D(_GrabTexture, uv - float2(0, offset.y)) * 0.2;

                // Sample UI sprite (for masking)
                fixed4 sprite = tex2D(_MainTex, i.uv);

                // Apply tint
                col *= _Tint;

                // 🔑 Mask blur using sprite alpha
                col.a *= sprite.a;
                col.rgb *= sprite.a;

                return col;
            }
            ENDCG
        }
    }
}