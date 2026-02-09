namespace OutsourceTracker.Tools;

/// <summary>
/// Defines a contract for interacting with a mapping tool, such as initializing a map, managing markers,
/// and focusing on specific locations. Implementations should handle map operations asynchronously
/// and ensure thread-safety where applicable.
/// </summary>
public interface IMapTool
{
    /// <summary>
    /// Asynchronously initializes the map with optional initial coordinates and additional arguments.
    /// </summary>
    /// <param name="initLat">The initial latitude to center the map on. Defaults to 47.23299 if not provided.</param>
    /// <param name="initlong">The initial longitude to center the map on. Defaults to -122.22538 if not provided.</param>
    /// <param name="additionalArgs">Optional additional arguments for map initialization, such as zoom level or map type.</param>
    /// <returns>A task that represents the asynchronous operation, completing with <c>true</c> if initialization succeeds; otherwise, <c>false</c>.</returns>
    Task<bool> InitializeMapAsync(double? initLat = 47.23299, double? initlong = -122.22538, object? additionalArgs = null);

    /// <summary>
    /// Asynchronously creates a new map marker with the specified properties.
    /// </summary>
    /// <param name="markerId">A unique identifier for the marker.</param>
    /// <param name="title">The title or label for the marker.</param>
    /// <param name="lat">The latitude coordinate for the marker's position.</param>
    /// <param name="lng">The longitude coordinate for the marker's position.</param>
    /// <param name="accuracy">Optional accuracy radius in meters (e.g., for displaying a circle around the marker). Defaults to 0.</param>
    /// <param name="infoHtml">Optional HTML content to display in an info window when the marker is clicked. Can be null.</param>
    /// <returns>A task that represents the asynchronous operation, completing with a string representing the created marker's ID or confirmation message.</returns>
    Task<string> CreateMapMarker(string markerId, string title, double lat, double lng, double accuracy = 0, string? infoHtml = null);

    /// <summary>
    /// Asynchronously edits an existing map marker with the specified properties.
    /// </summary>
    /// <param name="markerId">The unique identifier of the marker to edit.</param>
    /// <param name="title">The updated title or label for the marker.</param>
    /// <param name="lat">The updated latitude coordinate for the marker's position.</param>
    /// <param name="lng">The updated longitude coordinate for the marker's position.</param>
    /// <param name="accuracy">Optional updated accuracy radius in meters. Defaults to 0.</param>
    /// <param name="infoHtml">Optional updated HTML content for the info window. Can be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task EditMapMarker(string markerId, string title, double lat, double lng, double accuracy = 0, string? infoHtml = null);

    /// <summary>
    /// Asynchronously deletes the specified map marker.
    /// </summary>
    /// <param name="markerId">The unique identifier of the marker to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteMapMarker(string markerId);

    /// <summary>
    /// Asynchronously focuses the map on the specified marker, such as by centering or zooming to it.
    /// </summary>
    /// <param name="markerId">The unique identifier of the marker to focus on.</param>
    /// <param name="zoomLevel">The zoom level on the marker</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task FocusMapMarker(string markerId, int? zoomLevel = 10);
}