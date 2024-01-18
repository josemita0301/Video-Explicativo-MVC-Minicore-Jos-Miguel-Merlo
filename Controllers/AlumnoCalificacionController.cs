using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Controllers
{
    public class AlumnoCalificacionController
    {
        private FirestoreDb firestoreDb;

        public AlumnoCalificacionController()
        {
            firestoreDb = FirestoreDb.Create("blazorserverdb");
        }

        public async Task<bool> CreateAlumnoCalificacion(AlumnoCalificaciones newAlumnoCalificaciones)
        {
            try
            {
                CollectionReference alumnoCalificacionesRef = firestoreDb.Collection("AlumnoCalificaciones");

                Dictionary<string, object> alumnoCalificacionesDict = new Dictionary<string, object>
                {
                    { "AlumnoId", newAlumnoCalificaciones.AlumnoId},
                    { "Calificacion", newAlumnoCalificaciones.Calificacion},
                    { "Fecha", newAlumnoCalificaciones.Fecha}
                };

                await alumnoCalificacionesRef.AddAsync(alumnoCalificacionesDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la calificación de un alumno: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditAlumnoCalificacion(AlumnoCalificaciones alumnoCalificacionesToUpdate)
        {
            try
            {
                DocumentReference alumnoCalificacionRef = firestoreDb.Collection("AlumnoCalificaciones").Document(alumnoCalificacionesToUpdate.AlumnoCalificacionId);

                Dictionary<string, object> alumnoCalificacionesDict = new Dictionary<string, object>
                {
                    { "AlumnoId", alumnoCalificacionesToUpdate.AlumnoId},
                    { "Calificacion", alumnoCalificacionesToUpdate.Calificacion},
                    { "Fecha", alumnoCalificacionesToUpdate.Fecha}
                };

                await alumnoCalificacionRef.UpdateAsync(alumnoCalificacionesDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la calificación de un alumno: {ex.Message}");
                return false;
            }
        }

        public async Task<List<AlumnoCalificaciones>> GetAllAlumnosCalificacion()
        {
            List<AlumnoCalificaciones> alumnosCalificacion = new List<AlumnoCalificaciones>();

            try
            {
                Query alumnoCaliQuery = firestoreDb.Collection("AlumnoCalificaciones");
                QuerySnapshot alumnoCaliQuerySnapShot = await alumnoCaliQuery.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapShot in alumnoCaliQuerySnapShot.Documents)
                {
                    if (documentSnapShot.Exists)
                    {
                        Dictionary<string, object> alumno = documentSnapShot.ToDictionary();
                        string json = JsonConvert.SerializeObject(alumno);

                        AlumnoCalificaciones loopAlumnoCali = JsonConvert.DeserializeObject<AlumnoCalificaciones>(json);

                        loopAlumnoCali.AlumnoCalificacionId = documentSnapShot.Id;

                        loopAlumnoCali.Alumno = await GetAlumnoById(loopAlumnoCali.AlumnoId);

                        alumnosCalificacion.Add(loopAlumnoCali);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todas los alumnos: {ex.Message}");
                return alumnosCalificacion;
            }
            return alumnosCalificacion;
        }

        public async Task<Alumno> GetAlumnoById(string alumnoId)
        {
            Alumno alumno = new Alumno();

            try
            {
                DocumentReference docRef = firestoreDb.Collection("Alumnos").Document(alumnoId);
                DocumentSnapshot documentSnapShot = await docRef.GetSnapshotAsync();

                if (documentSnapShot.Exists)
                {
                    Dictionary<string, object> userData = documentSnapShot.ToDictionary();
                    string json = JsonConvert.SerializeObject(userData);

                    alumno = JsonConvert.DeserializeObject<Alumno>(json);

                    alumno.AlumnoId = alumnoId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el alumno con el id: {ex.Message}");
            }

            return alumno;
        }

        public async Task<AlumnoCalificaciones> GetAlumnoCalificacionesById(string alumnoCalifId)
        {
            AlumnoCalificaciones alumnoCalif = new AlumnoCalificaciones();

            try
            {
                DocumentReference docRef = firestoreDb.Collection("AlumnoCalificaciones").Document(alumnoCalifId);
                DocumentSnapshot documentSnapShot = await docRef.GetSnapshotAsync();

                if (documentSnapShot.Exists)
                {
                    Dictionary<string, object> userData = documentSnapShot.ToDictionary();
                    string json = JsonConvert.SerializeObject(userData);

                    alumnoCalif = JsonConvert.DeserializeObject<AlumnoCalificaciones>(json);

                    alumnoCalif.AlumnoCalificacionId = alumnoCalifId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el alumnoCalif con el id: {ex.Message}");
                return null;
            }

            return alumnoCalif;
        }

        public async Task<bool> DeleteAlumnoCalif(string alumnoCalifId)
        {
            try
            {
                DocumentReference alumnoCalifRef = firestoreDb.Collection("AlumnoCalificaciones").Document(alumnoCalifId);
                await alumnoCalifRef.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar el alumno calif: {ex.Message}");
                return false;
            }
        }
    }
}
