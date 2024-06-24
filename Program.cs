// See https://aka.ms/new-console-template for more information
using Microsoft.Graph;
using Microsoft.Graph.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

using System; // Add this line for basic system functions
using System.Data.Common;
using System.Drawing; // Add this line to work with images
using System.IO;
using System.Reflection;
using System.Web.Mvc; // Add this line to work with files and directories

namespace PDFMackup
{
    public class Program
    {
        static void Main(string[] args)
        {
            //PDFCotizacion();
            PDFCotizacion();
        }

        public static void PDFCotizacion()
        {

            try
            {
                var hideAgentName = true; //TODO: traer variable de configuracion al llamar el metodo
                var productType = 3; //TODO: traer variable de configuracion al llamar el metodo
                var datosCotizacion = 1; //TODO: traer datos de la cotizacion al llamar el metodo
                var datosPrimas = 1; //TODO: traer datos de las primas al llamar el metodo

                //Colores
                string beyondColor = "#326295";
                string priviligeColor = "#8dc341";
                string libertyColor = "#30b6b5";
                string legacyColor = "#841a66";

                //Imagenes
                var currentDirectory = Directory.GetCurrentDirectory();
                string piePaginaBg = Path.Combine(currentDirectory, "img", "pie_de_pagina.png");
                string logoLogoHeader = Path.Combine(currentDirectory, "img", "logoBgWhite.png");
                string piePaginaBgLine = Path.Combine(currentDirectory, "img", "pie_de_pagina_line.png");
                string logoAzul = Path.Combine(currentDirectory, "img", "logo_azul.png");

                int fontSize = 9;
                string versionPDF = "V.2-10.1.23C24-3";

                #region Textos Pagina 1
                string fueraUSA = "Fuera de USA";
                string dentroUSA = "Dentro de USA";
                string anual = "Anual";
                string semestral = "Semestral";
                string trimestral = "Trimestral";
                string deducible = "Deducible";
                string opcion = "Opción";
                string cubierto = "Cubierto";
                string noAplica = "No Aplica";
                string total = "Total";
                string nombreTitular = "Nombre del Titular:";
                string edadTitular = "Edad del Titular:";
                string edadConyugue = "Edad del Cónyugue:";
                string numeroAdultos = "Número de Adultos:";
                string numeroNinos = "Número de Niños:";
                string pais = "País:";
                string trasplateOrganos = "Trasplante de Órganos:";
                string complicacionesMaternidadP1 = "Complicaciones de Maternidad:";
                string agente = "Agente:";
                string codigo = "Código:";
                string telefono = "Teléfono:";
                string email = "C. Electrónico:";
                string fecha = "Fecha:";
                string hora = "Hora:";
                string cotizacionNro = "Cotización Nro.:";
                string notaPag1 = "Esta cotización es de carácter informativo y está sujeto a un análisis de riesgo\r\n*Solamente aplica un deducible por persona, por año póliza. Para pólizas de familia se aplicará un máximo de dos deducibles acumulados por póliza por año póliza.\r\nPara más información por favor referirse al Condicionado de cobertura de la póliza.\r\n";
                string currency = "$";
                string usd500 = currency + "500";
                string usd5000 = currency + "5.000";
                string usd1000 = currency + "1000";
                string usd10000 = currency + "10.000";
                string usd2000 = currency + "2000";
                string usd20000 = currency + "20.000";
                string usd30000 = currency + "30.000";
                string usd3000 = currency + "3000";
                #endregion

                #region Textos Pagina 2, 3, 4
                //Textos Banners
                //Pagina 2
                string notaBlack = "- Al menos de que se indique lo contrario todos los beneficios son por Año Póliza.";
                string descripcionCobertura = "DESCRIPCIÓN DE COBERTURA";
                string limiteCoberta = "LÍMITE DE COBERTURA";
                string hospitalizacion = "HOSPITALIZACIÓN";
                //Pagina 3
                string sercivioAmbulancia = "SERVICIO DE AMBULANCIA";
                string terapiasEspeciales = "TERAPIAS ESPECIALES / ALTERNATIVAS";
                string serviciosPreventivos = "SERVICIOS PREVENTIVOS Y ADICIONALES";
                string maternidad = "MATERNIDAD";
                //Pagina 4
                string servicioTransporte = "SERVICIOS DE TRAMPOSTACIÓN";
                string otrosBeneficios = "OTROS BENEFICIOS / SERVICIOS";
                //TODO: colocar las respuestas de los demas tipos de productos, actualmente solo tengo los de Privilege
                // Pagina 2
                string beneficiosMaximos = "Beneficio máximo";  //TODO: colocar recurso de idioma
                string elegibilidad = "Elegibilidad";  //TODO: colocar recurso de idioma
                string renovacion = "Renovación";  //TODO: colocar recurso de idioma
                string areaCobertura = "Área de cobertura";
                string tratamientoMedico = "Tratamiento médico para accidentes y emergencias fuera de la red"; //TODO: colocar recurso de idioma
                string opcionesDeducibles = "Opciones de deducibles\r\n- Por año póliza"; //TODO: colocar recurso de idioma
                string coaseguro = "Coaseguro"; //TODO: colocar recurso de idioma
                string periodoEspera = "Período de espera"; //TODO: colocar recurso de idioma
                string reduccionDeducible = "Reducción del deducible después de 3 años de no reclamo"; //TODO: colocar recurso de idioma
                string coberturaGratuita = "Cobertura gratuita para dependientes después del fallecimiento del titular"; //TODO: colocar recurso de idioma
                string servicioMedicoQuirurgico = "Servicios médicos y quirúrgicos";  //TODO: colocar recurso de idioma
                string honorariosMedicosMedicamentos = "Honorarios médicos y medicamentos ";  //TODO: colocar recurso de idioma
                string habitacionYAlimentacion = "Habitación y alimentación\r\n - Cuarto privado, semi-privado\r\n - Habitación estándar"; //TODO: colocar recurso de idioma
                string unidadDeCuidadosIntensivos = "Unidad de cuidados intensivos"; //TODO: colocar recurso de idioma
                string tratamientoDeCancer = "Tratamiento de cáncer (quimioterapia y radioterapia). Diálisis"; //TODO: colocar recurso de idioma
                string serviciosDeDiagnostico = "Servicios de diagnóstico (laboratorio, rayos X, resonancias magnéticas, TAC\r\n y ecografías)";
                string fisioterapiaRehabilitacion = "Fisioterapia / Rehabilitación"; // TODO: colocar recurso de idioma
                string alojamientoAcompananteMenor = "Alojamiento en el hospital para acompañante de un menor de edad"; // TODO: colocar recurso de idioma
                string cirugiaReduccionRiesgoCancer = "Cirugía de reducción de riesgo de cáncer o profiláctica\r\n -Período de espera\r\n -Pre-aprobado"; // TODO: colocar recurso de idioma
                string cirugiaBariatrica = "Cirugía bariátrica, de bypass gástrico y cualquier tipo de procedimiento quirúrgico\r\n destinado a la pérdida de peso y sus complicaciones/tratamientos.\r\n -Período de espera\r\n -Pre-aprobado"; // TODO: colocar recurso de idioma
                string cirugiaReconstructiva = "Cirugía reconstructiva en caso de accidente o malformaciones nasales sobre el septum\r\n - Medicamente necesario";
                string salaDeEmergencias = "Sala de emergencias";
                // Pagina 3
                string cirugiaAmbulatoria = "Cirugía ambulatoria";
                string servicioMedicoQuirurgicoP3 = "Servicios médicos / Quirúrgicos / Honorarios médicos";
                string medicamentos = "Medicamentos";
                string tratamientoDeCancerP3 = "Tratamiento de cáncer (quimioterapia y radioterapia). Diálisis";
                string serviciosDeDiagnosticoP3 = "Servicios de diagnóstico (laboratorio, rayos X, resonancias magnéticas, TAC\r\n y ecografías)";
                string fisioterapiaRehabilitacionP3 = "Fisioterapia / Rehabilitación\r\n - Pre-aprobado";
                string terapiaLenguaje = "Terapia de lenguaje";
                string consultaPsicologica = "Consulta psicológica";
                string chequeosMedicos = "Chequeos médicos de rutina anual\r\n - Sin deducible";
                string chequeoMedicosMenores = "Chequeo médico de rutina anual para menores de edad / Inmunizaciones\r\n - Sin deducible";
                string examenDentalPreventivo = "Examen dental preventivo\r\n - Período de espera 12 Meses\r\n - Sin deducible";
                string aparatosAuditivos = "Aparatos auditivos\r\n - Período de espera\r\n - Pre-aprobado";
                string servicioMaternidad = "Servicio de maternidad\r\n - Por cada maternidad cubierta\r\n - Período de espera por cada producto garantizado\r\n - Sin deducible";
                string complicacionesMaternidad = "Complicaciones de maternidad y/o complicaciones de nacimiento\r\n - Aplica deducible\r\n - Condiciones que sean resultados de un tratamiento de fertilidad están excluidos.";
                string preservacionCelulasMadre = "Preservación de células madre del cordón umbilical\r\n - Por cada recién nacido de una maternidad cubierta\r\n - Sin deducible";
                string maternidadDependientes = "Maternidad para dependientes\r\n - Solo aplica para aquellas dependientes que son >= 18 hasta 24 años";
                string inclusionAutomaticaMenorRecienNacido = "Inclusión automática de recién nacido a la póliza\r\n - Se tiene que notificar e incluir dentro de la póliza en los primeros 90 días después del nacimiento\r\n - Solo recién nacidos de una maternidad cubierta";
                // Pagina 4
                string ambulanciaLocal = "Ambulancia local\r\n -Sin deducible\r\n -Pre-aprobado";
                string ambulanciaAerea = "Ambulancia aérea\r\n -Sin deducible\r\n -Pre-aprobado";
                string repatriacionRestos = "Repatriación de restos mortales o servicio de cremación\r\n - Pre-aprobado";
                string asistenciaViaje = "Asistencia de viaje de regreso al país de residencia si es evacuado\r\n - Para el paciente y un acompañante ";
                string actividadesPeligrosas = "Actividades peligrosas y/o profesionales";
                string tratamientoDental = "Tratamiento dental de emergencia después de un accidente\r\n - Aplica deducible";
                string condicionesCognitivas = "Condiciones congénitas y hereditarias diagnosticadas antes de los 18 años\r\n - Nacidos en una maternidad cubierta";
                string condicionescognitivasHereditarias = "Condiciones congénitas y hereditarias diagnosticadas a partir de 18 años";
                string procedimientosTransplantes = "Procedimientos de trasplantes\r\n -Pre-aprobado";
                string equiposMedicos = "Equipos médicos y dispositivos ortopédicos\r\n -Pre-aprobado";
                string atencionMedicaDomiciliaria = "Atención médica domiciliaria (Solo para Venezuela)\r\n - Consulta sin deducible\r\n - Medicamentos y paraclínicos aplica deducible\r\n - Pre-aprobado";
                string telemedicina = "Telemedicina\r\n -Sin deducible";
                //Respuestas
                // Pagina 2
                string resBeneficiosMaximos = "$5.000.000";
                string resElegibilidad = "74 años";
                string resRenovacion = "Vitalicio";
                string resAreaCobertura = "Mundial";
                string resTratamientoMedico = "100% UCR";
                string resOpcionesDeducibles = "I. $500/$1,000 II. $1,000/$2,000 III. $2,000/$3,000 IV. $5,000 V. $10,000 VI. $20,000";
                string resCoaseguro = noAplica;
                string resPeriodoEspera = "60 días / 2 mes";
                string resReduccionDeducible = "Opciones I, II y III: eliminación del deducible por 1 año después del 3er año sin reclamos\r\nOpciones IV, V y VI: reducción del deducible hasta 60% por 1 año después del 3er año sin\r\nreclamos";
                string resCoberturaGratuita = "1 año de exoneración de prima";
                string resServicioMedicoQuirurgico = "100% UCR";
                string resHonorariosMedicosMedicamentos = "100% UCR";
                string resHabitacionYAlimentacion = "100% UCR";
                string resUnidadDeCuidadosIntensivos = "100% UCR";
                string resTratamientoDeCancer = "100% UCR Opciones I, II, III y IV";
                string resServiciosDeDiagnostico = "100% UCR";
                string resFisioterapiaRehabilitacion = "100% UCR";
                string resAlojamientoAcompananteMenor = "$250 por noche, máximo 30 noches";
                string resCirugiaReduccionRiesgoCancer = "$15,000 de por vida / 48 meses de periodo de espera";
                string resCirugiaBariatrica = "$7,000 de por vida / 18 meses de periodo de espera";
                string resCirugiaReconstructiva = "100% UCR";
                string resSalaDeEmergencias = "100% UCR";
                // Pagina 3
                string resCirugiaAmbulatoria = "100% UCR";
                string resServicioMedicoQuirurgicoP3 = "100% UCR";
                string resMedicamentos = "100% UCR";
                string resTratamientoDeCancerP3 = "100% UCR Opciones I, II, III y IV";
                string resServiciosDeDiagnosticoP3 = "100% UCR Opciones I, II, III y IV";
                string resFisioterapiaRehabilitacionP3 = "$12,000";
                string resTerapiaLenguaje = "$3,000";
                string resConsultaPsicologica = "9 visitas por año / 100% UCR";
                string resChequeosMedicos = "$350 por asegurado, por año póliza";
                string resChequeoMedicosMenores = "< 1 año = $150 por visita, hasta 5 visitas\r\n 1 a 17 años = $200 por año póliza";
                string resExamenDentalPreventivo = "$100 dentro del beneficio de chequeo médico de rutina";
                string resAparatoAuditivos = "$1,500 de por vida, 24 meses de periodo de espera";
                string resServicioMaternidad = "Parto normal: $6,000 Cesárea: $7,000\r\n - 14 meses de período de espera\r\n - Opciones I, II, y III";
                string resComplicacionesMaternidad = "Endoso: $150,000 adicional de por vida\r\n - 18 meses de período de espera\r\n - Opciones I y II";
                string resPreservacionCelulasMadre = "$1,000 Opciones I y II";
                string resMaternidadDependientes = "Parto normal: $800 Cesárea: $1000\r\n - 18 meses de período de espera\r\n - Opciones I y II";
                string resInclusionAutomaticaMenorRecienNacido = "Sin selección de riesgos";
                // Pagina 4
                string resAmbulanciaLocal = "100% UCR";
                string resAmbilanciaAerea = "$ 60,000";
                string resRepatriacionRestos = "$12,000";
                string resAsistenciaViaje = "$1,500 por persona";
                string resActividadesPeligrosas = "100% UCR (profesional o amateur)";
                string resTratamientoDental = "100% UCR";
                string resCondicionesCognitivas = "$75,000 UCR de por vida";
                string resCondicionesCognitivasHereditarias = "$150,000 UCR de por vida";
                string resProcedimientosTransplantes = "$350,000 (diagnóstico de por vida)\r\n $50,000 (máximo para preparación del donante)\r\n - Opciones: I, II y III\r\n ENDOSO para opciones: lV, V y VI";
                string resEquiposMedicos = "$12,000";
                string resAtencionMedicaDomiciliaria = "100% UCR\r\n - Opciones I, II, III y IV";
                string resTelemedicina = "100% UCR";
                #endregion

                string beneficios = "BENEFICIOS";

                string docColor = "";
                string textImg = "";
                string beneficiosTipo = "";
                switch (productType)
                {
                    case 1:
                        docColor = priviligeColor;
                        textImg = "PRIVILEGE-txt.png";
                        beneficiosTipo = beneficios + " PRIVILEGE";
                        break;
                    case 2:
                        docColor = libertyColor;
                        textImg = "LIBERTY-txt.png";
                        beneficiosTipo = beneficios + " LIBERTY";
                        resBeneficiosMaximos = "$10.000.000";
                        break;
                    case 3:
                        docColor = legacyColor;
                        textImg = "LEGACY-txt.png";
                        beneficiosTipo = beneficios + " LEGACY";
                        resBeneficiosMaximos = "100% UCR Dentro de la Red LOYAL USA Major Medical Services / 60% UCR fuera\r\nde la red";
                        break;
                    default:
                        docColor = beyondColor;
                        textImg = "BEYOND-txt.png";
                        beneficiosTipo = beneficios + " BEYOND";
                        resBeneficiosMaximos = "$10.000.000";
                        break;
                }

                var imgText = Path.Combine(currentDirectory, "img", textImg);

                // code in your main method
                var document = Document.Create(container =>
                {

                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter); //tipo de hoja A4
                        page.Margin(30);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontFamily("Lato"));


                        IContainer DefaultCellStyle(IContainer container, string backgroundColor, int border = 1, int pV = 1)
                        {
                            return container
                                .Border(border)
                                .BorderColor(Colors.Black)
                                .Background(backgroundColor)
                                .PaddingVertical(pV)
                                .PaddingHorizontal(5)
                                .AlignCenter()
                                .AlignMiddle();
                        }

                        page.Header().Column(column =>
                        {
                            #region Header Pagina 1 
                            var maxHeight = 120;
                            var maxWidth = 158;
                            column.Item().ShowOnce().Height(maxHeight).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().Width(maxWidth).Height(maxHeight)
                                .AspectRatio(1 / 2)
                                .Image(logoLogoHeader)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                                //table.Cell();
                                table.Cell().ColumnSpan(2).AlignRight().PaddingRight(20).PaddingTop(50).Width(237).Height(40)
                                .AspectRatio(1 / 6)
                                .Image(imgText)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });


                            //Inicio Pagina 1
                            //Textos Pagina 1
                            //foreach (var item in datosCotizacion)
                            //{
                            //    string agentName = item.NombreAngente;
                            //    if (hideAgentName)
                            //    {
                            //       agentName = "------";
                            //    }

                            //}
                            //Datos del Titular
                            column.Item().ShowOnce().PaddingTop(5).PaddingLeft(15).Table(table =>
                            {

                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                //Row1
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(nombreTitular).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(agente).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row2
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(edadTitular).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(codigo).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row3
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(numeroAdultos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(telefono).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row4
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(edadConyugue).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(email).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row5
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(numeroNinos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(fecha).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row6
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(pais).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(hora).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row7
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(trasplateOrganos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(cotizacionNro).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row8
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(complicacionesMaternidadP1).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(4).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);

                                IContainer CellStyleLessBorderTable(IContainer container) => DefaultCellStyle(container, Colors.White, 0, 0);
                            });
                            #endregion

                            #region Header Resto de las paginas
                            column.Item().SkipOnce().Height(maxHeight).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().ColumnSpan(3).AlignCenter().Width(maxWidth).Height(maxHeight)
                                .AspectRatio(1 / 2)
                                .Image(logoLogoHeader)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });
                            #endregion
                        });

