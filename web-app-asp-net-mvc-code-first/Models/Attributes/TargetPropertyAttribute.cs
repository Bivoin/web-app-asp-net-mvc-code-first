using System;

namespace web_app_asp_net_mvc_code_first.Models.Attributes
{
    public class TargetPropertyAttribute : Attribute
    {
        public TargetPropertyAttribute(string targetProperty)
        {
            TargetProperty = targetProperty;
        }

        public string TargetProperty { get; set; }
    }
}