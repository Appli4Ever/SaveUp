using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Entities;
using SaveUp.Web.API.Services;
using SaveUp.Web.API.Tests.MockServices;
using SaveUp.Web.API.Tests.TestHelper;

namespace SaveUp.Web.API.Tests.Services;

public class UserServiceTests
{
    [Test]
    public async Task CreateUser_UserModelValid_ErstelltUser()
    {

        var builder = new SaveUpDbContextBuilder();

        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        var result = await testee.CreateUser(vm);

        result.Username.Should().Be(vm.Username);

        var dbresult = await context.Users.FirstOrDefaultAsync();
        dbresult.Username.Should().Be(vm.Username);
    }

    [Test]
    public async Task CreateUser_UserModelValid_PasswordWirdNichtAlsKlarTextGespeichert()
    {

        var builder = new SaveUpDbContextBuilder();

        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        await testee.CreateUser(vm);

        var dbresult = await context.Users.FirstOrDefaultAsync();
        dbresult.Password.Should().NotBe(vm.Password);
    }

    [Test]
    public async Task CreateUser_UsernameBereitsVorhanden_LegtUserNichtAn()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
            Password = "TestPassoword"
        };

        var builder = new SaveUpDbContextBuilder().AddUsers(user);

        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        var result = await testee.CreateUser(vm);
        result.Should().BeNull();

        var dbresult = await context.Users.ToListAsync();
        dbresult.Count.Should().Be(1);
    }

    [Test]
    public async Task CheckLogin_LoginRichtig_GibtTokenZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.Token.Should().NotBeNullOrEmpty();
        result.LoginStatus.Should().Be(LoginStatus.Success);
    }

    [Test]
    public async Task CheckLogin_PasswordFalsch_GibtFaildZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "FalschesPassword",
            Username = "TestUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.LoginStatus.Should().Be(LoginStatus.Faild);
        result.Token.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task CheckLogin_UsernameFalsch_GibtFaildZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "FalscherUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.LoginStatus.Should().Be(LoginStatus.Faild);
        result.Token.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task CheckLogin_DrittesMalFalsch_GibtBlockedZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
            LoginTries = 2
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "FalschesPassword",
            Username = "TestUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.LoginStatus.Should().Be(LoginStatus.Blocked);
        result.Token.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task CheckLogin_LoginKorrekt_SetztVersucheZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
            LoginTries = 2
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.LoginStatus.Should().Be(LoginStatus.Success);

        var dbresult = await context.Users.FindAsync(1);
        dbresult.LoginTries.Should().Be(0);
    }

    [Test]
    public async Task CheckLogin_LoginBlocked_GibtBlockedZurueck()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
            LoginBlocked = true
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new UserViewModel()
        {
            Password = "TestPassword",
            Username = "TestUsername"
        };

        var result = await testee.CheckLogin(vm);
        result.LoginStatus.Should().Be(LoginStatus.Blocked);
        result.Token.Should().BeNullOrEmpty();
    }


    [Test]
    public async Task ChangePassword_UserVorhanden_AendertPasswort()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new PasswordViewModel()
        {
            Password = "NeuesPassword",
            VerifiyPassword = "NeuesPassword"
        };

        var oldUser = await context.Users.FirstOrDefaultAsync();
        var oldpassword = oldUser.Password;

        var result = await testee.ChangePassword(vm);
        result.Should().BeTrue();

        var hasher = new PasswordHasher<User>();
        var equal = hasher.VerifyHashedPassword(oldUser, oldpassword, vm.Password);

        equal.Should().Be(PasswordVerificationResult.Failed);
    }

    [Test]
    public async Task ChangePassword_UserNichtVorhanden_AendertPasswortNicht()
    {

        var builder = new SaveUpDbContextBuilder();
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new PasswordViewModel()
        {
            Password = "NeuesPassword",
            VerifiyPassword = "NeuesPassword"
        };

        var result = await testee.ChangePassword(vm);
        result.Should().BeFalse();
    }

    [Test]
    public async Task ChangePassword_PasswortNichtGleich_AendertPasswortNicht()
    {
        var user = new User()
        {
            Id = 1,
            Username = "TestUsername",
        };

        user.Password = new PasswordHasher<User>().HashPassword(user, "TestPassword");

        var builder = new SaveUpDbContextBuilder().AddUsers(user);
        var context = builder.Build();

        var testee = new UserService(context, new MockTokenService(), builder.TestIdentity);

        var vm = new PasswordViewModel()
        {
            Password = "NePassword",
            VerifiyPassword = "Neuespassword"
        };

        var result = await testee.ChangePassword(vm);
        result.Should().BeFalse();
    }
}