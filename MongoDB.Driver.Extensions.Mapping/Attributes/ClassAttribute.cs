using System;

namespace MongoDB.Driver.Extensions.Mapping.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ClassAttribute : Attribute
    {
    }
}
