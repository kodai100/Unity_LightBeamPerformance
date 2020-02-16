Shader "Noriben/BeamLight"
{
	Properties
	{
		[Header(Texture)]
		_NoiseTex ("NoiseTex", 2D) = "white" {}

		[Header(Color)]
		_Color ("Color" , Color) = (1.0, 1.0, 1.0, 1.0)
		_Intensity ("Light Intensity", Range(0, 10)) = 1 

		[Space]
		[Header(Size)]
		_ConeWidth ("Zoom", Range(-0.07, 0.5)) = 1
		_ConeLength ("Length", Range(0.01, 3)) = 1

		[Space]
		[Header(Noise)]
		_TexPower ("Noise power", Range(0, 1)) = 1
		_LightIndex ("Noise seed", float) = 0

		[Space]
		[Header(Soft)]
		_RimPower ("Edge soft", Range(0.01, 10.0)) = 3

		[Space]
		[Header(Gradation Height)]
		[Toggle] _GradOn ("Gradation Height ON", float) = 0
		_GradHeight ("Gradation Height", float) = 1
		_GradPower ("Gradation Power", Range(1, 0)) = 0.3

		[Space]
		[Header(Divide)]
		_Divide ("Divide", Range(0, 30)) = 0
		_DivideScroll("Divide Scroll", Range(-10, 10)) = 0
		_DividePower ("Divide Power", Range(0, 1)) = 0
		


	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		Cull Front
		Blend One One
		Zwrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature SliderOn
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD3;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0; //フレネル用
				UNITY_FOG_COORDS(1) //内部でTEXCOORD1を使っている
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD2;
				float3 worldPos : TEXCOORD3;
				float2 uv2 : TEXCOORD4; //テクスチャ用
			};

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float4 _Color;
			float _Intensity;
			float _RimPower;
			float _TexPower;
			float _GradHeight;
			float _GradPower;
			float _ConeWidth;
			float _ConeLength;
			float _Divide;
			float _DivideScroll;
			float _DividePower;
			float _LightIndex;		
			float _GradOn;
			

			//リマップ
			//InMinMax.xからInMinMax.yの範囲で入力された値Inが、OutMinMax.xからOutMinMax.yのスケールにリマップして出力される
			float remap(float In, float2 InMinMax, float2 OutMinMax)
			{
				return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
			}

			//ランダム
			float random1D (float p)
			{ 
            return frac(sin(p) * 10000);
        	}

			float random2D (float2 p)
			{ 
            return frac(sin(dot(p, fixed2(12.9898,78.233))) * 43758.5453);
        	}


			v2f vert (appdata v)
			{

				v2f o;

				//コーンの直径を大きくする
				//0から1にグラデーション状に大きくする
				float vertexHeight = (1 - v.uv.y) * _ConeWidth * 100; 
				//normal方向に拡大
				v.vertex.xz = v.vertex.xz + v.normal.xz * 1 * vertexHeight;
				v.vertex.y *= _ConeLength;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;//UnityObjectToWorldNormal(v.normal);
				
				o.uv = v.uv;
				o.uv2 = TRANSFORM_TEX(v.uv2, _NoiseTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				float3 fresnelviewDir = normalize(ObjSpaceViewDir(v.vertex));
				UNITY_TRANSFER_FOG(o,o.vertex);
				
				return o;
			}

			
			fixed4 frag (v2f i) : SV_Target
			{

				i.normal = normalize(i.normal);
				float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				viewDir = normalize(mul(unity_WorldToObject,float4(viewDir,0.0)));
				
				//光のグラデーション
				float grad = 0.1 / distance(i.uv.y, float2(1.0, 1.0));
				//端のだんだん消えていく感じのマスク
				float mask = smoothstep(0.1, 1.0, distance(i.uv.y, 0.0));
				//リムライト的処理エッジをやわらかく消す
				float rim = (pow(max(0, dot(i.normal, -viewDir)), _RimPower));

				//ビームの分割
				float pi = 3.14159265;
				float divide = sin(i.uv.x * pi * floor(_Divide) * 2 + (_Time.w * _DivideScroll)) + 1.0;
				divide = lerp(1.0, divide, _DividePower);
				//合成と明るさ調整・ビームが太くなるほど暗くする
				float baseCol = grad * 20 * _Intensity  * mask * rim * divide * pow(1.5, - _ConeWidth * 20);

				//ノイズ用uvスクロール
				i.uv2.x = i.uv2.x + _Time * 0.5;
				i.uv2.y = i.uv2.y + _Time * 0.1;

				//空気中のもやもや用ノイズテクスチャ
				float2 texUV = i.uv2;
				//ノイズテクスチャを位置によって適当にずらす（ほんとに適当）
				float randVal = random1D(_LightIndex + 1); // 適当なランダム値
				texUV += float2((i.worldPos.x + (_LightIndex + 1) * 2.345 + randVal) * 0.2357 * 0.01, (i.worldPos.y + (_LightIndex + 1) * 8.345 + randVal) * 0.324643 *  0.1 + 5);
				//_ConeWidthの値によってテクスチャをY軸に拡大
				//texUV.y = texUV.y + remap(_ConeWidth, float2(-0.6, 0.65), float2(4, 0)); 
				float4 tex = tex2D(_NoiseTex, texUV);
				tex = lerp(fixed4(1, 1, 1, 1), tex, _TexPower);
				float4 col = float4(baseCol, baseCol, baseCol, 1) * _Color * tex;

				//高さが0になるにつれてグラデーションで消える
				float worldHeight = saturate(i.worldPos.y * _GradPower - _GradHeight) ;
				worldHeight = lerp(1, worldHeight, _GradOn); //トグル
				col *= float4(worldHeight, worldHeight, worldHeight, 1);

				//いい感じの減衰
				float lm = length(_WorldSpaceCameraPos - i.worldPos);
				lm = clamp(lm * 0.3, 0.0, 1.0) - 0.0;
				col *= float4(lm, lm, lm, 1.0);
				
				//正面向いたときだけフラッシュ
				float xzlen = length(mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1.0)).xz);
				col *= clamp(0.6/xzlen,1.0,3.0)  - 0.0; //-0.0が明るさ調整

				//bloom爆発対策
				col = clamp(col, 0, 3);

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
