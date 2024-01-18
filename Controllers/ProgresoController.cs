using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Models;

namespace Video_Explicativo_MVC__Minicore__José_Miguel_Merlo.Controllers
{
    public class ProgresoController
    {
        private FirestoreDb firestoreDb;

        public ProgresoController()
        {
            firestoreDb = FirestoreDb.Create("blazorserverdb");
        }

        public async Task<bool> CreateProgreso(Progreso newProgreso)
        {
            try
            {
                CollectionReference progresoRef = firestoreDb.Collection("Progresos");

                Dictionary<string, object> progresoDict = new Dictionary<string, object>
                {
                    { "NombreProgreso", newProgreso.NombreProgreso},
                    { "NumeroProgreso", newProgreso.NumeroProgreso},
                    { "FechaInicio", newProgreso.FechaInicio},
                    { "FechaFin", newProgreso.FechaFin},
                    { "CantidadNotas", newProgreso.CantidadNotas},
                    { "Porcentaje", newProgreso.Porcentaje}
                };

                await progresoRef.AddAsync(progresoDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear progreso: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditProgreso(Progreso progresoToUpdate)
        {
            try
            {
                DocumentReference progresoRef = firestoreDb.Collection("Progresos").Document(progresoToUpdate.ProgresoId);

                Dictionary<string, object> progresoDict = new Dictionary<string, object>
                 {
                    { "NombreProgreso", progresoToUpdate.NombreProgreso},
                    { "NumeroProgreso", progresoToUpdate.NumeroProgreso},
                    { "FechaInicio", progresoToUpdate.FechaInicio},
                    { "FechaFin", progresoToUpdate.FechaFin},
                    { "CantidadNotas", progresoToUpdate.CantidadNotas}
                };

                await progresoRef.UpdateAsync(progresoDict);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar el progreso: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Progreso>> GetAllProgresos()
        {
            List<Progreso> progresos = new List<Progreso>();

            try
            {
                Query progresoQuery = firestoreDb.Collection("Progresos");
                QuerySnapshot progresoQuerySnapShot = await progresoQuery.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapShot in progresoQuerySnapShot.Documents)
                {
                    if (documentSnapShot.Exists)
                    {
                        Dictionary<string, object> progresoData = documentSnapShot.ToDictionary();
                        string json = JsonConvert.SerializeObject(progresoData);

                        Progreso loopProgreso = JsonConvert.DeserializeObject<Progreso>(json);
                        loopProgreso.ProgresoId = documentSnapShot.Id;

                        progresos.Add(loopProgreso);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los progresos: {ex.Message}");
                return progresos;
            }
            return progresos;
        }

        public async Task<Progreso> GetProgresoById(string progresoId)
        {
            Progreso progreso = new Progreso();

            try
            {
                DocumentReference docRef = firestoreDb.Collection("Progresos").Document(progresoId);
                DocumentSnapshot documentSnapShot = await docRef.GetSnapshotAsync();

                if (documentSnapShot.Exists)
                {
                    Dictionary<string, object> progresoData = documentSnapShot.ToDictionary();
                    string json = JsonConvert.SerializeObject(progresoData);

                    progreso = JsonConvert.DeserializeObject<Progreso>(json);
                    progreso.ProgresoId = progresoId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el progreso con el id: {ex.Message}");
            }

            return progreso;
        }

        public async Task<bool> DeleteProgreso(string progresoId)
        {
            try
            {
                DocumentReference progresoRef = firestoreDb.Collection("Progresos").Document(progresoId);
                await progresoRef.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar el progreso: {ex.Message}");
                return false;
            }
        }
    }
}