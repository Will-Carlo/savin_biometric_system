using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            int idTurno = this.capturaIdTurno();
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
                    return this.capturaMinutosAtrasoEntrada();
                }
                else if (indMov == 462)
                {
                    return this.capturaMinutosAtrasoSalida();
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
                var ultimoRegistro = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal)
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
        private int capturaIdTurno()
        {
            if (EsSabado())
            {
                return 3;
            }
            // queda pendiente validar los feriados
            else if(EsFeriado())
            {
                return 1;
            }
            //else if(EsSalidaExtra())
            //{
            //    return TipoSalidaExtra();
            //}
            else
            {
                var horaActual = DateTime.Now;
                var limiteHora = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);

                return horaActual < limiteHora ? 1 : 2;
            }

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
            return false;
        }
        // -------------------------------------------------------------------
        // SALIDAS EXTRAS
        private bool EsSalidaExtra(int IdPersonal)
        {
            if(this.getAnteriorIdTurno(IdPersonal) == this.capturaIdTurno() && this.getAnteriorIndMov(IdPersonal) == 462)
            {
                return true;
            }
            return false;
        }
        private int getAnteriorIdTurno(int IdPersonal)
        {
            int ultimoIdTurno = 0; // Valor por defecto si no se encuentra ningún registro

            using (var dbContext = new StoreContext()) // Reemplaza 'TuDbContext' con el nombre de tu clase DbContext
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
        private int capturaMinutosAtrasoEntrada()
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno();
            int MinTol = _functionsDataBase.MinutosDeTolerancia();

            // definimos los hoarios de entrada
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30 + MinTol, 0);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30 + MinTol, 0);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00 + MinTol, 0);


            // comparamos y hacemos la sumatoria de minutos
            if (idTurno == 1)
            {
                // 08:30:00
                return horaMarcada > tiempoInicioTurno1 ? (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes : 0;
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
        private int capturaMinutosAtrasoSalida()
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno();

            // definimos los hoarios de entrada
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
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