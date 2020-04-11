sampler uImage0 : register(s0);
float uTime;
float2 uImageSize;

float ffmod(float v, float m){
	int f = floor(v / m);
	return v - f * m;
}

float3 HUEtoRGB(float H)
{
    float R = abs(H * 6 - 3) - 1;
    float G = 2 - abs(H * 6 - 2);
    float B = 2 - abs(H * 6 - 4);
	return saturate(float3(R,G,B));
}

float4 rainbow(float2 coords : TEXCOORD0) : COLOR0 {
	float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	return float4(HUEtoRGB(ffmod(uTime * 0.01 + coords.x, 1)), color.a);
}



float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
	float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	float gs = dot(float3(0.3, 0.59, 0.11), color.rgb);
	return float4(gs, gs, gs, color.a);
}




technique Technique1 {
	pass Test {
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
	pass Rainbow {
		PixelShader = compile ps_2_0 rainbow();
	}

}