namespace OutsourceTracker.Tasks;

/// <summary>
/// Specifies the types of tasks that can be performed within the system.
/// </summary>
/// <remarks>Use this enumeration to categorize and identify different task operations. Each value represents a
/// distinct task type that may be used for task management, filtering, or processing logic.</remarks>
public enum TaskType : byte
{
    /// <summary>
    /// Represents a trailer movement operation where when it's completed the trailer location is updated.
    /// </summary>
    TrailerMove = 0
}
