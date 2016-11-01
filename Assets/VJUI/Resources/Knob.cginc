#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex   : POSITION;
    fixed4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float4 vertex    : SV_POSITION;
    fixed4 color     : COLOR;
    float2 texcoord  : TEXCOORD0;
};

sampler2D _MainTex;
float4 _MainTex_TexelSize;
fixed4 _Color;

v2f vert(appdata_t IN)
{
    v2f OUT;
    OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color;
    return OUT;
}

fixed4 frag(v2f IN) : SV_Target
{
    half2 uv = IN.texcoord.xy;

    half2 uv1 = normalize(half2(0.5 - uv.y, uv.x - 0.5));
    half2 uv2 = normalize(half2(ddx(uv.x), ddx(uv.y)));

    half a1 = lerp(1 - uv1.x, uv1.x - 1, uv1.y < 0);
    half a2 = lerp(1 - uv2.x, uv2.x - 1, uv2.y < 0);

    fixed4 c = tex2D(_MainTex, uv);
    half br = max((a1 < a2) * c.r, c.g) * 0.95 + 0.05;

    return half4(br, br, br, c.a) * IN.color;
}
