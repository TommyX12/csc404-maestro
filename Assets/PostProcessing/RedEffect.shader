Shader "Hidden/Custom/RedEffect"
{
	HLSLINCLUDE

#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	float _Blend;
	int _PixelSize;
	float4 _MainTex_TexelSize;

	float4 Frag(VaryingsDefault input) : SV_Target
	{
		float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);
		float3 luminance = color.rgb * float3(1.0, 0.0215, 0.07);
		color.rgb = lerp(color.rgb, luminance.xyz, _Blend.xxx);
		return color;
	}

		ENDHLSL

		SubShader
	{
		Cull Off ZWrite Off ZTest Always

			Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}