                        page.Content().PaddingTop(10).Column(column =>
                        {
                            #region Pagina 1
                            var celMaxHeight = 11;
                            //Tabla deducibles
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(deducible).AlignStart().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " I").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " II").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " III").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " IV").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " V").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " VI").FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor);
                                });

                                //Row 1
                                table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(fueraUSA).AlignStart().FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd500).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd1000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd2000).FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd5000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd10000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd20000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                //Row 2
                                table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(dentroUSA).AlignStart().FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd1000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd2000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd3000).FontSize(fontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            //Tabla Anual, Semestral, Trimestal
                            for (int i = 0; i < 3; i++)
                            {
                                var TextHead = "";
                                if (i == 2)
                                {
                                    TextHead = trimestral;
                                }
                                else if (i == 1)
                                {
                                    TextHead = semestral;
                                }
                                else
                                {
                                    TextHead = anual;
                                }

                                column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().ColumnSpan(8).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(TextHead).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor);
                                    });



                                    for (int j = 0; j < 7; j++)
                                    //foreach (var item in args) //  TODO: cambiar por el foreach de los datos 
                                    {
                                        table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(fueraUSA).AlignStart().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd500).FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("1000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("2000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("500").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("1000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("2000").AlignCenter().FontSize(fontSize);

                                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                                    }
                                });
                            }
                            //Version PDF
                            column.Item().PaddingTop(2).PaddingRight(20).Background(Colors.White).AlignRight().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).Light());
                                text.Span(versionPDF);
                            });
                            //Nota
                            column.Item().PaddingTop(10).PaddingLeft(20).PaddingRight(15).Background(Colors.White).AlignLeft().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).SemiBold());
                                text.Span(notaPag1);
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 1
                            #endregion

                            #region Pagina 2
                            var borderTb = 1;
                            var pV = 2;
                            var newCelHeight = 20;
                            var newFontSize = 7;
                            //Inicio Pagina 2
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            //Tabla DESCRIPCION DE COBERTURA / LIMITE DE COBERTURA
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(descripcionCobertura).FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(limiteCoberta).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(notaBlack).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Black, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(beneficiosMaximos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resBeneficiosMaximos).FontSize(6);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(elegibilidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resElegibilidad).FontSize(6);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(renovacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resRenovacion).FontSize(6);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(areaCobertura).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAreaCobertura).FontSize(6);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoMedico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoMedico).FontSize(6);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(opcionesDeducibles).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resOpcionesDeducibles).FontSize(6);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(coaseguro).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCoaseguro).FontSize(6);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(periodoEspera).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resPeriodoEspera).FontSize(6);
                                //Row 9
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(reduccionDeducible).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resReduccionDeducible).FontSize(6);
                                //Row 10
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(coberturaGratuita).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCoberturaGratuita).FontSize(6);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            //Tabla HOSPITALIZACIÓN
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(hospitalizacion).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMedicoQuirurgico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMedicoQuirurgico).FontSize(6);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(honorariosMedicosMedicamentos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resHonorariosMedicosMedicamentos).FontSize(6);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(habitacionYAlimentacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resHabitacionYAlimentacion).FontSize(6);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(unidadDeCuidadosIntensivos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resUnidadDeCuidadosIntensivos).FontSize(6);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDeCancer).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDeCancer).FontSize(6);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(serviciosDeDiagnostico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServiciosDeDiagnostico).FontSize(6);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(fisioterapiaRehabilitacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resFisioterapiaRehabilitacion).FontSize(6);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(alojamientoAcompananteMenor).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAlojamientoAcompananteMenor).FontSize(6);
                                //Row 9
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaReduccionRiesgoCancer).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaReduccionRiesgoCancer).FontSize(6);
                                //Row 10
                                table.Cell().Element(CellStyle).Height(45).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaBariatrica).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(45).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaBariatrica).FontSize(6);
                                //Row 11
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaReconstructiva).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaReconstructiva).FontSize(6);
                                //Row 12
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(salaDeEmergencias).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resSalaDeEmergencias).FontSize(6);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 2
                            #endregion

                            #region Pagina 3
                            //Inicio Pagina 3
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(sercivioAmbulancia).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaAmbulatoria).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaAmbulatoria).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMedicoQuirurgicoP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMedicoQuirurgicoP3).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(medicamentos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resMedicamentos).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDeCancerP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDeCancerP3).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(serviciosDeDiagnosticoP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServiciosDeDiagnosticoP3).FontSize(newFontSize);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(fisioterapiaRehabilitacionP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resFisioterapiaRehabilitacionP3).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(terapiasEspeciales).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(terapiaLenguaje).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTerapiaLenguaje).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(consultaPsicologica).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resConsultaPsicologica).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(serviciosPreventivos).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(chequeosMedicos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resChequeosMedicos).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(chequeoMedicosMenores).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resChequeoMedicosMenores).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(examenDentalPreventivo).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resExamenDentalPreventivo).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(aparatosAuditivos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAparatoAuditivos).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(maternidad).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMaternidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMaternidad).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(complicacionesMaternidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resComplicacionesMaternidad).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(preservacionCelulasMadre).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resPreservacionCelulasMadre).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(maternidadDependientes).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resMaternidadDependientes).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(inclusionAutomaticaMenorRecienNacido).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resInclusionAutomaticaMenorRecienNacido).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 3
                            #endregion

                            #region Pagina 4
                            //Inicio Pagina 4
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(servicioTransporte).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(ambulanciaLocal).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAmbulanciaLocal).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(ambulanciaAerea).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAmbilanciaAerea).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(repatriacionRestos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resRepatriacionRestos).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(asistenciaViaje).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAsistenciaViaje).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(otrosBeneficios).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(actividadesPeligrosas).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resActividadesPeligrosas).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDental).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDental).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(condicionesCognitivas).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCondicionesCognitivas).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(condicionescognitivasHereditarias).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCondicionesCognitivasHereditarias).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(procedimientosTransplantes).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resProcedimientosTransplantes).FontSize(newFontSize);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(equiposMedicos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resEquiposMedicos).FontSize(newFontSize);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(atencionMedicaDomiciliaria).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAtencionMedicaDomiciliaria).FontSize(newFontSize);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(telemedicina).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTelemedicina).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            //Version PDF
                            column.Item().PaddingTop(2).PaddingRight(20).Background(Colors.White).AlignRight().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).Light());
                                text.Span(versionPDF);
                            });
                            column.Item().PaddingTop(30).Height(136).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().ColumnSpan(3).AlignCenter().Width(180).Height(136)
                                .AspectRatio(1 / 2)
                                .Image(logoAzul)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });
                            //fin Pagina 4
                            #endregion
                        });


                        page.Footer().Column(col =>
                        {
                            #region Pie de Pagina Linea fina
                            col.Item().ShowIf(x => x.PageNumber != x.TotalPages).Layers(l =>
                            {
                                l
                                   .PrimaryLayer()
                                   .Height(18)
                                   .Image(piePaginaBgLine)
                                   .WithRasterDpi(300);

                                l.Layer().PaddingRight(10).AlignRight().AlignMiddle().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontSize(8).FontColor(Colors.White).SemiBold());
                                    text.Span("Pag. ");
                                    text.CurrentPageNumber();
                                    text.Span(" de ");
                                    text.TotalPages();
                                });
                            });
                            #endregion

                            #region Pie de Pagina ultima pagina
                            col.Item().ShowIf(x => x.PageNumber == x.TotalPages).Layers(l =>
                            {
                                l
                                   .PrimaryLayer()
                                   .Height(48)
                                   .Image(piePaginaBg)
                                   .WithRasterDpi(72);

                                l.Layer().PaddingRight(10).AlignRight().AlignMiddle().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.Size(8).FontColor(Colors.White).SemiBold());
                                    text.Span("Pag. ");
                                    text.CurrentPageNumber();
                                    text.Span(" de ");
                                    text.TotalPages();
                                });
                            });
                            #endregion

                        });
                    });
                });

                // Generate the PDF Preview
                document.ShowInPreviewer();

                // Set the license key
                //QuestPDF.Settings.License = LicenseType.Community;

                // Generate the PDF bytes
                //byte[] pdfBytes = document.GeneratePdf();

                // Specify the desired output file path on disk (C drive in this case)
                //string outputFilePath = Path.Combine("C:", "\\Users\\ferna\\Desktop\\Loyal\\PDFMackup", "output.pdf"); // Replace with your folder and filename

                // Create the directory if it doesn't exist (optional)
                //Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath)); // Create parent directory if needed

                // Save the PDF to disk
                //using (FileStream fs = new FileStream(outputFilePath, FileMode.Create))
                //{
                //    fs.Write(pdfBytes, 0, pdfBytes.Length);
                //    Console.WriteLine($"PDF saved successfully to: {outputFilePath}");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");

            }

        }

        public static void PDFAfinidad()
        {

            try
            {
                var hideAgentName = true; //TODO: traer variable de configuracion al llamar el metodo
                var productType = 3; //TODO: traer variable de configuracion al llamar el metodo
                var datosCotizacion = 1; //TODO: traer datos de la cotizacion al llamar el metodo
                var datosPrimas = 1; //TODO: traer datos de las primas al llamar el metodo

                //Colores
                string beyondColor = "#326295";
                string priviligeColor = "#8dc341";
                string libertyColor = "#30b6b5";
                string legacyColor = "#841a66";

                //Imagenes
                var currentDirectory = Directory.GetCurrentDirectory();
                string piePaginaBg = Path.Combine(currentDirectory, "img", "pie_de_pagina.png");
                string logoLogoHeader = Path.Combine(currentDirectory, "img", "logoBgWhite.png");
                string piePaginaBgLine = Path.Combine(currentDirectory, "img", "pie_de_pagina_line.png");
                string logoAzul = Path.Combine(currentDirectory, "img", "logo_azul.png");

                int fontSize = 9;
                string versionPDF = "V.2-10.1.23C24-3";

                #region Textos Pagina 1
                string fueraUSA = "Fuera de USA";
                string dentroUSA = "Dentro de USA";
                string anual = "Anual";
                string semestral = "Semestral";
                string trimestral = "Trimestral";
                string deducible = "Deducible";
                string opcion = "Opción";
                string cubierto = "Cubierto";
                string noAplica = "No Aplica";
                string total = "Total";
                string nombreTitular = "Nombre del Titular:";
                string edadTitular = "Edad del Titular:";
                string edadConyugue = "Edad del Cónyugue:";
                string numeroAdultos = "Número de Adultos:";
                string numeroNinos = "Número de Niños:";
                string pais = "País:";
                string trasplateOrganos = "Trasplante de Órganos:";
                string complicacionesMaternidadP1 = "Complicaciones de Maternidad:";
                string agente = "Agente:";
                string codigo = "Código:";
                string telefono = "Teléfono:";
                string email = "C. Electrónico:";
                string fecha = "Fecha:";
                string hora = "Hora:";
                string cotizacionNro = "Cotización Nro.:";
                string notaPag1 = "Esta cotización es de carácter informativo y está sujeto a un análisis de riesgo\r\n*Solamente aplica un deducible por persona, por año póliza. Para pólizas de familia se aplicará un máximo de dos deducibles acumulados por póliza por año póliza.\r\nPara más información por favor referirse al Condicionado de cobertura de la póliza.\r\n";
                string currency = "$";
                string usd500 = currency + "500";
                string usd5000 = currency + "5.000";
                string usd1000 = currency + "1000";
                string usd10000 = currency + "10.000";
                string usd2000 = currency + "2000";
                string usd20000 = currency + "20.000";
                string usd30000 = currency + "30.000";
                string usd3000 = currency + "3000";
                #endregion

                #region Textos Pagina 2, 3, 4
                //Textos Banners
                //Pagina 2
                string notaBlack = "- Al menos de que se indique lo contrario todos los beneficios son por Año Póliza.";
                string descripcionCobertura = "DESCRIPCIÓN DE COBERTURA";
                string limiteCoberta = "LÍMITE DE COBERTURA";
                string hospitalizacion = "HOSPITALIZACIÓN";
                //Pagina 3
                string sercivioAmbulancia = "SERVICIO DE AMBULANCIA";
                string terapiasEspeciales = "TERAPIAS ESPECIALES / ALTERNATIVAS";
                string serviciosPreventivos = "SERVICIOS PREVENTIVOS Y ADICIONALES";
                string maternidad = "MATERNIDAD";
                //Pagina 4
                string servicioTransporte = "SERVICIOS DE TRAMPOSTACIÓN";
                string otrosBeneficios = "OTROS BENEFICIOS / SERVICIOS";
                //TODO: colocar las respuestas de los demas tipos de productos, actualmente solo tengo los de Privilege
                // Pagina 2
                string beneficiosMaximos = "Beneficio máximo";  //TODO: colocar recurso de idioma
                string elegibilidad = "Elegibilidad";  //TODO: colocar recurso de idioma
                string renovacion = "Renovación";  //TODO: colocar recurso de idioma
                string areaCobertura = "Área de cobertura";
                string tratamientoMedico = "Tratamiento médico para accidentes y emergencias fuera de la red"; //TODO: colocar recurso de idioma
                string opcionesDeducibles = "Opciones de deducibles\r\n- Por año póliza"; //TODO: colocar recurso de idioma
                string coaseguro = "Coaseguro"; //TODO: colocar recurso de idioma
                string periodoEspera = "Período de espera"; //TODO: colocar recurso de idioma
                string reduccionDeducible = "Reducción del deducible después de 3 años de no reclamo"; //TODO: colocar recurso de idioma
                string coberturaGratuita = "Cobertura gratuita para dependientes después del fallecimiento del titular"; //TODO: colocar recurso de idioma
                string servicioMedicoQuirurgico = "Servicios médicos y quirúrgicos";  //TODO: colocar recurso de idioma
                string honorariosMedicosMedicamentos = "Honorarios médicos y medicamentos ";  //TODO: colocar recurso de idioma
                string habitacionYAlimentacion = "Habitación y alimentación\r\n - Cuarto privado, semi-privado\r\n - Habitación estándar"; //TODO: colocar recurso de idioma
                string unidadDeCuidadosIntensivos = "Unidad de cuidados intensivos"; //TODO: colocar recurso de idioma
                string tratamientoDeCancer = "Tratamiento de cáncer (quimioterapia y radioterapia). Diálisis"; //TODO: colocar recurso de idioma
                string serviciosDeDiagnostico = "Servicios de diagnóstico (laboratorio, rayos X, resonancias magnéticas, TAC\r\n y ecografías)";
                string fisioterapiaRehabilitacion = "Fisioterapia / Rehabilitación"; // TODO: colocar recurso de idioma
                string alojamientoAcompananteMenor = "Alojamiento en el hospital para acompañante de un menor de edad"; // TODO: colocar recurso de idioma
                string cirugiaReduccionRiesgoCancer = "Cirugía de reducción de riesgo de cáncer o profiláctica\r\n -Período de espera\r\n -Pre-aprobado"; // TODO: colocar recurso de idioma
                string cirugiaBariatrica = "Cirugía bariátrica, de bypass gástrico y cualquier tipo de procedimiento quirúrgico\r\n destinado a la pérdida de peso y sus complicaciones/tratamientos.\r\n -Período de espera\r\n -Pre-aprobado"; // TODO: colocar recurso de idioma
                string cirugiaReconstructiva = "Cirugía reconstructiva en caso de accidente o malformaciones nasales sobre el septum\r\n - Medicamente necesario";
                string salaDeEmergencias = "Sala de emergencias";
                // Pagina 3
                string cirugiaAmbulatoria = "Cirugía ambulatoria";
                string servicioMedicoQuirurgicoP3 = "Servicios médicos / Quirúrgicos / Honorarios médicos";
                string medicamentos = "Medicamentos";
                string tratamientoDeCancerP3 = "Tratamiento de cáncer (quimioterapia y radioterapia). Diálisis";
                string serviciosDeDiagnosticoP3 = "Servicios de diagnóstico (laboratorio, rayos X, resonancias magnéticas, TAC\r\n y ecografías)";
                string fisioterapiaRehabilitacionP3 = "Fisioterapia / Rehabilitación\r\n - Pre-aprobado";
                string terapiaLenguaje = "Terapia de lenguaje";
                string consultaPsicologica = "Consulta psicológica";
                string chequeosMedicos = "Chequeos médicos de rutina anual\r\n - Sin deducible";
                string chequeoMedicosMenores = "Chequeo médico de rutina anual para menores de edad / Inmunizaciones\r\n - Sin deducible";
                string examenDentalPreventivo = "Examen dental preventivo\r\n - Período de espera 12 Meses\r\n - Sin deducible";
                string aparatosAuditivos = "Aparatos auditivos\r\n - Período de espera\r\n - Pre-aprobado";
                string servicioMaternidad = "Servicio de maternidad\r\n - Por cada maternidad cubierta\r\n - Período de espera por cada producto garantizado\r\n - Sin deducible";
                string complicacionesMaternidad = "Complicaciones de maternidad y/o complicaciones de nacimiento\r\n - Aplica deducible\r\n - Condiciones que sean resultados de un tratamiento de fertilidad están excluidos.";
                string preservacionCelulasMadre = "Preservación de células madre del cordón umbilical\r\n - Por cada recién nacido de una maternidad cubierta\r\n - Sin deducible";
                string maternidadDependientes = "Maternidad para dependientes\r\n - Solo aplica para aquellas dependientes que son >= 18 hasta 24 años";
                string inclusionAutomaticaMenorRecienNacido = "Inclusión automática de recién nacido a la póliza\r\n - Se tiene que notificar e incluir dentro de la póliza en los primeros 90 días después del nacimiento\r\n - Solo recién nacidos de una maternidad cubierta";
                // Pagina 4
                string ambulanciaLocal = "Ambulancia local\r\n -Sin deducible\r\n -Pre-aprobado";
                string ambulanciaAerea = "Ambulancia aérea\r\n -Sin deducible\r\n -Pre-aprobado";
                string repatriacionRestos = "Repatriación de restos mortales o servicio de cremación\r\n - Pre-aprobado";
                string asistenciaViaje = "Asistencia de viaje de regreso al país de residencia si es evacuado\r\n - Para el paciente y un acompañante ";
                string actividadesPeligrosas = "Actividades peligrosas y/o profesionales";
                string tratamientoDental = "Tratamiento dental de emergencia después de un accidente\r\n - Aplica deducible";
                string condicionesCognitivas = "Condiciones congénitas y hereditarias diagnosticadas antes de los 18 años\r\n - Nacidos en una maternidad cubierta";
                string condicionescognitivasHereditarias = "Condiciones congénitas y hereditarias diagnosticadas a partir de 18 años";
                string procedimientosTransplantes = "Procedimientos de trasplantes\r\n -Pre-aprobado";
                string equiposMedicos = "Equipos médicos y dispositivos ortopédicos\r\n -Pre-aprobado";
                string atencionMedicaDomiciliaria = "Atención médica domiciliaria (Solo para Venezuela)\r\n - Consulta sin deducible\r\n - Medicamentos y paraclínicos aplica deducible\r\n - Pre-aprobado";
                string telemedicina = "Telemedicina\r\n -Sin deducible";
                //Respuestas
                // Pagina 2
                string resBeneficiosMaximos = "$5.000.000";
                string resElegibilidad = "74 años";
                string resRenovacion = "Vitalicio";
                string resAreaCobertura = "Mundial";
                string resTratamientoMedico = "100% UCR";
                string resOpcionesDeducibles = "I. $500/$1,000 II. $1,000/$2,000 III. $2,000/$3,000 IV. $5,000 V. $10,000 VI. $20,000";
                string resCoaseguro = noAplica;
                string resPeriodoEspera = "60 días / 2 mes";
                string resReduccionDeducible = "Opciones I, II y III: eliminación del deducible por 1 año después del 3er año sin reclamos\r\nOpciones IV, V y VI: reducción del deducible hasta 60% por 1 año después del 3er año sin\r\nreclamos";
                string resCoberturaGratuita = "1 año de exoneración de prima";
                string resServicioMedicoQuirurgico = "100% UCR";
                string resHonorariosMedicosMedicamentos = "100% UCR";
                string resHabitacionYAlimentacion = "100% UCR";
                string resUnidadDeCuidadosIntensivos = "100% UCR";
                string resTratamientoDeCancer = "100% UCR Opciones I, II, III y IV";
                string resServiciosDeDiagnostico = "100% UCR";
                string resFisioterapiaRehabilitacion = "100% UCR";
                string resAlojamientoAcompananteMenor = "$250 por noche, máximo 30 noches";
                string resCirugiaReduccionRiesgoCancer = "$15,000 de por vida / 48 meses de periodo de espera";
                string resCirugiaBariatrica = "$7,000 de por vida / 18 meses de periodo de espera";
                string resCirugiaReconstructiva = "100% UCR";
                string resSalaDeEmergencias = "100% UCR";
                // Pagina 3
                string resCirugiaAmbulatoria = "100% UCR";
                string resServicioMedicoQuirurgicoP3 = "100% UCR";
                string resMedicamentos = "100% UCR";
                string resTratamientoDeCancerP3 = "100% UCR Opciones I, II, III y IV";
                string resServiciosDeDiagnosticoP3 = "100% UCR Opciones I, II, III y IV";
                string resFisioterapiaRehabilitacionP3 = "$12,000";
                string resTerapiaLenguaje = "$3,000";
                string resConsultaPsicologica = "9 visitas por año / 100% UCR";
                string resChequeosMedicos = "$350 por asegurado, por año póliza";
                string resChequeoMedicosMenores = "< 1 año = $150 por visita, hasta 5 visitas\r\n 1 a 17 años = $200 por año póliza";
                string resExamenDentalPreventivo = "$100 dentro del beneficio de chequeo médico de rutina";
                string resAparatoAuditivos = "$1,500 de por vida, 24 meses de periodo de espera";
                string resServicioMaternidad = "Parto normal: $6,000 Cesárea: $7,000\r\n - 14 meses de período de espera\r\n - Opciones I, II, y III";
                string resComplicacionesMaternidad = "Endoso: $150,000 adicional de por vida\r\n - 18 meses de período de espera\r\n - Opciones I y II";
                string resPreservacionCelulasMadre = "$1,000 Opciones I y II";
                string resMaternidadDependientes = "Parto normal: $800 Cesárea: $1000\r\n - 18 meses de período de espera\r\n - Opciones I y II";
                string resInclusionAutomaticaMenorRecienNacido = "Sin selección de riesgos";
                // Pagina 4
                string resAmbulanciaLocal = "100% UCR";
                string resAmbilanciaAerea = "$ 60,000";
                string resRepatriacionRestos = "$12,000";
                string resAsistenciaViaje = "$1,500 por persona";
                string resActividadesPeligrosas = "100% UCR (profesional o amateur)";
                string resTratamientoDental = "100% UCR";
                string resCondicionesCognitivas = "$75,000 UCR de por vida";
                string resCondicionesCognitivasHereditarias = "$150,000 UCR de por vida";
                string resProcedimientosTransplantes = "$350,000 (diagnóstico de por vida)\r\n $50,000 (máximo para preparación del donante)\r\n - Opciones: I, II y III\r\n ENDOSO para opciones: lV, V y VI";
                string resEquiposMedicos = "$12,000";
                string resAtencionMedicaDomiciliaria = "100% UCR\r\n - Opciones I, II, III y IV";
                string resTelemedicina = "100% UCR";
                #endregion

                string beneficios = "BENEFICIOS";

                string docColor = "";
                string textImg = "";
                string beneficiosTipo = "";
                switch (productType)
                {
                    case 1:
                        docColor = priviligeColor;
                        textImg = "PRIVILEGE-txt.png";
                        beneficiosTipo = beneficios + " PRIVILEGE";
                        break;
                    case 2:
                        docColor = libertyColor;
                        textImg = "LIBERTY-txt.png";
                        beneficiosTipo = beneficios + " LIBERTY";
                        resBeneficiosMaximos = "$10.000.000";
                        break;
                    case 3:
                        docColor = legacyColor;
                        textImg = "LEGACY-txt.png";
                        beneficiosTipo = beneficios + " LEGACY";
                        resBeneficiosMaximos = "100% UCR Dentro de la Red LOYAL USA Major Medical Services / 60% UCR fuera\r\nde la red";
                        break;
                    default:
                        docColor = beyondColor;
                        textImg = "BEYOND-txt.png";
                        beneficiosTipo = beneficios + " BEYOND";
                        resBeneficiosMaximos = "$10.000.000";
                        break;
                }

                var imgText = Path.Combine(currentDirectory, "img", textImg);

                // code in your main method
                var document = Document.Create(container =>
                {

                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter); //tipo de hoja A4
                        page.Margin(30);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontFamily("Lato"));


                        IContainer DefaultCellStyle(IContainer container, string backgroundColor, int border = 1, int pV = 1)
                        {
                            return container
                                .Border(border)
                                .BorderColor(Colors.Black)
                                .Background(backgroundColor)
                                .PaddingVertical(pV)
                                .PaddingHorizontal(5)
                                .AlignCenter()
                                .AlignMiddle();
                        }

                        page.Header().Column(column =>
                        {
                            #region Header Pagina 1 
                            var maxHeight = 120;
                            var maxWidth = 158;
                            column.Item().ShowOnce().Height(maxHeight).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().Width(maxWidth).Height(maxHeight)
                                .AspectRatio(1 / 2)
                                .Image(logoLogoHeader)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                                //table.Cell();
                                table.Cell().ColumnSpan(2).AlignRight().PaddingRight(20).PaddingTop(50).Width(237).Height(40)
                                .AspectRatio(1 / 6)
                                .Image(imgText)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });


                            //Inicio Pagina 1
                            //Textos Pagina 1
                            //foreach (var item in datosCotizacion)
                            //{
                            //    string agentName = item.NombreAngente;
                            //    if (hideAgentName)
                            //    {
                            //       agentName = "------";
                            //    }

                            //}
                            //Datos del Titular
                            column.Item().ShowOnce().PaddingTop(5).PaddingLeft(15).Table(table =>
                            {

                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                //Row1
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(nombreTitular).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(agente).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row2
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(edadTitular).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(codigo).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row3
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(numeroAdultos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(telefono).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row4
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(edadConyugue).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(email).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row5
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(numeroNinos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(fecha).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row6
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(pais).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(hora).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row7
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(trasplateOrganos).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(2).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(cotizacionNro).FontSize(fontSize).SemiBold();
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);
                                //Row8
                                table.Cell().Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text(complicacionesMaternidadP1).FontSize(fontSize).SemiBold();
                                table.Cell().ColumnSpan(4).Element(CellStyleLessBorderTable).ExtendHorizontal().AlignLeft().Text("respuesta de").FontSize(fontSize);

                                IContainer CellStyleLessBorderTable(IContainer container) => DefaultCellStyle(container, Colors.White, 0, 0);
                            });
                            #endregion

                            #region Header Resto de las paginas
                            column.Item().SkipOnce().Height(maxHeight).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().ColumnSpan(3).AlignCenter().Width(maxWidth).Height(maxHeight)
                                .AspectRatio(1 / 2)
                                .Image(logoLogoHeader)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });
                            #endregion
                        });

                        page.Content().PaddingTop(10).Column(column =>
                        {
                            #region Pagina 1
                            var celMaxHeight = 11;
                            //Tabla deducibles
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(deducible).AlignStart().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " I").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " II").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " III").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " IV").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " V").FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).Text(opcion + " VI").FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor);
                                });

                                //Row 1
                                table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(fueraUSA).AlignStart().FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd500).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd1000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd2000).FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd5000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd10000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                table.Cell().RowSpan(2).Element(CellStyle).Text(usd20000).LineHeight(2).AlignCenter().FontSize(fontSize);
                                //Row 2
                                table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(dentroUSA).AlignStart().FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd1000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd2000).FontSize(fontSize);
                                table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd3000).FontSize(fontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            //Tabla Anual, Semestral, Trimestal
                            for (int i = 0; i < 3; i++)
                            {
                                var TextHead = "";
                                if (i == 2)
                                {
                                    TextHead = trimestral;
                                }
                                else if (i == 1)
                                {
                                    TextHead = semestral;
                                }
                                else
                                {
                                    TextHead = anual;
                                }

                                column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().ColumnSpan(8).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(TextHead).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor);
                                    });



                                    for (int j = 0; j < 7; j++)
                                    //foreach (var item in args) //  TODO: cambiar por el foreach de los datos 
                                    {
                                        table.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(fueraUSA).AlignStart().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text(usd500).FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("1000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("2000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("500").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("1000").AlignCenter().FontSize(fontSize);
                                        table.Cell().Element(CellStyle).Height(celMaxHeight).Text("2000").AlignCenter().FontSize(fontSize);

                                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                                    }
                                });
                            }
                            //Version PDF
                            column.Item().PaddingTop(2).PaddingRight(20).Background(Colors.White).AlignRight().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).Light());
                                text.Span(versionPDF);
                            });
                            //Nota
                            column.Item().PaddingTop(10).PaddingLeft(20).PaddingRight(15).Background(Colors.White).AlignLeft().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).SemiBold());
                                text.Span(notaPag1);
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 1
                            #endregion

                            #region Pagina 2
                            var borderTb = 1;
                            var pV = 2;
                            var newCelHeight = 20;
                            var newFontSize = 7;
                            //Inicio Pagina 2
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            //Tabla DESCRIPCION DE COBERTURA / LIMITE DE COBERTURA
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(descripcionCobertura).FontSize(fontSize).Bold().FontColor(Colors.White);
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(limiteCoberta).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(notaBlack).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Black, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(beneficiosMaximos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resBeneficiosMaximos).FontSize(6);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(elegibilidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resElegibilidad).FontSize(6);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(renovacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resRenovacion).FontSize(6);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(areaCobertura).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAreaCobertura).FontSize(6);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoMedico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoMedico).FontSize(6);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(opcionesDeducibles).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resOpcionesDeducibles).FontSize(6);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(coaseguro).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCoaseguro).FontSize(6);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(periodoEspera).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resPeriodoEspera).FontSize(6);
                                //Row 9
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(reduccionDeducible).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resReduccionDeducible).FontSize(6);
                                //Row 10
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(coberturaGratuita).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCoberturaGratuita).FontSize(6);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            //Tabla HOSPITALIZACIÓN
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(hospitalizacion).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMedicoQuirurgico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMedicoQuirurgico).FontSize(6);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(honorariosMedicosMedicamentos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resHonorariosMedicosMedicamentos).FontSize(6);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(habitacionYAlimentacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resHabitacionYAlimentacion).FontSize(6);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(unidadDeCuidadosIntensivos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resUnidadDeCuidadosIntensivos).FontSize(6);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDeCancer).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDeCancer).FontSize(6);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(serviciosDeDiagnostico).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServiciosDeDiagnostico).FontSize(6);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(fisioterapiaRehabilitacion).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resFisioterapiaRehabilitacion).FontSize(6);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(alojamientoAcompananteMenor).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAlojamientoAcompananteMenor).FontSize(6);
                                //Row 9
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaReduccionRiesgoCancer).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaReduccionRiesgoCancer).FontSize(6);
                                //Row 10
                                table.Cell().Element(CellStyle).Height(45).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaBariatrica).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(45).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaBariatrica).FontSize(6);
                                //Row 11
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaReconstructiva).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaReconstructiva).FontSize(6);
                                //Row 12
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(salaDeEmergencias).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resSalaDeEmergencias).FontSize(6);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 2
                            #endregion

                            #region Pagina 3
                            //Inicio Pagina 3
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(sercivioAmbulancia).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(cirugiaAmbulatoria).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCirugiaAmbulatoria).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMedicoQuirurgicoP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMedicoQuirurgicoP3).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(medicamentos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resMedicamentos).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDeCancerP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDeCancerP3).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(serviciosDeDiagnosticoP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServiciosDeDiagnosticoP3).FontSize(newFontSize);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(fisioterapiaRehabilitacionP3).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resFisioterapiaRehabilitacionP3).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(terapiasEspeciales).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(terapiaLenguaje).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTerapiaLenguaje).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(consultaPsicologica).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resConsultaPsicologica).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(serviciosPreventivos).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(chequeosMedicos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resChequeosMedicos).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(chequeoMedicosMenores).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resChequeoMedicosMenores).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(examenDentalPreventivo).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resExamenDentalPreventivo).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(aparatosAuditivos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAparatoAuditivos).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(maternidad).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(servicioMaternidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resServicioMaternidad).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(complicacionesMaternidad).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resComplicacionesMaternidad).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(preservacionCelulasMadre).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resPreservacionCelulasMadre).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(maternidadDependientes).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resMaternidadDependientes).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(inclusionAutomaticaMenorRecienNacido).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resInclusionAutomaticaMenorRecienNacido).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });

                            column.Item().PageBreak();
                            //Fin Pagina 3
                            #endregion

                            #region Pagina 4
                            //Inicio Pagina 4
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();

                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().Text(beneficiosTipo).AlignCenter().FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });
                            });
                            column.Item().PaddingTop(5).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(servicioTransporte).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(ambulanciaLocal).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAmbulanciaLocal).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(ambulanciaAerea).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(30).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAmbilanciaAerea).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(repatriacionRestos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resRepatriacionRestos).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(asistenciaViaje).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAsistenciaViaje).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            column.Item().PaddingTop(0).PaddingHorizontal(20).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().ColumnSpan(2).Element(CellStyle).Height(celMaxHeight).ExtendHorizontal().AlignCenter().AlignMiddle().Text(otrosBeneficios).FontSize(fontSize).Bold().FontColor(Colors.White);

                                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, docColor, borderTb, 2);
                                });

                                //Row 1
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(actividadesPeligrosas).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resActividadesPeligrosas).FontSize(newFontSize);
                                //Row 2
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(tratamientoDental).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTratamientoDental).FontSize(newFontSize);
                                //Row 3
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(condicionesCognitivas).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCondicionesCognitivas).FontSize(newFontSize);
                                //Row 4
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(condicionescognitivasHereditarias).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(15).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resCondicionesCognitivasHereditarias).FontSize(newFontSize);
                                //Row 5
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(procedimientosTransplantes).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resProcedimientosTransplantes).FontSize(newFontSize);
                                //Row 6
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(equiposMedicos).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resEquiposMedicos).FontSize(newFontSize);
                                //Row 7
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(atencionMedicaDomiciliaria).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(40).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resAtencionMedicaDomiciliaria).FontSize(newFontSize);
                                //Row 8
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(telemedicina).FontSize(newFontSize);
                                table.Cell().Element(CellStyle).Height(20).ExtendHorizontal().AlignLeft().AlignMiddle().Text(resTelemedicina).FontSize(newFontSize);

                                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                            });
                            //Version PDF
                            column.Item().PaddingTop(2).PaddingRight(20).Background(Colors.White).AlignRight().AlignMiddle().Text(text =>
                            {
                                text.DefaultTextStyle(x => x.Size(8).Light());
                                text.Span(versionPDF);
                            });
                            column.Item().PaddingTop(30).Height(136).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().ColumnSpan(3).AlignCenter().Width(180).Height(136)
                                .AspectRatio(1 / 2)
                                .Image(logoAzul)
                                .WithCompressionQuality(ImageCompressionQuality.High);
                            });
                            //fin Pagina 4
                            #endregion
                        });


                        page.Footer().Column(col =>
                        {
                            #region Pie de Pagina Linea fina
                            col.Item().ShowIf(x => x.PageNumber != x.TotalPages).Layers(l =>
                            {
                                l
                                   .PrimaryLayer()
                                   .Height(18)
                                   .Image(piePaginaBgLine)
                                   .WithRasterDpi(300);

                                l.Layer().PaddingRight(10).AlignRight().AlignMiddle().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontSize(8).FontColor(Colors.White).SemiBold());
                                    text.Span("Pag. ");
                                    text.CurrentPageNumber();
                                    text.Span(" de ");
                                    text.TotalPages();
                                });
                            });
                            #endregion

                            #region Pie de Pagina ultima pagina
                            col.Item().ShowIf(x => x.PageNumber == x.TotalPages).Layers(l =>
                            {
                                l
                                   .PrimaryLayer()
                                   .Height(48)
                                   .Image(piePaginaBg)
                                   .WithRasterDpi(72);

                                l.Layer().PaddingRight(10).AlignRight().AlignMiddle().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.Size(8).FontColor(Colors.White).SemiBold());
                                    text.Span("Pag. ");
                                    text.CurrentPageNumber();
                                    text.Span(" de ");
                                    text.TotalPages();
                                });
                            });
                            #endregion

                        });
                    });
                });

                // Generate the PDF Preview
                document.ShowInPreviewer();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");

            }

        }
    }
}
