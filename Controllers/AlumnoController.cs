using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Controllers
{
    public class AlumnoController
    {
        private FirestoreDb firestoreDb;

        public AlumnoController()
        {
            firestoreDb = FirestoreDb.Create("blazorserverdb");
        }

        public async Task<bool> CreateAlumno(Alumno newAlumno)
        {
            try
            {
                CollectionReference alumnoRef = firestoreDb.Collection("Alumnos");

                Dictionary<string, object> alumnoDict = new Dictionary<string, object>
                {
                    { "Nombre", newAlumno.Nombre}
                };

                await alumnoRef.AddAsync(alumnoDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el alumno: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditAlumno(Alumno alumnoToUpdate)
        {
            try
            {
                DocumentReference alumnoRef = firestoreDb.Collection("Alumnos").Document(alumnoToUpdate.AlumnoId);

                Dictionary<string, object> alumnoDict = new Dictionary<string, object>
                 {
                    { "Nombre", alumnoToUpdate.Nombre },
                };

                await alumnoRef.UpdateAsync(alumnoDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar el alumno: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Alumno>> GetAllAlumnos()
        {
            List<Alumno> alumnos = new List<Alumno>();

            try
            {
                Query alumnoQuery = firestoreDb.Collection("Alumnos");
                QuerySnapshot alumnoQuerySnapShot = await alumnoQuery.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapShot in alumnoQuerySnapShot.Documents)
                {
                    if (documentSnapShot.Exists)
                    {
                        Dictionary<string, object> alumno = documentSnapShot.ToDictionary();
                        string json = JsonConvert.SerializeObject(alumno);

                        Alumno loopAlumno = JsonConvert.DeserializeObject<Alumno>(json);

                        loopAlumno.AlumnoId = documentSnapShot.Id;

                        alumnos.Add(loopAlumno);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todas los alumnos: {ex.Message}");
                return alumnos;
            }
            return alumnos;
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

        public async Task<bool> DeleteAlumno(string alumnoId)
        {
            try
            {
                DocumentReference alumnoRef = firestoreDb.Collection("Alumnos").Document(alumnoId);
                await alumnoRef.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar el alumno: {ex.Message}");
                return false;
            }
        }
    }
}
