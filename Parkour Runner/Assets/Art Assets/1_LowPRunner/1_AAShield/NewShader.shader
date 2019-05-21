// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33771,y:32653,varname:node_4795,prsc:2|emission-5976-OUT,alpha-7126-OUT,voffset-2372-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32246,y:32756,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dc79d6ae79052624f8634342705fe414,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:2053,x:33157,y:32589,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2672,x:31559,y:33361,prsc:2,pt:False;n:type:ShaderForge.SFN_Transform,id:2440,x:31745,y:33361,varname:node_2440,prsc:2,tffrom:0,tfto:1|IN-2672-OUT;n:type:ShaderForge.SFN_ComponentMask,id:220,x:31938,y:33361,cmnt:We need only R and G values for UV,varname:node_220,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2440-XYZ;n:type:ShaderForge.SFN_RemapRange,id:9038,x:32138,y:33361,varname:node_9038,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-220-OUT;n:type:ShaderForge.SFN_Rotator,id:5072,x:32312,y:33361,varname:node_5072,prsc:2|UVIN-9038-OUT,SPD-7761-OUT;n:type:ShaderForge.SFN_Panner,id:1351,x:32491,y:33361,varname:node_1351,prsc:2,spu:0.3,spv:0.25|UVIN-5072-UVOUT,DIST-3724-OUT;n:type:ShaderForge.SFN_Tex2d,id:53,x:32712,y:33361,ptovrint:False,ptlb:node_53,ptin:_node_53,varname:_node_53,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:416a182828922d04d9e14327a5e9ab07,ntxv:2,isnm:False|UVIN-1351-UVOUT;n:type:ShaderForge.SFN_Time,id:2338,x:32138,y:33601,varname:node_2338,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3724,x:32395,y:33602,varname:node_3724,prsc:2|A-7761-OUT,B-2338-T;n:type:ShaderForge.SFN_ValueProperty,id:7761,x:32138,y:33543,ptovrint:False,ptlb:node_7761,ptin:_node_7761,varname:node_7761,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.23;n:type:ShaderForge.SFN_ValueProperty,id:9796,x:32694,y:33543,ptovrint:False,ptlb:node_9796,ptin:_node_9796,varname:node_9796,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.27;n:type:ShaderForge.SFN_Multiply,id:2372,x:32960,y:33447,varname:node_2372,prsc:2|A-53-R,B-9796-OUT,C-2440-XYZ;n:type:ShaderForge.SFN_Fresnel,id:5082,x:32246,y:33026,varname:node_5082,prsc:2|EXP-7098-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7098,x:32020,y:33026,ptovrint:False,ptlb:node_7098,ptin:_node_7098,varname:node_7098,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.7;n:type:ShaderForge.SFN_SwitchProperty,id:2144,x:32939,y:32947,ptovrint:False,ptlb:node_2144,ptin:_node_2144,varname:node_2144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-5722-OUT,B-5082-OUT;n:type:ShaderForge.SFN_Multiply,id:5869,x:32955,y:33084,varname:node_5869,prsc:2|A-534-A,B-53-A,C-6074-R;n:type:ShaderForge.SFN_Color,id:534,x:32246,y:32592,ptovrint:False,ptlb:node_534,ptin:_node_534,varname:node_534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Add,id:980,x:33111,y:32992,varname:node_980,prsc:2|A-2144-OUT,B-5869-OUT;n:type:ShaderForge.SFN_Clamp01,id:8052,x:33189,y:32788,varname:node_8052,prsc:2|IN-980-OUT;n:type:ShaderForge.SFN_Multiply,id:7126,x:33474,y:32802,varname:node_7126,prsc:2|A-8052-OUT,B-1848-OUT,C-2053-A,D-7489-A;n:type:ShaderForge.SFN_Slider,id:1848,x:33111,y:33190,ptovrint:False,ptlb:node_1848,ptin:_node_1848,varname:node_1848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:5722,x:32236,y:32963,varname:node_5722,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:657,x:32657,y:32569,varname:node_657,prsc:2|A-534-RGB,B-6074-R,C-53-RGB;n:type:ShaderForge.SFN_Multiply,id:2291,x:32657,y:32699,varname:node_2291,prsc:2|A-7489-RGB,B-5082-OUT;n:type:ShaderForge.SFN_Color,id:7489,x:32339,y:32318,ptovrint:False,ptlb:node_7489,ptin:_node_7489,varname:node_7489,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.4829559,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:1439,x:32912,y:32542,varname:node_1439,prsc:2|A-657-OUT,B-2291-OUT;n:type:ShaderForge.SFN_Multiply,id:5976,x:33351,y:32418,varname:node_5976,prsc:2|A-1439-OUT,B-4713-OUT,C-2053-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4713,x:32912,y:32497,ptovrint:False,ptlb:node_4713,ptin:_node_4713,varname:node_4713,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.8;proporder:6074-53-7761-9796-7098-2144-534-1848-7489-4713;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_53 ("node_53", 2D) = "black" {}
        _node_7761 ("node_7761", Float ) = 0.23
        _node_9796 ("node_9796", Float ) = 0.27
        _node_7098 ("node_7098", Float ) = 0.7
        [MaterialToggle] _node_2144 ("node_2144", Float ) = 0
        _node_534 ("node_534", Color) = (1,0,0,1)
        _node_1848 ("node_1848", Range(0, 1)) = 1
        _node_7489 ("node_7489", Color) = (0,0.4829559,1,1)
        _node_4713 ("node_4713", Float ) = 1.8
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_53; uniform float4 _node_53_ST;
            uniform float _node_7761;
            uniform float _node_9796;
            uniform float _node_7098;
            uniform fixed _node_2144;
            uniform float4 _node_534;
            uniform float _node_1848;
            uniform float4 _node_7489;
            uniform float _node_4713;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2338 = _Time;
                float4 node_2286 = _Time;
                float node_5072_ang = node_2286.g;
                float node_5072_spd = _node_7761;
                float node_5072_cos = cos(node_5072_spd*node_5072_ang);
                float node_5072_sin = sin(node_5072_spd*node_5072_ang);
                float2 node_5072_piv = float2(0.5,0.5);
                float3 node_2440 = mul( unity_WorldToObject, float4(v.normal,0) ).xyz;
                float2 node_5072 = (mul((node_2440.rgb.rg*0.5+0.5)-node_5072_piv,float2x2( node_5072_cos, -node_5072_sin, node_5072_sin, node_5072_cos))+node_5072_piv);
                float2 node_1351 = (node_5072+(_node_7761*node_2338.g)*float2(0.3,0.25));
                float4 _node_53_var = tex2Dlod(_node_53,float4(TRANSFORM_TEX(node_1351, _node_53),0.0,0));
                v.vertex.xyz += (_node_53_var.r*_node_9796*node_2440.rgb);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_2338 = _Time;
                float4 node_2286 = _Time;
                float node_5072_ang = node_2286.g;
                float node_5072_spd = _node_7761;
                float node_5072_cos = cos(node_5072_spd*node_5072_ang);
                float node_5072_sin = sin(node_5072_spd*node_5072_ang);
                float2 node_5072_piv = float2(0.5,0.5);
                float3 node_2440 = mul( unity_WorldToObject, float4(i.normalDir,0) ).xyz;
                float2 node_5072 = (mul((node_2440.rgb.rg*0.5+0.5)-node_5072_piv,float2x2( node_5072_cos, -node_5072_sin, node_5072_sin, node_5072_cos))+node_5072_piv);
                float2 node_1351 = (node_5072+(_node_7761*node_2338.g)*float2(0.3,0.25));
                float4 _node_53_var = tex2D(_node_53,TRANSFORM_TEX(node_1351, _node_53));
                float node_5082 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_7098);
                float3 emissive = (((_node_534.rgb*_MainTex_var.r*_node_53_var.rgb)+(_node_7489.rgb*node_5082))*_node_4713*i.vertexColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(saturate((lerp( 0.0, node_5082, _node_2144 )+(_node_534.a*_node_53_var.a*_MainTex_var.r)))*_node_1848*i.vertexColor.a*_node_7489.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
