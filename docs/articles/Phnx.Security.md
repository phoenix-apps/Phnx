# Phnx.Security

This library contains tools to help secure data, using high quality encryption, and encouraging the appropriate techniques and algorithms

# Quick Examples

For a full list of all things possible with Phnx.Security, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Security.html)

## PasswordProtectedEncryptionService

```cs
AesEncryption aes = new AesEncryption();
Sha256Hash hash = new Sha256Hash();

PasswordProtectedEncryptionService encryption = new PasswordProtectedEncryptionService(aes, hash);

string plaintext = "Sample data";
byte[] plaintextAsBytes = Encoding.UTF8.GetBytes(plaintext);
MemoryStream plaintextStream = new MemoryStream(plaintextAsBytes);
MemoryStream encryptedStream = new MemoryStream();

encryption.Encrypt(plaintextStream, "My super secret password", encryptedStream);

byte[] encryptedBytes = encryptedStream.ToArray();
string encryptedText = Encoding.UTF8.GetString(encryptedBytes);

// Outputs encrypted data
Console.WriteLine(encryptedText);

encryptedStream = new MemoryStream(encryptedBytes);
plaintextStream = new MemoryStream();

encryption.TryDecrypt(encryptedStream, "My super secret password", plaintextStream);

plaintextAsBytes = plaintextStream.ToArray();
plaintext = Encoding.UTF8.GetString(plaintextAsBytes);

// Outputs Sample data
Console.WriteLine(plaintext);
```

## PasswordHashManager

```cs
Pbkdf2Hash hash = new Pbkdf2Hash();
PasswordHashDefault hasher = new PasswordHashDefault(hash);

PasswordHashManager manager = new PasswordHashManager();
// Register the default hasher
manager.Add(1, hasher);

string password = "My super secret password";

byte[] hashedPassword = manager.HashWithLatest(password);

bool match = manager.PasswordMatchesHash(password, hashedPassword);

// Output: True
Console.WriteLine(match);

bool shouldUpdate = manager.ShouldUpdateHash(hashedPassword);

// Output: false
Console.WriteLine(shouldUpdate);
```