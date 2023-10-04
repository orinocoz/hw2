using System;

public class DiffieHellman
{
    private static readonly Random Random = new Random();

    public long GeneratePrivateKey(long max)
    {
        // Generate a random private key within the specified range (2 to max).
        return Random.Next(2, (int)max);
    }

    public long CalculatePublicKey(long privateKey, long prime, long generator)
    {
        // Calculate the public key: (generator ^ privateKey) % prime
        long publicKey = 1;
        for (long i = 0; i < privateKey; i++)
        {
            publicKey = (publicKey * generator) % prime;
        }
        return publicKey;
    }

    public long CalculateSharedSecret(long privateKey, long publicKey, long prime)
    {
        // Calculate the shared secret: (publicKey ^ privateKey) % prime
        long sharedSecret = 1;
        for (long i = 0; i < privateKey; i++)
        {
            sharedSecret = (sharedSecret * publicKey) % prime;
        }
        return sharedSecret;
    }
}

public class PrimeFactorization
{
    public static bool IsPrime(long n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;

        if (n % 2 == 0 || n % 3 == 0) return false;

        for (long i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0) return false;
        }

        return true;
    }

    public static (long p1, long p2) Factorize(long n)
    {
        for (long p = 2; p < n; p++)
        {
            if (n % p == 0 && IsPrime(p) && IsPrime(n / p))
            {
                return (p, n / p);
            }
        }

        throw new Exception("Failed to factorize into two prime numbers.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        long prime = 23; // Replace with your desired prime number.
        long generator = 5; // Replace with your desired generator.

        DiffieHellman dh = new DiffieHellman();

        long privateKeyAlice = dh.GeneratePrivateKey(prime);
        long privateKeyBob = dh.GeneratePrivateKey(prime);

        long publicKeyAlice = dh.CalculatePublicKey(privateKeyAlice, prime, generator);
        long publicKeyBob = dh.CalculatePublicKey(privateKeyBob, prime, generator);

        long sharedSecretAlice = dh.CalculateSharedSecret(privateKeyAlice, publicKeyBob, prime);
        long sharedSecretBob = dh.CalculateSharedSecret(privateKeyBob, publicKeyAlice, prime);

        Console.WriteLine($"Alice's shared secret: {sharedSecretAlice}");
        Console.WriteLine($"Bob's shared secret: {sharedSecretBob}");

        // Factorize the shared secret to get p1 and p2
        var (p1, p2) = PrimeFactorization.Factorize(sharedSecretAlice);
        Console.WriteLine($"p1: {p1}");
        Console.WriteLine($"p2: {p2}");
    }
}