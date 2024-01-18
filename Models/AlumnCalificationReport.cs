using Google.Cloud.Firestore.V1;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models
{
    public class AlumnCalificationReport
    {
        public AlumnCalificationReport()
        {
            alumnoCalificacionesP1 = new List<AlumnoCalificaciones>();
            alumnoCalificacionesP2 = new List<AlumnoCalificaciones>();
            alumnoCalificacionesP3 = new List<AlumnoCalificaciones>();
        }

        public Alumno alumn { get; set; }

        public List<AlumnoCalificaciones> alumnoCalificacionesP1 { get; set; }
        public List<AlumnoCalificaciones> alumnoCalificacionesP2 { get; set; }
        public List<AlumnoCalificaciones> alumnoCalificacionesP3 { get; set; }

        public double GetP1Average(int cantidadNotas, int porcentaje)
        {
            if(alumnoCalificacionesP1.Count == 0) return 0.0;

            return ((alumnoCalificacionesP1.Sum(ac => ac.Calificacion))/ cantidadNotas) * (((double)porcentaje)/100);
        }
        public double GetP2Average(int cantidadNotas, int porcentaje)
        {
            if (alumnoCalificacionesP2.Count == 0) return 0.0;

            return ((alumnoCalificacionesP2.Sum(ac => ac.Calificacion)) / cantidadNotas) * (((double)porcentaje) / 100);
        }

        public double GetP3Average(int cantidadNotas, int porcentaje)
        {
            if (alumnoCalificacionesP3.Count == 0) return 0.0;

            return ((alumnoCalificacionesP3.Sum(ac => ac.Calificacion)) / cantidadNotas) * (((double)porcentaje) / 100);
        }
    }
}
