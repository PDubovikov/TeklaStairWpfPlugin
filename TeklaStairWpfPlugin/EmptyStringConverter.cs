﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace TeklaStairWpfPlugin
{
    public class EmptyStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string) ? parameter : value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            return value; 
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
