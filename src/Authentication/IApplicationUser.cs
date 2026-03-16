using OutsourceTracker.Authentication.Passkey;

namespace OutsourceTracker.Authentication;

/// <summary>
/// Defines the core profile and identity contract for an authenticated application user in OutsourceTracker.
/// This generic interface combines personal/contact information with passkey storage capabilities,
/// serving as the primary abstraction for user entities across the system (e.g., in identity services,
/// repositories, Blazor components, and authorization policies).
/// </summary>
/// <typeparam name="TID">The type of the user's unique identifier (e.g., <see cref="Guid"/>, <see langword="int"/>).
/// Must be a value type (<see langword="struct"/>) for performance and storage efficiency in databases.</typeparam>
/// <remarks>
/// <para>
/// This interface extends <see cref="IPasskeyStore"/> to provide direct access to a user's registered
/// FIDO2/WebAuthn passkeys, enabling passwordless/2FA flows integrated with external providers (Microsoft 365,
/// Microsoft, Google OAuth).
/// </para>
/// <para>
/// Properties reflect a minimal but sufficient user profile for:
/// - Display and personalization in the Blazor frontend
/// - Contact and verification purposes
/// - Internal business logic (e.g., employee/freelancer identification via <see cref="EmployeeId"/> and <see cref="AlphaCode"/>)
/// - Role/claim-based authorization (roles/claims are typically handled separately via JWT or ASP.NET Core Identity)
/// </para>
/// <para>
/// Implementations are expected in the Backend project (e.g., EF Core entity <c>ApplicationUser</c>) and may
/// be projected into lighter DTOs for API responses or Blazor state.
/// </para>
/// </remarks>
public interface IApplicationUser<TID> : IPasskeyStore where TID : struct
{
    /// <summary>
    /// Gets the unique identifier for this user.
    /// </summary>
    /// <remarks>
    /// This is the primary key used across the system (database, JWT subject claim, foreign keys, etc.).
    /// Commonly a <see cref="Guid"/> for distributed systems or <see langword="int"/> for legacy compatibility.
    /// </remarks>
    /// <value>The user's ID of type <typeparamref name="TID"/>.</value>
    TID Id { get; }

    /// <summary>
    /// Gets the user's primary email address.
    /// </summary>
    /// <remarks>
    /// Used as the main login identifier for external OAuth providers (Microsoft 365, Google, etc.),
    /// email notifications, password recovery (if fallback enabled), and user lookup.
    /// Should be unique and normalized (lowercase) in storage.
    /// </remarks>
    /// <value>The user's email address.</value>
    string Email { get; }

    /// <summary>
    /// Gets the user's first (given) name.
    /// </summary>
    /// <value>The first name for display and personalization.</value>
    string FirstName { get; }

    /// <summary>
    /// Gets the user's last (family) name.
    /// </summary>
    /// <value>The last name for display and formal identification.</value>
    string LastName { get; }

    /// <summary>
    /// Gets the full name of the entity, including any relevant titles or suffixes.
    /// </summary>
    /// <remarks>The full name may include prefixes such as 'Dr.' or 'Mr.' and suffixes like 'Jr.' or 'Sr.'
    /// depending on the context of the entity.</remarks>
    string FullName { get; }

    /// <summary>
    /// Gets the user's phone number (if provided).
    /// </summary>
    /// <remarks>
    /// Optional field for contact, SMS-based 2FA fallback (if implemented), or verification.
    /// Format should be E.164 international (e.g., +12065551234) when stored/validated.
    /// </remarks>
    /// <value>The phone number, or empty/null if not set.</value>
    string PhoneNumber { get; }

    /// <summary>
    /// Gets the user's alphanumeric code (e.g., short identifier, badge number, or legacy employee code).
    /// </summary>
    /// <remarks>
    /// Domain-specific field for quick reference in business processes (e.g., dispatch, payroll, reporting).
    /// May be used in UI tables or printed materials.
    /// </remarks>
    /// <value>The alpha code, or empty if not applicable.</value>
    string AlphaCode { get; }

    /// <summary>
    /// Gets the formal employee or contractor ID assigned by the organization.
    /// </summary>
    /// <remarks>
    /// Useful for HR/payroll integration, compliance tracking, or linking to external systems.
    /// Distinct from <see cref="Id"/> (system PK) and <see cref="AlphaCode"/> (short alias).
    /// </remarks>
    /// <value>The employee/contractor ID, or empty if not assigned.</value>
    string EmployeeId { get; }
}