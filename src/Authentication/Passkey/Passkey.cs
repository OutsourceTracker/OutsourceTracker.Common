namespace OutsourceTracker.Authentication.Passkey;

/// <summary>
/// Concrete implementation of <see cref="IPasskey"/> representing a stored FIDO2/WebAuthn public-key credential (passkey).
/// This class is suitable for persistence (e.g., EF Core entity) and maps closely to the needs of fido2-net-lib's credential verification flow.
/// </summary>
/// <remarks>
/// - Use this as your database entity or DTO for passkey storage.
/// - <see cref="SignCount"/> must be updated after each successful assertion (set operation required).
/// - All byte[] properties are stored raw (as received from the authenticator / fido2-net-lib).
/// - Compatible with fido2-net-lib's <c>StoredCredential</c> pattern (Descriptor.Id → CredentialId, PublicKey, etc.).
/// - For EF Core: mark byte[] as <c>[Column(TypeName = "varbinary(max)")]</c> or use owned types if needed.
/// </remarks>
public class Passkey<TID> : IPasskey where TID : struct
{
    /// <inheritdoc />
    public required string DisplayName { get; init; }

    /// <inheritdoc />
    public required byte[] CredentialId { get; init; }

    /// <inheritdoc />
    public required byte[] PublicKey { get; init; }

    /// <inheritdoc />
    public required uint SignCount { get; set; }  // Note: writable — update after each assertion!

    /// <inheritdoc />
    public required DateTimeOffset RegisteredAt { get; init; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public string? AaGuid { get; init; }

    /// <inheritdoc />
    public bool IsResidentKey { get; init; }

    /// <inheritdoc />
    public string? Transport { get; init; }

    /// <summary>
    /// The internal primary key for this passkey record (e.g., database ID).
    /// Useful when linking to a user or querying/updating SignCount.
    /// </summary>
    public TID Id { get; init; } = default;

    /// <summary>
    /// The ID of the user this passkey belongs to (foreign key).
    /// Matches your existing user identifier (e.g., Guid from Identity or custom user table).
    /// </summary>
    public required TID UserId { get; init; }

    /// <summary>
    /// Optional backup state (reported by some authenticators).
    /// Helps detect if credential was copied/moved to another device.
    /// </summary>
    public bool? IsBackedUp { get; set; }

    /// <summary>
    /// Optional: nickname or model name derived from AAGUID lookup (for better UX).
    /// Can be populated later via FIDO Metadata Service (MDS).
    /// </summary>
    public string? AuthenticatorModel { get; set; }
}

/// <inheritdoc />
public class Passkey : Passkey<Guid> { }