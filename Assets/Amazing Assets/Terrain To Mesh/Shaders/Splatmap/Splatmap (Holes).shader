Shader "Amazing Assets/Terrain To Mesh/Splatmap (Holes)"
{
    Properties
    {


//[HideInInspector][CurvedWorldBendSettings] _CurvedWorldBendSettings("0|1|1", Vector) = (0, 0, 0, 0)


//Terrain To Mesh Properties/////////////////////////////////////////////////////////////////////////////////////////////////////////
[HideInInspector] [TerrainToMeshLayerCounter] _T2M_Layer_Count ("Layer Count", float) = 0		

[Space]
[HideInInspector] [NoScaleOffset] _T2M_SplatMap_0 ("Splat Map #10 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_0_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_Diffuse ("Paint Map 1 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_0_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_0_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_0_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_0_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_0_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_0_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_0_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_1_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_Diffuse ("Paint Map 1 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_1_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_1_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_1_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_1_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_1_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_1_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_1_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_2_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_Diffuse ("Paint Map 2 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_2_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_2_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_2_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_2_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_2_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_2_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_2_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_3_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_Diffuse ("Paint Map 3 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_3_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_3_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_3_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_3_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_3_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_3_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_3_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_1 ("Splat Map #1 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_4_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_Diffuse ("Paint Map 4 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_4_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_4_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_4_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_4_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_4_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_4_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_4_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_5_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_Diffuse ("Paint Map 5 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_5_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_5_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_5_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_5_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_5_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_5_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_5_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_6_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_Diffuse ("Paint Map 6 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_6_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_6_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_6_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_6_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_6_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_6_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_6_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_7_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_Diffuse ("Paint Map 7 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_7_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_7_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_7_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_7_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_7_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_7_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_7_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_2 ("Splat Map #2 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_8_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_Diffuse ("Paint Map 8 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_8_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_8_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_8_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_8_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_8_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_8_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_8_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)
			
[HideInInspector] _T2M_Layer_9_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_Diffuse ("Paint Map 9 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_9_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_9_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_9_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_9_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_9_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_9_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_9_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_10_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_Diffuse ("Paint Map 10 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_10_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_10_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_10_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_10_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_10_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_10_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_10_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_11_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_Diffuse ("Paint Map 11 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_11_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_11_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_11_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_11_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_11_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_11_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_11_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_3 ("Splat Map #3 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_12_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_Diffuse ("Paint Map 12 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_12_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_12_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_12_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_12_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_12_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_12_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_12_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)
			
[HideInInspector] _T2M_Layer_13_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_Diffuse ("Paint Map 13 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_13_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_13_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_13_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_13_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_13_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_13_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_13_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_14_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_Diffuse ("Paint Map 14 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_14_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_14_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_14_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_14_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_14_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_14_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_14_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_15_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_Diffuse ("Paint Map 15 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_15_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_15_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_15_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_15_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_15_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_15_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_15_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


//Texture 2D Array
[HideInInspector] [NoScaleOffset] _T2M_SplatMaps2DArray("SplatMaps 2D Array", 2DArray) = "black" {}
[HideInInspector] [NoScaleOffset] _T2M_DiffuseMaps2DArray("DiffuseMaps 2D Array", 2DArray) = "white" {}
[HideInInspector] [NoScaleOffset] _T2M_NormalMaps2DArray("NormalMaps 2D Array", 2DArray) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_MaskMaps2DArray("MaskMaps 2D Array", 2DArray) = "white" {}	 		 
		 
//Holesmap
[HideInInspector] [NoScaleOffset] _T2M_HolesMap ("Holes Map", 2D) = "white" {}

//Fallback use only
[HideInInspector] _Color("Color", Color) = (1, 1, 1, 1)								//Not used
[HideInInspector] [NoScaleOffset] _BaseColorMap("Fallback Diffuse", 2D) = "white" {}		
[HideInInspector] [NoScaleOffset] _NormalMap("Fallback Normal", 2D) = "bump" {}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="HDRenderPipeline"
            "RenderType"="HDLitShader"
            "Queue"="AlphaTest+0"
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            Blend One Zero
        
            
            
            ZWrite On
        
            
            
            ColorMask 0
        
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_SHADOWS
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   FragInputs.texCoord0
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.texCoord0
                //   AttributesMesh.uv0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            // #define ATTRIBUTES_NEED_TEXCOORD1
            // #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            // #define ATTRIBUTES_NEED_COLOR
            // #define VARYINGS_NEED_POSITION_WS
            // #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            // #define VARYINGS_NEED_TEXCOORD1
            // #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float4 texCoord0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float4 interp00 : TEXCOORD0; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyzw = input.texCoord0;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.texCoord0 = input.interp00.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    // output.positionRWS = input.positionRWS;
                    // output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    // output.texCoord1 = input.texCoord1;
                    // output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                // surfaceData.baseColor =             surfaceDescription.Albedo;
                // surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                // surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                // surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                // normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                // builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "META"
            Tags { "LightMode" = "META" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            
            Cull Off
        
            
            
            
            
            
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_LIGHT_TRANSPORT
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   SurfaceDescription.Albedo
                //   SurfaceDescription.Normal
                //   SurfaceDescription.Metallic
                //   SurfaceDescription.Emission
                //   SurfaceDescription.Smoothness
                //   SurfaceDescription.Occlusion
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.uv0
                //   AttributesMesh.uv1
                //   AttributesMesh.color
                //   AttributesMesh.uv2
                //   FragInputs.texCoord0
                //   VaryingsMeshToPS.texCoord0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            // #define VARYINGS_NEED_POSITION_WS
            // #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            // #define VARYINGS_NEED_TEXCOORD1
            // #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                float4 uv1 : TEXCOORD1; // optional
                float4 uv2 : TEXCOORD2; // optional
                float4 color : COLOR; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float4 texCoord0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float4 interp00 : TEXCOORD0; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyzw = input.texCoord0;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.texCoord0 = input.interp00.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceTangent; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float3 Albedo;
                        float3 Normal;
                        float Metallic;
                        float3 Emission;
                        float Smoothness;
                        float Occlusion;
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float3 _CustomFunction_313FDD5E_albedo_1;
                        float _CustomFunction_313FDD5E_alpha_2;
                        float3 _CustomFunction_313FDD5E_normal_3;
                        float _CustomFunction_313FDD5E_metallic_4;
                        float _CustomFunction_313FDD5E_smoothness_5;
                        float _CustomFunction_313FDD5E_occlusion_6;
                        SHG_TerrainToMeshCalculateLayersBlend_float(_UV_AAEF4E45_Out_0, _CustomFunction_313FDD5E_albedo_1, _CustomFunction_313FDD5E_alpha_2, _CustomFunction_313FDD5E_normal_3, _CustomFunction_313FDD5E_metallic_4, _CustomFunction_313FDD5E_smoothness_5, _CustomFunction_313FDD5E_occlusion_6);
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Albedo = _CustomFunction_313FDD5E_albedo_1;
                        surface.Normal = _CustomFunction_313FDD5E_normal_3;
                        surface.Metallic = _CustomFunction_313FDD5E_metallic_4;
                        surface.Emission = IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0));
                        surface.Smoothness = _CustomFunction_313FDD5E_smoothness_5;
                        surface.Occlusion = _CustomFunction_313FDD5E_occlusion_6;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                // output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                // output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                // input.positionOS = vertexDescription.VertexPosition;
                // input.normalOS = vertexDescription.VertexNormal;
                // input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    // output.positionRWS = input.positionRWS;
                    // output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    // output.texCoord1 = input.texCoord1;
                    // output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                surfaceData.baseColor =             surfaceDescription.Albedo;
                surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassLightTransport.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "SceneSelectionPass"
            Tags { "LightMode" = "SceneSelectionPass" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            
            
            
            ZWrite On
        
            
            
            ColorMask 0
        
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_DEPTH_ONLY
                #define SCENESELECTIONPASS
                #pragma editor_sync_compilation
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   FragInputs.texCoord0
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.texCoord0
                //   AttributesMesh.uv0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            // #define ATTRIBUTES_NEED_TEXCOORD1
            // #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            // #define ATTRIBUTES_NEED_COLOR
            // #define VARYINGS_NEED_POSITION_WS
            // #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            // #define VARYINGS_NEED_TEXCOORD1
            // #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float4 texCoord0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float4 interp00 : TEXCOORD0; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyzw = input.texCoord0;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.texCoord0 = input.interp00.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    // output.positionRWS = input.positionRWS;
                    // output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    // output.texCoord1 = input.texCoord1;
                    // output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                // surfaceData.baseColor =             surfaceDescription.Albedo;
                // surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                // surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                // surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                // normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                // builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "DepthOnly"
            Tags { "LightMode" = "DepthOnly" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            
            
            
            ZWrite On
        
            
            // Stencil setup
        Stencil
        {
           WriteMask 8
           Ref  8
           Comp Always
           Pass Replace
        }
        
            
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_DEPTH_ONLY
                #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
                #pragma multi_compile _ WRITE_NORMAL_BUFFER
                #pragma multi_compile _ WRITE_MSAA_DEPTH
                #define RAYTRACING_SHADER_GRAPH_HIGH
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Normal
                //   SurfaceDescription.Smoothness
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.uv0
                //   AttributesMesh.uv1
                //   AttributesMesh.color
                //   AttributesMesh.uv2
                //   AttributesMesh.uv3
                //   FragInputs.tangentToWorld
                //   FragInputs.positionRWS
                //   FragInputs.texCoord0
                //   FragInputs.texCoord1
                //   FragInputs.texCoord2
                //   FragInputs.texCoord3
                //   FragInputs.color
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.tangentWS
                //   VaryingsMeshToPS.normalWS
                //   VaryingsMeshToPS.positionRWS
                //   VaryingsMeshToPS.texCoord0
                //   VaryingsMeshToPS.texCoord1
                //   VaryingsMeshToPS.texCoord2
                //   VaryingsMeshToPS.texCoord3
                //   VaryingsMeshToPS.color
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                float4 uv1 : TEXCOORD1; // optional
                float4 uv2 : TEXCOORD2; // optional
                float4 uv3 : TEXCOORD3; // optional
                float4 color : COLOR; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float3 positionRWS; // optional
                float3 normalWS; // optional
                float4 tangentWS; // optional
                float4 texCoord0; // optional
                float4 texCoord1; // optional
                float4 texCoord2; // optional
                float4 texCoord3; // optional
                float4 color; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
                float4 interp02 : TEXCOORD2; // auto-packed
                float4 interp03 : TEXCOORD3; // auto-packed
                float4 interp04 : TEXCOORD4; // auto-packed
                float4 interp05 : TEXCOORD5; // auto-packed
                float4 interp06 : TEXCOORD6; // auto-packed
                float4 interp07 : TEXCOORD7; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyzw = input.tangentWS;
                output.interp03.xyzw = input.texCoord0;
                output.interp04.xyzw = input.texCoord1;
                output.interp05.xyzw = input.texCoord2;
                output.interp06.xyzw = input.texCoord3;
                output.interp07.xyzw = input.color;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.tangentWS = input.interp02.xyzw;
                output.texCoord0 = input.interp03.xyzw;
                output.texCoord1 = input.interp04.xyzw;
                output.texCoord2 = input.interp05.xyzw;
                output.texCoord3 = input.interp06.xyzw;
                output.color = input.interp07.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float3 Normal;
                        float Smoothness;
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float3 _CustomFunction_313FDD5E_albedo_1;
                        float _CustomFunction_313FDD5E_alpha_2;
                        float3 _CustomFunction_313FDD5E_normal_3;
                        float _CustomFunction_313FDD5E_metallic_4;
                        float _CustomFunction_313FDD5E_smoothness_5;
                        float _CustomFunction_313FDD5E_occlusion_6;
                        SHG_TerrainToMeshCalculateLayersBlend_float(_UV_AAEF4E45_Out_0, _CustomFunction_313FDD5E_albedo_1, _CustomFunction_313FDD5E_alpha_2, _CustomFunction_313FDD5E_normal_3, _CustomFunction_313FDD5E_metallic_4, _CustomFunction_313FDD5E_smoothness_5, _CustomFunction_313FDD5E_occlusion_6);
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Normal = _CustomFunction_313FDD5E_normal_3;
                        surface.Smoothness = _CustomFunction_313FDD5E_smoothness_5;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    output.positionRWS = input.positionRWS;
                    output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    output.texCoord1 = input.texCoord1;
                    output.texCoord2 = input.texCoord2;
                    output.texCoord3 = input.texCoord3;
                    output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                // surfaceData.baseColor =             surfaceDescription.Albedo;
                surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                // surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                // surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                // builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "GBuffer"
            Tags { "LightMode" = "GBuffer" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            
            
            ZTest Equal
        
            
            
            // Stencil setup
        Stencil
        {
           WriteMask 14
           Ref  10
           Comp Always
           Pass Replace
        }
        
            
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_GBUFFER
                #pragma multi_compile _ DEBUG_DISPLAY
                #pragma multi_compile _ LIGHTMAP_ON
                #pragma multi_compile _ DIRLIGHTMAP_COMBINED
                #pragma multi_compile _ DYNAMICLIGHTMAP_ON
                #pragma multi_compile _ SHADOWS_SHADOWMASK
                #pragma multi_compile DECALS_OFF DECALS_3RT DECALS_4RT
                #pragma multi_compile _ LIGHT_LAYERS
                #ifndef DEBUG_DISPLAY
    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
    #endif
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Albedo
                //   SurfaceDescription.Normal
                //   SurfaceDescription.Metallic
                //   SurfaceDescription.Emission
                //   SurfaceDescription.Smoothness
                //   SurfaceDescription.Occlusion
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   FragInputs.tangentToWorld
                //   FragInputs.positionRWS
                //   FragInputs.texCoord1
                //   FragInputs.texCoord2
                //   FragInputs.texCoord0
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.tangentWS
                //   VaryingsMeshToPS.normalWS
                //   VaryingsMeshToPS.positionRWS
                //   VaryingsMeshToPS.texCoord1
                //   VaryingsMeshToPS.texCoord2
                //   VaryingsMeshToPS.texCoord0
                //   AttributesMesh.uv1
                //   AttributesMesh.uv2
                //   AttributesMesh.uv0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            // #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                float4 uv1 : TEXCOORD1; // optional
                float4 uv2 : TEXCOORD2; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float3 positionRWS; // optional
                float3 normalWS; // optional
                float4 tangentWS; // optional
                float4 texCoord0; // optional
                float4 texCoord1; // optional
                float4 texCoord2; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
                float4 interp02 : TEXCOORD2; // auto-packed
                float4 interp03 : TEXCOORD3; // auto-packed
                float4 interp04 : TEXCOORD4; // auto-packed
                float4 interp05 : TEXCOORD5; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyzw = input.tangentWS;
                output.interp03.xyzw = input.texCoord0;
                output.interp04.xyzw = input.texCoord1;
                output.interp05.xyzw = input.texCoord2;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.tangentWS = input.interp02.xyzw;
                output.texCoord0 = input.interp03.xyzw;
                output.texCoord1 = input.interp04.xyzw;
                output.texCoord2 = input.interp05.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float3 Albedo;
                        float3 Normal;
                        float Metallic;
                        float3 Emission;
                        float Smoothness;
                        float Occlusion;
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float3 _CustomFunction_313FDD5E_albedo_1;
                        float _CustomFunction_313FDD5E_alpha_2;
                        float3 _CustomFunction_313FDD5E_normal_3;
                        float _CustomFunction_313FDD5E_metallic_4;
                        float _CustomFunction_313FDD5E_smoothness_5;
                        float _CustomFunction_313FDD5E_occlusion_6;
                        SHG_TerrainToMeshCalculateLayersBlend_float(_UV_AAEF4E45_Out_0, _CustomFunction_313FDD5E_albedo_1, _CustomFunction_313FDD5E_alpha_2, _CustomFunction_313FDD5E_normal_3, _CustomFunction_313FDD5E_metallic_4, _CustomFunction_313FDD5E_smoothness_5, _CustomFunction_313FDD5E_occlusion_6);
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Albedo = _CustomFunction_313FDD5E_albedo_1;
                        surface.Normal = _CustomFunction_313FDD5E_normal_3;
                        surface.Metallic = _CustomFunction_313FDD5E_metallic_4;
                        surface.Emission = IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0));
                        surface.Smoothness = _CustomFunction_313FDD5E_smoothness_5;
                        surface.Occlusion = _CustomFunction_313FDD5E_occlusion_6;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    output.positionRWS = input.positionRWS;
                    output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    output.texCoord1 = input.texCoord1;
                    output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                surfaceData.baseColor =             surfaceDescription.Albedo;
                surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassGBuffer.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "MotionVectors"
            Tags { "LightMode" = "MotionVectors" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            
            
            
            
            
            // Stencil setup
        Stencil
        {
           WriteMask 40
           Ref  40
           Comp Always
           Pass Replace
        }
        
            
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_MOTION_VECTORS
                #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
                #pragma multi_compile _ WRITE_NORMAL_BUFFER
                #pragma multi_compile _ WRITE_MSAA_DEPTH
                #define RAYTRACING_SHADER_GRAPH_HIGH
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Normal
                //   SurfaceDescription.Smoothness
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   FragInputs.positionRWS
                //   FragInputs.texCoord0
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.positionRWS
                //   VaryingsMeshToPS.texCoord0
                //   AttributesMesh.uv0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            // #define ATTRIBUTES_NEED_TEXCOORD1
            // #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            // #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            // #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            // #define VARYINGS_NEED_TEXCOORD1
            // #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float3 positionRWS; // optional
                float4 texCoord0; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float4 interp01 : TEXCOORD1; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyzw = input.texCoord0;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.positionRWS = input.interp00.xyz;
                output.texCoord0 = input.interp01.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float3 Normal;
                        float Smoothness;
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float3 _CustomFunction_313FDD5E_albedo_1;
                        float _CustomFunction_313FDD5E_alpha_2;
                        float3 _CustomFunction_313FDD5E_normal_3;
                        float _CustomFunction_313FDD5E_metallic_4;
                        float _CustomFunction_313FDD5E_smoothness_5;
                        float _CustomFunction_313FDD5E_occlusion_6;
                        SHG_TerrainToMeshCalculateLayersBlend_float(_UV_AAEF4E45_Out_0, _CustomFunction_313FDD5E_albedo_1, _CustomFunction_313FDD5E_alpha_2, _CustomFunction_313FDD5E_normal_3, _CustomFunction_313FDD5E_metallic_4, _CustomFunction_313FDD5E_smoothness_5, _CustomFunction_313FDD5E_occlusion_6);
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Normal = _CustomFunction_313FDD5E_normal_3;
                        surface.Smoothness = _CustomFunction_313FDD5E_smoothness_5;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    output.positionRWS = input.positionRWS;
                    // output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    // output.texCoord1 = input.texCoord1;
                    // output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                // surfaceData.baseColor =             surfaceDescription.Albedo;
                surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                // surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                // surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                // builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassMotionVectors.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
        Pass
        {
            // based on HDPBRPass.template
            Name "Forward"
            Tags { "LightMode" = "Forward" }
        
            //-------------------------------------------------------------------------------------
            // Render Modes (Blend, Cull, ZTest, Stencil, etc)
            //-------------------------------------------------------------------------------------
            Blend One Zero, One Zero
        
            
            ZTest Equal
        
            
            
            // Stencil setup
        Stencil
        {
           WriteMask 6
           Ref  0
           Comp Always
           Pass Replace
        }
        
            
            //-------------------------------------------------------------------------------------
            // End Render Modes
            //-------------------------------------------------------------------------------------
        
            HLSLPROGRAM
        
            #pragma target 4.5
            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
            //#pragma enable_d3d11_debug_symbols
        
            #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        
            #pragma multi_compile _ LOD_FADE_CROSSFADE
        
            //-------------------------------------------------------------------------------------
            // Graph Defines
            //-------------------------------------------------------------------------------------
                    // Shared Graph Keywords
                #define SHADERPASS SHADERPASS_FORWARD
                #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
                #pragma multi_compile _ DEBUG_DISPLAY
                #pragma multi_compile _ LIGHTMAP_ON
                #pragma multi_compile _ DIRLIGHTMAP_COMBINED
                #pragma multi_compile _ DYNAMICLIGHTMAP_ON
                #pragma multi_compile _ SHADOWS_SHADOWMASK
                #pragma multi_compile DECALS_OFF DECALS_3RT DECALS_4RT
                #pragma multi_compile USE_FPTL_LIGHTLIST USE_CLUSTERED_LIGHTLIST
                #pragma multi_compile SHADOW_LOW SHADOW_MEDIUM SHADOW_HIGH
                #ifndef DEBUG_DISPLAY
    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
    #endif
                // ACTIVE FIELDS:
                //   AlphaTest
                //   features.NormalDropOffTS
                //   SurfaceDescriptionInputs.uv0
                //   VertexDescriptionInputs.ObjectSpaceNormal
                //   VertexDescriptionInputs.ObjectSpaceTangent
                //   VertexDescriptionInputs.ObjectSpacePosition
                //   SurfaceDescription.Albedo
                //   SurfaceDescription.Normal
                //   SurfaceDescription.Metallic
                //   SurfaceDescription.Emission
                //   SurfaceDescription.Smoothness
                //   SurfaceDescription.Occlusion
                //   SurfaceDescription.Alpha
                //   SurfaceDescription.AlphaClipThreshold
                //   features.modifyMesh
                //   VertexDescription.VertexPosition
                //   VertexDescription.VertexNormal
                //   VertexDescription.VertexTangent
                //   FragInputs.tangentToWorld
                //   FragInputs.positionRWS
                //   FragInputs.texCoord1
                //   FragInputs.texCoord2
                //   FragInputs.texCoord0
                //   AttributesMesh.normalOS
                //   AttributesMesh.tangentOS
                //   AttributesMesh.positionOS
                //   VaryingsMeshToPS.tangentWS
                //   VaryingsMeshToPS.normalWS
                //   VaryingsMeshToPS.positionRWS
                //   VaryingsMeshToPS.texCoord1
                //   VaryingsMeshToPS.texCoord2
                //   VaryingsMeshToPS.texCoord0
                //   AttributesMesh.uv1
                //   AttributesMesh.uv2
                //   AttributesMesh.uv0
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Variant Definitions (active field translations to HDRP defines)
            //-------------------------------------------------------------------------------------
        
            // #define _MATERIAL_FEATURE_SPECULAR_COLOR 1
            // #define _SURFACE_TYPE_TRANSPARENT 1
            // #define _BLENDMODE_ALPHA 1
            // #define _BLENDMODE_ADD 1
            // #define _BLENDMODE_PRE_MULTIPLY 1
            // #define _DOUBLESIDED_ON 1
            #define _NORMAL_DROPOFF_TS	1
            // #define _NORMAL_DROPOFF_OS	1
            // #define _NORMAL_DROPOFF_WS	1
        
            //-------------------------------------------------------------------------------------
            // End Variant Definitions
            //-------------------------------------------------------------------------------------
        
            #pragma vertex Vert
            #pragma fragment Frag
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            // define FragInputs structure
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            //-------------------------------------------------------------------------------------
            // Active Field Defines
            //-------------------------------------------------------------------------------------
        
            // this translates the new dependency tracker into the old preprocessor definitions for the existing HDRP shader code
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            // #define ATTRIBUTES_NEED_TEXCOORD3
            // #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            // #define VARYINGS_NEED_TEXCOORD3
            // #define VARYINGS_NEED_COLOR
            // #define VARYINGS_NEED_CULLFACE
            #define HAVE_MESH_MODIFICATION
        
            //-------------------------------------------------------------------------------------
            // End Defines
            //-------------------------------------------------------------------------------------
        	
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #ifdef DEBUG_DISPLAY
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
            #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
        #if (SHADERPASS == SHADERPASS_FORWARD)
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
            #define HAS_LIGHTLOOP
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        #endif
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Decal/DecalUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitDecalData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            //Used by SceneSelectionPass
            int _ObjectId;
            int _PassValue;
        
            //-------------------------------------------------------------------------------------
            // Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
            // Generated Type: AttributesMesh
            struct AttributesMesh
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0; // optional
                float4 uv1 : TEXCOORD1; // optional
                float4 uv2 : TEXCOORD2; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            // Generated Type: VaryingsMeshToPS
            struct VaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION;
                float3 positionRWS; // optional
                float3 normalWS; // optional
                float4 tangentWS; // optional
                float4 texCoord0; // optional
                float4 texCoord1; // optional
                float4 texCoord2; // optional
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif // defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            };
            
            // Generated Type: PackedVaryingsMeshToPS
            struct PackedVaryingsMeshToPS
            {
                float4 positionCS : SV_POSITION; // unpacked
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
                float4 interp02 : TEXCOORD2; // auto-packed
                float4 interp03 : TEXCOORD3; // auto-packed
                float4 interp04 : TEXCOORD4; // auto-packed
                float4 interp05 : TEXCOORD5; // auto-packed
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC; // unpacked
                #endif // conditional
            };
            
            // Packed Type: VaryingsMeshToPS
            PackedVaryingsMeshToPS PackVaryingsMeshToPS(VaryingsMeshToPS input)
            {
                PackedVaryingsMeshToPS output = (PackedVaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyzw = input.tangentWS;
                output.interp03.xyzw = input.texCoord0;
                output.interp04.xyzw = input.texCoord1;
                output.interp05.xyzw = input.texCoord2;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToPS
            VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsMeshToPS input)
            {
                VaryingsMeshToPS output = (VaryingsMeshToPS)0;
                output.positionCS = input.positionCS;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.tangentWS = input.interp02.xyzw;
                output.texCoord0 = input.interp03.xyzw;
                output.texCoord1 = input.interp04.xyzw;
                output.texCoord2 = input.interp05.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif // conditional
                return output;
            }
            // Generated Type: VaryingsMeshToDS
            struct VaryingsMeshToDS
            {
                float3 positionRWS;
                float3 normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif // UNITY_ANY_INSTANCING_ENABLED
            };
            
            // Generated Type: PackedVaryingsMeshToDS
            struct PackedVaryingsMeshToDS
            {
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID; // unpacked
                #endif // conditional
                float3 interp00 : TEXCOORD0; // auto-packed
                float3 interp01 : TEXCOORD1; // auto-packed
            };
            
            // Packed Type: VaryingsMeshToDS
            PackedVaryingsMeshToDS PackVaryingsMeshToDS(VaryingsMeshToDS input)
            {
                PackedVaryingsMeshToDS output = (PackedVaryingsMeshToDS)0;
                output.interp00.xyz = input.positionRWS;
                output.interp01.xyz = input.normalWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            
            // Unpacked Type: VaryingsMeshToDS
            VaryingsMeshToDS UnpackVaryingsMeshToDS(PackedVaryingsMeshToDS input)
            {
                VaryingsMeshToDS output = (VaryingsMeshToDS)0;
                output.positionRWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif // conditional
                return output;
            }
            //-------------------------------------------------------------------------------------
            // End Interpolator Packing And Struct Declarations
            //-------------------------------------------------------------------------------------
        
            //-------------------------------------------------------------------------------------
            // Graph generated code
            //-------------------------------------------------------------------------------------
                    // Shared Graph Properties (uniform inputs)
                    CBUFFER_START(UnityPerMaterial)
                    CBUFFER_END
                
                // Vertex Graph Inputs
                    struct VertexDescriptionInputs
                    {
                        float3 ObjectSpaceNormal; // optional
                        float3 ObjectSpaceTangent; // optional
                        float3 ObjectSpacePosition; // optional
                    };
                // Vertex Graph Outputs
                    struct VertexDescription
                    {
                        float3 VertexPosition;
                        float3 VertexNormal;
                        float3 VertexTangent;
                    };
                    
                // Pixel Graph Inputs
                    struct SurfaceDescriptionInputs
                    {
                        float4 uv0; // optional
                    };
                // Pixel Graph Outputs
                    struct SurfaceDescription
                    {
                        float3 Albedo;
                        float3 Normal;
                        float Metallic;
                        float3 Emission;
                        float Smoothness;
                        float Occlusion;
                        float Alpha;
                        float AlphaClipThreshold;
                    };
                    
                // Shared Graph Node Functions
                
                    // fb4a9bd3282a461a42e374d4d2988410


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON


//Terrain To Mesh Keywords//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION


#include "Splatmap.cginc"
                
                // Vertex Graph Evaluation
                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _CustomFunction_B573E741_vertex_0;
                        float3 _CustomFunction_B573E741_normal_4;
                        SHG_TerrainToMeshCurvedWorld_float(IN.ObjectSpacePosition, IN.ObjectSpaceNormal, (float4(IN.ObjectSpaceTangent, 1.0)), _CustomFunction_B573E741_vertex_0, _CustomFunction_B573E741_normal_4);
                        description.VertexPosition = _CustomFunction_B573E741_vertex_0;
                        description.VertexNormal = _CustomFunction_B573E741_normal_4;
                        description.VertexTangent = IN.ObjectSpaceTangent;
                        return description;
                    }
                    
                // Pixel Graph Evaluation
                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 _UV_AAEF4E45_Out_0 = IN.uv0;
                        float3 _CustomFunction_313FDD5E_albedo_1;
                        float _CustomFunction_313FDD5E_alpha_2;
                        float3 _CustomFunction_313FDD5E_normal_3;
                        float _CustomFunction_313FDD5E_metallic_4;
                        float _CustomFunction_313FDD5E_smoothness_5;
                        float _CustomFunction_313FDD5E_occlusion_6;
                        SHG_TerrainToMeshCalculateLayersBlend_float(_UV_AAEF4E45_Out_0, _CustomFunction_313FDD5E_albedo_1, _CustomFunction_313FDD5E_alpha_2, _CustomFunction_313FDD5E_normal_3, _CustomFunction_313FDD5E_metallic_4, _CustomFunction_313FDD5E_smoothness_5, _CustomFunction_313FDD5E_occlusion_6);
                        float _CustomFunction_4208B4A3_clipValue_6;
                        SHG_TerrainToMeshCalculateClipValue_float(_UV_AAEF4E45_Out_0, _CustomFunction_4208B4A3_clipValue_6);
                        float _Vector1_19F10FD1_Out_0 = 0.5;
                        surface.Albedo = _CustomFunction_313FDD5E_albedo_1;
                        surface.Normal = _CustomFunction_313FDD5E_normal_3;
                        surface.Metallic = _CustomFunction_313FDD5E_metallic_4;
                        surface.Emission = IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0));
                        surface.Smoothness = _CustomFunction_313FDD5E_smoothness_5;
                        surface.Occlusion = _CustomFunction_313FDD5E_occlusion_6;
                        surface.Alpha = _CustomFunction_4208B4A3_clipValue_6;
                        surface.AlphaClipThreshold = _Vector1_19F10FD1_Out_0;
                        return surface;
                    }
                    
            //-------------------------------------------------------------------------------------
            // End graph generated code
            //-------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
            
            VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                // output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                // output.ViewSpaceNormal =             TransformWorldToViewDir(output.WorldSpaceNormal);
                // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                output.ObjectSpaceTangent =          input.tangentOS;
                // output.WorldSpaceTangent =           TransformObjectToWorldDir(input.tangentOS.xyz);
                // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                // output.ObjectSpaceBiTangent =        normalize(cross(input.normalOS, input.tangentOS) * (input.tangentOS.w > 0.0f ? 1.0f : -1.0f) * GetOddNegativeScale());
                // output.WorldSpaceBiTangent =         TransformObjectToWorldDir(output.ObjectSpaceBiTangent);
                // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                output.ObjectSpacePosition =         input.positionOS;
                // output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                // output.ViewSpacePosition =           TransformWorldToView(output.WorldSpacePosition);
                // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(TransformObjectToWorld(input.positionOS));
                // output.WorldSpaceViewDirection =     GetWorldSpaceNormalizeViewDir(output.WorldSpacePosition);
                // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(output.WorldSpacePosition), _ProjectionParams.x);
                // output.uv0 =                         input.uv0;
                // output.uv1 =                         input.uv1;
                // output.uv2 =                         input.uv2;
                // output.uv3 =                         input.uv3;
                // output.VertexColor =                 input.color;
                // output.BoneWeights =                 input.weights;
                // output.BoneIndices =                 input.indices;
            
                return output;
            }
            
            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
            {
                // build graph inputs
                VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
                // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)
                // vertexDescriptionInputs.TimeParameters = timeParameters;
            
                // evaluate vertex graph
                VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
            
                // copy graph output to the results
                input.positionOS = vertexDescription.VertexPosition;
                input.normalOS = vertexDescription.VertexNormal;
                input.tangentOS.xyz = vertexDescription.VertexTangent;
            
                return input;
            }
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : VertexAnimation.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        //-------------------------------------------------------------------------------------
            // TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
                FragInputs BuildFragInputs(VaryingsMeshToPS input)
                {
                    FragInputs output;
                    ZERO_INITIALIZE(FragInputs, output);
            
                    // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
                    // TODO: this is a really poor workaround, but the variable is used in a bunch of places
                    // to compute normals which are then passed on elsewhere to compute other values...
                    output.tangentToWorld = k_identity3x3;
                    output.positionSS = input.positionCS;       // input.positionCS is SV_Position
            
                    output.positionRWS = input.positionRWS;
                    output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
                    output.texCoord0 = input.texCoord0;
                    output.texCoord1 = input.texCoord1;
                    output.texCoord2 = input.texCoord2;
                    // output.texCoord3 = input.texCoord3;
                    // output.color = input.color;
                    #if _DOUBLESIDED_ON && SHADER_STAGE_FRAGMENT
                    output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #elif SHADER_STAGE_FRAGMENT
                    // output.isFrontFace = IS_FRONT_VFACE(input.cullFace, true, false);
                    #endif // SHADER_STAGE_FRAGMENT
            
                    return output;
                }
            #endif
                SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
                    // output.WorldSpaceNormal =            input.tangentToWorld[2].xyz;	// normal was already normalized in BuildTangentToWorld()
                    // output.ObjectSpaceNormal =           normalize(mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_M));           // transposed multiplication by inverse matrix to handle normal scale
                    // output.ViewSpaceNormal =             mul(output.WorldSpaceNormal, (float3x3) UNITY_MATRIX_I_V);         // transposed multiplication by inverse matrix to handle normal scale
                    // output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
                    // output.WorldSpaceTangent =           input.tangentToWorld[0].xyz;
                    // output.ObjectSpaceTangent =          TransformWorldToObjectDir(output.WorldSpaceTangent);
                    // output.ViewSpaceTangent =            TransformWorldToViewDir(output.WorldSpaceTangent);
                    // output.TangentSpaceTangent =         float3(1.0f, 0.0f, 0.0f);
                    // output.WorldSpaceBiTangent =         input.tangentToWorld[1].xyz;
                    // output.ObjectSpaceBiTangent =        TransformWorldToObjectDir(output.WorldSpaceBiTangent);
                    // output.ViewSpaceBiTangent =          TransformWorldToViewDir(output.WorldSpaceBiTangent);
                    // output.TangentSpaceBiTangent =       float3(0.0f, 1.0f, 0.0f);
                    // output.WorldSpaceViewDirection =     normalize(viewWS);
                    // output.ObjectSpaceViewDirection =    TransformWorldToObjectDir(output.WorldSpaceViewDirection);
                    // output.ViewSpaceViewDirection =      TransformWorldToViewDir(output.WorldSpaceViewDirection);
                    // float3x3 tangentSpaceTransform =     float3x3(output.WorldSpaceTangent,output.WorldSpaceBiTangent,output.WorldSpaceNormal);
                    // output.TangentSpaceViewDirection =   mul(tangentSpaceTransform, output.WorldSpaceViewDirection);
                    // output.WorldSpacePosition =          input.positionRWS;
                    // output.ObjectSpacePosition =         TransformWorldToObject(input.positionRWS);
                    // output.ViewSpacePosition =           TransformWorldToView(input.positionRWS);
                    // output.TangentSpacePosition =        float3(0.0f, 0.0f, 0.0f);
                    // output.AbsoluteWorldSpacePosition =  GetAbsolutePositionWS(input.positionRWS);
                    // output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionRWS), _ProjectionParams.x);
                    output.uv0 =                         input.texCoord0;
                    // output.uv1 =                         input.texCoord1;
                    // output.uv2 =                         input.texCoord2;
                    // output.uv3 =                         input.texCoord3;
                    // output.VertexColor =                 input.color;
                    // output.FaceSign =                    input.isFrontFace;
                    // output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            
                    return output;
                }
            
            #if !defined(SHADER_STAGE_RAY_TRACING)
            
                // existing HDRP code uses the combined function to go directly from packed to frag inputs
                FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
                {
                    UNITY_SETUP_INSTANCE_ID(input);
                    VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
                    return BuildFragInputs(unpacked);
                }
            #endif
            
            //-------------------------------------------------------------------------------------
            // END TEMPLATE INCLUDE : SharedCode.template.hlsl
            //-------------------------------------------------------------------------------------
            
        
        
            void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
            {
                // setup defaults -- these are used if the graph doesn't output a value
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion = 1.0;
                surfaceData.specularOcclusion = 1.0; // This need to be init here to quiet the compiler in case of decal, but can be override later.
        
                // copy across graph values, if defined
                surfaceData.baseColor =             surfaceDescription.Albedo;
                surfaceData.perceptualSmoothness =  surfaceDescription.Smoothness;
                surfaceData.ambientOcclusion =      surfaceDescription.Occlusion;
                surfaceData.metallic =              surfaceDescription.Metallic;
                // surfaceData.specularColor =         surfaceDescription.Specular;
        
                // These static material feature allow compile time optimization
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                // normal delivered to master node
                float3 normalSrc = float3(0.0f, 0.0f, 1.0f);
                normalSrc = surfaceDescription.Normal;
        
                // compute world space normal
        #if _NORMAL_DROPOFF_TS
                GetNormalWS(fragInputs, normalSrc, surfaceData.normalWS, doubleSidedConstants);
        #elif _NORMAL_DROPOFF_OS
        		surfaceData.normalWS = TransformObjectToWorldNormal(normalSrc);
        #elif _NORMAL_DROPOFF_WS
        		surfaceData.normalWS = normalSrc;
        #endif
        
                surfaceData.geomNormalWS = fragInputs.tangentToWorld[2];
                surfaceData.tangentWS = normalize(fragInputs.tangentToWorld[0].xyz);    // The tangent is not normalize in tangentToWorld for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
        
        #if HAVE_DECALS
                if (_EnableDecals)
                {
                    // Both uses and modifies 'surfaceData.normalWS'.
                    DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, surfaceDescription.Alpha);
                    ApplyDecalToSurfaceData(decalSurfaceData, surfaceData);
                }
        #endif
        
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
        
        #ifdef DEBUG_DISPLAY
                if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
                {
                    // TODO: need to update mip info
                    surfaceData.metallic = 0;
                }
        
                // We need to call ApplyDebugToSurfaceData after filling the surfarcedata and before filling builtinData
                // as it can modify attribute use for static lighting
                ApplyDebugToSurfaceData(fragInputs.tangentToWorld, surfaceData);
        #endif
        
                // By default we use the ambient occlusion with Tri-ace trick (apply outside) for specular occlusion as PBR master node don't have any option
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(ClampNdotV(dot(surfaceData.normalWS, V)), surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
            }
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
        #endif
        
                float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                // doubleSidedConstants = float3(-1.0, -1.0, -1.0);
                // doubleSidedConstants = float3( 1.0,  1.0, -1.0);
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants);
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
            //-------------------------------------------------------------------------------------
            // Pass Includes
            //-------------------------------------------------------------------------------------
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassForward.hlsl"
            //-------------------------------------------------------------------------------------
            // End Pass Includes
            //-------------------------------------------------------------------------------------
        
            ENDHLSL
        }
        
    }
    CustomEditor "AmazingAssets.TerrainToMesh.Editor.SplatmapShaderGUI"
FallBack "Hidden/Amazing Assets/Terrain To Mesh/Fallback/Splatmap (Holes)"
}

