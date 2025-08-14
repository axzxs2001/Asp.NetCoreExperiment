using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentityPasskeyOptions>(options =>
{
    // Explicitly set the Relying Party ID (domain)
    options.ServerDomain = "example.com";

    // Configure authenticator timeout
    options.AuthenticatorTimeout = TimeSpan.FromMinutes(3);

    // Configure challenge size
    options.ChallengeSize = 64;

});


var app = builder.Build();


app.MapGet("/token", async () =>
{

    var signInManager = app.Services.GetRequiredService<SignInManager<IdentityUser>>();
    // Makes passkey options for use with the JS `navigator.credentials.create()` API:
    var optionsJson = await signInManager.MakePasskeyCreationOptionsAsync(new()
    {
        Id = "a",
        Name = "gsw",
        DisplayName = "桂素伟",
    });


    // Makes passkey options for use with the JS `navigator.credentials.get()` API:
    // var optionsJson = await signInManager.MakePasskeyRequestOptionsAsync(user);

});

app.MapGet("/token1", async () =>
{
    SignInManager<IdentityUser> signInManager = app.Services.GetRequiredService<SignInManager<IdentityUser>>();
    // 'credentialJson' is the JSON-serialized result from `navigator.credentials.create()`.
    var attestationResult = await signInManager.PerformPasskeyAttestationAsync("");
    if (attestationResult.Succeeded)
    {
    
        //var addPasskeyResult = await UserManager.AddOrUpdatePasskeyAsync(user, attestationResult.Passkey);
        // ...
    }
    else { /* ... */ }

});

app.Run();

