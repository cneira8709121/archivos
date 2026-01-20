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
using System.Windows.Shapes;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Tmp
{
  /// <summary>
  /// Lógica de interacción para Window6.xaml
  /// </summary>
  public partial class Window6 : Window
  {
    public Window6()
    {
      InitializeComponent();
      //Sipod.I.Impresion.ConfiguracionInicialImpresora();
      //Sipod.I.InfoGeneral.PrecargarParametros();
    }

    private void Imprimir_Click(object sender, RoutedEventArgs e)
    {

      var da = (RUV.I.DeclaracionActual = new Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion());
      da.DeclaracionNumero = "A1234567890";
      da.DescripcionHechos.Narracion = @"Himno Nacional de Colombia 

Himno Nacional de la República de Colombia escrito por el presidente Rafael Núñez en 1887, originalmente como una oda para celebrar la independencia de Cartagena; fue interpretado por primera vez el 11 de noviembre del mismo año, día en que se celebra tal evento.

La música fue compuesta por el italiano Oreste Sindici a instancias del actor José Domingo Torres, durante el mandato del Presidente Núñez y presentado al público por el propio Gobierno en el Salón de Grados del Palacio de San Carlos el 6 de diciembre de 1887. La canción adquirió gran popularidad y fue rápidamente adoptada, aunque de manera espontánea, como el Himno Nacional de Colombia.

La proclamación oficial llegó en forma de Ley 33 del 28 de octubre de 1920. La ley 198 del año 1995, la cual legisla los símbolos nacionales, convirtió en obligatoria su difusión en todas las emisoras del país tanto a las 6 de la mañana como a las 6 de la tarde. – Fuente Wikipedia

Audio y letra tomado del sitio oficial de la Presidencia de Colombia

CORO

¡Oh gloria inmarcesible!
 ¡Oh júbilo inmortal!
 ¡En surcos de dolores
 El bien germina ya.

Primera estrofa

Cesó la horrible noche
 La libertad sublime
 Derrama las auroras
 De su invencible luz.
 La humanidad entera,
 Que entre cadenas gime,
 Comprende las palabras
 Del que murió en la cruz

Segunda estrofa

“Independencia” grita
 El mundo americano:
 Se baña en sangre de héroes
 La tierra de Colón.
 Pero este gran principio: “el rey no es soberano”
 Resuena, Y los que sufren
 Bendicen su pasión.

Tercera estrofa

Del Orinoco el cauce
 Se colma de despojos,
 De sangre y llanto un río Se mira allí correr.
 En Bárbula no saben
 Las almas ni los ojos
 Si admiración o espanto
 Sentir o padecer.

Cuarta estrofa

A orillas del Caribe
 Hambriento un pueblo lucha Horrores prefiriendo
 A pérfida salud.
 !Oh, sí¡ de Cartagena
 La abnegación es mucha,
 Y escombros de la muerte
 desprecian su virtud.

Quinta estrofa

De Boyacá en los campos
 El genio de la gloria
 Con cada espiga un héroe
 invicto coronó.
 Soldados sin coraza
 Ganaron la victoria;
 Su varonil aliento
 De escudo les sirvió.

Sexta estrofa

Bolívar cruza el Ande
 Que riega dos océanos
 Espadas cual centellas
 Fulguran en Junín.
 Centauros indomables
 Descienden a los llanos
 Y empieza a presentirse
 De la epopeya el fin.

Séptima estrofa

La trompa victoriosa
 Que en Ayacucho truena
 En cada triunfo crece
 Su formidable son.
 En su expansivo empuje
 La libertad se estrena,
 Del cielo Americano
 Formando un pabellón.

Octava estrofa

La Virgen sus cabellos
 Arranca en agonía
 Y de su amor viuda
 Los cuelga del ciprés.
 Lamenta su esperanza
 Que cubre losa fría;
 Pero glorioso orgullo
 circunda su alba tez.

Novena estrofa

La Patria así se forma
 Termópilas brotando;
 Constelación de cíclopes Su noche iluminó;
 La flor estremecida
 Mortal el viento hallando
 Debajo los laureles
 Seguridad buscó

Décima estrofa

Mas no es completa gloria Vencer en la batalla,
 Que al brazo que combate Lo anima la verdad.
 La independencia sola
 El gran clamor no acalla:
 Si el sol alumbra a todos
 Justicia es libertad.

Undécima estrofa

Del hombre los derechos
 Nariño predicando,
 El alma de la lucha
 Profético enseñó.
 Ricaurte en San Mateo
 En átomos volando
 “Deber antes que vida”,
 Con llamas escribió.";


      da.DescripcionHechos.Narracion = @"Un sistema (del latín systema, proveniente del griego σύστημα) es un objeto compuesto cuyos componentes se relacionan con al menos algún otro componente; puede ser material o conceptual.1 Todos los sistemas tienen composición, estructura y entorno, pero sólo los sistemas materiales tienen mecanismo, y sólo algunos sistemas materiales tienen figura (forma). Según el sistemismo, todos los objetos son sistemas o componentes de algún sistema.2 Por ejemplo, un núcleo atómico es un sistema material físico compuesto de protones y neutrones relacionados por la interacción nuclear fuerte; una molécula es un sistema material químico compuesto de átomos relacionados por enlaces químicos; una célula es un sistema material biológico compuesto de orgánulos relacionados por enlaces químicos no-covalentes y rutas metabólicas; una corteza cerebral es un sistema material psicológico (mental) compuesto de neuronas relacionadas por potenciales de acción y neurotransmisores; un ejército es un sistema material social y parcialmente artificial compuesto de personas y artefactos relacionados por el mando, el abastecimiento, la comunicación y la guerra; el anillo de los números enteros es un sistema conceptual algebraico compuesto de números positivos, negativos y el cero relacionados por la suma y la multiplicación; y una teoría científica es un sistema conceptual lógico compuesto de hipótesis, definiciones y teoremas relacionados por la correferencia y la deducción (implicación).

Un sistema material, sistema concreto o sistema real es una cosa compuesta (por dos o más cosas relacionadas) que posee propiedades que no poseen sus componentes, llamadas propiedades emergentes; por ejemplo, la tensión superficial es una propiedad emergente que poseen los líquidos pero que no poseen sus moléculas componentes. Al ser cosas, los sistemas materiales poseen las propiedades de las cosas, como tener energía (e intercambiarla), tener historia, yuxtaponerse con otras cosas y ocupar una posición en el espacio tiempo.

El esfuerzo por encontrar leyes generales del comportamiento de los sistemas materiales es el que funda la teoría de sistemas y, más en general, el enfoque de la investigación científica a la que se alude como sistemismo, sistémica o pensamiento sistémico, en cuyo marco se encuentran disciplinas y teorías como la cibernética, la teoría de la información, la teoría del caos, la dinámica de sistemas y otras.
[editar]
Análisis CEEM

El análisis más sencillo del concepto de sistema material es el que incluye los conceptos de composición, entorno, estructura y mecanismo (CEEM, por sus siglas). La composición de un sistema es el conjunto de sus partes componentes. El entorno o ambiente de un sistema es el conjunto de las cosas que actúan sobre los componentes del sistema, o sobre las que los componentes del sistema actúan. La estructura interna o endoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema. La estructura externa o exoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema y los elementos de su entorno. La estructura total de un sistema es la unión de su exoestructura y su endoestructura. Las relaciones más importantes son los vínculos o enlaces, aquellas que afectan a los componentes relacionados; las relaciones espaciotemporales no son vínculos. El mecanismo de un sistema es el conjunto de procesos internos que lo hacen cambiar algunas propiedades, mientras que conserva otras.

Además, la frontera de un sistema es el conjunto de componentes que están directamente vinculados (sin nada interpuesto) con los elementos de su entorno. La frontera de un sistema físico puede ser rígida o móvil, permeable o impermeable, conductor térmico (adiabática) o no, conductor eléctrico o no, e incluso puede ser aislante de frecuencias de audio. Además, algunos sistemas tienen figura (forma); pero no todo sistema con frontera tiene necesariamente figura. Si hay algún intercambio de materia entre un sistema físico y su entorno a través de su frontera, entonces el sistema es abierto; de lo contrario, el sistema es cerrado. Si un sistema cerrado tampoco intercambia energía, entonces el sistema es aislado. En rigor, el único sistema aislado es el universo. Si un sistema posee la organización necesaria para controlar su propio desarrollo, asegurando la continuidad de su composición y estructura (homeostasis) y la de los flujos y transformaciones con que funciona (homeorresis) –mientras las perturbaciones producidas desde su entorno no superen cierto grado–, entonces el sistema es autopoyético.

El análisis más sencillo del concepto de sistema material es el que incluye los conceptos de composición, entorno, estructura y mecanismo (CEEM, por sus siglas). La composición de un sistema es el conjunto de sus partes componentes. El entorno o ambiente de un sistema es el conjunto de las cosas que actúan sobre los componentes del sistema, o sobre las que los componentes del sistema actúan. La estructura interna o endoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema. La estructura externa o exoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema y los elementos de su entorno. La estructura total de un sistema es la unión de su exoestructura y su endoestructura. Las relaciones más importantes son los vínculos o enlaces, aquellas que afectan a los componentes relacionados; las relaciones espaciotemporales no son vínculos. El mecanismo de un sistema es el conjunto de procesos internos que lo hacen cambiar algunas propiedades, mientras que conserva otras.

Además, la frontera de un sistema es el conjunto de componentes que están directamente vinculados (sin nada interpuesto) con los elementos de su entorno. La frontera de un sistema físico puede ser rígida o móvil, permeable o impermeable, conductor térmico (adiabática) o no, conductor eléctrico o no, e incluso puede ser aislante de frecuencias de audio. Además, algunos sistemas tienen figura (forma); pero no todo sistema con frontera tiene necesariamente figura. Si hay algún intercambio de materia entre un sistema físico y su entorno a través de su frontera, entonces el sistema es abierto; de lo contrario, el sistema es cerrado. Si un sistema cerrado tampoco intercambia energía, entonces el sistema es aislado. En rigor, el único sistema aislado es el universo. Si un sistema posee la organización necesaria para controlar su propio desarrollo, asegurando la continuidad de su composición y estructura (homeostasis) y la de los flujos y transformaciones con que funciona (homeorresis) –mientras las perturbaciones producidas desde su entorno no superen cierto grado–, entonces el sistema es autopoyético.";


      da.VerificacionProcedimiento.EnmendarDeclaracionTexto = @"Undécima estrofa

Del hombre los derechos
Nariño predicando,
El alma de la lucha
Profético enseñó.
Ricaurte en San Mateo
En átomos volando
“Deber antes que vida”,
Con llamas escribió.";

      da.VerificacionProcedimiento.ObservacionesSobreDiligenciamiento = @"Un sistema (del latín systema, proveniente del griego σύστημα) es un objeto compuesto cuyos componentes se relacionan con al menos algún otro componente; puede ser material o conceptual.1 Todos los sistemas tienen composición, estructura y entorno, pero sólo los sistemas materiales tienen mecanismo, y sólo algunos sistemas materiales tienen figura (forma). Según el sistemismo, todos los objetos son sistemas o componentes de algún sistema.2 Por ejemplo, un núcleo atómico es un sistema material físico compuesto de protones y neutrones relacionados por la interacción nuclear fuerte; una molécula es un sistema material químico compuesto de átomos relacionados por enlaces químicos; una célula es un sistema material biológico compuesto de orgánulos relacionados por enlaces químicos no-covalentes y rutas metabólicas; una corteza cerebral es un sistema material psicológico (mental) compuesto de neuronas relacionadas por potenciales de acción y neurotransmisores; un ejército es un sistema material social y parcialmente artificial compuesto de personas y artefactos relacionados por el mando, el abastecimiento, la comunicación y la guerra; el anillo de los números enteros es un sistema conceptual algebraico compuesto de números positivos, negativos y el cero relacionados por la suma y la multiplicación; y una teoría científica es un sistema conceptual lógico compuesto de hipótesis, definiciones y teoremas relacionados por la correferencia y la deducción (implicación).

Un sistema material, sistema concreto o sistema real es una cosa compuesta (por dos o más cosas relacionadas) que posee propiedades que no poseen sus componentes, llamadas propiedades emergentes; por ejemplo, la tensión superficial es una propiedad emergente que poseen los líquidos pero que no poseen sus moléculas componentes. Al ser cosas, los sistemas materiales poseen las propiedades de las cosas, como tener energía (e intercambiarla), tener historia, yuxtaponerse con otras cosas y ocupar una posición en el espacio tiempo.

El esfuerzo por encontrar leyes generales del comportamiento de los sistemas materiales es el que funda la teoría de sistemas y, más en general, el enfoque de la investigación científica a la que se alude como sistemismo, sistémica o pensamiento sistémico, en cuyo marco se encuentran disciplinas y teorías como la cibernética, la teoría de la información, la teoría del caos, la dinámica de sistemas y otras.
[editar]
Análisis CEEM

El análisis más sencillo del concepto de sistema material es el que incluye los conceptos de composición, entorno, estructura y mecanismo (CEEM, por sus siglas). La composición de un sistema es el conjunto de sus partes componentes. El entorno o ambiente de un sistema es el conjunto de las cosas que actúan sobre los componentes del sistema, o sobre las que los componentes del sistema actúan. La estructura interna o endoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema. La estructura externa o exoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema y los elementos de su entorno. La estructura total de un sistema es la unión de su exoestructura y su endoestructura. Las relaciones más importantes son los vínculos o enlaces, aquellas que afectan a los componentes relacionados; las relaciones espaciotemporales no son vínculos. El mecanismo de un sistema es el conjunto de procesos internos que lo hacen cambiar algunas propiedades, mientras que conserva otras.

Además, la frontera de un sistema es el conjunto de componentes que están directamente vinculados (sin nada interpuesto) con los elementos de su entorno. La frontera de un sistema físico puede ser rígida o móvil, permeable o impermeable, conductor térmico (adiabática) o no, conductor eléctrico o no, e incluso puede ser aislante de frecuencias de audio. Además, algunos sistemas tienen figura (forma); pero no todo sistema con frontera tiene necesariamente figura. Si hay algún intercambio de materia entre un sistema físico y su entorno a través de su frontera, entonces el sistema es abierto; de lo contrario, el sistema es cerrado. Si un sistema cerrado tampoco intercambia energía, entonces el sistema es aislado. En rigor, el único sistema aislado es el universo. Si un sistema posee la organización necesaria para controlar su propio desarrollo, asegurando la continuidad de su composición y estructura (homeostasis) y la de los flujos y transformaciones con que funciona (homeorresis) –mientras las perturbaciones producidas desde su entorno no superen cierto grado–, entonces el sistema es autopoyético.

El análisis más sencillo del concepto de sistema material es el que incluye los conceptos de composición, entorno, estructura y mecanismo (CEEM, por sus siglas). La composición de un sistema es el conjunto de sus partes componentes. El entorno o ambiente de un sistema es el conjunto de las cosas que actúan sobre los componentes del sistema, o sobre las que los componentes del sistema actúan. La estructura interna o endoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema. La estructura externa o exoestructura de un sistema es el conjunto de relaciones entre los componentes del sistema y los elementos de su entorno. La estructura total de un sistema es la unión de su exoestructura y su endoestructura. Las relaciones más importantes son los vínculos o enlaces, aquellas que afectan a los componentes relacionados; las relaciones espaciotemporales no son vínculos. El mecanismo de un sistema es el conjunto de procesos internos que lo hacen cambiar algunas propiedades, mientras que conserva otras.

Además, la frontera de un sistema es el conjunto de componentes que están directamente vinculados (sin nada interpuesto) con los elementos de su entorno. La frontera de un sistema físico puede ser rígida o móvil, permeable o impermeable, conductor térmico (adiabática) o no, conductor eléctrico o no, e incluso puede ser aislante de frecuencias de audio. Además, algunos sistemas tienen figura (forma); pero no todo sistema con frontera tiene necesariamente figura. Si hay algún intercambio de materia entre un sistema físico y su entorno a través de su frontera, entonces el sistema es abierto; de lo contrario, el sistema es cerrado. Si un sistema cerrado tampoco intercambia energía, entonces el sistema es aislado. En rigor, el único sistema aislado es el universo. Si un sistema posee la organización necesaria para controlar su propio desarrollo, asegurando la continuidad de su composición y estructura (homeostasis) y la de los flujos y transformaciones con que funciona (homeorresis) –mientras las perturbaciones producidas desde su entorno no superen cierto grado–, entonces el sistema es autopoyético.";

      for (int i = 0; i < 18; i++)
        da.PersonasAfectadas.ListaPersonas.Add(ObtenerPA(i + 1));

      RUV.I.Configuraciones.Impresion.ImprimirDeclaracion(da);

      textBlock1.Text = "Terminado";
    }

    Random R = new Random();
    int[] ListaHechosVictimizantes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
    int[] ListaDiscapacidades = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 98, 99 };
    int[] PertenenciasEtnicas = new int[] { 162, 166, 170, 171, 4541, 169 };
    clsPersonaAfectada ObtenerPA(int consecutivo)
    {
      clsPersonaAfectada Resultado = new clsPersonaAfectada()
      {
        ComunidadEtnica1 = R.Next(15),
        ComunidadEtnica2 = R.Next(15),
        EstadoCivil = R.Next(15),
        Genero = R.Next(2),
        GestanteLactante = R.Next(2),
        ID = 1,
        MujerCabezaDeHogar = R.Next(2),
        NumeroConsecutivo = consecutivo,
        NumeroDocumento = "94307617",
        OtraComunidadEtnica = "Otra",
        PertenenciaEtnica = PertenenciasEtnicas[R.Next(6)],
        PrimerApellido = "Fernández",
        SegundoApellido = "Ricaurte",
        PrimerNombre = "Néstor",
        SegundoNombre = "Arturo",
        RegimenEspecial = R.Next(4),
        Relacion = R.Next(4),
        TipoDocumento = R.Next(4)
      };

      int Qty = R.Next(ListaHechosVictimizantes.Count());
      int Pos = 0;
      for (int i = 0; i <= Qty; i++)
      {
        Pos = R.Next(Qty);
        if (!Resultado.HechosVictimizantes.Contains(Pos))
          Resultado.HechosVictimizantes.Add(Pos);
      }

      Qty = R.Next(ListaDiscapacidades.Count());
      Pos = 0;
      for (int i = 0; i <= Qty; i++)
      {
        Pos = R.Next(Qty);
        if (!Resultado.Discapacidades.Contains(Pos))
          Resultado.Discapacidades.Add(Pos);
      }

      return Resultado;

    }
  }
}
