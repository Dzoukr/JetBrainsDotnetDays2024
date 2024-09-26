module JetBrainsDotnetDays2024.Shared.API

open JetBrainsDotnetDays2024.Shared.Definition

type Service = {
    GetDefinition : unit -> Async<Definition>
}
with
    static member RouteBuilder _ m = sprintf "/api/service/%s" m