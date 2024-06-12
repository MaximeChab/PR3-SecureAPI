namespace PR3_SecureAPI.Models
{
    public class Poste
    {
        public int Id { get; set; }

        public string Numero { get; set; }

        public string MacAdress { get; set; }

        public long SalleId { get; set; }

        public bool IsConnected { get; set; }
    }
}
