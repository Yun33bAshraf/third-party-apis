using System;

namespace IApply.Frontend.Common.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnWidthAttribute : Attribute
{
    public string Width { get; }
    
    public ColumnWidthAttribute(string width)
    {
        Width = width;
    }
}
