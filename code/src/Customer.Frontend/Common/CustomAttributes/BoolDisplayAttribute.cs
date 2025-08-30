namespace OpenProfileAPI.Frontend.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BoolDisplayAttribute : Attribute
    {
        public string TrueText { get; }
        public string FalseText { get; }

        public BoolDisplayAttribute(string trueText, string falseText)
        {
            TrueText = trueText;
            FalseText = falseText;
        }
    }
}
