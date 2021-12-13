using System;                       //CC: Remove this unused 'using'
using System.Collections.Generic;   //CC: Remove this unused 'using'
using System.Text;                  //CC: Remove this unused 'using'
using System.ComponentModel;
using System.Reflection;            //CC: Remove this unused 'using'

namespace ULaw.ApplicationProcessor.Enums
{
    //CC: Typically I would not append 'Enum' to the name of these enumerations in the same way that I would not append 'Class' to every class name
    //CC: The shorter '[Description]' can be used for the attributes
    //CC: I would use Pascal Case for the attribute names.
    //CC: The default type for an Enum is 'int' so there is no need to specify it
    public enum DegreeGradeEnum : int
    {
        [DescriptionAttribute("1st")]
        first,
        [DescriptionAttribute("2:1")]
        twoOne,
        [DescriptionAttribute("2:2")]
        twoTwo,
        [DescriptionAttribute("3rd")]
        third
    }
    
    public enum DegreeSubjectEnum : int
    {
        [DescriptionAttribute("Law")]
        law,
        [DescriptionAttribute("Law and Business")]
        lawAndBusiness,
        [DescriptionAttribute("Maths")]
        maths,
        [DescriptionAttribute("English")]
        English     //CC: This one starts with a capital letter where the others do not
    }

}
