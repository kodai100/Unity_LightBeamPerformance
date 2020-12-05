Shader "Custom/Laser"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_Dimmer("Dimmer", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent-2"}
        LOD 100

		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			float4 _Color;
			float _Dimmer;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float Flicker() {
				return 0.5 * sin(_Time.z * 50) + 0.5;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                
				fixed4 col = fixed4(_Color.r, _Color.g, _Color.b, 1) * _Dimmer * Flicker();
                return col;
            }
            ENDCG
        }
    }
}
