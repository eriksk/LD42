Shader "Custom/Water" 
{
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecondaryTex ("Secondary (RGB)", 2D) = "white" {}
      	_BumpMap ("Bumpmap", 2D) = "bump" {}
      	_BumpMap2 ("Bumpmap2", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows 
		// #pragma vertex:vert
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondaryTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondaryTex;
        	float2 uv_BumpMap;
        	float2 uv_BumpMap2;
			// float2 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
      	sampler2D _BumpMap;
      	sampler2D _BumpMap2;
		
		// sampler2D _CameraDepthNormalsTexture;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)
		
		// float sampleDepth(float2 uv)
		// {
		// 	float depth;
		// 	float3 normal;
		// 	DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, uv), depth, normal);
		// 	return depth;
		// }

		// void vert (inout appdata_full v, out Input o) {
		// 	UNITY_INITIALIZE_OUTPUT(Input, o);
    	// 	o.screenPos = ComputeScreenPos(v.vertex);
		// }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 c2 = tex2D(_SecondaryTex, IN.uv_SecondaryTex);

			fixed4 n = 
				(tex2D(_BumpMap, IN.uv_BumpMap) + 
				tex2D(_BumpMap2, IN.uv_BumpMap2)) * 0.5;

			// float depth = sampleDepth(IN.screenPos);
			o.Albedo = (c.rgb + c2.rgb) * 0.5 * _Color;
        	o.Normal = UnpackNormal(n);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
