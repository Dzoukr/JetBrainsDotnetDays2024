module JetBrainsDotnetDays2024.Server.WebApp

open Giraffe
open Giraffe.GoodRead
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.Extensions.Logging
open JetBrainsDotnetDays2024.Shared.API

let service = {
    GetDefinition = fun _ -> Definition2.full |> async.Return
}

let webApp : HttpHandler =
    let remoting logger =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Service.RouteBuilder
        |> Remoting.fromValue service
        |> Remoting.withErrorHandler (Remoting.errorHandler logger)
        |> Remoting.buildHttpHandler
    choose [
        Require.services<ILogger<_>> remoting
        htmlFile "public/index.html"
    ]