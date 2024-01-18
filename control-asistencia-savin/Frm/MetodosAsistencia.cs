﻿using control_asistencia_savin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Frm
{
    internal class MetodosAsistencia
    {
        private string _capturaHoraMarcado = "";
        public void setCapturaHoraMarcado(string capturaHoraMarcado)
        {
            _capturaHoraMarcado = capturaHoraMarcado;
        }
        public string getCapturaHoraMarcado()
        {
            return _capturaHoraMarcado;
        }
        public string getHora()
        {
            DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
            return getDateToString.ToString("HH:mm:ss");
        }
        public int capturaMinAtraso(int IdPersonal)
        {
            // Parse the captured time string to a DateTime object
            DateTime horaMarcada = DateTime.Parse(_capturaHoraMarcado);

            // Define the comparison times
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00, 0);


            // Get the values from the other functions
            int idTurno = capturaIdTurno();
            int indMov = capturaIndMov(IdPersonal);

            // Calculate the exceeded minutes according to the conditions
            if (idTurno == 1 && indMov == 461)
            {
                // For the first condition, compare with 08:30:00
                return horaMarcada > tiempoInicioTurno1 ? (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes : 0;
            }
            else if (idTurno == 2 && indMov == 461)
            {
                // For the second condition, compare with 14:30:00
                return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
            }
            else if (idTurno == 3 && indMov == 461)
            {
                return horaMarcada > tiempoInicioTurno3 ? (int)(horaMarcada - tiempoInicioTurno3).TotalMinutes : 0;
            }

            // If none of the conditions are met, return 0
            return 0;
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
        public int capturaIndMov(int idPersonal)
        {
            using (var context = new StoreContext())
            {
                var ultimoRegistro = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == idPersonal)
                    .OrderByDescending(a => a.Id) // Asumiendo que 'Id' es un autoincremento y representa el más reciente.
                    .Select(a => a.IndTipoMovimiento)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                // Si el valor es 1, devolver 2. Si el valor es 2 o no hay registros, devolver 1.
                return ultimoRegistro == 461 ? 462 : 461;
            }
        }
        public int capturaIdTurno()
        {
            if (!EsSabado())
            {
                var horaActual = DateTime.Now;
                var limiteHora = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);

                return horaActual < limiteHora ? 1 : 2;
            }
            return 3;
        }
        public bool validarTurno(int idPersonal, int idTurno)
        {
            using (var context = new StoreContext())
            {
                // Busca si existe un turno asignado que coincida con el idPersonal y idTurno
                var asignacion = context.RrhhTurnoAsignados
                                        .Any(ta => ta.IdPersonal == idPersonal && ta.IdTurno == idTurno);

                return asignacion; // True si existe la asignación, False de lo contrario
            }
        }
        public int getIdTurno(int idPersonal)
        {
            int idTurno = this.capturaIdTurno();
            if(validarTurno(idPersonal, idTurno))
            {
                return idTurno;
            }
            else
            {
                MessageBox.Show("El porsonal no tiene asignado el turno: " + turnoNombre2(idTurno));
                return 0;
            }
        }
        //public string turnoNombre(int idTurno)
        //{
        //    using (var context = new StoreContext())
        //    {
        //        var turno = context.RrhhTurnos.Find(idTurno);
        //        return turno?.Nombre; // Devuelve el nombre si se encuentra el turno, o null si no se encuentra
        //    }
        //}
        public string turnoNombre2(int idTurno)
        {
            switch (idTurno)
            {
                case 1: return "MAÑANA";
                case 2: return "TARDE";
                case 3: return "SÁBADO";
            }
            return "";
        }
        public bool EsSabado()
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Saturday;
        }
        public void AddAsistencia(RrhhAsistencia item)
        {
            using (var db = new StoreContext())
            {
                db.RrhhAsistencia.Add(item);
                db.SaveChanges();
            }
        }
    }
}
