namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.TransitionEvents;

public class TransitionEvents
{
    public class StepOne
    {
        public int Id { get; set;}
        public string Name { get; set; } = string.Empty;
        public Guid CorrelationId { get; set; }
    }

    public class StepTwo
    {
        public string Description { get; set; } = string.Empty;
        public Guid CorrelationId { get; set; }
    }

    public class StepThree
    {
        public Guid CorrelationId { get; set; }
        public string Result { get; set; } = string.Empty;
    }
}
