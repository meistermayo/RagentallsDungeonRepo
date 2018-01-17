Shader "Custom/vertexInflationShader" {
	Properties {
		_MainTex ("Texture",2D) = "white" {}
		_Amount ("Extrusion Amount", Range(-1,1)) = 0.5
		_Direction ("0 < x < 0",Range(-1,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		struct Input {
			float2 uv_MainTex;
		};

		float _Amount;
		float _Direction;

		void vert (inout appdata_full v)
		{
			if (_Direction > 0)
				v.vertex.xyz += v.normal * _Amount;
			else 
				v.vertex.xyz -= v.normal * _Amount;
		}

		sampler2D _MainTex;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D (_MainTex,IN.uv_MainTex).rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
