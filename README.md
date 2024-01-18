# Mini Core Explicación

## Descripción
El proyecto es una aplicación web diseñada para registrar estudiantes, sus calificaciones en diferentes etapas de progreso y calcular su rendimiento general. Utiliza Blazor Server para la interfaz de usuario y Google Firestore para el almacenamiento de datos. La aplicación permite registrar a los estudiantes junto con sus calificaciones en tres etapas de progreso distintas. Cada progreso tiene un peso específico en la calificación final (25%, 35% y 40%). Además, calcula si un estudiante necesita una calificación específica en la tercera etapa para aprobar el curso, considerando que la calificación mínima para aprobar es 6.

## Funcionalidades
- Registro y manejo de estudiantes y sus calificaciones.
- Cálculo de calificaciones en tres etapas de progreso con diferentes ponderaciones (25%, 35%, 40%).
- Determinación de calificaciones necesarias en la última etapa para aprobar el curso (calificación mínima de 6).

## Tecnologías Utilizadas
- .NET
- Blazor Server
- Google Firestore

## Estructura del Proyecto
- **Modelos:** `Alumno`, `AlumnoCalificacion`, `Progreso`
- **Controladores:** `CoreController`, `ProgresoController`, `AlumnoController`, `AlumnoCalificacionController`
- **Vista (Blazor):** Presentación de informes de calificaciones y cálculos necesarios para aprobar.
- ## Explicación Técnica

El proyecto se estructura en varias partes clave:

### Modelos:
- **Alumno**: Representa a los estudiantes.
- **AlumnoCalificacion**: Maneja las calificaciones de los alumnos.
- **Progreso**: Define las etapas de progreso de las calificaciones.

### Controladores:
- **CoreController**: Construye el reporte de calificaciones de los alumnos. Obtiene y procesa listas de progresos, alumnos y sus calificaciones.
- **ProgresoController**, **AlumnoController**, **AlumnoCalificacionController**: Controladores para operaciones específicas de cada modelo.

### Vista (Blazor):
- Presenta un informe de las calificaciones de los estudiantes en cada etapa de progreso.
- Calcula los puntos necesarios en el Progreso 3 para que un estudiante apruebe el curso.

### Integración con Google Firestore:
- Almacena y recupera datos de alumnos, calificaciones y progresos en Google Firestore.
