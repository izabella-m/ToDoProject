namespace ToDoAPI.Models;

public class ResponseModel<T>
    {
        public T? Dados { get; set; } // dado genérico e pode ser nulo
        public string Message { get; set; } = string. Empty;
        public bool Status { get; set; } = true;
    }
    