namespace GastosAPI.Utilidades
{
    public class ResponseApi<T>
    {
        public bool status { get; set; }
        public string? msg { get; set; }
        public T? value { get; set; }
        public string? token { get; set; }
    }
}
