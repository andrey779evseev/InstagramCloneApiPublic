using Domain.Interfaces.Utils.PasswordHasher;
using Domain.Settings.Utils.PasswordHasher;
using Infrastructure.Utils.PasswordHasher;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Tests;

public class PasswordHasherTests
{
    private IPasswordHasher _hasher = null!;

    [SetUp]
    public void Setup()
    {
        var options = Options.Create(new HashingSettings());
        _hasher = new PasswordHasher(options);
    }

    [Test(Description = "Success password validation")]
    public void SuccessPasswordValidation()
    {
        const string password = "sK!153Yog6gy";
        var hash = _hasher.Hash(password);
        var verified = _hasher.Check(hash, password);
        Assert.IsTrue(verified);
    }

    [Test(Description = "Failed password validation")]
    public void FailedPasswordValidation()
    {
        const string password1 = "sK!153Yog6gy";
        const string password2 = "sK!153Yog6gyy";
        var hash = _hasher.Hash(password1);
        var verified = _hasher.Check(hash, password2);
        Assert.IsFalse(verified);
    }
}