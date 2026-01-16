Shader "Custom/flashlightRevealShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}   // Texture reference
        _Color ("Color", Color) = (1, 1, 1, 1)
        _FlashlightPosition ("Flashlight Position", Vector) = (0, 0, 0, 1)
        _FlashlightRange ("Flashlight Range", Float) = 5.0
        _IsFlashlightOn ("Is Flashlight On", Float) = 0.0 // New property to control fade logic
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Enable transparency
            ZWrite Off
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;  // Texture coordinates
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            // Texture and properties
            sampler2D _MainTex;  // Texture sampler
            float4 _Color;
            float4 _FlashlightPosition;
            float _FlashlightRange;
            float _IsFlashlightOn; // Property to check if flashlight is on

            // Vertex shader to pass information
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            // Fragment shader for calculating transparency
            fixed4 frag(v2f i) : SV_Target
            {
                // If flashlight is off, no fade is applied
                if (_IsFlashlightOn == 0.0f)
                {
                    return tex2D(_MainTex, i.uv) * _Color; // No fade, just the texture and color
                }

                // Distance from flashlight to the object
                float distanceToFlashlight = distance(i.worldPos, _FlashlightPosition.xyz);
                
                // Smooth fade effect based on distance, but only if flashlight is on
                float fadeFactor = smoothstep(_FlashlightRange - 1.0f, _FlashlightRange, distanceToFlashlight);

                // Sample texture
                fixed4 texColor = tex2D(_MainTex, i.uv);
                
                // Apply texture color, tint, and fade
                fixed4 col = texColor * _Color;
                col.a *= fadeFactor; // Control transparency with fade

                return col;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
