// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PBRMaskTint"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_SAM("SAM", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		_EmissionColor("EmissionColor", Color) = (1,0.6413792,0,0)
		_EmissionPower("EmissionPower", Float) = 2
		_Color01("Color01", Color) = (0,0.1394524,0.8088235,0)
		_Color03("Color03", Color) = (1,0.6827587,0,0)
		_Color02("Color02", Color) = (0.4557808,0,0.6176471,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float4 _Color01;
		uniform float4 _Color02;
		uniform float4 _Color03;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EmissionColor;
		uniform float _EmissionPower;
		uniform sampler2D _SAM;
		uniform float4 _SAM_ST;
		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode2 = tex2D( _Albedo, uv_Albedo );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode5 = tex2D( _Mask, uv_Mask );
			float4 temp_cast_0 = (tex2DNode5.r).xxxx;
			float4 temp_cast_1 = (tex2DNode5.g).xxxx;
			float4 temp_cast_2 = (tex2DNode5.b).xxxx;
			float4 blendOpDest20 = (min(temp_cast_0,_Color01) + min( temp_cast_1 , _Color02 ) + min( temp_cast_2 , _Color03 ) );
			float4 lerpResult19 = lerp( tex2DNode2 , ( ( saturate( ( tex2DNode2 * blendOpDest20 ) )) * 2.0 ) , ( tex2DNode5.r + tex2DNode5.g + tex2DNode5.b ));
			o.Albedo =blendOpDest20;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = ( ( tex2D( _Emission, uv_Emission ) * _EmissionColor ) * _EmissionPower ).rgb;
			float2 uv_SAM = /*i.uv_texcoord **/ _SAM_ST.xy;// +_SAM_ST.zw;
			float4 tex2DNode4 = tex2D( _SAM, uv_SAM );
			o.Metallic = tex2DNode4.b;
			o.Smoothness = tex2DNode4.r;
			o.Occlusion = tex2DNode4.g;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
