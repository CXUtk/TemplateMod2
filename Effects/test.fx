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
// 透明度参数
float uOpacity;
// 二级颜色参数
float3 uSecondaryColor;
// 时间参数（原版有传入
float uTime;
// 屏幕分辨率
float2 uScreenResolution;
// 屏幕位置
float2 uScreenPosition;
// 
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize0;
float2 uImageSize1;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    float v = dot(color, float3(0.3, 0.59, 0.11));
    return float4(v, v, v, color.a);
}

technique Technique1
{
    pass Test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
