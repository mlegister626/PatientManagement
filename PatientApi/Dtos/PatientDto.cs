namespace PatientApi.Dtos
{
    /// <summary>
    /// DTO returned to clients when reading patient data.
    /// </summary>
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public int FacilityId { get; set; } = 0;
    }
}
