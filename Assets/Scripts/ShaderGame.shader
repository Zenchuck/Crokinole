Shader "Unlit/ShaderGame"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Atlas("Sprite Atlas",2D) ="white"{}
		_Test("Test Vector",Vector) = (0,0,0,0)
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
			#include "RayMarching.cginc"

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
			float4 _MainTex_ST;

			sampler2D _Atlas;

			half4 _Test;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half t = sin(_Time.x)+.5*cos(_Time.y)+.2*sin(_Time.z);
				half2 p = i.uv-half2(.5,.5-t*.1);
				//p.x*= _ScreenParams.x/_ScreenParams.y;

				//fixed4 bg = tex2D(_MainTex, i.uv);

				p = rotate(p,sin(t));
				half3 warp = doTunnel(_Atlas,p,_Time.x);


//				fixed w = length(p);
//
//				fixed d = clamp((w - .12)/.005, 0.0, 1.0); 
//				fixed e = clamp((w - .121)/.005, 0.0, 1.0);
//				fixed f = clamp((w - .24)/.005, 0.0, 1.0); 
//				fixed g = clamp((w - .241)/.005, 0.0, 1.0);
//				fixed h = clamp((w - .36)/.005, 0.0, 1.0); 
//				fixed j = clamp((w - .361)/.005, 0.0, 1.0);
//				fixed k = d*(1-e)+f*(1-g)+h*(1-j); 
//
//				fixed4 so = lerp(float4(1,1,1,1),float4(0,0,0,1),k);
//				bg = lerp(bg,so,bg.a);

				//fixed4 stars = lerp(bg,bg*10,warp.g);
				i.uv.y -=warp.r*.2;
				i.uv.x +=warp.r*.2;
				fixed4 bg = tex2D(_MainTex, i.uv);
				fixed4 stars = lerp(bg,bg*5,warp.b);

				return stars;
			}
			ENDCG
		}
	}
}
