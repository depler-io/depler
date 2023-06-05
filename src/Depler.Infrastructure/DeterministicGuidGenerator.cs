using System.Security.Cryptography;
using System.Text;
using Depler.Validation;

namespace Depler.Infrastructure;

/// <summary>
/// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
/// http://code.logos.com/blog/2011/04/generating_a_deterministic_guid.html
/// </summary>
public class DeterministicGuidGenerator
{
    public static class Namespaces
    {
        public static readonly Guid Repositories = Guid.Parse("58b89a61-270c-4d75-9026-2debd050da67");
    }

    public static Guid Create(Guid namespaceId, string name)
    {
        Must.NotBeEmpty(namespaceId);
        Must.NotBeNullOrEmpty(name);

        // Convert the name to a sequence of octets (as defined by the standard or conventions of its namespace) (step 3)
        // ASSUME: UTF-8 encoding is always appropriate
        var nameBytes = Encoding.UTF8.GetBytes(name);

        return Create(namespaceId, nameBytes);
    }

    public static Guid Create(Guid namespaceId, byte[] nameBytes)
    {
        // Always use version 5 (version 3 is MD5, version 5 is SHA1)
        const int version = 5;

        Must.NotBeEmpty(namespaceId);
        Must.NotBeNullOrEmpty(nameBytes);

        // Convert the namespace UUID to network order (step 3)
        var namespaceBytes = namespaceId.ToByteArray();
        SwapByteOrder(namespaceBytes);

        // Compute the hash of the name space ID concatenated with the name (step 4)
        byte[] hash;
        using (var algorithm = SHA1.Create())
        {
            var newInputBuffer = new byte[namespaceBytes.Length + nameBytes.Length];
            Array.Copy(namespaceBytes, 0, newInputBuffer, 0, namespaceBytes.Length);
            Array.Copy(nameBytes, 0, newInputBuffer, namespaceBytes.Length + 1, nameBytes.Length);
            hash = algorithm.ComputeHash(newInputBuffer, 0, newInputBuffer.Length);
        }

        // Most bytes from the hash are copied straight to the bytes of the new
        // GUID (steps 5-7, 9, 11-12)
        var newGuid = new byte[16];
        Array.Copy(hash, 0, newGuid, 0, 16);

        // Set the four most significant bits (bits 12 through 15) of the time_hi_and_version
        // field to the appropriate 4-bit version number from Section 4.1.3 (step 8)
        newGuid[6] = (byte)((newGuid[6] & 0x0F) | (version << 4));

        // Set the two most significant bits (bits 6 and 7) of the clock_seq_hi_and_reserved
        // to zero and one, respectively (step 10)
        newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);

        // Convert the resulting UUID to local byte order (step 13)
        SwapByteOrder(newGuid);
        return new Guid(newGuid);
    }

    private static void SwapByteOrder(byte[] guid)
    {
        SwapBytes(guid, 0, 3);
        SwapBytes(guid, 1, 2);
        SwapBytes(guid, 4, 5);
        SwapBytes(guid, 6, 7);
    }

    private static void SwapBytes(byte[] guid, int left, int right)
    {
        (guid[left], guid[right]) = (guid[right], guid[left]);
    }
}

