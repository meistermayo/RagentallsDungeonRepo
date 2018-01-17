Shader "Custom/screenChopShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white"{}
		_BumpMap ("Bumpmap",2D) = "bump"{}
		_Width ("Ring Count",Range(0.0,100.0)) =  0.0
		_Offset ("Cull Percent",Range(0.0,1.0)) =  0.0
		_Inset ("Rotation",Range(0.0,100.0)) =  0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Cull Off
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 worldPos;
		};

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Width;
		float _Offset;
		float _Inset;

		void surf (Input IN, inout SurfaceOutput o)
		{
			clip (frac((IN.worldPos.y+IN.worldPos.z*_Inset) * _Width) - _Offset);
			o.Albedo = tex2D (_MainTex,IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}

	FallBack "Diffuse"
}
