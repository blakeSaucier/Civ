open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer
open Giraffe
open Civ.Game.Router
let webApp =
    choose [
        route "/ping"   >=> text "pong"
        gameRouter
    ]

type Startup() =
    member __.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore
        services.AddSpaStaticFiles (fun config ->
            config.RootPath <- "ClientApp/build")

    member __.Configure (app : IApplicationBuilder)
                        (env : IHostEnvironment)
                        (loggerFactory : ILoggerFactory) =
        // Add Giraffe to the ASP.NET Core pipeline
        app.UseGiraffe webApp
        app.UseStaticFiles() |> ignore
        app.UseSpaStaticFiles()
        app.UseSpa (fun spa ->
            spa.Options.SourcePath <- "ClientApp"
            if env.IsDevelopment() then
                spa.UseReactDevelopmentServer(npmScript = "start"))

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .UseStartup<Startup>()
                    |> ignore)
        .Build()
        .Run()
    0