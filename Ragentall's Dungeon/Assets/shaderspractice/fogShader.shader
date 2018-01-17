Shader "Custom/fogShader" {
	Properties {
	_MainTex ("Texture", 2D) = "white" {}
	_FogColor ("Fog Color", Color) = (0.0,0.0,0.0,1.0)
	_Distance ("Distance",Range(0,10)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert finalcolor:col vertex:vert
		struct Input{
			float2 uv_MainTex;
			half _fog;
		};

		float _Distance;

		void vert (inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input,data);
			float4 hpos = float4(v.normal.x,v.normal.y,v.normal.z,1);
			hpos.xy /= hpos.w;
			data._fog = min (1, dot (hpos.xy, hpos.xy) * _Distance);
		}

		fixed4 _FogColor;
		void col (Input IN, SurfaceOutput o, inout fixed4 _color)
		{
			fixed3 fogColor = _FogColor.rgb;
			_color.rgb = lerp ( _color.rgb, fogColor, IN._fog);
		}

		sampler2D _MainTex;
		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
