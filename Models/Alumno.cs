using Google.Cloud.Firestore;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models
{
    [FirestoreData]
    public class Alumno
    {
        public Alumno()
        {
                
        }
        public string AlumnoId { get; set; }

        [FirestoreProperty]
        public string Nombre { get; set; }
    }
}
