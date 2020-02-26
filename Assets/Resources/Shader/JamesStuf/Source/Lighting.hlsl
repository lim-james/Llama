void MainLight_half(half3 WorldPosition, out half3 Direction, out half3 Color, out half DistanceAttenuation, out half ShadowAttenuation)
{
#if SHADERGRAPH_PREVIEW
	Direction = half3(0.5, 0.5, 0);
	Color = 1;
	DistanceAttenuation = 1;
	ShadowAttenuation = 1;
#else
#if SHADOWS_SCREEN
	half4 clipPos = TransformWorldToHClip(WorldPosition);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
#endif
	Light light = GetMainLight(shadowCoord);
	Direction = light.direction;
	Color = light.color;
	DistanceAttenuation = light.distanceAttenuation;
	ShadowAttenuation = light.shadowAttenuation;
#endif
}

void DirectSpecular_half(half3 Specular, half Smoothness, half3 Direction, half4 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
	Out = 0;
#else
	Smoothness = exp2(10 * Smoothness + 1);
	WorldNormal = normalize(WorldNormal);
	WorldView = SafeNormalize(WorldView);
	Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, half4(Specular, 0), Smoothness);
#endif
}

void AdditionalLights_half(half3 SpecularColor, half Smoothness, half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Diffuse, out half3 Specular)
{
	half3 diffuseColor = 0;
	half3 specularColor = 0;

#ifndef SHADERGRAPH_PREVIEW
	Smoothness = exp2(10 * Smoothness + 1);
	WorldNormal = normalize(WorldNormal);
	WorldView = SafeNormalize(WorldView);
	int pixelLightCount = GetAdditionalLightsCount();
	for (int i = 0; i < pixelLightCount; ++i)
	{
		Light light = GetAdditionalLight(i, WorldPosition);
		half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
		diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
		specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, half4(SpecularColor, 0), Smoothness);
	}
#endif

	Diffuse = diffuseColor;
	Specular = specularColor;
}