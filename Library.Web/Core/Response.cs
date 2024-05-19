namespace Library.Web.Core
{
    // Clase genérica para representar una respuesta estándar de operaciones.
    public class Response<T>
    {
        // Indica si la operación fue exitosa.
        public bool IsSucces { get; set; }

        // Mensaje descriptivo de la operación.
        public string Message { get; set; }

        // Lista de errores si ocurrieron durante la operación.
        public List<string> Errors { get; set; }

        // Resultado de la operación, puede ser de cualquier tipo T.
        public T Result { get; set; }
    }
}
