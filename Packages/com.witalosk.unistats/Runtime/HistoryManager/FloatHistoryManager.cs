namespace UniStats
{
    public class FloatHistoryManager : IHistoryManager<float>
    {
        public float CurrentValue { get; private set; }
        public float AverageValue { get; private set; }
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }
        public float[] Values => _samples;
        
        private readonly float[] _samples;
        private readonly short _sampleNum;
        private short _currentSampleNum;
        private short _currentIndex;
        
        public FloatHistoryManager(int sampleNum = 512)
        {
            _sampleNum = (short)sampleNum;
            _samples = new float[sampleNum];
        }

        public void AddValue(float currentValue)
        {
            CurrentValue = currentValue;
            _samples[_currentIndex] = currentValue;
            
            _currentIndex++;
            if (_currentIndex >= _sampleNum) _currentIndex = 0;
            
            if (_currentSampleNum < _sampleNum) _currentSampleNum++;

            float sum = default;
            MinValue = float.MaxValue;
            MaxValue = float.MinValue;
            for (int i = 0; i < _currentSampleNum; i++)
            {
                sum += _samples[i];
                MinValue = _samples[i].CompareTo(MinValue) < 0 ? _samples[i] : MinValue;
                MaxValue = _samples[i].CompareTo(MaxValue) > 0 ? _samples[i] : MaxValue;
            }
            AverageValue = sum / _currentSampleNum;
        }
        
        public float[] GetLatestValues(int numValues)
        {
            float[] values = new float[numValues];
            for (int i = 0; i < numValues; i++)
            {
                values[i] = _samples[(_currentIndex - i - 1 + _sampleNum) % _sampleNum];
            }

            return values;
        }
        
        public float[] GetLatestValues(int numValues, out float maxValue)
        {
            float[] values = new float[numValues];
            maxValue = float.MinValue;
            for (int i = 0; i < numValues; i++)
            {
                values[i] = _samples[(_currentIndex - i - 1 + _sampleNum) % _sampleNum];
                maxValue = values[i] > maxValue ? values[i] : maxValue;
            }

            return values;
        }
    }
}