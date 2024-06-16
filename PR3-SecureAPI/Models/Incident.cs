namespace PR3_SecureAPI.Models
{
    public class Incident
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UtilisateurId { get; set; }

        public int? PosteId { get; set; }

        public int? SalleId { get; set; }

        public int? EtablissementId { get; set; }

    }
}
