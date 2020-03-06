// Screen Shader 模板
// ----------------------------
// 以下参数*必须*添加
// 因为这些参数都有原版的默认传入值
// 如果缺失将导致NullReference Exception
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

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    float2 pixelS = float2(1.0 / uScreenResolution.x, 1.0 / uScreenResolution.y);
    float4 blend = color;
    for (int i = -1; i <= 1; i++) {
        for (int j = -1; j <= 1; j++) {
            blend += gauss[i + 1][j + 1] * tex2D(uImage0, float2(coords.x + i * pixelS.x, coords.y + j * pixelS.y));
        }
    }
    return blend;
}

technique Technique1
{
    pass Test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
