Shader "Custom/Voronoi" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		float2 hash(float2 uv)
		{
			return float2(frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453123),frac(sin(dot(uv, float2(19.9898, 71.233)))*82138.5453123));
		}
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float nearestDist = 200;//not great not terrible
			for (int y = -1; y <= 1; y++) {
				for (int x = -1; x <= 1; x++) {
					float2 pointP = hash(float2(floor(IN.uv_MainTex.x * 10 + x), floor(IN.uv_MainTex.y * 10 + y)));
					pointP = 0.5 + 0.5*sin(_Time + 6.2831*pointP);
						nearestDist =  min(distance(pointP + float2(x, y), (IN.uv_MainTex * 10) % 1), nearestDist);
				}
			}
			float breath = lerp(0.5, 1, _SinTime.w / 2);
			//nearestDist = distance(hash(float2(floor(IN.uv_MainTex.x * 10), floor(IN.uv_MainTex.y * 10))), (IN.uv_MainTex*10)%1);
			fixed4 c = (tex2D (_MainTex, IN.uv_MainTex)*breath*2) * _Color * pow(nearestDist,3);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Emission = c.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
