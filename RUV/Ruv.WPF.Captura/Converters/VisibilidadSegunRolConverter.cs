using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(List<eRolesUsuario>), typeof(Visibility))]
    class VisibilidadSegunRolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<eRolesUsuario> lstRoles = (List<eRolesUsuario>)value;
            if (string.IsNullOrEmpty(parameter.ToString()) || lstRoles == null || lstRoles.Count == 0) return Visibility.Collapsed;

            eRolesUsuario rol;
            try
            {
                rol = (eRolesUsuario)Enum.Parse(typeof(eRolesUsuario), parameter.ToString());
            }
            catch
            {
                return Visibility.Collapsed;
            }

            if (lstRoles.Contains(rol)) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
