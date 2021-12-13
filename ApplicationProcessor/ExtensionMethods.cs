using System;
using System.Collections.Generic;       //CC: Remove this unused 'using'
using System.Text;                      //CC: Remove this unused 'using'
using System.ComponentModel;
using System.Reflection;

namespace ULaw.ApplicationProcessor
{
    static class ExtensionMethods
    {
        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            //CC: Remove this blank line
            {
            //CC: Remove this blank line
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DescriptionAttribute),
            //CC: Remove this blank line
                                              false);

                if (attrs != null && attrs.Length > 0) //CC: Could write as 'if (attrs?.Length > 0)'
            //CC: Add opening '{'
                    return ((DescriptionAttribute)attrs[0]).Description;
            //CC: Add closing '}'
            }
            //CC: There is no custom attribute so maybe throw an exception rather
            //    than go unnoticed with wrong string being returned?
            return en.ToString();
        }
    }

}
