Shader "Custom/Shader(specular)" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 10
	}

	SubShader 
	{
		Tags { "LightMode"="ForwardBase" }
			Pass
			{
				CGPROGRAM

				#pragma vertex vert 
				#pragma fragment frag

				uniform float4 _Color;
				uniform float4 _SpecColor;
				uniform float _Shininess;

				uniform float4 _LightColor0;

				struct vertexInput 
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct vertexOutput 
				{
					float4 pos : SV_POSITION;
					float4 col : COLOR;
				};

				vertexOutput vert(vertexInput v)
				{
					vertexOutput o;

					// Vectors
					float3 normalDirection = normalize( mul(float4(v.normal, 0), unity_WorldToObject).xyz);
					float3 viewDirection = normalize( float3( float4 (_WorldSpaceCameraPos.xyz, 1) - UnityObjectToClipPos(v.vertex).xyz ) );
					float3 lightDirection;
					float atten = 1;

					// Lighting
					lightDirection = normalize(_WorldSpaceCameraPos.xyz);
					float3 diffuseReflection = atten * _LightColor0.xyz * max( 0, dot( normalDirection, lightDirection));
					float3 specularReflection = atten * _SpecColor.rgb * max( 0, dot( normalDirection, lightDirection)) * pow ( max(0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess );
					float3 lightFinal = diffuseReflection + specularReflection + UNITY_LIGHTMODEL_AMBIENT;

					o.col = float4(lightFinal * _Color, 1);
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}

				float4 frag(vertexOutput i) : COLOR
				{
					return i.col;
				}
		
				ENDCG
			}
	}
	FallBack "Diffuse"
}
