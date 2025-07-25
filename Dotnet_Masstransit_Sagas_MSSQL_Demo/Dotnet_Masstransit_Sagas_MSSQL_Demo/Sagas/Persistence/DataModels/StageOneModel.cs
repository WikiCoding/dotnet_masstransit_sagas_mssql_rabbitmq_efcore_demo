﻿using System.ComponentModel.DataAnnotations;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels;

public class StageOneModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}
