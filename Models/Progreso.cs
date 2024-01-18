using Google.Cloud.Firestore;
using Google.Type;
using System.Globalization;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models
{
    [FirestoreData]
    public class Progreso
    {

        public Progreso() { }

        public string ProgresoId { get; set; }

        [FirestoreProperty]
        public string NombreProgreso { get; set; }

        [FirestoreProperty]
        public int NumeroProgreso { get; set; }

        [FirestoreProperty]
        public string FechaInicio { get; set; }

        [FirestoreProperty]
        public string FechaFin { get; set; }

        [FirestoreProperty]
        public int CantidadNotas { get; set; }

        [FirestoreProperty]
        public int Porcentaje { get; set; }

        public System.DateTime GetFechaInicioProgreso()
        {
            return System.DateTime.ParseExact(this.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public System.DateTime GetFechaFinProgreso()
        {
            return System.DateTime.ParseExact(this.FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

    }
}
