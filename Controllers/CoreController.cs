using Google.Cloud.Firestore.V1;
using System.Reflection.Metadata.Ecma335;
using Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Controllers
{
    public class CoreController
    {
        public static async Task<List<AlumnCalificationReport>> BuildReporteAlumnosCalifición()
        {
            List<AlumnCalificationReport> listaToReturn = new List<AlumnCalificationReport>();

            ProgresoController ProgresoController = new ProgresoController();
            AlumnoController AlumnoController = new AlumnoController();
            AlumnoCalificacionController AlumnoCalificacionController = new AlumnoCalificacionController();

            List<Progreso> progresos = new List<Progreso>();
            List<Alumno> alumnos = new List<Alumno>();
            List<AlumnoCalificaciones> alumnoCalificaciones = new List<AlumnoCalificaciones>();

            progresos = await ProgresoController.GetAllProgresos();
            progresos = progresos.OrderBy(p => p.NumeroProgreso).ToList();

            alumnos = await AlumnoController.GetAllAlumnos();
            alumnoCalificaciones = await AlumnoCalificacionController.GetAllAlumnosCalificacion();

            foreach (Alumno alumno in alumnos)
            {
                AlumnCalificationReport ACLoop = new AlumnCalificationReport();
                ACLoop.alumn = alumno;

                List<AlumnoCalificaciones> calificacionesDelAlumno = alumnoCalificaciones.Where(ac => ac.AlumnoId == alumno.AlumnoId).ToList();

                foreach (AlumnoCalificaciones alumnoClif in calificacionesDelAlumno)
                {
                    if (progresos[0].GetFechaInicioProgreso() <= alumnoClif.GetFechaCalificacion() && alumnoClif.GetFechaCalificacion() <= progresos[0].GetFechaFinProgreso())
                    {
                        ACLoop.alumnoCalificacionesP1.Add(alumnoClif);
                    }
                    if (progresos[1].GetFechaInicioProgreso() <= alumnoClif.GetFechaCalificacion() && alumnoClif.GetFechaCalificacion() <= progresos[1].GetFechaFinProgreso())
                    {
                        ACLoop.alumnoCalificacionesP2.Add(alumnoClif);
                    }
                    if (progresos[2].GetFechaInicioProgreso() <= alumnoClif.GetFechaCalificacion() && alumnoClif.GetFechaCalificacion() <= progresos[2].GetFechaFinProgreso())
                    {
                        ACLoop.alumnoCalificacionesP3.Add(alumnoClif);
                    }
                }

                listaToReturn.Add(ACLoop);
            }

            return listaToReturn;
        }
    }
}
