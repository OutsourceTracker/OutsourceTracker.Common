using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Services.ModelService;

public readonly ref struct ModelResult
{
    public bool Success { get; }

    public string? ErrorMessage { get; }

    private ModelResult(bool success, string? errorMessage = null)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public static ModelResult Ok() => new ModelResult(true);
}
