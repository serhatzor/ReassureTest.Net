namespace ReassureTest
{
    public class Projection
    {
        public readonly bool HasResult;
        public readonly object Value;

        public static readonly Projection Ignore = new Projection(false, null);

        public static Projection Use(object value) => new Projection(true, value);

        /// <summary> Use static methods to create instances </summary>
        private Projection(bool hasResult, object value)
        {
            HasResult = hasResult;
            Value = value;
        }
    }
}