Shader "Unlit/ColorPicker"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Hue ("Hue", Range(0, 1)) = 0
		_Saturation("Saturation", Range(0, 1)) = 0
		_Value("Value", Range(0, 1)) = 0
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float _Hue;
			float _Saturation;
			float _Value;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			//these functions are from http://www.chilliant.com/rgb2hsv.html still don't know the math for this lmao
			float3 HUEtoRGB(in float H)
			{
				float R = abs(H * 6 - 3) - 1;
				float G = 2 - abs(H * 6 - 2);
				float B = 2 - abs(H * 6 - 4);
				return saturate(float3(R, G, B));
			}
			float3 HSVtoRGB(in float3 HSV)
			{
				float3 RGB = HUEtoRGB(HSV.x);
				return ((RGB - 1) * HSV.y + 1) * HSV.z;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = float4(HSVtoRGB(float3(_Hue,i.uv)),1);// tex2D(_MainTex, i.uv);
				float cursorDist = distance(float2(_Saturation, _Value), i.uv);
				if (_Value < 0.33f)
				{
					col /= cursorDist<0.03f || cursorDist > 0.06f;
				}
				else {
					col *= cursorDist<0.03f || cursorDist > 0.06f;
				}
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
