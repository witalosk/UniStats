using System.Text;
using UnityEngine;

namespace UniStats
{
    public class SystemInfoProvider : MonoBehaviour, ITextProvider
    {
        public string Text => _text;

        private string _text;
        private Vector2Int _currentScreenWindowResolution = new();
        private readonly StringBuilder _textBuilder = new();

        private void BuildText()
        {
            _textBuilder.Clear();
            _currentScreenWindowResolution.x = Screen.width;
            _currentScreenWindowResolution.y = Screen.height;
            var res = Screen.currentResolution;
            
            // ScreenResolution
            _textBuilder.AppendLine("Screen: "
                                    + res.width
                                    + "x"
                                    + res.height
                                    + "@"
                                    +  Mathf.RoundToInt((float)res.refreshRateRatio.value)
                                    + "Hz");
            
            // ScreenWindowResolution
            _textBuilder.AppendLine("Window: "
                                    + _currentScreenWindowResolution.x
                                    + "x"
                                    + _currentScreenWindowResolution.y
                                    + "@"
                                    + Mathf.RoundToInt((float)res.refreshRateRatio.value)
                                    + "Hz["
                                    + (int)Screen.dpi
                                    + "dpi]");
            
            // GraphicsDeviceVersion
            _textBuilder.AppendLine("Graphics API: "
                                    + SystemInfo.graphicsDeviceVersion);
            
            // GraphicsDeviceName
            _textBuilder.AppendLine("GPU: "
                                    + SystemInfo.graphicsDeviceName);
            
            // GraphicsMemorySize
            _textBuilder.AppendLine("VRAM: "
                                    + SystemInfo.graphicsMemorySize
                                    + "MB. Max texture size: "
                                    + SystemInfo.maxTextureSize
                                    + "px. Shader level: "
                                    + SystemInfo.graphicsShaderLevel);
            
            // ProcessorType
            _textBuilder.AppendLine("CPU: "
                                    + SystemInfo.processorType
                                    + " ["
                                    + SystemInfo.processorCount
                                    + " cores]");
            
            // SystemMemorySize
            _textBuilder.AppendLine("RAM: "
                                    + SystemInfo.systemMemorySize
                                    + " MB");
            
            // OperationSystem
            _textBuilder.Append("OS: "
                                    + SystemInfo.operatingSystem
                                    + " ["
                                    + SystemInfo.deviceType
                                    + "]");
            
            _text = _textBuilder.ToString();
        }
        
        public void Init()
        {
            BuildText();
        }

        private void Update()
        {
            if (_currentScreenWindowResolution.x != Screen.width || _currentScreenWindowResolution.y != Screen.height)
            {
                BuildText();
            }
        }

        private void OnDestroy()
        {
            _textBuilder.Clear();
        }
    }
}