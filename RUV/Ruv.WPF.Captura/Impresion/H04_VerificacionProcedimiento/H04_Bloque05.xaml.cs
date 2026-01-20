using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Lógica de interacción para H04_Bloque02.xaml
  /// </summary>
  public partial class H04_Bloque05 : UserControl, IEncabezadoImpresion
  {
      public H04_Bloque05()
      {
          InitializeComponent();

          if (RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.Firmas != null && RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.Firmas.Count > 0) {
              
              var firmaDeclarante = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.Firmas.FirstOrDefault(x => x.firmaOwner == FirmaOwner.DECLARANTE);
              if (firmaDeclarante != null && firmaDeclarante.firma != null)
                  imgFirmaDeclarante.Source = Signature(firmaDeclarante.firma);

              var firmaTutor = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.Firmas.FirstOrDefault(x => x.firmaOwner == FirmaOwner.TUTOR);
              if (firmaTutor != null && firmaTutor.firma != null)
                  imgFirmaTutor.Source = Signature(firmaTutor.firma);

          }

          imgFirmaFuncionario.Source = Signature(RUV.I.Usuario.ImagenFirmaDigital);
      }

    public bool RepiteEnCadaPagina
    {
      get { return false; }
    }

    public int Orden
    {
      get { return 5; }
    }

    private BitmapImage Signature(byte[] imgData)
    {
        BitmapImage biImg = new BitmapImage();
        try
        {
            MemoryStream ms = new MemoryStream(imgData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
        }
        catch (Exception)
        {
            return null;
        }

        return biImg;
    }
  }
}
