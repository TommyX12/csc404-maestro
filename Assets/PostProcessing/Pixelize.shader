Shader "Hidden/Custom/Pixelize"
{
	HLSLINCLUDE

#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	float _Blend;
	int _PixelSize;
	float4 _MainTex_TexelSize;

	float4 Frag(VaryingsDefault input) : SV_Target
	{
		float width = _MainTex_TexelSize.z;
		float height = _MainTex_TexelSize.w;
		float i_width = _MainTex_TexelSize.x;
		float i_height = _MainTex_TexelSize.y;

		float4 color = float4(0,0,0,0);
		
		int x = input.texcoord.x * width;
		int y = input.texcoord.y * height; 

		int base_x = (x / _PixelSize)*_PixelSize;
		int base_y = (y / _PixelSize)*_PixelSize;

		float2 base = float2(base_x*i_width, base_y*i_height);

		//for (int i = 0; i < _PixelSize*_PixelSize; i++) {
		//	float2 step = float2(i_width*(i%_PixelSize), i_height*(i / _PixelSize));
		//	float2 sample_pos = base + step;
		//	color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, sample_pos);
		//}

		//color = color / (_PixelSize*_PixelSize);

		color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, base);

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