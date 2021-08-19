#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 worldPos, out float3 direction, out float3 color) {
#ifdef SHADERGRAPH_PREVIEW
	direction = float3(0.5, 0.5, 0);
	color = 1;
#else
	Light mainLight = GetMainLight(0);
	direction = mainLight.direction;
	color = mainLight.color;
#endif
}
#endif