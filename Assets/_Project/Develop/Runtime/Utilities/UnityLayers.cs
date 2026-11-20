using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities
{
    public static class UnityLayers
    {
        public static readonly int LayerDefault = 0;
        public static readonly int LayerMaskDefault = 1 << 0;
        public static readonly int LayerTransparentFX = 1;
        public static readonly int LayerMaskTransparentFX = 1 << 1;
        public static readonly int LayerIgnoreRaycast = 2;
        public static readonly int LayerMaskIgnoreRaycast = 1 << 2;
        public static readonly int LayerWater = 4;
        public static readonly int LayerMaskWater = 1 << 4;
        public static readonly int LayerUI = 5;
        public static readonly int LayerMaskUI = 1 << 5;
    }
}
