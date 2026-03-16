namespace OutsourceTracker.Authentication.Passkey;

/// <summary>
/// Represents a public-key credential (passkey) registered for a user using WebAuthn/FIDO2 standards.
/// This interface defines the core information needed to identify, store, and verify a passkey during authentication ceremonies.
/// </summary>
/// <remarks>
/// Implementations are typically persisted in the backend database (e.g., via EF Core) and used during registration
/// and assertion/verification flows with libraries such as fido2-net-lib.
/// <para>
/// The properties align with key fields from the Web Authentication (WebAuthn) specification and common requirements
/// for secure FIDO2 credential storage, including replay protection (SignCount), authenticator metadata (AaGuid),
/// discoverable credential support (IsResidentKey), and allowed transports.
/// </para>
/// <para>
/// All byte arrays should be stored exactly as received from the authenticator (raw bytes, no encoding modifications).
/// </para>
/// </remarks>
public interface IPasskey
{
    /// <summary>
    /// Gets the human-readable name for this passkey, typically chosen by the user during registration
    /// (e.g., "Austin's Windows Hello", "iPhone 16 Pro Face ID", "YubiKey 5 NFC").
    /// </summary>
    /// <value>A user-friendly display name for the credential, used in UI lists.</value>
    string DisplayName { get; }

    /// <summary>
    /// Gets the unique credential ID for this passkey.
    /// </summary>
    /// <remarks>
    /// This is the raw binary value returned by the authenticator during registration.
    /// It acts as the primary lookup key when the authenticator presents an assertion.
    /// Corresponds to <c>PublicKeyCredential.id</c> (base64url-encoded on the client, but stored as raw bytes here).
    /// Typically 16–64 bytes in length.
    /// </remarks>
    /// <value>The raw bytes of the credential ID.</value>
    byte[] CredentialId { get; }

    /// <summary>
    /// Gets the public key of the passkey credential in COSE_Key format.
    /// </summary>
    /// <remarks>
    /// This is the raw CBOR-encoded COSE_Key structure returned by the authenticator during registration.
    /// It is used by the relying party to verify signatures during authentication assertions.
    /// Store and return this value exactly as received — do not encode, decode, or modify it.
    /// Corresponds to the raw bytes from <c>AuthenticatorAttestationResponse.getPublicKey()</c>.
    /// </remarks>
    /// <value>The raw COSE_Key bytes representing the public key.</value>
    byte[] PublicKey { get; }

    /// <summary>
    /// Gets or sets the signature counter value for replay attack protection.
    /// </summary>
    /// <remarks>
    /// The authenticator increments this counter each time it generates an assertion.
    /// The relying party must verify that the presented sign count is strictly greater than
    /// the previously stored value, then update it to the new value.
    /// Critical for preventing cloned credential replay attacks.
    /// Corresponds to the <c>signCount</c> field in authenticator data.
    /// </remarks>
    /// <value>The current signature counter (uint).</value>
    uint SignCount { get; }

    /// <summary>
    /// Gets the UTC timestamp when this passkey was successfully registered.
    /// </summary>
    /// <value>The registration date and time (UTC).</value>
    DateTimeOffset RegisteredAt { get; }

    /// <summary>
    /// Gets the AAGUID (Authenticator Attestation GUID) identifying the authenticator model/family.
    /// </summary>
    /// <remarks>
    /// This 128-bit GUID uniquely identifies the authenticator model (e.g., Windows Hello, Touch ID, YubiKey).
    /// Useful for display purposes ("Registered with YubiKey 5"), logging, or policy decisions.
    /// Corresponds to the AAGUID in the authenticator data during registration.
    /// </remarks>
    /// <value>The 16-byte AAGUID as a string (usually hex or GUID format), or null if not available.</value>
    string? AaGuid { get; }

    /// <summary>
    /// Gets a value indicating whether this credential is a resident/discoverable key.
    /// </summary>
    /// <remarks>
    /// Resident (discoverable) credentials can be used for passwordless login without a username first —
    /// the authenticator can enumerate them.
    /// Set during registration based on <c>residentKey</c> preference and authenticator support.
    /// Important for UX decisions (e.g., show "Use passkey" button without email prompt).
    /// </remarks>
    /// <value><c>true</c> if this is a resident/discoverable credential; otherwise, <c>false</c>.</value>
    bool IsResidentKey { get; }

    /// <summary>
    /// Gets the allowed transports for this credential as reported by the authenticator.
    /// </summary>
    /// <remarks>
    /// Comma-separated list or single value indicating supported methods (e.g., "usb", "nfc", "ble", "internal").
    /// Comes from the <c>transports</c> field in attestation or from authenticatorGetInfo.
    /// Useful for UI hints ("Use USB security key") or fallback logic.
    /// May be null if not reported or not applicable.
    /// </remarks>
    /// <value>A string representing allowed transports, or null if unknown/not set.</value>
    string? Transport { get; }
}