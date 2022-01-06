using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.DbImplementation;

namespace BNPL_Web.DataAccessLayer.Helpers
{
    public class Converter : TypeConverter
    {
        //public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //{
        //    return sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);
        //}

        //public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        //{
        //    if (value is string)
        //        return new ApplicationUser((string)value);

        //    return base.ConvertFrom(context, culture, value);
        //}
    }
}
