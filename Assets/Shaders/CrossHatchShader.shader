Shader "Custom/CrossHatchShader"
{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _LitTex("Light Hatch", 2D) = "white" {}
        _MedTex("Medium Hatch", 2D) = "white" {}
        _HvyTex("Heavy Hatch", 2D) = "white" {}
        _Repeat("Repeat Tile", float) = 4
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf CrossHatch

            sampler2D _MainTex;
            sampler2D _LitTex;
            sampler2D _MedTex;
            sampler2D _HvyTex;
            fixed _Repeat;

            struct MySurfaceOutput
            {
                fixed3 Albedo;
                fixed3 Normal;
                fixed3 Emission;
                fixed Gloss;
                fixed Alpha;
                fixed val;
                float2 screenUV;
                float3 Pos;
            };

            struct Input {
                float2 uv_MainTex;
                float4 screenPos;
                float3 worldPos;
            };

            void surf(Input IN, inout MySurfaceOutput o) {
                //uncomment to use object space hatching
                //            o.screenUV = IN.uv_MainTex * _Repeat;
                //uncomment to use screen space hatching
                o.screenUV = IN.screenPos.xy * 4 / IN.screenPos.w;
                half v = length(tex2D(_MainTex, IN.uv_MainTex).rgb) * 0.33;
                o.val = v;
                o.Pos = IN.worldPos;
            } 

            half4 LightingCrossHatch(MySurfaceOutput s, half3 lightDir, half atten)
            {
                half3 pointLightDir = s.Pos - _WorldSpaceLightPos0; 
                half NdotL = 1 - dot(s.Normal, lightDir);

                half4 cLit = tex2D(_LitTex, s.screenUV);
                half4 cMed = tex2D(_MedTex, s.screenUV);
                half4 cHvy = tex2D(_HvyTex, s.screenUV);
                half4 c;

                half v = saturate(length(_LightColor0.rgb) * (NdotL * atten * 2) * s.val);

                c.rgb = lerp(cHvy, cLit, v);
                c.rgb = lerp(c.rgb, cLit, v);
                c.a = s.Alpha;

                return c;
            }

            ENDCG
        }
            Fallback "Diffuse"
}
