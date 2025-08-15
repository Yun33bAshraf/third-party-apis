namespace IApply.Frontend.Common.CustomAttributes
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IgnoreInTableAttribute : Attribute
    {
        public IgnoreInTableAttribute() { }
    }

}
