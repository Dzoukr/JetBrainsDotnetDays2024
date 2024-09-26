module JetBrainsDotnetDays2024.Client.Pages.Index

open Feliz
open Elmish
open JetBrainsDotnetDays2024.Client.Server
open JetBrainsDotnetDays2024.Client.Renderers
open Feliz.UseElmish
open JetBrainsDotnetDays2024.Shared.Definition

type private State = {
    Definition : Definition option
}

type private Msg =
    | LoadDefinition
    | DefinitionLoaded of ServerResult<Definition>

let private init () = { Definition = None }, Cmd.ofMsg LoadDefinition

let private update (msg:Msg) (state:State) : State * Cmd<Msg> =
    match msg with
    | LoadDefinition -> state, Cmd.OfAsync.eitherAsResult (fun _ -> service.GetDefinition()) DefinitionLoaded
    | DefinitionLoaded (Ok def) -> { state with Definition = Some def }, Cmd.none
    | DefinitionLoaded (Error _) -> state, Cmd.none


[<ReactComponent>]
let IndexView () =
    let state, dispatch = React.useElmish(init, update, [| |])

    state.Definition
    |> Option.map RenderDefinition
    |> Option.defaultValue (Html.div "Loading...")
