using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Frm
{
    internal class MetodosAsistencia
    {
        private ApiService.FunctionsDataBase _functionsDataBase = new FunctionsDataBase();
        private readonly ApiService.ApiService _apiService = new ApiService.ApiService();


        private string _capturaHoraMarcado = "";
        // PROCEDIMIENTOS PARA EL REGISTRO DE ASISTENCIA
        public int getIdTurno(int idPersonal)
        {
            int idTurno = this.capturaIdTurno(idPersonal);
            if(validarTurno(idPersonal, idTurno))
            {
                return idTurno;
            }
            else
            {
                MessageBox.Show("El personal no tiene asignado el turno: " + turnoNombre2(idTurno));
                return 0;
            }
        
        }
        public string getHoraMarcado()
        {
            return _capturaHoraMarcado;
        }
        public int getMinutosAtraso(int IdPersonal)
        {
            // capturamos el tipo de turno para sumar si se atrasa o si sale antes
            int indMov = capturaTipoMovimiento(IdPersonal);
            if (!EsSalidaExtra(IdPersonal))
            {
                if (indMov == 461)
                {
                    return this.capturaMinutosAtrasoEntrada(IdPersonal);
                }
                else if (indMov == 462)
                {
                    return this.capturaMinutosAtrasoSalida(IdPersonal);
                }
            }
            else
            {
                return this.capturaMinutosSalidaExtra(IdPersonal);
            }
            

            return 0;
        }
        public int getIndTipoMovimiento(int IdPersonal)
        {
            return this.capturaTipoMovimiento(IdPersonal);
        }
        public int getIdPuntoAsistencia()
        {
           return this.capturaIdPuntoAsistencia();
        }
        // -------------------------------------------------------------------
        // PROCEDIMIENTOS PARA VALIDAR EN CLASES DE CÓDIGO Y HUELLA
        public int capturaTipoMovimiento(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                // Capturamos la fecha de hoy y la convertimos a su representación en cadena
                string fechaHoyStr = DateTime.Today.ToString("yyyy-MM-dd");

                var ultimoRegistro = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal &&
                                 a.HoraMarcado.StartsWith(fechaHoyStr))
                    .OrderByDescending(a => a.Id) // Asumiendo que 'Id' es un autoincremento y representa el más reciente.
                    .Select(a => a.IndTipoMovimiento)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                // Si el valor es 1, devolver 2. Si el valor es 2 o no hay registros, devolver 1.
                return ultimoRegistro == 461 ? 462 : 461;
            }
        }
        public void setCapturaHoraMarcado(string capturaHoraMarcado)
        {
            _capturaHoraMarcado = capturaHoraMarcado;
        }
        public string getHora()
        {
            DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
            return getDateToString.ToString("HH:mm:ss");
        }
        public void setAddAsistencia(RrhhAsistencia item)
        {
            using (var db = new StoreContext())
            {
                db.RrhhAsistencia.Add(item);
                db.SaveChanges();
            }
        }
        // -------------------------------------------------------------------

        private int capturaIdPuntoAsistencia()
        {
            using (var context = new StoreContext())
            {
                var ultimoRegistro = context.RrhhPuntoAsistencia
                    .Select(a => a.Id)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                return ultimoRegistro;
            }
        }
        private int capturaIdTurno(int IdPersonal)
        {
            var horaActual = DateTime.Parse(this._capturaHoraMarcado);
            var AntesDeLas_14_29 = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 14, 29, 0);
            var EsDespuesDe_12_30 = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 12, 30, 0);
            //MessageBox.Show("Hora Actual: " + horaActual + "\nAntes de las 14:29: " + AntesDeLas_14_29 + "\nDespués de 12:30: " + EsDespuesDe_12_30);
            //var horaActual;
            if (EsSabado())
            {
                return 3;
            }
            else
            {
                if (this.existeAnteriorIdTurno(IdPersonal))
                {
                    if (this.getAnteriorIdTurno(IdPersonal) == 1)
                    {
                        if (horaActual < AntesDeLas_14_29)
                        {
                            if (horaActual > EsDespuesDe_12_30 && this.getIndTipoMovimiento(IdPersonal) == 462)
                            {
                                // Aquí es clave para el cambio de turno al medio día
                                return 1;
                            }
                            else
                            {
                                return 2;
                            }

                        }
                        else
                        {
                            //MessageBox.Show("Aquí 2");
                            return 2;
                        }

                    } else if(this.getAnteriorIdTurno(IdPersonal) == 2){
                        return 2;
                    }
                }
                else
                {
                    if (horaActual > EsDespuesDe_12_30)
                    {
                        //MessageBox.Show("Aquí 3");
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                //var horaActual = DateTime.Now;
                //var limiteHora = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);
                //return horaActual < limiteHora ? 1 : 2;
            }
            return 0;
        }
        private bool validarTurno(int idPersonal, int idTurno)
        {
            using (var context = new StoreContext())
            {
                // Busca si existe un turno asignado que coincida con el idPersonal y idTurno
                var asignacion = context.RrhhTurnoAsignados
                                        .Any(ta => ta.IdPersonal == idPersonal && ta.IdTurno == idTurno);

                return asignacion; // True si existe la asignación, False de lo contrario
            }
        }
        private string turnoNombre2(int idTurno)
        {
            switch (idTurno)
            {
                case 1: return "MAÑANA";
                case 2: return "TARDE";
                case 3: return "SÁBADO";
                case 4: return "SAlIDA EXTRA MAÑNA";
                case 5: return "SAlIDA EXTRA TARDE";
            }
            return "";
        }
        private bool EsSabado()
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Saturday;
        }
        private bool EsFeriado()
        {

            DateTime fechaHora = DateTime.Now;
            DateOnly fechaActual = new DateOnly(fechaHora.Year, fechaHora.Month, fechaHora.Day);


            try
            {
                using (var context = new StoreContext())
                {
                    // Obtener la lista de feriados
                    List<RrhhFeriado> feriados = context.RrhhFeriados.ToList();

                    // Verificar si alguna fecha de feriado coincide con la fecha actual
                    foreach (var feriado in feriados)
                    {
                        if (feriado.Fecha == fechaActual) // Error CS0019 aquí
                        {
                            return true; // Hay un feriado en la fecha actual
                        }
                    }
                }

                return false; // No hay feriado en la fecha actual
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción que ocurra durante la consulta
                Console.WriteLine($"Error al comprobar feriados: {ex.Message}");
                return false;
            }
        }
        // -------------------------------------------------------------------
        // SALIDAS EXTRAS
        private bool EsSalidaExtra(int IdPersonal)
        {
            if(this.getAnteriorIdTurno(IdPersonal) == this.capturaIdTurno(IdPersonal) && this.getAnteriorIndMov(IdPersonal) == 462)
            {
                return true;
            }
            return false;
        }
        private int getAnteriorIdTurno(int IdPersonal)
        {
            int ultimoIdTurno = 0; // Valor por defecto si no se encuentra ningún registro

            using (var dbContext = new StoreContext()) 
            {
                // Consulta para obtener el ID del último registro para el IdPersonal dado
                var ultimoRegistro = dbContext.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal)
                    .OrderByDescending(a => a.Id) // Ordenar por el ID en orden descendente para obtener el último registro
                    .FirstOrDefault();

                if (ultimoRegistro != null)
                {
                    ultimoIdTurno = ultimoRegistro.IdTurno;
                }
            }

            return ultimoIdTurno;
        }
        private int getAnteriorIndMov(int IdPersonal)
        {
            int indTipoMovimiento = 0; // Valor predeterminado en caso de no encontrar registro

            using (var context = new StoreContext())
            {
                // Consulta LINQ para obtener el ind_tipo_movimiento del último registro para el IdPersonal dado
                var ultimoRegistro = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal)
                    .OrderByDescending(a => a.Id)
                    .FirstOrDefault(); // Obtiene el primer elemento o null si no hay registros

                if (ultimoRegistro != null)
                {
                    indTipoMovimiento = (int)ultimoRegistro.IndTipoMovimiento;
                }
            }

            return indTipoMovimiento;
        }
        private int capturaMinutosSalidaExtra(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado); // 16:00:00
            DateTime horaAnterior = DateTime.Parse(this.getAnteriorHora(IdPersonal)); // 15:00:00

            // Hacemos las modificaciones en la base de datos local y en la API
            _functionsDataBase.ModificarHoraMarcado(IdPersonal, this.getAnteriorHora(IdPersonal), this.capturaIdPuntoAsistencia());
            _apiService.ModificarAsistenciaAsync(IdPersonal, this.getAnteriorHora(IdPersonal), this.capturaIdPuntoAsistencia());
            
            // comparamos y hacemos la sumatoria de minutos
            return  (int)(horaMarcada - horaAnterior).TotalMinutes;
            
            //return 69;
        }
        private string getAnteriorHora(int IdPersonal)
        {
            string horaMarcado = "";

            using (var dbContext = new StoreContext())
            {
                // Consulta para obtener el último registro de asistencia para el idPersonal dado
                var ultimoRegistro = dbContext.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal)
                    .OrderByDescending(a => a.Id)
                    .FirstOrDefault();

                // Verificar si se encontró un registro
                if (ultimoRegistro != null)
                {
                    horaMarcado = ultimoRegistro.HoraMarcado;
                }
            }

            return horaMarcado;
        }
        // -------------------------------------------------------------------
        // MINUTOS DE ATRASOS DE ENTRADA Y SALIDA
        private int capturaMinutosAtrasoEntrada(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno(IdPersonal);
            int MinTol = _functionsDataBase.MinutosDeTolerancia();
            //MessageBox.Show("MinTol:" + MinTol);
            //int MinTol = 0;


            // definimos los hoarios de entrada
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
            tiempoInicioTurno1 = tiempoInicioTurno1.AddMinutes(MinTol);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
            tiempoInicioTurno2 = tiempoInicioTurno2.AddMinutes(MinTol);
            //MessageBox.Show("entrada tarde:" + tiempoInicioTurno2);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00, 0);
            tiempoInicioTurno3 = tiempoInicioTurno3.AddMinutes(MinTol);



            // comparamos y hacemos la sumatoria de minutos
            if (idTurno == 1)
            {
                // 08:30:00
                return horaMarcada > tiempoInicioTurno1 ? this.alertPersonal(horaMarcada, tiempoInicioTurno1, IdPersonal, "Estás llegando tarde") : 0;
            }
            else if (idTurno == 2 )
            {
                // 14:30:00
                return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
            }
            else if (idTurno == 3)
            {
                // 09:00:00
                return horaMarcada > tiempoInicioTurno3 ? (int)(horaMarcada - tiempoInicioTurno3).TotalMinutes : 0;
            }

            // por default
            return 0;
        }
        private int capturaMinutosAtrasoSalida(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno(IdPersonal);

            // definimos los hoarios de salida
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 12, 30, 0);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 19, 00, 0);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 13, 00, 0);


            // comparamos y hacemos la sumatoria de minutos
            if (idTurno == 1)
            {
                // 08:30:00
                return horaMarcada < tiempoInicioTurno1 ? (int)(tiempoInicioTurno1 - horaMarcada).TotalMinutes : 0;
            }
            else if (idTurno == 2)
            {
                // 14:30:00
                return horaMarcada < tiempoInicioTurno2 ? (int)(tiempoInicioTurno2 - horaMarcada).TotalMinutes : 0;
            }
            else if (idTurno == 3)
            {
                // 09:00:00
                return horaMarcada < tiempoInicioTurno3 ? (int)(tiempoInicioTurno3 - horaMarcada).TotalMinutes : 0;
            }

            // por default
            return 0;
        }

        // NOTIFICACIONES
        private int alertPersonal(DateTime horaMarcada, DateTime tiempoInicioTurno1, int IdPersonal, String MessageAlert)
        {

            int min = (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes;
            MessageBox.Show(this.NombrePersonal(IdPersonal) + "estás llegando tarde por "+ min + " minutos.");
            return min;
        }

        public string NombrePersonal(int idPersonal)
        {
            string nombreCompleto = "";

            // Suponiendo que dbContext es tu instancia de DbContext
            using (var dbContext = new StoreContext())
            {
                // Buscar el personal por su id
                var personal = dbContext.RrhhPersonals
                                        .FirstOrDefault(p => p.Id == idPersonal);

                // Verificar si se encontró el personal
                if (personal != null)
                {
                    // Construir el nombre completo
                    nombreCompleto = $"{personal.Nombres} {personal.Paterno} {personal.Materno}";
                }
                else
                {
                    // Si no se encuentra el personal, puedes manejar esta situación
                    nombreCompleto = "Empleado no encontrado";
                }
            }

            return nombreCompleto;
        }
        // FUNCIONES PARA CAPTURAR EL IDTURNO
        // -------------------------------------------------------------------
        private bool existeAnteriorIdTurno(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy
                DateTime fechaHoy = DateTime.Today;

                // Convertir la fecha de hoy a su representación en cadena
                string fechaHoyStr = fechaHoy.ToString("yyyy-MM-dd");

                // Verificar si hay algún registro para la fecha de hoy
                bool existeRegistroHoy = context.RrhhAsistencia
                    .Any(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoyStr));

                return existeRegistroHoy;
            }
        }
        // REGISTRAR FALTAS
        // -------------------------------------------------------------------
        public void RegistrarFaltasDelDia()
        {
            if (!EsFeriado())
            {
                using (var context = new StoreContext())
                {
                    // Obtener la fecha de hoy
                    DateTime fechaHoy = DateTime.Today;
                    DateTime fechaHoy2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 00, 01);


                    // Consulta para obtener los IdPersonal que no tienen registros en RrhhAsistencia para la fecha de hoy
                    var idPersonalSinAsistencia = context.RrhhPersonals
                        .Where(personal =>
                            !context.RrhhAsistencia.Any(asistencia =>
                                asistencia.IdPersonal == personal.Id &&
                                asistencia.HoraMarcado == fechaHoy.ToString("yyyy-MM-dd")
                            )
                        )
                        .Select(personal => personal.Id)
                        .ToList();

                    // Imprimir los IdPersonal que cumplen con la condición
                    //Console.WriteLine("IdPersonal sin asistencia para hoy:");
                    foreach (var idPersonal in idPersonalSinAsistencia)
                    {
                        RrhhAsistencia auxAsis = new RrhhAsistencia
                        {
                            IdTurno = 2,
                            IdPersonal = idPersonal,
                            HoraMarcado = fechaHoy2.ToString(),
                            MinutosAtraso = 600,
                            IndTipoMovimiento = 461,
                            IdPuntoAsistencia = getIdPuntoAsistencia()
                        };
                        _apiService.RegistrarAsistenciaAsync( auxAsis );
                        //Console.WriteLine(idPersonal);
                    }
                }
            }
        }
    }
}

        //public int capturaMinAtraso()
        //{
        //    // Parse the captured time string to a DateTime object
        //    DateTime horaMarcada = DateTime.Parse(_capturaHoraMarcado);

        //    // Define the comparison times
        //    var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
        //    var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
        //    var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00, 0);


        //    // Get the values from the other functions
        //    int idTurno = capturaIdTurno();
        //    int indMov = capturaIndMov();

        //    // Calculate the exceeded minutes according to the conditions
        //    if (idTurno == 1 && indMov == 461)
        //    {
        //        return horaMarcada > tiempoInicioTurno1 ? (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes : 0;
        //    }
        //    else if (idTurno == 2 && indMov == 461)
        //    {
        //        return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
        //    }
        //    else if (idTurno == 3 && indMov == 461)
        //    {
        //        return horaMarcada > tiempoInicioTurno3 ? (int)(horaMarcada - tiempoInicioTurno3).TotalMinutes : 0;
        //    }

        //    return 0;
        //}

        //public int capturaIndMov()
        //{
        //    var horaActual = DateTime.Now;
        //    var medioDia = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 12, 0, 0);
        //    var tarde = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);
        //    var noche = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 18, 0, 0);

        //    if (horaActual < medioDia)
        //    {
        //        return 461;
        //    }
        //    else if (horaActual >= medioDia && horaActual < tarde)
        //    {
        //        return 462;
        //    }
        //    else if (horaActual >= tarde && horaActual < noche)
        //    {
        //        return 461;
        //    }
        //    else
        //    {
        //        return 462;
        //    }
        //}
        
        // capturamos su tipo de movimiento, salida o entrada
        // aquí capturamos si es 1, 2 o 3, los feriados no está validados
        //public string turnoNombre(int idTurno)
        //{
        //    using (var context = new StoreContext())
        //    {
        //        var turno = context.RrhhTurnos.Find(idTurno);
        //        return turno?.Nombre; // Devuelve el nombre si se encuentra el turno, o null si no se encuentra
        //    }
        //}

        //Automatiza el turno pendiente
