// Screen Shader 模板
// ----------------------------
// 以下参数*必须*添加
// 因为这些参数都有原版的默认传入值
// 如果缺失将导致Null Reference Exception
// 0号采样贴图
sampler uImage0 : register(s0);
// 1号采样贴图
sampler uImage1 : register(s1);
// 颜色参数（默认传入白色，自己设定
float3 uColor;
// 透明度参数（自己设定
float uOpacity;
// 二级颜色参数
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float2 uEffectPos;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize0;
float2 uImageSize1;

float gauss[3][3] = {
    0.075, 0.124, 0.075,
    0.124, 0.204, 0.124,
    0.075, 0.124, 0.075
};
float mod(float x, float y) {
	return x - y * floor(x / y);
}


float2 rotate(float2 vec, float r) {
	return mul(float1x2(vec), float2x2(cos(r), -sin(r), sin(r), cos(r)));
}

float lenFix(float2 vec) {
	return length(vec) * float2(uScreenResolution.y / uScreenResolution.x, 1);
}

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
	float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	// float2 uv = uTargetPosition / uScreenResolution;
	float2 uv = uEffectPos / uScreenResolution;
	float2 dis = (coords - uv) * float2(uScreenResolution.x / uScreenResolution.y, 1);
	
	float distance = length(dis);
	if (distance > 0.3)
		return color;
	float l = abs(sin((distance / 0.3) * 3.14));
	return tex2D(uImage0, uv + dis * l);
}
technique Technique1 {
	pass Test {
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
