using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Xml;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  /// <summary>
  /// Rutinas de serialización y de-serialización.
  /// </summary>
  public partial class clsDeclaracion : clsEntidadBase
  {
    //public System.Xml.Schema.XmlSchema GetSchema()
    //{
    //  return null;
    //}

    //public void ReadXml(System.Xml.XmlReader reader)
    //{
    //  XmlSerializer XmlDes = null;
    //  string txt = null;
    //  Anexos.Clear();

    //  XmlDocument Doc = new XmlDocument();
    //  Doc.Load(reader);
    //  //----------------------------------------------
    //  DeclaracionNumero = NodeReader(Doc, "DeclaracionNumero").ReadElementContentAsString();
    //  //----------------------------------------------
    //  txt = NodeReader(Doc, "ID").ReadElementContentAsString();
    //  if (string.IsNullOrWhiteSpace(txt))
    //    ID = null;
    //  else
    //    ID = Convert.ToInt32(txt);
    //  //----------------------------------------------
    //  EstadoRegistro = (eEstadoRegistro)Enum.Parse(typeof(eEstadoRegistro),
    //    NodeReader(Doc, "EstadoRegistro").ReadElementContentAsString());
    //  //----------------------------------------------
    //  XmlDes = new System.Xml.Serialization.XmlSerializer(typeof(clsTomaDeclaracion));
    //  TomaDeclaracion = XmlDes.Deserialize(NodeReader(Doc, "clsTomaDeclaracion")) as clsTomaDeclaracion;
    //  //----------------------------------------------
    //  XmlDes = new System.Xml.Serialization.XmlSerializer(typeof(clsPersonasAfectadas));
    //  PersonasAfectadas = XmlDes.Deserialize(NodeReader(Doc, "clsPersonasAfectadas")) as clsPersonasAfectadas;
    //  //----------------------------------------------
    //  XmlDes = new System.Xml.Serialization.XmlSerializer(typeof(clsDescripcionHechos));
    //  DescripcionHechos = XmlDes.Deserialize(NodeReader(Doc, "clsDescripcionHechos")) as clsDescripcionHechos;
    //  //----------------------------------------------
    //  XmlDes = new System.Xml.Serialization.XmlSerializer(typeof(clsVerificacionProcedimiento));
    //  VerificacionProcedimiento = XmlDes.Deserialize(NodeReader(Doc, "clsVerificacionProcedimiento")) as clsVerificacionProcedimiento;
    //  //----------------------------------------------
    //  foreach (XmlNode UnNodo in Doc.SelectNodes("/clsDeclaracion/Anexos/Anexo"))
    //  {
    //    switch (UnNodo.Attributes["Tipo"].Value.Split('.').Last())
    //    {
    //      case "clsAnexo01": Anexos.Add(XmlAnexoReader<clsAnexo01>(UnNodo) as IAnexo); break;
    //      case "clsAnexo02": Anexos.Add(XmlAnexoReader<clsAnexo02>(UnNodo) as IAnexo); break;
    //      case "clsAnexo03": Anexos.Add(XmlAnexoReader<clsAnexo03>(UnNodo) as IAnexo); break;
    //      case "clsAnexo04": Anexos.Add(XmlAnexoReader<clsAnexo04>(UnNodo) as IAnexo); break;
    //      case "clsAnexo05": Anexos.Add(XmlAnexoReader<clsAnexo05>(UnNodo) as IAnexo); break;
    //      case "clsAnexo06": Anexos.Add(XmlAnexoReader<clsAnexo06>(UnNodo) as IAnexo); break;
    //      case "clsAnexo07": Anexos.Add(XmlAnexoReader<clsAnexo07>(UnNodo) as IAnexo); break;
    //      case "clsAnexo08": Anexos.Add(XmlAnexoReader<clsAnexo08>(UnNodo) as IAnexo); break;
    //      case "clsAnexo09": Anexos.Add(XmlAnexoReader<clsAnexo09>(UnNodo) as IAnexo); break;
    //      case "clsAnexo10": Anexos.Add(XmlAnexoReader<clsAnexo10>(UnNodo) as IAnexo); break;
    //      case "clsAnexo11": Anexos.Add(XmlAnexoReader<clsAnexo11>(UnNodo) as IAnexo); break;
    //    }
    //  }
    //  //----------------------------------------------
    //  txt = NodeReader(Doc, "Hechos").ReadElementContentAsString();
    //  TomaDeclaracion.Hechos.Clear();
    //  foreach (var item in txt.Split(' '))
    //  {
    //    TomaDeclaracion.Hechos.Add(Convert.ToInt32(item));
    //  }
    //}

    //XmlNodeReader NodeReader(XmlDocument doc, string ruta)
    //{
    //  try
    //  {
    //    XmlNode Nodo = doc.SelectSingleNode("/clsDeclaracion/" + ruta);
    //    //XmlNode Nodo2 = doc.SelectSingleNode("/declaracion/" + ruta);

    //    XmlNodeReader NodoR = new XmlNodeReader(Nodo);
    //    NodoR.Read();
    //    return NodoR;
    //  }
    //  catch (Exception ex)
    //  {

    //  }

    //  return null;


    //}

    //T1 XmlAnexoReader<T1>(XmlNode nodo) where T1 : class
    //{
    //  XmlNodeReader NodoR = new XmlNodeReader(nodo.FirstChild);
    //  var XmlDes = new System.Xml.Serialization.XmlSerializer(typeof(T1));
    //  return XmlDes.Deserialize(NodoR) as T1;
    //}

    //public void WriteXml(System.Xml.XmlWriter writer)
    //{
    //  XmlSerializer Serializador = null;

    //  writer.WriteElementString("DeclaracionNumero", DeclaracionNumero);
    //  writer.WriteElementString("ID", Convert.ToString(ID));
    //  writer.WriteElementString("EstadoRegistro", EstadoRegistro.ToString());

    //  Serializador = new XmlSerializer(TomaDeclaracion.GetType());
    //  Serializador.Serialize(writer, TomaDeclaracion);

    //  Serializador = new XmlSerializer(DescripcionHechos.GetType());
    //  Serializador.Serialize(writer, DescripcionHechos);

    //  Serializador = new XmlSerializer(VerificacionProcedimiento.GetType());
    //  Serializador.Serialize(writer, VerificacionProcedimiento);

    //  Serializador = new XmlSerializer(PersonasAfectadas.GetType());
    //  Serializador.Serialize(writer, PersonasAfectadas);

    //  writer.WriteStartElement("Anexos");
    //  foreach (var UnAnexo in Anexos)
    //  {
    //    if (UnAnexo is clsAnexo01) XmlAnexoWriter<clsAnexo01>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo02) XmlAnexoWriter<clsAnexo02>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo03) XmlAnexoWriter<clsAnexo03>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo04) XmlAnexoWriter<clsAnexo04>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo05) XmlAnexoWriter<clsAnexo05>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo06) XmlAnexoWriter<clsAnexo06>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo07) XmlAnexoWriter<clsAnexo07>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo08) XmlAnexoWriter<clsAnexo08>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo09) XmlAnexoWriter<clsAnexo09>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo10) XmlAnexoWriter<clsAnexo10>(writer, UnAnexo);
    //    if (UnAnexo is clsAnexo11) XmlAnexoWriter<clsAnexo11>(writer, UnAnexo);
    //  }
    //  writer.WriteEndElement();

    //  StringBuilder SB = new StringBuilder();
    //  foreach (var item in TomaDeclaracion.Hechos)
    //    if (SB.Length == 0)
    //      SB.Append(item);
    //    else
    //      SB.AppendFormat(" {0}", item);
    //  writer.WriteElementString("Hechos", SB.ToString());
    //}

    //void XmlAnexoWriter<T1>(System.Xml.XmlWriter writer, object dato) where T1 : class
    //{
    //  writer.WriteStartElement("Anexo");
    //  writer.WriteAttributeString("Tipo", typeof(T1).ToString());
    //  var Serializador = new XmlSerializer(typeof(T1));
    //  Serializador.Serialize(writer, dato as T1);
    //  writer.WriteEndElement();
    //}
  }
}
