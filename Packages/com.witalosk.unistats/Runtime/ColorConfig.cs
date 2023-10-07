using System;
using UnityEngine;

namespace UniStats
{
    [Serializable]
    public class ColorConfig
    {
        public float HighThreshold = 28f;
        public float LowThreshold = 26f;
        public Color HighColor = new (0f, 0.6078f, 0.5647f);
        public Color MiddleColor = new (0.9333f, 0.7485f, 0.3173f);
        public Color LowColor = new (0.8823f, 0.4259f, 0.3794f);
    }
}