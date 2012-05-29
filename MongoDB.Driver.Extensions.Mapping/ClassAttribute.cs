using System;

namespace MongoDB.Driver.Extensions.Mapping
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ClassAttribute : Attribute
    {
    }
}
