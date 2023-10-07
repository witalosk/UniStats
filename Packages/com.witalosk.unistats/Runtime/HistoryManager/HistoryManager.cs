namespace UniStats
{
    public class FloatHistoryManager : HistoryManagerBase<float>
    {
        protected override float Min => float.MinValue;
        protected override float Max => float.MaxValue;
        
        public FloatHistoryManager(int maxValues) : base(maxValues) { }

        protected override float Add(float a, float b)
        {
            return a + b;
        }

        protected override float Divide(float a, short b)
        {
            return a / b;
        }
    }
}