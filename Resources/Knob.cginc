// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

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
fixed4 _Color;
fixed3 _Highlight;

v2f vert(appdata_t IN)
{
    v2f OUT;
    OUT.vertex = UnityObjectToClipPos(IN.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color;
    return OUT;
}

fixed4 frag(v2f IN) : SV_Target
{
    float2 uv = IN.texcoord.xy;

    half2 uv1 = normalize(half2(0.5 - uv.y, uv.x - 0.5));
    half2 uv2 = normalize(half2(ddx(uv.x), ddx(uv.y)));

    half a1 = lerp(1 - uv1.x, uv1.x - 1, uv1.y < 0);
    half a2 = lerp(1 - uv2.x, uv2.x - 1, uv2.y < 0);

    fixed4 c0 = tex2D(_MainTex, uv);
    fixed3 c1 = c0.rrr * IN.color.rgb;
    fixed3 c2 = lerp(c1, _Highlight, max(c0.g, a1 < a2));

    return fixed4(c2, c0.a * IN.color.a);
}
