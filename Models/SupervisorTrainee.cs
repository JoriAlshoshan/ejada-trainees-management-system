namespace EjadaTraineesManagementSystem.Models
{
    public class SupervisorTrainee
    {
        public string SupervisorId { get; set; }
        public Users SupervisorUser { get; set; }

        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }
    }
}
