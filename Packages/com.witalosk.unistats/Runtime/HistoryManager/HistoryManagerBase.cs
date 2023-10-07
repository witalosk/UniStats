using System;

namespace UniStats
{
    public abstract class HistoryManagerBase<T> : IHistoryManager<T>
        where T : IComparable<T>
    {
        public T CurrentValue { get; private set; }
        public T AverageValue { get; private set; }
        public T MinValue { get; private set; }
        public T MaxValue { get; private set; }
        public T[] Values => _samples;

        protected abstract T Min { get; }
        protected abstract T Max { get; }
        
        private readonly T[] _samples;
        private readonly short _sampleNum;
        private short _currentSampleNum;
        private short _currentIndex;
        
        public HistoryManagerBase(int sampleNum = 512)
        {
            _sampleNum = (short)sampleNum;
            _samples = new T[sampleNum];
        }

        public void AddValue(T currentValue)
        {
            CurrentValue = currentValue;
            _samples[_currentIndex] = currentValue;
            
            _currentIndex++;
            if (_currentIndex >= _sampleNum) _currentIndex = 0;
            
            if (_currentSampleNum < _sampleNum) _currentSampleNum++;

            T sum = default;
            MinValue = Max;
            MaxValue = Min;
            for (int i = 0; i < _currentSampleNum; i++)
            {
                sum = Add(sum, _samples[i]);
                MinValue = _samples[i].CompareTo(MinValue) < 0 ? _samples[i] : MinValue;
                MaxValue = _samples[i].CompareTo(MaxValue) > 0 ? _samples[i] : MaxValue;
            }
            AverageValue = Divide(sum, _currentSampleNum);
        }
        
        public T[] GetLatestValues(int numValues)
        {
            T[] values = new T[numValues];
            for (int i = 0; i < numValues; i++)
            {
                values[i] = _samples[(_currentIndex - i - 1 + _sampleNum) % _sampleNum];
            }

            return values;
        }

        protected abstract T Add(T a, T b);
        protected abstract T Divide(T a, short b);
    }
}