using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Rutinas para la edición de los bienes.
    /// </summary>
    public partial class A01 : UserControl, ISeccionRegistro
    {
        IEditableCollectionView _BienesVistaEditable;
        ICollectionView _BienesICV = null;
        /// <summary>
        /// Vista de las entidades con capacidad de edición.
        /// </summary>
        //public IEditableCollectionView BienesVistaEditable
        //{
        //  get
        //  {
        //    if (_BienesVistaEditable == null)
        //    {
        //      if (PersonaActual != null)
        //        _BienesICV = CollectionViewSource.GetDefaultView(PersonaActual.Bienes);
        //      else if (PersonaEdicion != null)
        //        _BienesICV = CollectionViewSource.GetDefaultView(PersonaEdicion.Bienes);
        //      _BienesICV.Filter = new Predicate<object>(Sipod.I.Util.FiltroEntidadNoEliminada);
        //      _BienesVistaEditable = (IEditableCollectionView)_BienesICV;
        //    }
        //    return _BienesVistaEditable;
        //  }
        //  set { _BienesVistaEditable = value; }
        //}

        /// <summary>
        /// Prepara la vista editable para una colección de entidades.
        /// </summary>
        void PrepararVistaBienes(clsAnexo01_Victima persona)
        {
            if (persona != null)
            {
                _BienesICV = CollectionViewSource.GetDefaultView(persona.Bienes);
                _BienesICV.Filter = new Predicate<object>(RUV.I.Util.FiltroEntidadNoEliminada);
                persona.BienesVistaEditable = (IEditableCollectionView)_BienesICV;
                _BienesVistaEditable = persona.BienesVistaEditable;
            }
        }

        /// <summary>
        /// Agregar una nueva entidad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgregarEntidad(object sender, RoutedEventArgs e)
        {
            var NuevoBien = _BienesVistaEditable.AddNew() as clsAnexo01_Victima_Bien;

            RUV.I.Util.EntidadEstablecerSiguienteId(
              _BienesICV.SourceCollection as IEnumerable<clsEntidadBase>,
              NuevoBien);

            NuevoBien.EstadoRegistro = eEstadoRegistro.Insertar;
        }

        /// <summary>
        /// Borrar una entidad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitarEntidad(object sender, RoutedEventArgs e)
        {
            DataGrid DG = sender as DataGrid;
            if (DG.SelectedItem != null)
            {
                var Elemento = DG.SelectedItem as clsAnexo01_Victima_Bien;
                OperacionBorrarEntidad(Elemento);
            }
        }

        /// <summary>
        /// Marca o borra una entidad.
        /// </summary>
        /// <param name="bien"></param>
        void OperacionBorrarEntidad(clsAnexo01_Victima_Bien bien)
        {
            if (bien.EstadoRegistro == eEstadoRegistro.Insertar)
            {
                _BienesVistaEditable.CancelNew();
                _BienesVistaEditable.Remove(bien);
            }
            else
            {
                _BienesVistaEditable.CancelEdit();
                bien.EstadoRegistro = eEstadoRegistro.Eliminado;
            }
            _BienesICV.Refresh();
        }

        /// <summary>
        /// Se lanza al postear un cambio en una fila de la grilla.
        /// Sucede cuando la fila editada o insertada pierde el foco.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrillaBienes_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Si la entidad no está correctamente ingresada, borrarla.
                var Entidad = e.Row.Item as clsAnexo01_Victima_Bien;

                List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
                int validacionesSaltadas = 0;
                if (!RUV.I.ValidadorEntidades.EntidadEsValida(Entidad, Requeridas, ref validacionesSaltadas))
                {
                    e.Cancel = true;
                    OperacionBorrarEntidad(Entidad);
                }
            }
        }
    }
}