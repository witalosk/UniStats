#ifndef UNISTATS_HLSL_INCLUDE_COMMON
#define UNISTATS_HLSL_INCLUDE_COMMON

half4 BlendScreen(half4 base, half4 blend)
{
    return 1 - (1 - base) * (1 - blend);
}



#endif // UNISTATS_HLSL_INCLUDE_COMMON