namespace OutsourceTracker.Authentication.Passkey;

/// <summary>
/// Defines a contract for accessing a user's collection of registered passkeys (FIDO2/WebAuthn public-key credentials).
/// This interface provides read-only access to the passkeys associated with a user, typically implemented by user entities
/// or view models that aggregate credential data for authentication flows.
/// </summary>
/// <remarks>
/// <para>
/// Implementations of this interface are commonly used in:
/// - User domain models (e.g., <c>IUserWithPasskeys</c> or <c>ApplicationUser</c>) to expose passkeys during assertion ceremonies.
/// - Blazor components or services that list/manage passkeys in the UI (e.g., "Your registered security keys").
/// - Backend services that prepare <c>PublicKeyCredentialRequestOptions</c> by collecting allowable credentials.
/// </para>
/// <para>
/// This is intentionally read-only (<see cref="IReadOnlyCollection{T}"/>) because passkey management (add/remove/update SignCount)
/// should be handled through dedicated commands/endpoints, not direct collection mutation, to enforce security invariants
/// (e.g., incrementing SignCount only after successful verification).
/// </para>
/// <para>
/// When using fido2-net-lib, this collection is typically used to build the <c>AllowCredentials</c> list for assertion options.
/// </para>
/// </remarks>
public interface IPasskeyStore
{
    /// <summary>
    /// Gets a read-only collection of all passkeys registered for the associated user.
    /// </summary>
    /// <remarks>
    /// Returns all stored <see cref="IPasskey"/> instances, including resident/discoverable and server-side credentials.
    /// The collection should reflect the current database state at the time of access.
    /// <para>
    /// In practice:
    /// - For assertion flows: iterate this collection to build <c>PublicKeyCredentialDescriptor</c> entries.
    /// - For UI: display <see cref="IPasskey.DisplayName"/>, registration date, authenticator model, etc.
    /// - Empty collection indicates no passkeys registered (fallback to other auth methods).
    /// </para>
    /// Implementations must never return null; return an empty collection if no passkeys exist.
    /// </remarks>
    /// <value>An immutable view of the user's passkeys.</value>
    IReadOnlyCollection<IPasskey> Passkeys { get; }
}