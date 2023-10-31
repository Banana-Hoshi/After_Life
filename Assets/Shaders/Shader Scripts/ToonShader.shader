Shader "Custom/ToonShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_OutlineColor("OutlineColor", Color) = (0, 0, 0, 1)
		_OutlineSize("OutlineSize", Range(1, 5)) = 1
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RampTex("Ramp Texture", 2D) = "white"{}
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimPower("Rim Power", Range(0.5, 15.0)) = 15.0
	}

	SubShader
	{
		Tags 
		{ 
			"Queue" = "Transparent"
		}
		LOD 200

		Pass
		{
			Stencil{
			Ref 1
			Comp Always
			}

			Cull Off
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float _OutlineSize;
			float4 _OutlineColor;

			struct appdata 
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f 
			{
				float4 pos : POSITION;
				float4 color : COLOR;
				float3 normal : NORMAL;
			};

			v2f vert(appdata v) 
			{
				v.vertex.xyz *= _OutlineSize;
				
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = _OutlineColor;
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}

			ENDCG
		}

		Tags {"RenderType" = "Opaque"}
		LOD 200

		Stencil
		{
			Ref 1
			Comp always
			Pass replace
		}

		CGPROGRAM
		#pragma surface surf ToonRamp
		#pragma target 3.0

		float4 _Color;
		float4 _RimColor;
		float _RimPower;
		sampler2D _RampTex;
		sampler2D _MainTex;

		float4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float diff = (dot(s.Normal, lightDir) * 0.5 + 0.5) * atten;
			float2 rh = diff;
			float3 ramp = tex2D(_RampTex, rh).rgb;

			float4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
			c.a = s.Alpha;
			return c;
		}

		struct Input
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			o.Alpha = c.a;
		}

		ENDCG
	}
	
	FallBack "Diffuse"
}
